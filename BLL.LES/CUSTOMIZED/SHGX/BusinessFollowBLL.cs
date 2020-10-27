using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public partial class BusinessFollowBLL
    {
        #region Common
        BusinessFollowDAL dal = new BusinessFollowDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BusinessExpenseInInfo></returns>
        public List<BusinessFollowInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public BusinessFollowInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(BusinessFollowInfo info)
        {
            info.ReceiveNo = info.ReceiveNo.Split(',')[0];
            info.OutputNo = info.OutputNo.Split(',')[0];
            info.RunsheetNo = info.RunsheetNo.Split(',')[0];
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string receiveNo = CommonBLL.GetFieldValue(fields, "RECEIVE_NO");
            if (!string.IsNullOrEmpty(receiveNo))
                fields = CommonBLL.SetFieldValue(fields, "RECEIVE_NO", receiveNo.Split(',')[0]);
            string outputNo = CommonBLL.GetFieldValue(fields, "OUTPUT_NO");
            if (!string.IsNullOrEmpty(outputNo))
                fields = CommonBLL.SetFieldValue(fields, "OUTPUT_NO", outputNo.Split(',')[0]);
            string runsheetNo = CommonBLL.GetFieldValue(fields, "RUNSHEET_NO");
            if (!string.IsNullOrEmpty(runsheetNo))
                fields = CommonBLL.SetFieldValue(fields, "RUNSHEET_NO", runsheetNo.Split(',')[0]);

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<BusinessFollowInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        #endregion
    }
}
