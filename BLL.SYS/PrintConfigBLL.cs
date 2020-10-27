using DAL.SYS;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// PrintConfigBLL
    /// </summary>
    public class PrintConfigBLL
    {
        #region Common
        /// <summary>
        /// PrintConfigDAL
        /// </summary>
        PrintConfigDAL dal = new PrintConfigDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PrintConfigInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PrintConfigInfo SelectInfo(int id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertInfo(PrintConfigInfo info)
        {
            ///��ӡ���ô���١���ӡ�������Ƣ�Ϊ�����ȫ���ֶβ������ظ�
            int cnt = dal.GetCounts("[PRINT_CONFIG_CODE] = N'" + info.PrintConfigCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000680");
            cnt = dal.GetCounts("[PRINT_CONFIG_NAME] = N'" + info.PrintConfigName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000681");
            ///��ӡģ���ļ����Ƣ�Ϊ��������ܴ�.
            if (info.PrintTemplateFilename.Contains('.'))
                throw new Exception("MC:0x00000682");///No special characters can be found in the file name
            return dal.Add(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(int id, string loginUser)
        {
            ///״̬�߱���Ϊ10.�Ѵ���
            int cnt = dal.GetCounts("[ID] = " + id + " and [STATUS] = " + (int)DataStatusConstants.CREATED + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000683");
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, int id)
        {
            ///��ӡ�������Ƣ�Ϊ�����ȫ���ֶβ������ظ�
            string printConfigName = CommonBLL.GetFieldValue(fields, "PRINT_CONFIG_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [PRINT_CONFIG_NAME] = N'" + printConfigName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000681");
            ///��ӡģ���ļ����Ƣ�Ϊ��������ܴ�.
            string printTemplateFilename = CommonBLL.GetFieldValue(fields, "PRINT_TEMPLATE_FILENAME");
            if (printTemplateFilename.Contains('.'))
                throw new Exception("MC:0x00000682");
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        ///  ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfo(int id, string loginUser)
        {
            ///״̬�߱���Ϊ10.�Ѵ���
            int cnt = dal.GetCounts("[ID] = " + id + " and [STATUS] = " + (int)DataStatusConstants.CREATED + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000683");
            return dal.UpdateInfo("[STATUS] = " + (int)DataStatusConstants.ENABLED + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE()", id) > 0 ? true : false;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfo(int id, string loginUser)
        {
            ///״̬�߱���Ϊ20.������
            int cnt = dal.GetCounts("[ID] = " + id + " and [STATUS] = " + (int)DataStatusConstants.ENABLED + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000730");
            return dal.UpdateInfo("[STATUS] = " + (int)DataStatusConstants.INVALID + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE()", id) > 0 ? true : false;
        }

        /// <summary>
        ///ͨ�����ô�������ļ�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PrintConfigInfo GetInfoByCode(string printConfigCode)
        {
            return dal.GetInfoByCode(printConfigCode);
        }
        
        /// <summary>
        /// Task 95 ��ȡ����Դ
        /// </summary>
        /// <param name="printConfigCode"></param>
        /// <returns></returns>
        public DataTable GetPrintData(string printConfigCode, params object[] keys)
        {
            return new DataTable();
        }

        public List<PrintConfigInfo> GetList(string textWhere)
        {
            return dal.GetList(textWhere);
        }
        #endregion
    }






}

