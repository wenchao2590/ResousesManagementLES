using DAL.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public partial class PrintConfigUploadBLL
    {
        public bool UploadFileFinished(string key, string loginUser)
        {
            return new PrintConfigDAL().UpdateInfo("[LAST_UPLOADFILE_TIME] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "'", int.Parse(key)) > 0 ? true : false;
        }
    }
}
