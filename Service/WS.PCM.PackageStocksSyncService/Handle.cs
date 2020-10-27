namespace WS.PCM.PackageStocksSyncService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DM.LES;
    using BLL.LES;
    using System.Transactions;
    public class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        private string loginUser = "PackageStocksSyncService";
        private PackageStocksBLL packageStocksBLL =new PackageStocksBLL();
        #endregion

        #region Handler
        public void Handler()
        {
            ///获取交易处理状态⑲为10.未处理的包装交易记录--TT_PCM_PACKAGE_TRAN_DETAIL
            List<PackageTranDetailInfo> packageTranDetailInfos = new PackageTranDetailBLL().GetList("[STATUS] = " + (int)PackageTranStateConstants.UNTREATED + "", "[ID]");
            if (packageTranDetailInfos.Count == 0) return;
            ///获取涉及的所有包装库存--TT_PCM_PACKAGE_STOCKS
            List<PackageStocksInfo> packageStocksInfos
                = packageStocksBLL.GetList("[PLANT] in ('" + string.Join("','", packageTranDetailInfos.Select(d => d.Plant).ToArray()) + "') "
                + "and [WM_NO] in ('" + string.Join("','", packageTranDetailInfos.Select(d => d.TargetWm).ToArray()) + "') "
                + "and [ZONE_NO] in ('" + string.Join("','", packageTranDetailInfos.Select(d => d.TargetZone).ToArray()) + "') "
                + "and [DLOC] in ('" + string.Join("','", packageTranDetailInfos.Select(d => d.TargetDloc).ToArray()) + "')", string.Empty);
            ///存储区
            List<ZonesInfo> zonesInfos = new ZonesBLL().GetList("[ZONE_NO] in ('" + string.Join("','", packageTranDetailInfos.Select(d => d.ZoneNo).ToArray()) + "',"
                + "'" + string.Join("','", packageTranDetailInfos.Select(d => d.TargetZone).ToArray()) + "')", string.Empty);
            ///工厂
            List<PlantInfo> plantInfos = new PlantBLL().GetListForInterfaceDataSync();
            ///供应商
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetListForInterfaceDataSync(packageTranDetailInfos.Select(d => d.SupplierNum).ToList());
            ///物料
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsBLL().GetListForInterfaceDataSync(packageTranDetailInfos.Select(d => d.PartNo).ToList());
            ///逐条处理
            foreach (var packageTranDetailInfo in packageTranDetailInfos)
            {
                ///物料信息
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == packageTranDetailInfo.PartNo && d.Plant == packageTranDetailInfo.Plant);
                ///创建库存对象
                PackageStocksInfo packageStocksInfo = null;
                StringBuilder stringBuilder = new StringBuilder();
                
                switch (packageTranDetailInfo.TranType.GetValueOrDefault())
                {
                    ///对于交易类型②为10//随货入库  
                    case (int)PackageTranTypeConstants.FullInbound:
                        ///需要对工厂⑤、目标仓库⑪、目标存储区⑫、目标库位⑬的包装库存数据
                        packageStocksInfo = GetPackageStocksData(packageStocksInfos, packageTranDetailInfo, false);
                        if (packageStocksInfo == null)
                            packageStocksInfo = HandlingPackageStocksData(packageTranDetailInfo, maintainPartsInfo, false);
                        ///对其满包装数⑭以及库存数⑫进行累加，累加数量为包装数量，完成后标记交易处理状态⑲为20.已处理
                        stringBuilder.AppendFormat(packageStocksBLL.FullPackageStocksUpSql(packageStocksInfo, packageTranDetailInfo, loginUser));
                        break;
                    ///对于交易类型②为20//随货移库  
                    case (int)PackageTranTypeConstants.FullMovement:
                        ///来源减少
                        ///需要对工厂⑤、来源仓库⑪、来源存储区⑫、来源库位⑬的包装库存数据
                        packageStocksInfo = GetPackageStocksData(packageStocksInfos, packageTranDetailInfo, true);
                        if (packageStocksInfo == null)
                            packageStocksInfo = HandlingPackageStocksData(packageTranDetailInfo, maintainPartsInfo, true);
                        ///对其满包装数⑭以及库存数⑫进行扣减，扣减数量为包装数量，完成后标记交易处理状态⑲为20.已处理
                        stringBuilder.AppendFormat(packageStocksBLL.FullPackageStocksDownSql(packageStocksInfo, packageTranDetailInfo, loginUser));
                        ///目标增加
                        ///需要对工厂⑤、目标仓库⑪、目标存储区⑫、目标库位⑬的包装库存数据
                        packageStocksInfo = GetPackageStocksData(packageStocksInfos, packageTranDetailInfo, false);
                        if (packageStocksInfo == null)
                            packageStocksInfo = HandlingPackageStocksData(packageTranDetailInfo, maintainPartsInfo, false);
                        ///对其满包装数⑭以及库存数⑫进行累加，累加数量为包装数量，完成后标记交易处理状态⑲为20.已处理
                        stringBuilder.AppendFormat(packageStocksBLL.FullPackageStocksUpSql(packageStocksInfo, packageTranDetailInfo, loginUser));
                        break;
                    ///对于交易类型②为30//随货出库  
                    case (int)PackageTranTypeConstants.FullOutbound:
                        ///需要对工厂⑤、来源仓库⑪、来源存储区⑫、来源库位⑬的包装库存数据
                        packageStocksInfo = GetPackageStocksData(packageStocksInfos, packageTranDetailInfo, true);
                        if (packageStocksInfo == null)
                            packageStocksInfo = HandlingPackageStocksData(packageTranDetailInfo, maintainPartsInfo, true);
                        ///对其满包装数⑭以及库存数⑫进行扣减，扣减数量为包装数量，完成后标记交易处理状态⑲为20.已处理
                        stringBuilder.AppendFormat(packageStocksBLL.FullPackageStocksDownSql(packageStocksInfo, packageTranDetailInfo, loginUser));
                        break;
                    ///对于交易类型②为40//空器具入库  
                    case (int)PackageTranTypeConstants.EmptyInbound:
                        ///需要对工厂⑤、目标仓库⑪、目标存储区⑫、目标库位⑬的包装库存数据
                        packageStocksInfo = GetPackageStocksData(packageStocksInfos, packageTranDetailInfo, false);
                        if (packageStocksInfo == null)
                            packageStocksInfo = HandlingPackageStocksData(packageTranDetailInfo, maintainPartsInfo, false);
                        ///对其空包装数⑭以及库存数⑫进行累加，累加数量为包装数量，完成后标记交易处理状态⑲为20.已处理
                        stringBuilder.AppendFormat(packageStocksBLL.EmptyPackageStocksUpSql(packageStocksInfo, packageTranDetailInfo, loginUser));
                        break;
                    ///对于交易类型②为50//空器具移库  
                    case (int)PackageTranTypeConstants.EmptyMovement:
                        ///来源减少
                        ///需要对工厂⑤、来源仓库⑪、来源存储区⑫、来源库位⑬的包装库存数据
                        packageStocksInfo = GetPackageStocksData(packageStocksInfos, packageTranDetailInfo, true);
                        if (packageStocksInfo == null)
                            packageStocksInfo = HandlingPackageStocksData(packageTranDetailInfo, maintainPartsInfo, true);
                        ///对其空包装数⑭以及库存数⑫进行扣减，扣减数量为包装数量，完成后标记交易处理状态⑲为20.已处理
                        stringBuilder.AppendFormat(packageStocksBLL.EmptyPackageStocksDownSql(packageStocksInfo, packageTranDetailInfo, loginUser));
                        ///目标增加
                        ///需要对工厂⑤、目标仓库⑪、目标存储区⑫、目标库位⑬的包装库存数据
                        packageStocksInfo = GetPackageStocksData(packageStocksInfos, packageTranDetailInfo, false);
                        if (packageStocksInfo == null)
                            packageStocksInfo = HandlingPackageStocksData(packageTranDetailInfo, maintainPartsInfo, false);
                        ///对其空包装数⑭以及库存数⑫进行累加，累加数量为包装数量，完成后标记交易处理状态⑲为20.已处理
                        stringBuilder.AppendFormat(packageStocksBLL.EmptyPackageStocksUpSql(packageStocksInfo, packageTranDetailInfo, loginUser));
                        break;
                    ///对于交易类型②为60//空器具出库  
                    case (int)PackageTranTypeConstants.EmptyOutbound:
                        ///需要对工厂⑤、来源仓库⑪、来源存储区⑫、来源库位⑬的包装库存数据
                        packageStocksInfo = GetPackageStocksData(packageStocksInfos, packageTranDetailInfo, true);
                        if (packageStocksInfo == null)
                            packageStocksInfo = HandlingPackageStocksData(packageTranDetailInfo, maintainPartsInfo, true);
                        ///对其空包装数⑭以及库存数⑫进行扣减，扣减数量为包装数量，完成后标记交易处理状态⑲为20.已处理
                        stringBuilder.AppendFormat(packageStocksBLL.EmptyPackageStocksDownSql(packageStocksInfo, packageTranDetailInfo, loginUser));
                        break;
                }
                ///执行
                using (TransactionScope trans = new TransactionScope())
                {
                    if (stringBuilder.Length > 0)
                        BLL.LES.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                    trans.Complete();
                }
            }
        }

        /// <summary>
        /// 创建新器具库存
        /// </summary>
        /// <param name="packageTranDetailInfo"></param>
        /// <param name="maintainPartsInfo"></param>
        /// <param name="sourceFlag"></param>
        /// <returns></returns>
        private PackageStocksInfo HandlingPackageStocksData(PackageTranDetailInfo packageTranDetailInfo,  MaintainPartsInfo maintainPartsInfo, bool sourceFlag)
        {
            ///新建包装库存对象
            PackageStocksInfo packageStocksInfo = packageStocksBLL.CreatePackageStocksInfo(loginUser);
            /// 更新物料基础信息
            packageStocksBLL.UpdateMaintainPartsInfo(maintainPartsInfo, ref packageStocksInfo);
            /// 来源库存对象信息填充
            if (sourceFlag)
                packageStocksBLL.GetSourcePackageStocksInfo(packageTranDetailInfo, ref packageStocksInfo);
            ///目标库存对象信息填充
            else
                packageStocksBLL.GetTargetPackageStocksInfo(packageTranDetailInfo, ref packageStocksInfo);
            ///获取库存数据主键
                packageStocksInfo.Id = packageStocksBLL.GetPackageStocksId(packageStocksInfo);
            return packageStocksInfo;
        }
        /// <summary>
        /// 根据条件筛选器具库存数据
        /// </summary>
        /// <param name="packageStocksInfos"></param>
        /// <param name="packageTranDetailInfo"></param>
        /// <param name="sourceFlag"></param>
        /// <returns></returns>
        private PackageStocksInfo GetPackageStocksData(List<PackageStocksInfo> packageStocksInfos, PackageTranDetailInfo packageTranDetailInfo,bool sourceFlag)
        {
            PackageStocksInfo packageStocksInfo = new PackageStocksInfo();
            ///来源
            if (sourceFlag)
            {
                packageStocksInfo = packageStocksInfos.FirstOrDefault(d =>
                        d.Plant == packageTranDetailInfo.Plant
                        && d.WmNo == packageTranDetailInfo.WmNo
                        && d.ZoneNo == packageTranDetailInfo.ZoneNo
                        && d.Dloc == packageTranDetailInfo.Dloc);
            }
            ///目标
            else
            {
                packageStocksInfo = packageStocksInfos.FirstOrDefault(d =>
                       d.Plant == packageTranDetailInfo.Plant
                       && d.WmNo == packageTranDetailInfo.TargetWm
                       && d.ZoneNo == packageTranDetailInfo.TargetZone
                       && d.Dloc == packageTranDetailInfo.TargetDloc);
            }
            return packageStocksInfo;
        }
        #endregion
    }
}