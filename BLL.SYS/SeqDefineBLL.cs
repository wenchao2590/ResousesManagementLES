using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// SeqDefineBLL
    /// </summary>
    public class SeqDefineBLL
    {
        #region Common
        /// <summary>
        /// SeqDefineDAL
        /// </summary>
        SeqDefineDAL dal = new SeqDefineDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SeqDefineInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SeqDefineInfo info)
        {
            int cnt = dal.GetCounts("[SEQ_CODE] = N'" + info.SeqCode + "'");
            if (cnt > 0)
                throw new Exception("0x00000030");///序列号规则代码重复

            cnt = dal.GetCounts("[SEQ_NAME] = N'" + info.SeqName + "'");
            if (cnt > 0)
                throw new Exception("0x00000031");///序列号规则名称重复
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
            string seqCode = CommonBLL.GetFieldValue(fields, "SEQ_CODE");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [SEQ_CODE] = N'" + seqCode + "'");
            if (cnt > 0)
                throw new Exception("0x00000030");///序列号规则代码重复

            string seqName = CommonBLL.GetFieldValue(fields, "SEQ_NAME");
            cnt = dal.GetCounts("[ID] <> " + id + " and [SEQ_NAME] = N'" + seqName + "'");
            if (cnt > 0)
                throw new Exception("0x00000031");///序列号规则名称重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SeqDefineInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <param name="seqCode">序列号名称</param>
        /// <param name="manualParams">手工参数组</param>
        /// <returns>可用的序列号</returns>
        public string GetCurrentCode(string seqCode, params string[] manualParams)
        {
            return dal.GetCurrentCode(seqCode, manualParams);
        }
    }
}
