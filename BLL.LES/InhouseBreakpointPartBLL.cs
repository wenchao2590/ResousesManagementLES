using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class InhouseBreakpointPartBLL
    {
        #region Common
        InhouseBreakpointPartDAL dal = new InhouseBreakpointPartDAL();
        public List<InhouseBreakpointPartInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public InhouseBreakpointPartInfo SelectInfo(string inhouseBreakpointNo)
        {
            return dal.GetInfo(inhouseBreakpointNo);
        }

        public bool InsertInfo(InhouseBreakpointPartInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(string inhouseBreakpointNo)
        {
            return dal.Delete(inhouseBreakpointNo) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, string inhouseBreakpointNo)
        {
            return dal.UpdateInfo(fields, inhouseBreakpointNo) > 0 ? true : false;
        }

        #endregion
    }
}

