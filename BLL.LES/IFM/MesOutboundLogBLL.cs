namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Transactions;

    /// <summary>
    /// MesOutboundLogBLL
    /// </summary>
    public class MesOutboundLogBLL
    {
        #region Common
        /// <summary>
        /// MesOutboundLogDAL
        /// </summary>
        MesOutboundLogDAL dal = new MesOutboundLogDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<MesOutboundLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListForPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MesOutboundLogInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        #region Private
        /// <summary>
        /// �ط�
        /// </summary>
        /// <param name="rowsKeyValues">������ʱ��^�ָ�</param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ResendInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///��֤�Ƿ�����ط����� ExecuteResult;ҵ����� 40, ������� 50
            int cnt = dal.GetCounts("" +
                "[EXECUTE_RESULT] in (" + (int)ExecuteResultConstants.Error + "," + (int)ExecuteResultConstants.Exception + ") and " +
                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")");
            if (cnt != rowsKeyValues.Count)
                throw new Exception("MC:0x00000200");///ִ�н����ҵ�����ͳ�����󣬲����ط���

            StringBuilder @string = new StringBuilder();
            foreach (var rowsKeyValue in rowsKeyValues)
            {
                @string.AppendLine(CommonBLL.GetUpdateResultLogSql("MES", Convert.ToInt64(rowsKeyValue), ExecuteResultConstants.Resend, string.Empty, string.Empty, string.Empty, loginUser));
            }
            using (var trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="rowsKeyValues">������ʱ��^�ָ�</param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///��֤�Ƿ����ȡ������ ExecuteResult;ҵ����� 40, ������� 50 �ύ��10 �ط���60
            int cnt = dal.GetCounts("" +
                "[EXECUTE_RESULT] in (" + (int)ExecuteResultConstants.Error + "," + (int)ExecuteResultConstants.Exception + "," + (int)ExecuteResultConstants.Submit + "," + (int)ExecuteResultConstants.Resend + ") and " +
                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")");
            if (cnt != rowsKeyValues.Count)
                throw new Exception("MC:0x00000201");///ִ�н��Ϊ�����л���ɲ���ȡ��

            StringBuilder @string = new StringBuilder();
            foreach (var rowsKeyValue in rowsKeyValues)
            {
                @string.AppendLine(CommonBLL.GetUpdateResultLogSql("MES", Convert.ToInt64(rowsKeyValue), ExecuteResultConstants.Cancel, string.Empty, string.Empty, string.Empty, loginUser));
            }
            using (var trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// ��ȡ�ȴ����͵���������
        /// </summary>
        /// <returns></returns>
        public List<MesOutboundLogInfo> GetListForUnsend()
        {
            return dal.GetListForUnsend();
        }
        #endregion
    }
}

