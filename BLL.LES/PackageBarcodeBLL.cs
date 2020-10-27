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
    public class PackageBarcodeBLL
    {
        #region Common
        PackageBarcodeDAL dal = new PackageBarcodeDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PackageBarcodeInfo></returns>
        public List<PackageBarcodeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageBarcodeInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(PackageBarcodeInfo info)
        {
            info.BarcodeNo = new SeqDefineDAL().GetCurrentCode("BARCODE_NO");
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            PackageBarcodeInfo info = dal.GetInfo(id);
            if (info.Status != (int)PackageBarcodeStatusConstants.Created)
                throw new Exception("MC:0x00000683");///状态必须为已创建
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            PackageBarcodeInfo info = dal.GetInfo(id);
            if (info.Status != (int)PackageBarcodeStatusConstants.Created)
                throw new Exception("MC:0x00000683");///状态必须为已创建
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
            List<PackageBarcodeInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("MC:0x00000084");///数据有误
            List<PackageBarcodeDetailInfo> packages = new PackageBarcodeDetailDAL().GetList("[PACKAGE_BARCODE_FID] in ('" + string.Join("','", info.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);

            foreach (var item in info)
            {

                if (item == null)
                    throw new Exception("MC:0x00000084");///数据有误

                List<PackageBarcodeDetailInfo> infos = packages.Where(d => d.PackageBarcodeFid == item.Fid).ToList();
                if (infos == null || infos.Count() == 0)
                    throw new Exception("MC:0x00000442");///当包装器具托明细中至少有一条有效数据时允许提交

                if (item.Status != (int)PackageBarcodeStatusConstants.Created)
                    throw new Exception("MC:0x00000128");///状态为10.已创建时可以进行提交，更新状态为20.已提交
            }
            string sql = "update [LES].[TT_PCM_PACKAGE_BARCODE] set [STATUS] = " + (int)PackageBarcodeStatusConstants.GroupSupport + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] IN (" + string.Join(",", rowsKeyValues) + ")";
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

            if (dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)PackageBarcodeStatusConstants.GroupSupport, "[ID]").Count != rowsKeyValues.Count)
                throw new Exception("MC:0x00000462");///状态为已组托才能进行完成操作

            List<PackageBarcodeInfo> packageBarcodes = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (packageBarcodes == null || packageBarcodes.Count() == 0)
                throw new Exception("MC:0x00000084");///数据错误

            DataTable dt = CommonDAL.ExecuteDataTableBySql("select [SUPPLIER_NUM],[DOCK],[WM_NO],[ZONE_NO] from [LES].[TT_PCM_PACKAGE_BARCODE] where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  group by [SUPPLIER_NUM],[DOCK],[WM_NO],[ZONE_NO]");



            List<PackageBarcodeDetailInfo> packageBarcodeDetails = new PackageBarcodeDetailDAL().GetList("[PACKAGE_BARCODE_FID] in ('" + string.Join("','", packageBarcodes.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<PackageBarcodeInfo> barcodeInfos = packageBarcodes.Where(d => d.Dock == dt.Rows[i]["DOCK"].ToString() && d.SupplierNum == dt.Rows[i]["SUPPLIER_NUM"].ToString() && d.WmNo == dt.Rows[i]["WM_NO"].ToString() && d.ZoneNo == dt.Rows[i]["ZONE_NO"].ToString()).ToList();
                if (barcodeInfos == null)
                    throw new Exception("MC:0x00000084");///数据错误

                Guid guid = Guid.NewGuid();
                string OrderNo = new SeqDefineDAL().GetCurrentCode("POUT_ORDER_NO");
                stringBuilder.Append("insert into [LES].[TT_PCM_PACKAGE_OUTBOUND]([FID],[ORDER_NO],[S_WM_NO],[S_ZONE_NO],[SUPPLIER_NUM],[T_DOCK],[STATUS],[VALID_FLAG],[CREATE_DATE],[CREATE_USER])values(N'" + guid + "',N'" + OrderNo + "',N'" + barcodeInfos.FirstOrDefault().WmNo + "',N'" + barcodeInfos.FirstOrDefault().ZoneNo + "',N'" + barcodeInfos.FirstOrDefault().SupplierNum + "',N'" + barcodeInfos.FirstOrDefault().Dock + "'," + (int)WmmOrderStatusConstants.Published + ",1,GETDATE(),N'" + loginUser + "')\n");

                stringBuilder.Append("update [LES].[TT_PCM_PACKAGE_BARCODE] set [PACKAGE_ORDER_NO] = N'" + OrderNo + "',[PACKAGE_ORDER_TYPE] = 001,[PACKAGE_ORDER_FID] = N'" + guid + "',[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "'  where [ID] in ('" + string.Join("','", barcodeInfos.Select(d => d.Id).ToArray()) + "')\n");
                foreach (var items in barcodeInfos)
                {
                    List<PackageBarcodeDetailInfo> infos = packageBarcodeDetails.Where(d => d.PackageBarcodeFid == items.Fid).ToList();
                    foreach (var info in infos)
                    {
                        Guid fid = new Guid();
                        stringBuilder.Append("insert into [LES].[TT_PCM_PACKAGE_OUTBOUND_DETAIL] ([FID],[ORDER_FID],[ORDER_NO],[S_WM_NO],[S_ZONE_NO],[S_DLOC],[SUPPLIER_NUM],[PACKAGE_MODEL],[PACKAGE_CNAME],[PACKAGE_TYPE],[PACKAGE_QTY],[PACKAGE_BARCODE],[PACKAGE_BARCODE_FID],[VALID_FLAG],[CREATE_DATE],[CREATE_USER])values(");
                        stringBuilder.Append("N'" + fid + "',N'" + guid + "',N'" + OrderNo + "',N'" + items.WmNo + "',N'" + items.ZoneNo + "',N'" + items.Dloc + "',N'" + items.SupplierNum + "',N'" + info.PackageModel + "',N'" + info.PackageCname + "',N'" + info.PackageType + "',N'" + info.PackageQty + "',N'" + items.BarcodeNo + "',N'" + items.Fid + "',1,GETDATE(),N'" + loginUser + "'");
                        stringBuilder.Append(")\n");
                    }
                }
            }
            stringBuilder.Append("update [LES].[TT_PCM_PACKAGE_BARCODE] set [STATUS] = N'" + (int)PackageBarcodeStatusConstants.GroupSingle + "', [MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "'  where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")\n");
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(stringBuilder.ToString()))
                    CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
                trans.Complete();
            }
            return true;
        }
    }
}

