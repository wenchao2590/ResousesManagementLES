using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class OutbounddeliveryBLL
    {
        #region Common
        OutbounddeliveryDAL dal = new OutbounddeliveryDAL();
        public List<OutbounddeliveryInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public OutbounddeliveryInfo SelectInfo(long outbounddeliveryId)
        {
            return dal.GetInfo(outbounddeliveryId);
        }

        public long InsertInfo(OutbounddeliveryInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(long outbounddeliveryId)
        {
            return dal.Delete(outbounddeliveryId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long outbounddeliveryId)
        {
            return dal.UpdateInfo(fields, outbounddeliveryId) > 0 ? true : false;
        }

        #endregion
    }
}

