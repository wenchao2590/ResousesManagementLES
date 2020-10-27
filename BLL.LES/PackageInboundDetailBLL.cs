using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PackageInboundDetailBLL
    {
        #region Common
        PackageInboundDetailDAL dal = new PackageInboundDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PackageInboundDetailInfo></returns>
        public List<PackageInboundDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageInboundDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(PackageInboundDetailInfo info)
        {
            int Cnt = dal.GetCounts(" and [PACKAGE_MODEL] = N'" + info.PackageModel + "' and [PACKAGE_STATUS] = '" + info.PackageStatus + "'");
            if (Cnt > 0)
                throw new Exception("MC:0x00000487");///包装型号⑨+状态⑪相同的数据不允许重复添加
            Cnt = new PackageInboundDAL().GetList("[FID] = N'" + info.OrderFid + "' and [STATUS] <> " + (int)PackageInboundStatusConstants.Created, string.Empty).Count();
            if (Cnt > 0)
                throw new Exception("MC:0x00000488");///已创建状态才可进行物料添加
            PackageInboundInfo package = new PackageInboundDAL().GetList("[FID] = N'" + info.OrderFid + "'", string.Empty).FirstOrDefault();
            info.SWmNo = package.SWmNo;
            info.SZoneNo = package.SZoneNo;
            info.TWmNo = package.TWmNo;
            info.TZoneNo = package.TZoneNo;
            info.SupplierNum = package.SupplierNum;
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            PackageInboundDetailInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据有误
            List<PackageInboundInfo> inboundInfo = new PackageInboundDAL().GetList(" [FID] = N'" + info.OrderFid + "'", string.Empty);
            if (inboundInfo == null)
                throw new Exception("MC:0x00000084");///数据有误
            if (inboundInfo.FirstOrDefault().Status != (int)PackageInboundStatusConstants.Created)
                throw new Exception("MC:0x00000415");///状态为10.已创建时可以进行修改或删除
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            PackageInboundDetailInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据有误
            List<PackageInboundInfo> inboundInfo = new PackageInboundDAL().GetList(" [FID] = N'" + info.OrderFid + "'", string.Empty);
            if (inboundInfo == null)
                throw new Exception("MC:0x00000084");///数据有误
            if (inboundInfo.FirstOrDefault().Status != (int)PackageInboundStatusConstants.Created)
                throw new Exception("MC:0x00000441");///状态为10.已创建时可以进行修改或删除
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

