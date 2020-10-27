using BLL.SYS;
using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// CodeBLL
    /// </summary>
    public class CodeBLL
    {
        #region Common
        /// <summary>
        /// CodeDAL
        /// </summary>
        CodeDAL dal = new CodeDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<CodeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(CodeInfo info)
        {
            int cnt = dal.GetCounts("[CODE_NAME] = N'" + info.CodeName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000003");///系统代码已存在

            cnt = dal.GetCounts("[CODE_NAME_CN] = N'" + info.CodeNameCn + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000008");///系统代码名称已存在

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
            string codeName = CommonBLL.GetFieldValue(fields, "CODE_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [CODE_NAME] = N'" + codeName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000003");///系统代码已存在

            string codeNameCn = CommonBLL.GetFieldValue(fields, "CODE_NAME_CN");
            cnt = dal.GetCounts("[ID] <> " + id + " and [CODE_NAME_CN] = N'" + codeNameCn + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000008");///系统代码名称已存在

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyDate"></param>
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
        public CodeInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        public List<CodeItemDatasourceInfo> GetDataSource(string codeName)
        {
            return dal.GetDataSource(codeName);
        }
        /// <summary>
        /// 获取数据量
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }
    }
}
