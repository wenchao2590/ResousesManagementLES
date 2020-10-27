using DAL.GJS;
using DAL.SYS;
using DM.GJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.GJS
{
    public class CljcdBLL
    {
        #region Common
        CljcdDAL dal = new CljcdDAL();
        public List<CljcdInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public CljcdInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public CljcdInfo InsertInfo(CljcdInfo info)
        {
            info.ValidFlag = true;
            info.Jcdh = new SeqDefineDAL().GetCurrentCode("JCDH");
            info.Id = dal.Add(info);
            return info;
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

