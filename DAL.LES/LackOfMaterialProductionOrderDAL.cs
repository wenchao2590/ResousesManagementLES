using DM.LES;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Infrustructure.Utilities;

namespace DAL.LES
{
    public partial class LackOfMaterialProductionOrderDAL
    {
        public DataTable GetListByPage(Guid menuFid, string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + "(select row_number() over(order by T1.[ID] desc) as rownumber,T1.ID,T1.PART_NO as PartNo,T1.LACK_QTY as LackQty,T2.PRODUCTION_ORDER_NO as ProductionOrderNo,T2.ASSEMBLY_LINE as AssemblyLine,T2.ORDER_DATE as OrderDate,T1.PART_PURCHASER as PartPurchaser from [LES].[TT_ATP_LACK_OF_MATERIAL_PRODUCTION_ORDER_DETAIL] T1 "
                + " left join "
                + " (select c1.ASSEMBLY_LINE,c1.PRODUCTION_ORDER_NO,c2.ORDER_DATE,c1.FID,c1.LACK_ORDER_FID from [LES].[TT_ATP_LACK_OF_MATERIAL_PRODUCTION_ORDER] c1 "
                + " left join [LES].[TT_BAS_PULL_ORDERS] c2 on c1.PRODUCTION_ORDER_FID=c2.FID where c1.VALID_FLAG=1 and c2.VALID_FLAG=1 ) T2 "
                + " on T1.PRODUCTION_ORDER_FID=T2.FID "
                +  whereText + " and [LACK_ORDER_FID] = N'" + menuFid+"' )"
                + "T where rownumber > " + (pageIndex - 1) * pageRow + " ";
          Database db = DatabaseFactory.CreateDatabase();
          DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataTable dr = db.ExecuteDataTable(dbCommand);
          
          return dr;
      }
        /// <summary>
        /// 分页查询
        /// </summary>        
        /// <param name="textWhere">查询条件</param>
        /// <param name="orderText">排序字段</param>            
        /// <returns></returns>
        public int GetCount(string textWhere)
        {
            if (string.IsNullOrEmpty(textWhere))
                textWhere = string.Empty;
            
            string sql = "select count(*) from (select row_number() over(order by T1.[ID] desc) as rownumber,T1.ID,T1.PART_NO,T1.LACK_QTY,T2.PRODUCTION_ORDER_NO,T2.ASSEMBLY_LINE,T2.ORDER_DATE,T1.PART_PURCHASER as PartPurchaser from [LES].[TT_ATP_LACK_OF_MATERIAL_PRODUCTION_ORDER_DETAIL] T1  left join  (select c1.ASSEMBLY_LINE,c1.PRODUCTION_ORDER_NO,c2.ORDER_DATE,c1.FID,c1.LACK_ORDER_FID from [LES].[TT_ATP_LACK_OF_MATERIAL_PRODUCTION_ORDER] c1  left join [LES].[TT_BAS_PULL_ORDERS] c2 on c1.PRODUCTION_ORDER_FID=c2.FID where c1.VALID_FLAG=1 and c2.VALID_FLAG=1 ) T2  on T1.PRODUCTION_ORDER_FID=T2.FID  where " + textWhere + " )T where rownumber > 0 ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 分页查询
        /// </summary>        
        /// <param name="textWhere">查询条件</param>
        /// <param name="orderText">排序字段</param>            
        /// <returns></returns>
        public DataTable DataTable(string textWhere)
        {
            if (string.IsNullOrEmpty(textWhere))
                textWhere = string.Empty;

            string sql = "select * from (select row_number() over(order by T1.[ID] desc) as rownumber,T1.ID,T1.PART_NO,T1.LACK_QTY,T2.PRODUCTION_ORDER_NO,T2.ASSEMBLY_LINE,T2.ORDER_DATE,T1.PART_PURCHASER from [LES].[TT_ATP_LACK_OF_MATERIAL_PRODUCTION_ORDER_DETAIL] T1  left join  (select c1.ASSEMBLY_LINE,c1.PRODUCTION_ORDER_NO,c2.ORDER_DATE,c1.FID,c1.LACK_ORDER_FID from [LES].[TT_ATP_LACK_OF_MATERIAL_PRODUCTION_ORDER] c1  left join [LES].[TT_BAS_PULL_ORDERS] c2 on c1.PRODUCTION_ORDER_FID=c2.FID where c1.VALID_FLAG=1 and c2.VALID_FLAG=1 ) T2  on T1.PRODUCTION_ORDER_FID=T2.FID  where  VALID_FLAG = 1 " + textWhere + " )T where rownumber > 0 ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataTable dr = db.ExecuteDataTable(dbCommand);
            return dr;
        }
        public DataTable GetListExcel(Guid menuFid, string textWhere, string textOrder,List<string> columns)
        {
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
            string sql = "select "+ string.Join(", ", columns.ToArray()) + " from ("
                + "select row_number() over(order by T1.[ID] desc) as rownumber,T1.ID,T1.PART_NO,T1.LACK_QTY,T2.PRODUCTION_ORDER_NO,T2.ASSEMBLY_LINE,T2.ORDER_DATE,T1.PART_PURCHASER from [LES].[TT_ATP_LACK_OF_MATERIAL_PRODUCTION_ORDER_DETAIL] T1 "
                + " left join "
                + " (select c1.ASSEMBLY_LINE,c1.PRODUCTION_ORDER_NO,c2.ORDER_DATE,c1.FID,c1.LACK_ORDER_FID from [LES].[TT_ATP_LACK_OF_MATERIAL_PRODUCTION_ORDER] c1 "
                + " left join [LES].[TT_BAS_PULL_ORDERS] c2 on c1.PRODUCTION_ORDER_FID=c2.FID where c1.VALID_FLAG=1 and c2.VALID_FLAG=1 ) T2 "
                + " on T1.PRODUCTION_ORDER_FID=T2.FID "
                + whereText + " and [LACK_ORDER_FID] = N'" + menuFid + "' ) T1";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataTable dr = db.ExecuteDataTable(dbCommand);

            return dr;
        }
    }
}
