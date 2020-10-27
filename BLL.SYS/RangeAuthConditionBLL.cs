namespace BLL.SYS
{
    using DAL.SYS;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// UserRoleConditionBLL
    /// </summary>
    public class RangeAuthConditionBLL
    {
        #region Common
        /// <summary>
        /// UserRoleConditionDAL
        /// </summary>
        RangeAuthConditionDAL dal = new RangeAuthConditionDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<RangeAuthConditionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<RangeAuthConditionInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(RangeAuthConditionInfo info)
        {
            int cnt = dal.GetCounts("[CONDITION_NAME] = N'" + info.ConditionName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000263");///权限条件重复

            cnt = dal.GetCounts("[ATTRIBUTE_NAME] = N'" + info.AttributeName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000263");///权限条件重复

            return dal.Add(info);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string conditionName = CommonBLL.GetFieldValue(fields, "CONDITION_NAME");
            int cnt = dal.GetCounts("[CONDITION_NAME] = N'" + conditionName + "' and [ID] <> " + id + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000263");///权限条件重复

            string attributeName = CommonBLL.GetFieldValue(fields, "ATTRIBUTE_NAME");
            cnt = dal.GetCounts("[ATTRIBUTE_NAME] = N'" + attributeName + "' and [ID] <> " + id + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000263");///权限条件重复

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            ///获取外键
            Guid conditionFid = dal.GetFid(id);
            if (conditionFid == Guid.Empty)
                throw new Exception("MC:0x00000084");///数据错误
            int cnt = new UserRoleRangeAuthDAL().GetCounts("[CONDITION_FID] = N'" + conditionFid + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000117");///权限条件已被角色设置，不可以删除

            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RangeAuthConditionInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}
