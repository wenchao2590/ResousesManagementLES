namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Transactions;
    /// <summary>
    /// SapPartsBLL
    /// </summary>
    public partial class SapPartsBLL
    {
        #region Common
        /// <summary>
        /// SapPartsDAL
        /// </summary>
        SapPartsDAL dal = new SapPartsDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SapPartsInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SapPartsInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SapPartsInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
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
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<SapPartsInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        #endregion

        #region Private
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<SapPartsInfo> sapPartsInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (sapPartsInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            foreach (var sapPartsInfo in sapPartsInfos)
            {
                if (sapPartsInfo.ProcessFlag.GetValueOrDefault() != (int)ProcessFlagConstants.Untreated &&
                    sapPartsInfo.ProcessFlag.GetValueOrDefault() != (int)ProcessFlagConstants.Suspend)
                    throw new Exception("MC:0x00000521");///状态必须为未处理或挂起 
            }
            string sql = "update [LES].[TI_IFM_SAP_PARTS] set " +
                    "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Cancel + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 重发
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ResendInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///入库单
            List<SapPartsInfo> sapPartsInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (sapPartsInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            foreach (var sapPartsInfo in sapPartsInfos)
            {
                if (sapPartsInfo.ProcessFlag.GetValueOrDefault() != (int)ProcessFlagConstants.Suspend)
                    throw new Exception("MC:0x00000524");///仅同步状态为挂起时可以更新已重发

                int cnt = dal.GetCounts("" +
                    "[MATNR] = N'" + sapPartsInfo.Matnr + "' and " +
                    "[WERKS] = N'" + sapPartsInfo.Werks + "' and " +
                    "[ID] > " + sapPartsInfo.Id + "");
                if (cnt > 0)
                    throw new Exception("MC:0x00000522");///相同工厂、物料图号必须为最新数据 
            }

            string sql = "update [LES].[TI_IFM_SAP_PARTS] set " +
                    "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Resend + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        #endregion
    }
}

