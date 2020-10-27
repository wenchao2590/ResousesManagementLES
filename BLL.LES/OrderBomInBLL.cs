using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class OrderBomInBLL
    {
        #region Common
        OrderBomInDAL dal = new OrderBomInDAL();
        public List<OrderBomInInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public List<OrderBomInInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        public OrderBomInInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(OrderBomInInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(long id)
        {
            return dal.Delete(id) > 0 ? true : false;
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        /// <summary>
        ///  获取未生产计划拉动的生产单
        /// </summary>
        /// <param name="SendTime"></param>
        /// <param name="AssemblyLine"></param>
        /// <param name="Fid"></param>
        /// <returns></returns>
        //public List<OrderBomInInfo> GetList(DateTime SendTime,string AssemblyLine,Guid Fid)
        //{
        //    string sql = string.Format(@"select [ZORD_NO],[ZCOM_NO],[SUPPLIER_NUM],[ZQTY] from LES.TI_ODS_ORDER_BOM_IN with(nolock) where [VALID_FLAG] = 1 and [ZORD_NO] in (select [ORDER_NO] from LES.TT_BAS_PULL_ORDERS  where [VALID_FLAG] = 1 and [PLAN_EXECUTE_TIME] <= N'{0}' and [ASSEMBLY_LINE] = N'{1}' and [ID] in (select [ORDER_ID] from LES.TT_MPM_PLAN_PULL_CREATE_STATUS where [VALID_FLAG] = 1 and [STATUS] = 10 and [PART_BOX_FID] = N'{2}'))", SendTime, AssemblyLine, Fid);
        //    return dal.GetList(sql);
        //}
        /// <summary>
        ///  获取未生产计划拉动的生产单
        /// </summary>
        /// <param name="SumTime"></param>
        /// <param name="AssemblyLine"></param>
        /// <param name="Fid"></param>
        /// <returns></returns>
        //public List<OrderBomInInfo> GetList(int SumTime, string AssemblyLine, Guid Fid)
        //{
        //    string sql = string.Format(@"select [ZORD_NO],[ZCOM_NO],[SUPPLIER_NUM],[ZQTY] from LES.TI_ODS_ORDER_BOM_IN with(nolock) where [VALID_FLAG] = 1 and [ZORD_NO] in (select [ORDER_NO] from LES.TT_BAS_PULL_ORDERS  where [VALID_FLAG] = 1 and (dateadd(minute,-{0},[ORDER_DATE]) >= [ORDER_DATE] and dateadd(minute,-{0},[ORDER_DATE])<dateadd(day,1,[ORDER_DATE])) and [ASSEMBLY_LINE] = N'{1}' and [ID] in (select [ORDER_ID] from LES.TT_MPM_PLAN_PULL_CREATE_STATUS where [VALID_FLAG] = 1 and [STATUS] = 10 and [PART_BOX_FID] = N'{2}'))", SumTime, AssemblyLine, Fid);
        //    return dal.GetList(sql);
        //}
    }
}

