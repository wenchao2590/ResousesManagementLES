namespace WS.SAP.SyncInboundService
{
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// SyncPlantStructureBLL
    /// </summary>
    public class SyncPlantStructureBLL
    {
        /// 同步工厂布局
        /// </summary>
        /// <returns></returns>
        public static void Sync(string loginUser)
        {
            ///从中间表提取未处理工厂布局数据
            List<SapPlantStructureInfo> sapPlantStructureInfos = new SapPlantStructureBLL().GetListByPage("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", "[ID] asc", 1, 1000, out int dataCnt);
            if (dataCnt == 0) return;
            StringBuilder stringBuilder = new StringBuilder();
            ///获取所有有效工厂信息
            List<PlantInfo> plantInfos = new PlantBLL().GetList(string.Empty, "ID");
            ///获取所有有效车间信息
            List<WorkshopInfo> workshopInfos = new WorkshopBLL().GetList(string.Empty, "ID");
            ///获取所有有效生产线信息
            List<AssemblyLineInfo> assemblyLineInfos = new AssemblyLineBLL().GetList(string.Empty, "ID");
            ///获取所有有效工段信息
            List<WorkshopSectionInfo> workshopSectionInfos = new WorkshopSectionBLL().GetList(string.Empty, "ID");
            ///获取所有有效工位信息
            List<LocationInfo> locationInfos = new LocationBLL().GetList(string.Empty, "ID");
            ///已处理完成的ID
            List<long> dealedIds = new List<long>();
            ///逐条处理中间表数据
            foreach (var sapPlantStructureInfo in sapPlantStructureInfos)
            {
                #region 工厂
                if (string.IsNullOrEmpty(sapPlantStructureInfo.Werks))
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PLANT_STRUCTURE] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'3x00000019'," +///工厂信息不存在
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapPlantStructureInfo.Id + ";");
                    continue;
                }
                ///
                PlantInfo plantInfo = plantInfos.FirstOrDefault(d => d.Plant == sapPlantStructureInfo.Werks);
                ///如果业务表工厂中不存在,就添加
                if (plantInfo == null)
                {
                    #region TM_BAS_PLANT
                    ///将这样的数据更新为挂起状态
                    stringBuilder.AppendFormat("insert into [LES].[TM_BAS_PLANT] ("
                      + "[FID] ,"
                      + "[PLANT] ,"
                      + "[PLANT_NAME] ,"
                      + "[VALID_FLAG] ,"
                      + "[CREATE_USER] ,"
                      + "[CREATE_DATE],"
                      + "[SAP_PLANT_CODE]) values ("
                      + "NEWID() ,"  //// FID - uniqueidentifier
                      + "N'{0}' ,"  //// PLANT - nvarchar(5)
                      + "N'{1}' ,"  //// PLANT_NAME - nvarchar(100)                    
                      + "{2} ,"  //// VALID_FLAG - bit
                      + "N'{3}' ,"  //// CREATE_USER - nvarchar(50)
                      + "GETDATE(),"//// CREATE_DATE - datetime
                      + "N'{4}'); ",///SAP_PLANT_CODE
                        sapPlantStructureInfo.Werks,//// PLANT - nvarchar(5),0
                        sapPlantStructureInfo.Name1, //// PLANT_NAME - nvarchar(100),1
                        1, //// VALID_FLAG - bit,2
                        loginUser,//// CREATE_USER - nvarchar(50),3
                        sapPlantStructureInfo.Werks);///SAP_PLANT_CODE,4
                    #endregion
                    ///添加到工厂集合
                    PlantInfo plant = new PlantInfo();
                    plant.Plant = sapPlantStructureInfo.Werks;
                    plantInfos.Add(plant);
                }
                else
                {
                    ///更新工厂名称
                    stringBuilder.AppendFormat("update [LES].[TM_BAS_PLANT] " +
                        "set [PLANT_NAME] = N'" + sapPlantStructureInfo.Name1 + "'," +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID] = " + plantInfo.Id + ";");
                }
                #endregion

                #region 车间
                if (string.IsNullOrEmpty(sapPlantStructureInfo.Zbm))
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PLANT_STRUCTURE] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000079'," +///车间代码不能为空
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapPlantStructureInfo.Id + ";");
                    continue;
                }
                ///如果存在工厂, 判断(工厂-部门)
                WorkshopInfo workshopInfo = workshopInfos.FirstOrDefault(d => d.Workshop == sapPlantStructureInfo.Zbm && d.Plant == sapPlantStructureInfo.Werks);
                ///如果部门不存在进行添加
                if (workshopInfo == null)
                {
                    #region TM_BAS_WORKSHOP
                    stringBuilder.AppendFormat("insert into [LES].[TM_BAS_WORKSHOP] ("
                      + "[FID] ,"
                      + "[PLANT] ,"
                      + "[WORKSHOP] ,"
                      + "[WORKSHOP_NAME] ,"
                      + "[COMMENTS] ,"
                      + "[VALID_FLAG] ,"
                      + "[CREATE_USER] ,"
                      + "[CREATE_DATE] )VALUES  ( "
                      + "NEWID() ," //// FID - uniqueidentifier
                      + "N'{0}' ," //// PLANT - nvarchar(20)
                      + "N'{1}' ," //// WORKSHOP - nvarchar(20)
                      + "N'{2}' ," //// WORKSHOP_NAME - nvarchar(100)
                      + "NULL ," //// COMMENTS - nvarchar(200)         
                      + "{3} ," //// VALID_FLAG - bit
                      + "N'{4}' ," //// CREATE_USER - nvarchar(50)
                      + "GETDATE()) ;", //// CREATE_DATE - datetime  
                        sapPlantStructureInfo.Werks,//// PLANT - nvarchar(20),0
                        sapPlantStructureInfo.Zbm,//// WORKSHOP - nvarchar(20),1
                        sapPlantStructureInfo.Zbmms,//// WORKSHOP_NAME - nvarchar(100),2
                        1,//// VALID_FLAG - bit,3
                        loginUser);//// CREATE_USER - nvarchar(50),4
                    #endregion

                    WorkshopInfo workshop = new WorkshopInfo();
                    workshop.Plant = sapPlantStructureInfo.Werks;
                    workshop.Workshop = sapPlantStructureInfo.Zbm;
                    workshopInfos.Add(workshop);
                }
                else
                {
                    ///更新车间名称
                    stringBuilder.AppendFormat("update [LES].[TM_BAS_WORKSHOP] " +
                        "set [WORKSHOP_NAME] = N'" + sapPlantStructureInfo.Zbmms + "'," +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID] = " + workshopInfo.Id + ";");
                }
                #endregion

                #region 生产线
                if (string.IsNullOrEmpty(sapPlantStructureInfo.Zcj))
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PLANT_STRUCTURE] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000105'," +///生产线代码不能为空
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapPlantStructureInfo.Id + ";");
                    continue;
                }
                ///如果存在工厂,部门, 判断(工厂-部门-生产线)
                AssemblyLineInfo assemblyLineInfo = assemblyLineInfos.FirstOrDefault(d =>
                d.AssemblyLine == sapPlantStructureInfo.Zcj &&
                d.Workshop == sapPlantStructureInfo.Zbm &&
                d.Plant == sapPlantStructureInfo.Werks);
                ///如果生产线不存在进行添加
                if (assemblyLineInfo == null)
                {
                    #region TM_BAS_ASSEMBLY_LINE
                    stringBuilder.AppendFormat("insert into [LES].[TM_BAS_ASSEMBLY_LINE]("
                     + "[FID] ,"
                     + "[PLANT] ,"
                     + "[WORKSHOP] ,"
                     + "[ASSEMBLY_LINE] ,"
                     + "[ASSEMBLY_LINE_NAME] ,"
                     + "[VALID_FLAG] ,"
                     + "[CREATE_USER] ,"
                     + "[CREATE_DATE]) values ("
                     + "NEWID() ," //// FID - uniqueidentifier
                     + "N'{0}' ," //// PLANT - nvarchar(20)
                     + "N'{1}' ," //// WORKSHOP - nvarchar(20)
                     + "N'{2}' ," //// ASSEMBLY_LINE - nvarchar(20)
                     + "N'{3}' ," //// ASSEMBLY_LINE_NAME - nvarchar(100)
                     + "{4} ," //// VALID_FLAG - bit
                     + "N'{5}' ," //// CREATE_USER - nvarchar(50)
                     + "GETDATE()) ;", //// CREATE_DATE - datetime      
                     sapPlantStructureInfo.Werks,//// PLANT - nvarchar(20),0
                     sapPlantStructureInfo.Zbm,//// WORKSHOP - nvarchar(20),1
                     sapPlantStructureInfo.Zcj,//// ASSEMBLY_LINE - nvarchar(20),2
                     sapPlantStructureInfo.Zcjms,//// ASSEMBLY_LINE_NAME - nvarchar(100),3
                     1,//// VALID_FLAG - bit,4
                     loginUser);//// CREATE_USER - nvarchar(50),5
                    #endregion

                    AssemblyLineInfo assemblyLine = new AssemblyLineInfo();
                    assemblyLine.Plant = sapPlantStructureInfo.Werks;
                    assemblyLine.Workshop = sapPlantStructureInfo.Zbm;
                    assemblyLine.AssemblyLine = sapPlantStructureInfo.Zcj;
                    assemblyLineInfos.Add(assemblyLine);
                }
                else
                {
                    ///更新生产线名称
                    stringBuilder.AppendFormat("update [LES].[TM_BAS_ASSEMBLY_LINE] " +
                        "set [ASSEMBLY_LINE_NAME] = N'" + sapPlantStructureInfo.Zcjms + "'," +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID] = " + assemblyLineInfo.Id + ";");
                }
                #endregion

                #region 工段
                if (string.IsNullOrEmpty(sapPlantStructureInfo.LineNo))
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PLANT_STRUCTURE] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000088'," +///工段代码不能为空
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapPlantStructureInfo.Id + ";");
                    continue;
                }
                ///如果存在 工厂-部门-生产线,判断工段
                WorkshopSectionInfo workshopSectionInfo = workshopSectionInfos.FirstOrDefault(d =>
                d.WorkshopSection == sapPlantStructureInfo.LineNo &&
                d.AssemblyLine == sapPlantStructureInfo.Zcj &&
                d.Workshop == sapPlantStructureInfo.Zbm &&
                d.Plant == sapPlantStructureInfo.Werks);
                ///如果工段不存在进行添加
                if (workshopSectionInfo == null)
                {
                    #region TM_BAS_WORKSHOP_SECTION
                    stringBuilder.AppendFormat("insert into [LES].[TM_BAS_WORKSHOP_SECTION] ("
                      + "[FID] ,"
                      + "[PLANT] ,"
                      + "[WORKSHOP] ,"
                      + "[ASSEMBLY_LINE] ,"
                      + "[WORKSHOP_SECTION] ,"
                      + "[WORKSHOP_SECTION_NAME] ,"
                      + "[VALID_FLAG] ,"
                      + "[CREATE_USER] ,"
                      + "[CREATE_DATE] )VALUES  ("
                      + "NEWID() ," //// FID - uniqueidentifier
                      + "N'{0}' ," //// PLANT - nvarchar(20)
                      + "N'{1}' ," //// WORKSHOP - nvarchar(20)
                      + "N'{2}' ," //// ASSEMBLY_LINE - nvarchar(20)
                      + "N'{3}' ," //// WORKSHOP_SECTION - nvarchar(20)
                      + "N'{4}' ," //// WORKSHOP_SECTION_NAME - nvarchar(200       
                      + "{5} ," //// VALID_FLAG - bit
                      + "N'{6}' ," //// CREATE_USER - nvarchar(50)
                      + "GETDATE());",//// CREATE_DATE - datetime 
                       sapPlantStructureInfo.Werks,//// PLANT - nvarchar(20),0
                       sapPlantStructureInfo.Zbm,//// WORKSHOP - nvarchar(20),1
                       sapPlantStructureInfo.Zcj, //// ASSEMBLY_LINE - nvarchar(20),2
                       sapPlantStructureInfo.LineNo,//// WORKSHOP_SECTION - nvarchar(20),3
                       sapPlantStructureInfo.LineNoms,//// WORKSHOP_SECTION_NAME - nvarchar(200),4
                       1,//// VALID_FLAG - bit,5
                       loginUser);//// CREATE_USER - nvarchar(50),6
                    #endregion

                    WorkshopSectionInfo workshopSection = new WorkshopSectionInfo();
                    workshopSection.Plant = sapPlantStructureInfo.Werks;
                    workshopSection.Workshop = sapPlantStructureInfo.Zbm;
                    workshopSection.AssemblyLine = sapPlantStructureInfo.Zcj;
                    workshopSection.WorkshopSection = sapPlantStructureInfo.LineNo;
                    workshopSectionInfos.Add(workshopSection);
                }
                else
                {
                    ///更新工段名称
                    stringBuilder.AppendFormat("update [LES].[TM_BAS_WORKSHOP_SECTION] " +
                        "set [WORKSHOP_SECTION_NAME] = N'" + sapPlantStructureInfo.LineNoms + "'," +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID] = " + workshopSectionInfo.Id + ";");
                }
                #endregion

                #region 工位
                if (string.IsNullOrEmpty(sapPlantStructureInfo.Vlsch))
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PLANT_STRUCTURE] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000172'," +///工位代码不能为空
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapPlantStructureInfo.Id + ";");
                    continue;
                }
                ///如果存在 工厂-部门-生产线-工段,判断工位
                LocationInfo locationInfo = locationInfos.FirstOrDefault(d =>
                d.Location == sapPlantStructureInfo.Vlsch &&
                d.WorkshopSection == sapPlantStructureInfo.LineNo &&
                d.AssemblyLine == sapPlantStructureInfo.Zcj &&
                d.Workshop == sapPlantStructureInfo.Zbm &&
                d.Plant == sapPlantStructureInfo.Werks);
                if (locationInfo == null)
                {
                    #region TM_BAS_LOCATION
                    stringBuilder.AppendFormat("insert into [LES].[TM_BAS_LOCATION] ("
                      + "[FID] ,"
                      + "[PLANT] ,"
                      + "[WORKSHOP] ,"
                      + "[ASSEMBLY_LINE] ,"
                      + "[WORKSHOP_SECTION] ,"
                      + "[LOCATION] ,"
                      + "[LOCATION_NAME] ,"
                      + "[VALID_FLAG] ,"
                      + "[CREATE_USER] ,"
                      + "[CREATE_DATE],"
                      + "[SEQUENCE_NO]) values ( "
                      + "NEWID() ," //// FID - uniqueidentifier
                      + "N'{0}' ," //// PLANT - nvarchar(20)
                      + "N'{1}' ," //// WORKSHOP - nvarchar(20)
                      + "N'{2}' ," //// ASSEMBLY_LINE - nvarchar(20)
                      + "N'{3}' ," //// WORKSHOP_SECTION - nvarchar(20)
                      + "N'{4}' ," //// LOCATION - nvarchar(20)
                      + "N'{5}' ," //// LOCATION_NAME - nvarchar(50)          
                      + "{6} ," //// VALID_FLAG - bit
                      + "N'{7}' ," //// CREATE_USER - nvarchar(50)                   
                      + "GETDATE()," //// CREATE_DATE - datetime
                      + "{8}) ;",   ///
                         sapPlantStructureInfo.Werks, //// PLANT - nvarchar(20),0
                         sapPlantStructureInfo.Zbm,//// WORKSHOP - nvarchar(20),1
                         sapPlantStructureInfo.Zcj,//// ASSEMBLY_LINE - nvarchar(20),2
                         sapPlantStructureInfo.LineNo,//// WORKSHOP_SECTION - nvarchar(20),3
                         sapPlantStructureInfo.Vlsch,//// LOCATION - nvarchar(20),4
                         sapPlantStructureInfo.Txt,//// LOCATION_NAME - nvarchar(50),5
                         1,//// VALID_FLAG - bit,6
                         loginUser,//// CREATE_USER - nvarchar(50),7
                         sapPlantStructureInfo.Zsx); ///xsx nvarchar(20)
                    #endregion

                    LocationInfo location = new LocationInfo();
                    location.Plant = sapPlantStructureInfo.Werks;
                    location.Workshop = sapPlantStructureInfo.Zbm;
                    location.AssemblyLine = sapPlantStructureInfo.Zcj;
                    location.WorkshopSection = sapPlantStructureInfo.LineNo;
                    location.Location = sapPlantStructureInfo.Vlsch;
                    locationInfos.Add(location);
                }
                else
                {
                    ///更新工位名称
                    stringBuilder.AppendFormat("update [LES].[TM_BAS_LOCATION] " +
                        "set [LOCATION_NAME] = N'" + sapPlantStructureInfo.Txt + "'," +
                        "[SEQUENCE_NO]=N'" + sapPlantStructureInfo.Zsx + "', " +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID] = " + locationInfo.Id + ";");
                }
                #endregion

                dealedIds.Add(sapPlantStructureInfo.Id);
            }

            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态 工厂布局中间表 TI_IFM_SAP_PLANT_STRUCTURE
                stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PLANT_STRUCTURE] " +
                    "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                    "[PROCESS_TIME] = GETDATE()," +
                    "[COMMENTS] = NULL," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");

            using (var trans = new TransactionScope())
            {
                if (stringBuilder.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                trans.Complete();
            }
        }
    }
}
