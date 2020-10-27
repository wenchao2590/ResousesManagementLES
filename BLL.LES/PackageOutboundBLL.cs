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
    public class PackageOutboundBLL
    {
        #region Common
        PackageOutboundDAL dal = new PackageOutboundDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PackageOutboundInfo></returns>
        public List<PackageOutboundInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageOutboundInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(PackageOutboundInfo info)
        {
            info.OrderNo = new SeqDefineDAL().GetCurrentCode("POUT_ORDER_NO");
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            PackageOutboundInfo info = dal.GetInfo(id);
            if (info.Status != (int)WmmOrderStatusConstants.Created)
                throw new Exception("MC:0x00000415");///状态为10.已创建时可以进行修改或删除
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            PackageOutboundInfo info = dal.GetInfo(id);
            if (info.Status != (int)WmmOrderStatusConstants.Created)
                throw new Exception("MC:0x00000441");///	状态为10.已创建时可以进行修改或删除
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
            List<PackageOutboundInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("MC:0x00000084");///数据有误
            List<PackageOutboundDetailInfo> packages = new PackageOutboundDetailDAL().GetList("[ORDER_FID] in ('" + string.Join("','", info.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);

            foreach (var item in info)
            {

                if (item == null)
                    throw new Exception("MC:0x00000084");///数据有误

                List<PackageOutboundDetailInfo> infos = packages.Where(d => d.OrderFid == item.Fid).ToList();
                if (infos == null || infos.Count() == 0)
                    throw new Exception("MC:0x00000442");///明细中至少有一条有效数据时允许提交

                if (item.Status != (int)PackageBarcodeStatusConstants.Created)
                    throw new Exception("MC:0x00000128");///状态为10.已创建时可以进行提交，更新状态为20.已提交
            }
            string sql = "update [LES].[TT_PCM_PACKAGE_OUTBOUND] set [STATUS] = " + (int)WmmOrderStatusConstants.Published + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] IN (" + string.Join(",", rowsKeyValues) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql); ;
        }
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CompleteInfos(List<string> rowsKeyValues, string loginUser)
        {
            if (dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)WmmOrderStatusConstants.Published, "[ID]").Count() != rowsKeyValues.Count())
                throw new Exception("MC:0x00000446");///	状态为20.已发布时可以进行完成操作，更新状态为30.已完成

            List<PackageOutboundInfo> packageBarcodes = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (packageBarcodes == null || packageBarcodes.Count() == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<PackageOutboundDetailInfo> packageBarcodeDetails = new PackageOutboundDetailDAL().GetList("[ORDER_FID] in ('" + string.Join("','", packageBarcodes.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in packageBarcodeDetails)
            {
                PackageOutboundInfo info = packageBarcodes.Where(d => d.Fid == item.OrderFid).FirstOrDefault();
                stringBuilder.Append("insert into [LES].[TT_PCM_PACKAGE_TRAN_DETAIL]([FID],[TRAN_NO],[TRAN_TYPE],[BARCODE_DATA],[PART_NO],[PLANT],[ASSEMBLY_LINE],[SUPPLIER_NUM],[WM_NO],[ZONE_NO],[DLOC],[TARGET_WM],[TARGET_ZONE],[TARGET_DLOC],[PACKAGE_NO],[PACKAGE_CNAME],[PACKAGE_ENAME],[PACKAGE],[PACKAGE_QTY],[STATUS],[VALID_FLAG],[CREATE_USER],[CREATE_DATE])values(");
                int TranType = new int();
                if (string.IsNullOrEmpty(item.TWmNo) && string.IsNullOrEmpty(item.TZoneNo))
                    TranType = (int)PackageTranTypeConstants.EmptyOutbound;
                else
                    TranType = (int)PackageTranTypeConstants.EmptyMovement;

                stringBuilder.Append("newid(),N'" + info.OrderNo + "',N'" + TranType + "',NULL,NULL,N'" + info.Plant + "',NULL,N'" + item.SupplierNum + "',N'" + item.SWmNo + "',N'" + item.SZoneNo + "',N'" + item.SDloc + "',N'" + item.TWmNo + "',N'" + item.TZoneNo + "',N'" + item.TDloc + "',N'" + item.PackageModel + "',N'" + item.PackageCname + "',NULL,NULL,N'" + item.PackageQty + "'," + (int)PackageTranStateConstants.UNTREATED + ",1,N'" + loginUser + "',GETDATE()");
                stringBuilder.Append(")");
            }
            stringBuilder.Append("update [LES].[TT_PCM_PACKAGE_OUTBOUND] set [STATUS] = N'" + (int)WmmOrderStatusConstants.Completed + "', [MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "'  where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")");
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(stringBuilder.ToString()))
                    CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
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
            string sql = "select T1.*,T2.[SUPPLIER_NAME] from [LES].[TT_PCM_PACKAGE_OUTBOUND] T1 with(nolock)  " +
                "left join [LES].[TM_BAS_SUPPLIER] T2 with(nolock) on T2.[SUPPLIER_NUM] = T1.[SUPPLIER_NUM] and T2.[VALID_FLAG] = 1 " +
                "where T1.[VALID_FLAG] = 1 and T1.[ID] in (" + string.Join(",", rowsKeyValues) + ");" +
                "select * from [LES].[TT_PCM_PACKAGE_OUTBOUND_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [ORDER_FID] in (select [FID] from [LES].[TT_PCM_PACKAGE_OUTBOUND] with(nolock) " +
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
            string sql = "update [LES].[TT_PCM_PACKAGE_OUTBOUND] set " +
                "[PRINT_TIME] = GETDATE()," +
                "[PRINT_COUNT] = isnull([PRINT_COUNT],0) + 1," +
                "[LAST_PRINT_USER] = N'" + loginUser + "' where " +
                "[ID] in (" + string.Join(",", rowsKeyValues) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion
    }
}

