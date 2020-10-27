using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PackageStocksBLL
    {
        #region Common
        PackageStocksDAL dal = new PackageStocksDAL();
        public List<PackageStocksInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageStocksInfo SelectInfo(int stockId)
        {
            return dal.GetInfo(stockId);
        }

        public  long InsertInfo(PackageStocksInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(PackageStocksInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int stockId)
        {
            return dal.Delete(stockId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int stockId)
        {
            return dal.UpdateInfo(fields, stockId) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }
        public List<PackageStocksInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        #endregion

        /// <summary>
        /// 满包装入库
        /// </summary>
        /// <param name="packageStocksInfo"></param>
        /// <param name="packageTranDetailInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string FullPackageStocksUpSql(PackageStocksInfo packageStocksInfo, PackageTranDetailInfo packageTranDetailInfo, string loginUser)
        {
            return "update [LES].[TT_PCM_PACKAGE_STOCKS] "
               + "set [STOCK] = [STOCK] + " + packageTranDetailInfo.PackageQty.GetValueOrDefault() + ""
               + ",[FULL_STOCK] = [FULL_STOCK] + " + packageTranDetailInfo.PackageQty.GetValueOrDefault() + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] =" + packageStocksInfo.Id + ";"
               + "update [LES].[TT_PCM_PACKAGE_TRAN_DETAIL] "
               + "set [STATUS] = " + (int)PackageTranStateConstants.TREATED + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] = " + packageTranDetailInfo.Id + ";";
        }
        /// <summary>
        /// 满包装出库
        /// </summary>
        /// <param name="packageStocksInfo"></param>
        /// <param name="packageTranDetailInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string FullPackageStocksDownSql(PackageStocksInfo packageStocksInfo, PackageTranDetailInfo packageTranDetailInfo, string loginUser)
        {
            return "update [LES].[TT_PCM_PACKAGE_STOCKS] "
               + "set [STOCK] = [STOCK] - " + packageTranDetailInfo.PackageQty.GetValueOrDefault() + ""
               + ",[FULL_STOCK] = [FULL_STOCK] - " + packageTranDetailInfo.PackageQty.GetValueOrDefault() + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] =" + packageStocksInfo.Id + ";"
               + "update [LES].[TT_PCM_PACKAGE_TRAN_DETAIL] "
               + "set [STATUS] = " + (int)PackageTranStateConstants.TREATED + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] = " + packageTranDetailInfo.Id + ";";
        }
        /// <summary>
        /// 空包装入库
        /// </summary>
        /// <param name="packageStocksInfo"></param>
        /// <param name="packageTranDetailInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string EmptyPackageStocksUpSql(PackageStocksInfo packageStocksInfo, PackageTranDetailInfo packageTranDetailInfo, string loginUser)
        {
            return "update [LES].[TT_PCM_PACKAGE_STOCKS] "
               + "set [STOCK] = [STOCK] + " + packageTranDetailInfo.PackageQty.GetValueOrDefault() + ""
               + ",[EMPTY_STOCK] = [EMPTY_STOCK] + " + packageTranDetailInfo.PackageQty.GetValueOrDefault() + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] =" + packageStocksInfo.Id + ";"
               + "update [LES].[TT_PCM_PACKAGE_TRAN_DETAIL] "
               + "set [STATUS] = " + (int)PackageTranStateConstants.TREATED + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] = " + packageTranDetailInfo.Id + ";";
        }
        /// <summary>
        /// 空包装出库
        /// </summary>
        /// <param name="packageStocksInfo"></param>
        /// <param name="packageTranDetailInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string EmptyPackageStocksDownSql(PackageStocksInfo packageStocksInfo, PackageTranDetailInfo packageTranDetailInfo, string loginUser)
        {
            return "update [LES].[TT_PCM_PACKAGE_STOCKS] "
               + "set [STOCK] = [STOCK] - " + packageTranDetailInfo.PackageQty.GetValueOrDefault() + ""
               + ",[EMPTY_STOCK] = [EMPTY_STOCK] - " + packageTranDetailInfo.PackageQty.GetValueOrDefault() + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] =" + packageStocksInfo.Id + ";"
               + "update [LES].[TT_PCM_PACKAGE_TRAN_DETAIL] "
               + "set [STATUS] = " + (int)PackageTranStateConstants.TREATED + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] = " + packageTranDetailInfo.Id + ";";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public PackageStocksInfo CreatePackageStocksInfo(string loginUser)
        {
            PackageStocksInfo packageStocksInfo = new PackageStocksInfo();

            ///CreateUser
            packageStocksInfo.CreateUser = loginUser;
            ///CreateDate
            packageStocksInfo.CreateDate = DateTime.Now;
            ///ValidFlag
            packageStocksInfo.ValidFlag = true;
            ///Fid
            packageStocksInfo.Fid = Guid.NewGuid();
            return packageStocksInfo;
        }

        /// <summary>
        /// 更新物料基础数据
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="plant"></param>
        /// <param name="stocksInfo"></param>
        public void UpdateMaintainPartsInfo(MaintainPartsInfo maintainPartsInfo, ref PackageStocksInfo packageStocksInfo)
        {
            if (maintainPartsInfo == null) return;
            ///信息员
            packageStocksInfo.Informationer = maintainPartsInfo.InfoPerson;
        }

        /// <summary>
        /// 根据交易记录获取来源库存对象
        /// </summary>
        /// <param name="packageTranDetailInfo"></param>
        /// <param name="packageStocksInfo"></param>
        public void GetSourcePackageStocksInfo(PackageTranDetailInfo packageTranDetailInfo, ref PackageStocksInfo packageStocksInfo)
        {
            ///工厂
            packageStocksInfo.Plant = packageTranDetailInfo.Plant;
            ///器具号
            packageStocksInfo.PackageNo = packageTranDetailInfo.PackageNo;
            ///存贮区编码
            packageStocksInfo.ZoneNo = packageTranDetailInfo.ZoneNo;
            ///仓库编码
            packageStocksInfo.WmNo = packageTranDetailInfo.WmNo;
            ///库位
            packageStocksInfo.Dloc = packageTranDetailInfo.Dloc;
        }

        /// <summary>
        /// 根据交易记录获取目标库存对象
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        /// <param name="zonesInfo"></param>
        /// <param name="stocksInfo"></param>
        public void GetTargetPackageStocksInfo(PackageTranDetailInfo packageTranDetailInfo, ref PackageStocksInfo packageStocksInfo)
        {
            ///工厂
            packageStocksInfo.Plant = packageTranDetailInfo.Plant;
            ///器具号
            packageStocksInfo.PackageNo = packageTranDetailInfo.PackageNo;
            ///存贮区编码
            packageStocksInfo.ZoneNo = packageTranDetailInfo.TargetZone;
            ///仓库编码
            packageStocksInfo.WmNo = packageTranDetailInfo.TargetWm;
            ///库位
            packageStocksInfo.Dloc = packageTranDetailInfo.TargetDloc;
        }
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="packageStocksInfo"></param>
        /// <returns></returns>
        public long GetPackageStocksId(PackageStocksInfo packageStocksInfo)
        {
            return dal.Add(packageStocksInfo); 
        }
    }
}

