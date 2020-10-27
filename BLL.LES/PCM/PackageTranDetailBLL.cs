using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// PackageTranDetailBLL
    /// </summary>
    public partial class PackageTranDetailBLL
    {
        #region Common
        /// <summary>
        /// PackageTranDetailDAL
        /// </summary>
        PackageTranDetailDAL dal = new PackageTranDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PackageTranDetailInfo></returns>
        public List<PackageTranDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PackageTranDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PackageTranDetailInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<PackageTranDetailInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        #endregion

        #region Private
        /// <summary>
        /// 根据入库单明细获取包装器具的交易记录
        /// </summary>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="logUser"></param>
        /// <param name="orderNo"></param>
        /// <param name="plant"></param>
        /// <param name="wmNo"></param>
        /// <param name="assemblyLine"></param>
        /// <returns></returns>
        public static string CreatePackageTranDetailsSql(List<ReceiveDetailInfo> receiveDetailInfos, string loginUser)
        {
            List<PackageApplianceInfo> packageApplianceInfos = new PackageApplianceDAL().GetList("" +
                "[PACKAGE_NO] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.PackageModel).ToArray()) + "')", string.Empty);
            string sql = string.Empty;
            foreach (ReceiveDetailInfo receiveDetailInfo in receiveDetailInfos)
            {
                ///创建包装交易对象
                PackageTranDetailInfo packageTranDetailInfo = CreatePackageTranDetailInfo(loginUser);
                ///填充入库明细数据
                GetPackageTranDetailInfo(receiveDetailInfo, ref packageTranDetailInfo);
                ///包装基础数据
                PackageApplianceInfo packageApplianceInfo = packageApplianceInfos.FirstOrDefault(d => d.PackageNo == receiveDetailInfo.PackageModel);
                ///填充包装基础数据
                GetPackageTranDetailInfo(packageApplianceInfo, ref packageTranDetailInfo);
                ///
                sql += PackageTranDetailDAL.CreatePackageTranDetailSql(packageTranDetailInfo);
            }
            return sql;
        }
        /// <summary>
        /// 根据出库单明细获取包装器具的交易记录
        /// </summary>
        /// <param name="outputDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreatePackageTranDetailsSql(List<OutputDetailInfo> outputDetailInfos, string loginUser)
        {
            List<PackageApplianceInfo> packageApplianceInfos = new PackageApplianceDAL().GetList("" +
                "[PACKAGE_NO] in ('" + string.Join("','", outputDetailInfos.Select(d => d.PackageModel).ToArray()) + "')", string.Empty);
            string sql = string.Empty;
            foreach (OutputDetailInfo outputDetailInfo in outputDetailInfos)
            {
                ///
                PackageTranDetailInfo packageTranDetailInfo = CreatePackageTranDetailInfo(loginUser);
                ///
                GetPackageTranDetailInfo(outputDetailInfo, ref packageTranDetailInfo);
                ///
                PackageApplianceInfo packageApplianceInfo = packageApplianceInfos.FirstOrDefault(d => d.PackageNo == outputDetailInfo.PackageModel);
                GetPackageTranDetailInfo(packageApplianceInfo, ref packageTranDetailInfo);
                ///
                sql += PackageTranDetailDAL.CreatePackageTranDetailSql(packageTranDetailInfo);
            }
            return sql;
        }
        /// <summary>
        /// 创建包装交易对象
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static PackageTranDetailInfo CreatePackageTranDetailInfo(string loginUser)
        {
            PackageTranDetailInfo packageTranDetailInfo = new PackageTranDetailInfo();
            packageTranDetailInfo.CreateUser = loginUser;
            packageTranDetailInfo.CreateDate = DateTime.Now;
            packageTranDetailInfo.ValidFlag = true;
            return packageTranDetailInfo;
        }
        /// <summary>
        /// ReceiveDetailInfo->PackageTranDetailInfo
        /// </summary>
        /// <param name="receiveDetailInfo"></param>
        /// <param name="packageTranDetailInfo"></param>
        public static void GetPackageTranDetailInfo(ReceiveDetailInfo receiveDetailInfo, ref PackageTranDetailInfo packageTranDetailInfo)
        {
            packageTranDetailInfo.TranNo = receiveDetailInfo.TranNo;
            ///随货入库
            packageTranDetailInfo.TranType = (int)PackageTranTypeConstants.FullInbound;
            packageTranDetailInfo.BarcodeData = string.Empty;
            packageTranDetailInfo.PartNo = receiveDetailInfo.PartNo;
            packageTranDetailInfo.Plant = receiveDetailInfo.Plant;
            packageTranDetailInfo.AssemblyLine = receiveDetailInfo.AssemblyLine;
            packageTranDetailInfo.SupplierNum = receiveDetailInfo.SupplierNum;
            packageTranDetailInfo.WmNo = receiveDetailInfo.WmNo;
            packageTranDetailInfo.ZoneNo = receiveDetailInfo.ZoneNo;
            packageTranDetailInfo.Dloc = receiveDetailInfo.Dloc;
            packageTranDetailInfo.TargetWm = receiveDetailInfo.TargetWm;
            packageTranDetailInfo.TargetZone = receiveDetailInfo.TargetZone;
            packageTranDetailInfo.TargetDloc = receiveDetailInfo.TargetDloc;
            packageTranDetailInfo.PackageNo = receiveDetailInfo.PackageModel;
            packageTranDetailInfo.Package = receiveDetailInfo.Package;
            packageTranDetailInfo.PackageQty = receiveDetailInfo.ActualBoxNum;
            packageTranDetailInfo.Comments = receiveDetailInfo.Comments;
        }
        /// <summary>
        /// OutputDetailInfo->PackageTranDetailInfo
        /// </summary>
        /// <param name="outputDetailInfo"></param>
        /// <param name="packageTranDetailInfo"></param>
        private static void GetPackageTranDetailInfo(OutputDetailInfo outputDetailInfo, ref PackageTranDetailInfo packageTranDetailInfo)
        {
            packageTranDetailInfo.TranNo = outputDetailInfo.TranNo;
            packageTranDetailInfo.TranType = (int)PackageTranTypeConstants.FullMovement;
            packageTranDetailInfo.BarcodeData = string.Empty;
            packageTranDetailInfo.PartNo = outputDetailInfo.PartNo;
            packageTranDetailInfo.Plant = outputDetailInfo.Plant;
            packageTranDetailInfo.AssemblyLine = outputDetailInfo.AssemblyLine;
            packageTranDetailInfo.SupplierNum = outputDetailInfo.SupplierNum;
            packageTranDetailInfo.WmNo = outputDetailInfo.WmNo;
            packageTranDetailInfo.ZoneNo = outputDetailInfo.ZoneNo;
            packageTranDetailInfo.Dloc = outputDetailInfo.Dloc;
            packageTranDetailInfo.TargetWm = outputDetailInfo.TargetWm;
            packageTranDetailInfo.TargetZone = outputDetailInfo.TargetZone;
            packageTranDetailInfo.TargetDloc = outputDetailInfo.TargetDloc;
            packageTranDetailInfo.PackageNo = outputDetailInfo.PackageModel;
            packageTranDetailInfo.Package = outputDetailInfo.Package;
            packageTranDetailInfo.PackageQty = outputDetailInfo.ActualBoxNum;
            packageTranDetailInfo.Comments = outputDetailInfo.Comments;
        }
        /// <summary>
        /// PackageApplianceInfo->PackageTranDetailInfo
        /// </summary>
        /// <param name="packageApplianceInfo"></param>
        /// <param name="packageTranDetailInfo"></param>
        public static void GetPackageTranDetailInfo(PackageApplianceInfo packageApplianceInfo, ref PackageTranDetailInfo packageTranDetailInfo)
        {
            if (packageApplianceInfo == null) return;
            packageTranDetailInfo.PackageCname = packageApplianceInfo.PackageCname;
            packageTranDetailInfo.PackageEname = packageApplianceInfo.PackageEname;
        }
        #endregion
    }
}

