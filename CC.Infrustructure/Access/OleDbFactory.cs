using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrustructure.Access
{
    public class OleDbFactory
    {
        private static OleDbConnection _oleDbConn;
        private static readonly object LockObject = new object();

        /// <summary>
        /// Create DB Connection
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection CreateDbConnection()
        {
            //todo: liuxj 写死了文件名称，不是一个好的做法，待优化
            var accessPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MES", "StandaloneDB.mdb");
            if (!File.Exists(accessPath))
            {
                throw new FileNotFoundException();
            }

            var configStr = ConfigurationManager.ConnectionStrings["StandaloneConnection"].ConnectionString;
            var connectionStr = string.Format(configStr, accessPath);

            if (_oleDbConn == null)
            {
                lock (LockObject)
                {
                    if (_oleDbConn == null)
                    {
                        _oleDbConn = new OleDbConnection(connectionStr);
                    }
                }
            }

            return _oleDbConn;
        }
    }
}
