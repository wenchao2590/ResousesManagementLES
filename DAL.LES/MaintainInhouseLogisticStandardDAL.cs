using DM.LES;
using DM.SYS;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace DAL.LES
{
    public partial class MaintainInhouseLogisticStandardDAL
    {
        /// <summary>
        /// 根据看板零件类、零件号获取看板环大小
        /// </summary>
        /// <param name="inhouseSystemMode"></param>
        /// <param name="inhousePartClass"></param>
        /// <param name="partNo"></param>
        /// <returns></returns>
        public int GetKanbanCircleCnt(string inhousePartClass, string partNo)
        {
            ///INHOUSE_SYSTEM_MODE
            ///INHOUSE_PART_CLASS
            ///PART_NO
            string sql = "select [KANBAN_CIRCLE_CNT] from [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] with(nolock) "
                + "where [VALID_FLAG] = 1 and [STATUS] = @STATUS and [INHOUSE_SYSTEM_MODE] = @INHOUSE_SYSTEM_MODE and [INHOUSE_PART_CLASS] = @INHOUSE_PART_CLASS and [PART_NO] = @PART_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, (int)BasicDataStatusConstants.Enable);
            db.AddInParameter(cmd, "@INHOUSE_SYSTEM_MODE", DbType.Int32, (int)PullModeConstants.Kanban);
            db.AddInParameter(cmd, "@INHOUSE_PART_CLASS", DbType.AnsiString, inhousePartClass);
            db.AddInParameter(cmd, "@PART_NO", DbType.AnsiString, partNo);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt32(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partNos"></param>
        /// <returns></returns>
        public List<MaintainInhouseLogisticStandardInfo> GetListForInterfaceDataSync(List<string> partNos)
        {
            string sql = "select [ID],[PART_NO],[SUPPLIER_NUM],[INHOUSE_SYSTEM_MODE],[INHOUSE_PART_CLASS] "
                + "from [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_NO] in ('" + string.Join("','", partNos.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<MaintainInhouseLogisticStandardInfo> list = new List<MaintainInhouseLogisticStandardInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    MaintainInhouseLogisticStandardInfo info = new MaintainInhouseLogisticStandardInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.PartNo = DBConvert.GetString(dr, dr.GetOrdinal("PART_NO"));
                    info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                    info.InhouseSystemMode = DBConvert.GetString(dr, dr.GetOrdinal("INHOUSE_SYSTEM_MODE"));
                    info.InhousePartClass = DBConvert.GetString(dr, dr.GetOrdinal("INHOUSE_PART_CLASS"));
                    list.Add(info);
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inhouseSystemMode"></param>
        /// <param name="inhousePartClass"></param>
        /// <param name="partsBoxInfo"></param>
        /// <returns></returns>
        public bool UpdatePartsBoxInfo(PartsBoxInfo partsBoxInfo)
        {
            string sql = "update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] "
                + "set [PLANT] = @PLANT,[WORKSHOP] = @WORKSHOP,[ASSEMBLY_LINE] = @ASSEMBLY_LINE "
                + "where [INHOUSE_SYSTEM_MODE] =@INHOUSE_SYSTEM_MODE and [INHOUSE_PART_CLASS] = @INHOUSE_PART_CLASS and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PLANT", DbType.AnsiString, partsBoxInfo.Plant);
            db.AddInParameter(cmd, "@WORKSHOP", DbType.AnsiString, partsBoxInfo.Workshop);
            db.AddInParameter(cmd, "@ASSEMBLY_LINE", DbType.AnsiString, partsBoxInfo.AssemblyLine);
            db.AddInParameter(cmd, "@INHOUSE_SYSTEM_MODE", DbType.AnsiString, partsBoxInfo.PullMode.ToString());
            db.AddInParameter(cmd, "@INHOUSE_PART_CLASS", DbType.AnsiString, partsBoxInfo.BoxParts);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
        /// <summary>
        /// 根据生产线+物料号集合获取需要校验物料库存的基础数据
        /// </summary>
        /// <param name="assemblyLine"></param>
        /// <param name="partNos"></param>
        /// <returns></returns>
        public List<MaintainInhouseLogisticStandardInfo> GetCheckMaterialStockList(string assemblyLine, List<string> partNos)
        {
            string sql = "select [PART_NO],[SUPPLIER_NUM],[S_ZONE_NO],[S_WM_NO] from [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] with(nolock) "
                + "where [VALID_FLAG] = 1 and [CHECK_MATERIAL_STOCK_FLAG] =1 and [ASSEMBLY_LINE] = @ASSEMBLY_LINE and [PART_NO] in ('" + string.Join("','", partNos.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ASSEMBLY_LINE", DbType.AnsiString, assemblyLine);
            List<MaintainInhouseLogisticStandardInfo> list = new List<MaintainInhouseLogisticStandardInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    MaintainInhouseLogisticStandardInfo info = new MaintainInhouseLogisticStandardInfo();
                    info.PartNo = DBConvert.GetString(dr, dr.GetOrdinal("PART_NO"));
                    info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                    info.SZoneNo = DBConvert.GetString(dr, dr.GetOrdinal("S_ZONE_NO"));
                    info.SWmNo = DBConvert.GetString(dr, dr.GetOrdinal("S_WM_NO"));
                    list.Add(info);
                }
            }
            return list;
        }
        /// <summary>
        /// 根据外键获取对象
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public MaintainInhouseLogisticStandardInfo GetInfoByFid(Guid fid)
        {
            string sql = "select * from [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] with(nolock) " +
                "where [VALID_FLAG] = 1 and [FID] = @FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FID", DbType.Guid, fid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateMaintainInhouseLogisticStandardInfo(dr);
            }
            return null;
        }
    }
}
