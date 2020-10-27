using DM.LES;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DM.SYS;

namespace DAL.LES
{
    public partial class PullOrderBomDAL
    {
        /// <summary>
        /// 根据窗口时间(在此窗口时间之前)、生产线、零件类
        /// 获取未生成过该零件类计划拉动单的生产订单BOM
        /// </summary>
        /// <param name="currentWindowTime"></param>
        /// <param name="assemblyLine"></param>
        /// <param name="partBoxFid"></param>
        /// <returns></returns>
        public List<PullOrderBomInfo> GetUnPlanPullingOrders(DateTime currentWindowTime, DateTime nextWindowTime, string sqlwhere, Guid partBoxFid)
        {
            string sql = "select [ZORDNO],[ZKWERK],[ZCOMNO],[ZQTY],[ZDATE],[SUPPLIER_NUM],[ORDERFID] "
                + "from [LES].[TT_BAS_PULL_ORDER_BOM] with(nolock) "
                + "where [VALID_FLAG] = 1 "
                + "and [ZORDNO] in (select [ORDER_NO] from [LES].[TT_BAS_PULL_ORDERS] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PLAN_EXECUTE_TIME] >= @PLAN_EXECUTE_TIME and [PLAN_EXECUTE_TIME] < @PLAN_EXECUTE_TIME_N " + sqlwhere + " "
                + "and [FID] in (select [ORDER_FID] from [LES].[TT_MPM_PLAN_PULL_CREATE_STATUS] with(nolock) "
                + "where [VALID_FLAG] = 1 and [STATUS] = @STATUS and [PART_BOX_FID] = @PART_BOX_FID))";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PLAN_EXECUTE_TIME", DbType.DateTime, currentWindowTime);
            db.AddInParameter(cmd, "@PLAN_EXECUTE_TIME_N", DbType.DateTime, nextWindowTime);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, (int)ProcessFlagConstants.Untreated);
            db.AddInParameter(cmd, "@PART_BOX_FID", DbType.Guid, partBoxFid);
            List<PullOrderBomInfo> list = new List<PullOrderBomInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    PullOrderBomInfo info = new PullOrderBomInfo();
                    info.Orderfid = DBConvert.GetGuidNullable(dr, dr.GetOrdinal("ORDERFID"));
                    info.Zordno = DBConvert.GetString(dr, dr.GetOrdinal("ZORDNO"));
                    info.Zkwerk = DBConvert.GetString(dr, dr.GetOrdinal("ZKWERK"));
                    info.Zcomno = DBConvert.GetString(dr, dr.GetOrdinal("ZCOMNO"));
                    info.Zqty = DBConvert.GetInt32Nullable(dr, dr.GetOrdinal("ZQTY"));
                    info.Zdate = DBConvert.GetDateTimeNullable(dr, dr.GetOrdinal("ZDATE"));
                    info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                    list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据生产订单号获取生产订单物料清单
        /// </summary>
        /// <param name="productOrderNo"></param>
        /// <returns></returns>
        public List<PullOrderBomInfo> GetPartList(string productOrderNo)
        {
            string sql = "select [ZCOMNO],[ZQTY],[SUPPLIER_NUM] from [LES].[TT_BAS_PULL_ORDER_BOM] with(nolock) " +
                "where [VALID_FLAG] = 1 and [ZORDNO] = @ZORDNO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ZORDNO", DbType.AnsiString, productOrderNo);
            List<PullOrderBomInfo> list = new List<PullOrderBomInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    PullOrderBomInfo info = new PullOrderBomInfo();
                    info.Zcomno = DBConvert.GetString(dr, dr.GetOrdinal("ZCOMNO"));
                    info.Zqty = DBConvert.GetInt32Nullable(dr, dr.GetOrdinal("ZQTY"));
                    info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                    list.Add(info);
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuFid"></param>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <returns></returns>
        public List<PullOrderBomInfo> GetListByPage(Guid menuFid, string textWhere, string textOrder, int pageIndex, int pageRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
            string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where [VALID_FLAG] = 1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and [VALID_FLAG] = 1";
            }
            else
                whereText += " where [VALID_FLAG] = 1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TT_BAS_PULL_ORDER_BOM]  WITH(NOLOCK) " + whereText + " and [ORDERFID] = N'" + menuFid + "' ) T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PullOrderBomInfo> list = new List<PullOrderBomInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreatePullOrderBomInfo(dr));
                }
            }
            return list;
        }
    }
}
