using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class CustomerBLL
    {
        #region Common
        CustomerDAL dal = new CustomerDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<CustomerInfo></returns>
        public List<CustomerInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public CustomerInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="info"></param>
        /// <returns>自增主键</returns>
        public long InsertInfo(CustomerInfo info)
        {
            int cnt = dal.GetCounts("[CUST_CODE] = N'" + info.CustCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000081");///客户代码不允许重复
            cnt = dal.GetCounts("[CUST_NAME] = N'" + info.CustName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000082");///客户名称不允许重复
            cnt = dal.GetCounts("[CUST_NICKNAME] = N'" + info.CustNickname + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000083");///客户简称不允许重复
            return dal.Add(info);
        }
        /// <summary>
        /// 按主键逻辑删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns>是否删除成功</returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 按主键、字段更新数据
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns>是否更新成功</returns>
        public bool UpdateInfo(string fields, long id)
        {
            string custCode = CommonBLL.GetFieldValue(fields, "CUST_CODE");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [CUST_CODE] = N'" + custCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000081");///客户代码不允许重复

            string custName = CommonBLL.GetFieldValue(fields, "CUST_NAME");
            cnt = dal.GetCounts("[ID] <> " + id + " and [CUST_NAME] = N'" + custName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000082");///客户名称不允许重复

            string custNickname = CommonBLL.GetFieldValue(fields, "CUST_NICKNAME");
            cnt = dal.GetCounts("[ID] <> " + id + " and [CUST_NICKNAME] = N'" + custNickname + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000083");///客户简称不允许重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

