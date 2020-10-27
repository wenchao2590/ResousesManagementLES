using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PackageOutboundDetailBLL
    {
        #region Common
        PackageOutboundDetailDAL dal = new PackageOutboundDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PackageOutboundDetailInfo></returns>
        public List<PackageOutboundDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageOutboundDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(PackageOutboundDetailInfo info)
        {
            int count = dal.GetCounts("[PACKAGE_MODEL] = N'" + info.PackageModel + "' and [ORDER_FID] = N'" + info.OrderFid + "'");
            if (count > 0)
                throw new Exception("MC:0x00000444");///包装型号⑨不允许重复添加
            PackageOutboundInfo outboundInfo = new PackageOutboundDAL().GetList("[FID] = N'" + info.OrderFid + "'", string.Empty).FirstOrDefault();
            info.SWmNo = outboundInfo.SWmNo;
            info.TWmNo = outboundInfo.TWmNo;
            info.SZoneNo = outboundInfo.SZoneNo;
            info.TZoneNo = outboundInfo.TZoneNo;
            return dal.Add(info);
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

