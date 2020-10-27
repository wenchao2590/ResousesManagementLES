namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// LackOfMaterialBLL
    /// </summary>
    public class LackOfMaterialBLL
    {
        #region Common
        LackOfMaterialDAL dal = new LackOfMaterialDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<LackOfMaterialInfo></returns>
        public List<LackOfMaterialInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 主键获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LackOfMaterialInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(LackOfMaterialInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// GetCounts
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }
        #endregion

        /// <summary>
        /// 更新缺件表状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UpdateStatus(int status, long id, string comments, string loginUser)
        {
            if (status == (int)LackOfMaterialStatusConstants.Calculating)
                return dal.UpdateInfo("[STATUS] = " + status + ",[COMMENTS] = N'" + comments + "',[EXECUTE_START_TIME] = GETDATE(),[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "'", id) > 0 ? true : false;
            return dal.UpdateInfo("[STATUS] = " + status + ",[COMMENTS] = N'" + comments + "',[EXECUTE_END_TIME] = GETDATE(),[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "'", id) > 0 ? true : false;
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<LackOfMaterialInfo> lackOfMaterialInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (lackOfMaterialInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            string sql = string.Empty;
            foreach (var lackOfMaterialInfo in lackOfMaterialInfos)
            {
                if (lackOfMaterialInfo.Status.GetValueOrDefault() != (int)LackOfMaterialStatusConstants.Completed)
                    throw new Exception("MC:0x00000430");///只有状态为计算完成时才能进行发布
                int cnt = new LackOfMaterialDetailDAL().GetCounts("[LACK_ORDER_FID] = N'" + lackOfMaterialInfo.Fid.GetValueOrDefault() + "' and [FEEDBACK_FLAG] = 1");
                if (cnt == 0)
                    throw new Exception("MC:0x00000431");///缺件表未进行反馈
                sql += "update [LES].[TT_ATP_LACK_OF_MATERIAL] " +
                    "set [STATUS] = " + (int)LackOfMaterialStatusConstants.Feedbacked + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + lackOfMaterialInfo.Id + ";";
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
    }
}

