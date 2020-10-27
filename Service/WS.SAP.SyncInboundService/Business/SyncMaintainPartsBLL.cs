using BLL.LES;
namespace WS.SAP.SyncInboundService
{
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// SyncMaintainPartsBLL
    /// </summary>
    public class SyncMaintainPartsBLL
    {
        /// <summary>
        /// SAP物料基础数据同步
        /// </summary>
        /// <returns></returns>
        public static void Sync(string loginUser)
        {
            ///从中间表提取未处理数据集合
            List<SapPartsInfo> sapPartsInfos = new SapPartsBLL().GetListByPage("[PROCESS_FLAG] in(" + (int)ProcessFlagConstants.Untreated + "," + (int)ProcessFlagConstants.Resend + ")", "[ID]", 1, 1000, out int dataCnt);
            if (dataCnt == 0) return;
            ///获取业务表中要变更的数据集合,准备对比
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsBLL().GetListForInterfaceDataSync(sapPartsInfos.Select(d => d.Matnr).ToList());
            ///执行的stringBuilder语句
            StringBuilder stringBuilder = new StringBuilder();
            ///获取工厂信息
            List<PlantInfo> plantInfos = new PlantBLL().GetListForInterfaceDataSync();
            ///已处理完成的ID
            List<long> dealedIds = new List<long>();
            ///逐条处理中间表数据
            foreach (var sapPartsInfo in sapPartsInfos)
            {
                PlantInfo plantInfo = plantInfos.FirstOrDefault(d => d.SapPlantCode == sapPartsInfo.Werks);
                if (plantInfo == null)
                {
                    ///将这样的数据更新为挂起状态
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PARTS] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'3x00000019'," + ///工厂信息不存在
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapPartsInfo.Id + ";");
                    continue;
                }
                ///标识该物料需要删除
                if (sapPartsInfo.Mstae == "1")
                {
                    ///根据工厂代码+物料编号对物料信息进行逻辑删除
                    stringBuilder.AppendLine(" update [LES].[TM_BAS_MAINTAIN_PARTS] " +
                        "set [VALID_FLAG] = 0,[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                        "where [PART_NO] = N'" + sapPartsInfo.Matnr + "' and [PLANT] = N'" + plantInfo.Plant + "' and [VALID_FLAG] = 1;");
                    dealedIds.Add(sapPartsInfo.Id);
                    continue;
                }
                ///物料号①、物料中文名称②为必填项
                if (string.IsNullOrEmpty(sapPartsInfo.MaktxZh) || string.IsNullOrEmpty(sapPartsInfo.Matnr))
                {
                    ///将这样的数据更新为挂起状态
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PARTS] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'3x00000020'," +///物料号、物料中文名称为必填项
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapPartsInfo.Id + ";");
                    continue;
                }
                ///当前业务数据表中此工厂的该物料信息时需要新增
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == sapPartsInfo.Matnr && d.Plant == plantInfo.Plant);
                if (maintainPartsInfo == null)
                {
                    #region TM_BAS_MAINTAIN_PARTS

                    string partCname = string.Empty;
                    string partEname = string.Empty;
                    string partDname = string.Empty;
                    ///BFDA、1中文、E英文、D德文
                    switch (sapPartsInfo.Spras.ToUpper())
                    {
                        case "1": partCname = sapPartsInfo.MaktxZh.Replace("'", "''"); break;
                        case "E": partEname = sapPartsInfo.MaktxZh.Replace("'", "''"); break;
                        case "D": partDname = sapPartsInfo.MaktxZh.Replace("'", "''"); break;
                    }

                    stringBuilder.AppendLine("insert into [LES].[TM_BAS_MAINTAIN_PARTS] ("
                        + "[FID],"
                        + "[PART_NO],"
                        + "[PLANT],"
                        + "[PART_CLS],"
                        + "[PART_CNAME],"
                        + "[PART_ENAME],"
                        + "[PART_DNAME],"
                        + "[PART_UNITS],"
                        + "[MRP_CONTROL],"
                        + "[MRP_TYPE],"
                        + "[CREATE_USER],"
                        + "[CREATE_DATE],"
                        + "[VALID_FLAG],"
                        + "[PART_PURCHASER]"
                        + ") values ("
                        + "NEWID(),"///FID
                        + "N'" + sapPartsInfo.Matnr.Replace("'", "''") + "',"///PART_NO
                        + "N'" + plantInfo.Plant + "',"///PLANT
                        + "N'" + sapPartsInfo.Mtart + "',"///PART_CLS
                        + "N'" + partCname + "',"///PART_CNAME
                        + "N'" + partEname + "',"///PART_ENAME
                        + "N'" + partDname + "',"///PART_DNAME
                        + "N'" + sapPartsInfo.Meins + "',"///PART_UNITS
                        + "N'" + sapPartsInfo.Dispo + "',"///MRP_CONTROL
                        + "N'" + sapPartsInfo.Dismm + "',"///MRP_TYPE
                        + "N'" + loginUser + "',"///CREATE_USER
                        + "GETDATE(),"///CREATE_DATE
                        + "1"///VALID_FLAG
                        +",N'"+ sapPartsInfo.Ekgrp + "'"   ///PART_PURCHASER
                        + ");");
                    #endregion
                    dealedIds.Add(sapPartsInfo.Id);

                    maintainPartsInfos.Add(
                        new MaintainPartsInfo()
                        {
                            Plant = plantInfo.Plant,
                            PartNo = sapPartsInfo.Matnr
                        });
                    continue;
                }
                else
                {
                    string partCname = string.Empty;
                    string partEname = string.Empty;
                    string partDname = string.Empty;
                    ///BFDA、1中文、E英文、D德文
                    switch (sapPartsInfo.Spras.ToUpper())
                    {
                        case "1": partCname = sapPartsInfo.MaktxZh.Replace("'","''"); break;
                        case "E": partEname = sapPartsInfo.MaktxZh.Replace("'", "''"); break;
                        case "D": partDname = sapPartsInfo.MaktxZh.Replace("'", "''"); break;
                    }

                    stringBuilder.AppendLine("update [LES].[TM_BAS_MAINTAIN_PARTS] set "
                        + "[PART_CLS] = N'" + sapPartsInfo.Mtart + "',"
                        + (string.IsNullOrEmpty(partCname) ? string.Empty : "[PART_CNAME] =N'" + partCname + "',")
                        + (string.IsNullOrEmpty(partEname) ? string.Empty : "[PART_ENAME] =N'" + partEname + "',")
                        + (string.IsNullOrEmpty(partDname) ? string.Empty : "[PART_DNAME] =N'" + partDname + "',")
                        + "[PART_UNITS] = N'" + sapPartsInfo.Meins + "',"
                        + "[MRP_CONTROL] = N'" + sapPartsInfo.Dispo + "',"
                        + "[MRP_TYPE] = N'" + sapPartsInfo.Dismm + "'," 
                        + "[PART_PURCHASER]=N'"+ sapPartsInfo.Ekgrp + "'," 
                        + "[MODIFY_USER] = N'" + loginUser + "',"
                        + "[MODIFY_DATE] = GETDATE() "
                        + "where [PART_NO] = '" + maintainPartsInfo.PartNo + "' AND [PLANT]='" + maintainPartsInfo.Plant + "';");
                    dealedIds.Add(sapPartsInfo.Id);
                }

            }
            ///
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_PARTS] " +
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
