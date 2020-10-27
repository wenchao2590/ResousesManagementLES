using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    public class PackageInboundBLL
    {
        #region Common
        PackageInboundDAL dal = new PackageInboundDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PackageInboundInfo></returns>
        public List<PackageInboundInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageInboundInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(PackageInboundInfo info)
        {
            /// 器具入库单号 ①系统自动根据规则创建
            info.OrderNo = new SeqDefineDAL().GetCurrentCode("ORDER_NO");
            info.Status = (int)PackageInboundStatusConstants.Created;
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            PackageInboundInfo info = dal.GetInfo(id);
            if (info.Status != (int)PackageInboundStatusConstants.Created)
                throw new Exception("MC:0x00000415");///状态为10.已创建时可以进行修改或删除
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            PackageInboundInfo info = dal.GetInfo(id);
            if (info.Status != (int)PackageInboundStatusConstants.Created)
                throw new Exception("MC:0x00000441");///状态为10.已创建时可以进行修改或删除
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool SubmitInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///状态必须已创建
            List<PackageInboundInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("MC:0x00000084");///数据有误
            string sql = string.Empty;
            List<PackageInboundDetailInfo> inboundDetailInfos = new PackageInboundDetailDAL().GetList(" [ORDER_FID] IN ('" + string.Join("','", info.Select(d => d.Fid).ToArray()) + "')", string.Empty);
            List<PackageApplianceInfo> PackageAppliance = new PackageApplianceDAL().GetList("[PACKAGE_NO] IN ('" + string.Join("','", inboundDetailInfos.Select(d => d.PackageModel).ToArray()) + "')", string.Empty);
            List<PartsStockInfo> partsStocks = new PartsStockDAL().GetList("[INBOUND_PACKAGE_MODEL] IN ('" + string.Join("','", inboundDetailInfos.Select(d => d.PackageModel).ToArray()) + "')", string.Empty);

            foreach (var item in info)
            {
                if (item == null)
                    throw new Exception("MC:0x00000084");///数据有误
                if (item.Status != (int)PackageInboundStatusConstants.Created)
                    throw new Exception("MC:0x00000128");///状态为10.已创建时可以进行提交，更新状态为20.已提交
                List<PackageInboundDetailInfo> packageInboundDetails = inboundDetailInfos.Where(d => d.OrderFid == item.Fid).ToList();

                sql += "update [LES].[TT_PCM_PACKAGE_INBOUND] set [STATUS] = " + (int)PackageInboundStatusConstants.Published + " where [ID] = " + item.Id + ";";
                ///根据器具入库单明细生成包装器具交易记录
                foreach (var items in packageInboundDetails)
                {
                    string packageCname = string.Empty;
                    PackageApplianceInfo infos = PackageAppliance.FirstOrDefault(d => d.PackageNo == items.PackageModel);
                    if (infos == null)
                        packageCname = "NULL";
                    else
                        packageCname = infos.PackageCname;

                    PartsStockInfo stockInfo = partsStocks.FirstOrDefault(d => d.InboundPackageModel == items.PackageModel);
                    string inboundPackage = string.Empty;
                    if (stockInfo == null)
                        inboundPackage = "NULL";
                    else
                        inboundPackage = stockInfo.InboundPackage.ToString();

                    sql += "insert into [LES].[TT_PCM_PACKAGE_TRAN_DETAIL] ([FID],[PLANT],[TRAN_NO],[TRAN_TYPE],[PACKAGE_CNAME],[PACKAGE],[SUPPLIER_NUM],[PACKAGE_NO],[WM_NO],[ZONE_NO],[DLOC],[TARGET_WM],[TARGET_ZONE],[TARGET_DLOC],[PACKAGE_QTY],[STATUS],[COMMENTS],[VALID_FLAG],[CREATE_USER],[CREATE_DATE])"
                       + "VALUES(newid(),"
                       + "N'" + item.Plant + "',"
                       + "N'" + item.OrderNo + "',"
                       + (int)PackageTranTypeConstants.EmptyInbound + ","
                       + "N'" + packageCname + "',"
                        + inboundPackage + ","
                       + "N'" + items.SupplierNum + "',"
                       + "N'" + items.PackageModel + "',"
                       + "N'" + items.SWmNo + "',"
                       + "N'" + items.SZoneNo + "',"
                       + "N'" + items.SDloc + "',"
                       + "N'" + items.TWmNo + "',"
                       + "N'" + items.TZoneNo + "',"
                       + "N'" + items.TDloc + "',"
                       + "N'" + items.PackageQty + "',"
                       + "N'" + items.PackageStatus + "',"
                       + "N'" + items.Comments + "',"
                       + "1,"
                       + "N'" + loginUser + "',"
                       + "GETDATE()"
                       + ");";
                }

            }
            using (TransactionScope trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }


        #region Print
        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintPackageInboundDatas(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "select T1.*,T2.[SUPPLIER_NAME] from [LES].[TT_PCM_PACKAGE_INBOUND] T1 with(nolock)  " +
                "left join [LES].[TM_BAS_SUPPLIER] T2 with(nolock) on T2.[SUPPLIER_NUM] = T1.[SUPPLIER_NUM] and T2.[VALID_FLAG] = 1 " +
                "where T1.[VALID_FLAG] = 1 and T1.[ID] in (" + string.Join(",", rowsKeyValues) + ");" +
                "select * from [LES].[TT_PCM_PACKAGE_INBOUND_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [ORDER_FID] in (select [FID] from [LES].[TT_PCM_PACKAGE_INBOUND] with(nolock) " +
                "where [ID] in (" + string.Join(",", rowsKeyValues) + ") and [VALID_FLAG] = 1);";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }
        /// <summary>
        /// 打印后回调函数
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        public bool PrintPackageInboundCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "update [LES].[TT_PCM_PACKAGE_INBOUND] set " +
                "[PRINT_TIME] = GETDATE()," +
                "[PRINT_COUNT] = isnull([PRINT_COUNT],0) + 1," +
                "[LAST_PRINT_USER] = N'" + loginUser + "' where " +
                "[ID] in (" + string.Join(",", rowsKeyValues) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion
    }
}

