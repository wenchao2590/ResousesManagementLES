using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region Imported Namespace
using DM.LES;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

#endregion

namespace DAL.LES
{
    public partial class QmisOutboundLogDAL
    {
        /// <summary>
        /// 重发
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ResendInfos(string Keys)
        {
            string sql = string.Format(@"UPDATE [LES].[TI_QMIS_SAP_OUTBOUND_LOG]  SET EXECUTE_RESULT=60 WHERE [VALID_FLAG] = 1  AND ID IN ({0}) and [Execute_Result] in (" + (int)ExecuteResultConstants.Error + "," + (int)ExecuteResultConstants.Exception + ")", Keys);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            return int.Parse("0" + db.ExecuteNonQuery(dbCommand)) > 0 ? true : false;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool CancelInfos(string Keys)
        {
            string sql = string.Format(@"UPDATE [LES].[TI_QMIS_SAP_OUTBOUND_LOG] SET EXECUTE_RESULT=70 WHERE [VALID_FLAG] = 1  AND ID IN ({0}) and [Execute_Result] in (" + (int)ExecuteResultConstants.Error + "," + (int)ExecuteResultConstants.Exception + "," + (int)ExecuteResultConstants.Submit + "," + (int)ExecuteResultConstants.Resend + ")", Keys);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            return int.Parse("0" + db.ExecuteNonQuery(dbCommand)) > 0 ? true : false;
        }
    }
}
