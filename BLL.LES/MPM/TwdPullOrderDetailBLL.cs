using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class TwdPullOrderDetailBLL
    {
        #region Common
        TwdPullOrderDetailDAL dal = new TwdPullOrderDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<TwdPullOrderDetailInfo></returns>
        public List<TwdPullOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public TwdPullOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(TwdPullOrderDetailInfo info)
        {
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


        #region Interface
        /// <summary>
        /// Create TwdPullOrderDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>TwdPullOrderDetailInfo</returns>
        public static TwdPullOrderDetailInfo CreateTwdPullOrderDetailInfo(string loginUser)
        {
            TwdPullOrderDetailInfo info = new TwdPullOrderDetailInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = true;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;
            ///CREATE_USER,创建用户
            info.CreateUser = loginUser;

            ///PART_VERSION,物料版本
            info.PartVersion = null;

            return info;
        }
        /// <summary>
        /// MaintainInhouseLogisticStandardInfo -> TwdPullOrderDetailInfo
        /// </summary>
        /// <param name="logisticStandardInfo"></param>
        /// <param name="info"></param>
        public static void GetTwdPullOrderDetailInfo(MaintainInhouseLogisticStandardInfo logisticStandardInfo, ref TwdPullOrderDetailInfo info)
        {
            if (logisticStandardInfo == null) return;
            ///SUPPLIER_NUM,供应商代码
            info.SupplierNum = logisticStandardInfo.SupplierNum;
            ///WORKSHOP_SECTION,工段
            info.WorkshopSection = logisticStandardInfo.WorkshopSection;
            ///LOCATION,工位
            info.Location = logisticStandardInfo.Location;
            ///PART_NO,物料号
            info.PartNo = logisticStandardInfo.PartNo;
            ///PART_CNAME,物料中文描述
            info.PartCname = logisticStandardInfo.PartCname;
            ///PART_ENAME,物料英文描述
            info.PartEname = logisticStandardInfo.PartEname;
            ///MEASURING_UNIT_NO,单位
            //info.MeasuringUnitNo = logisticStandardInfo.PartUnits;
            ///PACKAGE,单包装数量
            info.Package = logisticStandardInfo.InboundPackage;
            ///PACKAGE_MODEL,包装编号
            info.PackageModel = logisticStandardInfo.InboundPackageModel;
        }
        /// <summary>
        /// TwdPullOrderInfo -> TwdPullOrderDetailInfo
        /// </summary>
        /// <param name="twdPullOrderInfo"></param>
        /// <param name="info"></param>
        public static void GetTwdPullOrderDetailInfo(TwdPullOrderInfo twdPullOrderInfo, ref TwdPullOrderDetailInfo info)
        {
            if (twdPullOrderInfo == null) return;
            ///ORDER_FID,拉动单外键
            info.OrderFid = twdPullOrderInfo.Fid;
            ///ORDER_CODE,拉动单号
            info.OrderCode = twdPullOrderInfo.OrderCode;
        }

        public static void GetTwdPullOrderDetailInfo(ref TwdPullOrderDetailInfo info)
        {

        }
        #endregion

    }
}

