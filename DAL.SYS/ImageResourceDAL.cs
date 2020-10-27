using DM.SYS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.SYS
{
    public partial class ImageResourceDAL
    {
        public int UpdateImage(long id, byte[] imageResource, string modifyUser)
        {
            string sql = "update [dbo].[TS_SYS_IMAGE_RESOURCE] "
                + "set [IMAGE_RESOURCE] = @IMAGE_RESOURCE,[MODIFY_USER] = @MODIFY_USER,[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @ID";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ID", DbType.Int64, id);
            db.AddInParameter(cmd, "@IMAGE_RESOURCE", DbType.Binary, imageResource);
            db.AddInParameter(cmd, "@MODIFY_USER", DbType.String, modifyUser);
            return int.Parse("0" + db.ExecuteNonQuery(cmd));
        }
        /// <summary>
        /// 读取图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] ReadImage(long id)
        {
            string sql = "select [IMAGE_RESOURCE] from [dbo].[TS_SYS_IMAGE_RESOURCE] with(nolock) where [ID] = @ID";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ID", DbType.Int64, id);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return null;
            return (byte[])result;
        }
    }
}
