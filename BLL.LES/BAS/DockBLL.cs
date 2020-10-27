using BLL.LES;
using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class DockBLL
    {
        #region Common
        DockDAL dal = new DockDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<DockInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DockInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>DockInfo Collection </returns>
		public List<DockInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        /// <summary>
        /// ��֤-��ӵ���
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(DockInfo info)
        {
            ///���ڴ���ڡ��������Ƣ���ͬһ�ֿ����ܷ�Χ�ڲ������ظ���������
            int dockCnt = dal.GetCounts("[DOCK] = N'" + info.Dock + "' and [WM_NO] = N'" + info.WmNo + "'");
            if (dockCnt > 0)
                throw new Exception("Err_:MC:0x00000687"); //ͬһ�ֿ��µ��ڴ��벻�����ظ�
            int dockNameCnt = dal.GetCounts("[DOCK_NAME] = N'" + info.DockName + "' and [WM_NO] = N'" + info.WmNo + "'");
            if (dockNameCnt > 0)
                throw new Exception("Err_:MC:0x00000688"); //ͬһ�ֿ��µ������Ʋ������ظ�
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            ///�������Ƣ���ͬһ�ֿ����ܷ�Χ�ڲ������ظ���������
            string dockName = CommonBLL.GetFieldValue(fields, "DOCK_NAME");
            string wmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            if (string.IsNullOrEmpty(dockName))
                throw new Exception("Err_:MC:0x00000723");///�������Ʋ�����Ϊ��
            if (string.IsNullOrEmpty(wmNo))
                throw new Exception("Err_:MC:0x00000721");///�ֿⲻ����Ϊ��
            int cnt = dal.GetCounts("[ID] <> " + id + " and [DOCK_NAME] = N'" + dockName + "' and [WM_NO] = N'" + wmNo + "'");
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000688");///ͬһ�ֿ��µ������Ʋ������ظ�
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

