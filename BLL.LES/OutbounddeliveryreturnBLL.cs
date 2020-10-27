using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class OutbounddeliveryreturnBLL
    {
        #region Common
        OutbounddeliveryreturnDAL dal = new OutbounddeliveryreturnDAL();
        public List<OutbounddeliveryreturnInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public OutbounddeliveryreturnInfo SelectInfo(long outbounddeliveryreturnId)
        {
            return dal.GetInfo(outbounddeliveryreturnId);
        }

        public long InsertInfo(OutbounddeliveryreturnInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(long outbounddeliveryreturnId)
        {
            return dal.Delete(outbounddeliveryreturnId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long outbounddeliveryreturnId)
        {
            return dal.UpdateInfo(fields, outbounddeliveryreturnId) > 0 ? true : false;
        }

        #endregion
    }
}

