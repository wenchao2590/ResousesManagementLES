namespace BLL.SYS
{
    using DAL.SYS;
    using DM.SYS;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class OperationLogBLL
    {
        #region Common
        OperationLogDAL dal = new OperationLogDAL();
        public List<OperationLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public long InsertInfo(OperationLogInfo info)
        {
            return dal.Add(info);
        }
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        public OperationLogInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        /// <summary>
        /// 记录主数据操作日志
        /// </summary>
        /// <param name="opData"></param>
        /// <param name="operationType"></param>
        /// <param name="tableName"></param>
        /// <param name="pageUrl"></param>
        /// <param name="browserInfo"></param>
        /// <param name="ipAddress"></param>
        /// <param name="createUser"></param>
        /// <returns></returns>
        public bool InsertLog(string opData, int operationType, string tableName
            , string pageUrl, string browserInfo, string ipAddress, string createUser)
        {
            OperationLogInfo info = new OperationLogInfo();
            info.Fid = Guid.NewGuid();
            info.OperationContext = opData;
            info.OperationType = operationType;
            info.TableName = tableName;
            info.IpAddress = ipAddress;
            info.PageUrl = pageUrl;
            info.BrowserInfo = browserInfo;
            info.CreateUser = createUser;
            info.CreateDate = DateTime.Now;
            info.ValidFlag = true;
            return dal.Add(info) > 0 ? true : false;
        }
    }
}
