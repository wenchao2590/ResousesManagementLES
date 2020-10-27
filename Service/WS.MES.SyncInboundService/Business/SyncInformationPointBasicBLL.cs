namespace WS.MES.SyncInboundService
{
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System;
    using Infrustructure.Logging;

    public partial class SyncInformationPointBasicBLL
    {

        /// <summary>
        /// MES-LES-006-信息点基础信息同步 信息点基础数据
        /// </summary>
        /// <returns></returns>
        public static void SyncInformationPointBasic(string loginUser)
        {

            ///sql 添加语句
            StringBuilder sql = new StringBuilder();

            ///获取未处理的信息点中间表数据
            List<MesInformationPointBasicInfo>   mesInformationPointBasicInfos = new MesInformationPointBasicBLL().GetListByPage("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", "[ID]", 1, 1000, out int dataCnt);
            if (dataCnt == 0) return;

            List<long> dealedIds = new List<long>();

            
            
            ///循环未处理状态的中间表信息
            //foreach (var mesInformationPointBasicInfo in mesInformationPointBasicInfos)
            //{
            //    ///判断是否存在该物料如果不存在修改为挂起状态, 且Common=7x00000017   (物料号不存在)
            //    MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == qmisCheckModeInfo.PartNo);
            //    if (maintainPartsInfo == null)
            //    {
            //        sql.Append("UPDATE [LES].[TI_IFM_QMIS_CHECK_MODE] SET [PROCESS_FLAG]="+(int)ProcessFlagConstants.Suspend+" , [COMMENTS]='7x00000017' WHERE [ID]='"+ qmisCheckModeInfo.Id + "';" );
            //        continue;
            //    }

            //    ///判断是否存在该供应商, 如果不存在修改为挂起状态, 且Common=7x00000018  (供应商不存在)
            //    SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == qmisCheckModeInfo.SupplierNo);
            //    if (supplierInfo == null)
            //    {
            //        sql.Append("UPDATE [LES].[TI_IFM_QMIS_CHECK_MODE] SET [PROCESS_FLAG]=" + (int)ProcessFlagConstants.Suspend + " , [COMMENTS]='7x00000018' WHERE [ID]='" + qmisCheckModeInfo.Id + "';");
            //        continue;
            //    }

            //    ///检查检验模式, 如果检验模式不存在0,1,2 之间., 修改为挂起状态. 且Common=7x00000019(检验模式不存在) 0:免检；1：抽检；2：批检
            //    if (qmisCheckModeInfo.CheckMode.Trim() !="0" && qmisCheckModeInfo.CheckMode.Trim() != "1"&& qmisCheckModeInfo.CheckMode.Trim() != "2")
            //    {
            //        sql.Append("UPDATE [LES].[TI_IFM_QMIS_CHECK_MODE] SET [PROCESS_FLAG]=" + (int)ProcessFlagConstants.Suspend + " , [COMMENTS]='7x00000019' WHERE [ID]='" + qmisCheckModeInfo.Id + "';");
            //        continue;
            //    }


            //    ///添加执行语句, 如果存在,就修改检验模式,  如果不存在进行新增
            //    sql.Append("if  exists(select * from LES.TM_BAS_PART_INSPECTION_MODE with(nolock) where "
            //            + "[PART_NO] = N'" + qmisCheckModeInfo.PartNo + "' and "
            //            + "[SUPPLIER_NUM] = N'" + qmisCheckModeInfo.SupplierNo + "' and "
            //            + "[VALID_FLAG] = 1) "
            //            + "UPDATE  [LES].[TM_BAS_PART_INSPECTION_MODE] SET "
            //            + "[INSPECTION_MODE]='" + qmisCheckModeInfo.CheckMode + "'"
            //            + " WHERE [VALID_FLAG]=1 AND [PART_NO]='" + qmisCheckModeInfo.PartNo + "' AND [SUPPLIER_NUM]='" + qmisCheckModeInfo.SupplierNo + "'; ");
            //    sql.AppendLine();

            //    ///添加执行语句, 如果不存在就添加. 
            //    sql.AppendFormat("if not exists (select * from LES.TM_BAS_PART_INSPECTION_MODE with(nolock) where "
            //       + "[PART_NO] = N'{0}' and "
            //       + "[SUPPLIER_NUM] = N'{1}' and " 
            //       + "[VALID_FLAG] = 1) "
            //       + "INSERT INTO [LES].[TM_BAS_PART_INSPECTION_MODE] ("
            //       + "[FID] ,"
            //       + "[PART_NO] ,"
            //       + "[SUPPLIER_NUM] ,"
            //       + "[INSPECTION_MODE] ,"
            //       + "[VALID_FLAG] ,"
            //       + "[CREATE_DATE] ,"
            //       + "[CREATE_USER] ) VALUES  (   "
            //       + "NEWID() ," /// FID - uniqueidentifier
            //       + "N'{2}' ," /// PART_NO - nvarchar(32)
            //       + "N'{3}' ," /// SUPPLIER_NUM - nvarchar(32)
            //       + "{4} ," /// INSPECTION_MODE - int
            //       + "1 ," /// VALID_FLAG - bit
            //       + "GETDATE() ," /// CREATE_DATE - datetime
            //       + "N'{5}' ) ; ",/// CREATE_USER - nvarchar(64)  
            //       qmisCheckModeInfo.PartNo,
            //       qmisCheckModeInfo.SupplierNo,
            //       qmisCheckModeInfo.PartNo,
            //       qmisCheckModeInfo.SupplierName,
            //       Convert.ToInt32(qmisCheckModeInfo.CheckMode),
            //       loginUser);
            //    sql.AppendLine();
            //    dealedIds.Add(qmisCheckModeInfo.Id);
            //}

            if (dealedIds.Count > 0)
                ///中间表数据更新为已处理状态, 修改时间,修改人
                sql.Append("update [LES].[TI_IFM_QMIS_CHECK_MODE] "
                    + "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + ",[PROCESS_TIME] = GETDATE() , [MODIFY_DATE]=GETDATE(),[MODIFY_USER]='" + loginUser   
                    + "' where [ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");
            sql.AppendLine();

            if (sql.ToString().Length > 0)
            {
                Log.WriteLogToFile(sql.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\SQL-Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sql.ToString());
            }

        }
     
    }
}
;