using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// BarcodeStatusBLL
    /// </summary>
    public class BarcodeStatusBLL
    {
        #region Common
        /// <summary>
        /// BarcodeStatusDAL
        /// </summary>
        BarcodeStatusDAL dal = new BarcodeStatusDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BarcodeStatusInfo></returns>
        public List<BarcodeStatusInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BarcodeStatusInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(BarcodeStatusInfo info)
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

        #endregion

        /// <summary>
        /// 获取条码状态
        /// </summary>
        /// <param name="barcodeData"></param>
        /// <returns></returns>
        public List<BarcodeStatusInfo> GetBarcodeStatusInfos(string barcodeData)
        {
            List<BarcodeStatusInfo> barcodeStatusInfos = dal.GetList("[BARCODE_DATA] = N'" + barcodeData + "'", "[ID] desc");

            return barcodeStatusInfos;
        }

        #region Interface
        /// <summary>
        /// Create BarcodeStatusInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>BarcodeStatusInfo</returns>
        public static BarcodeStatusInfo CreateBarcodeStatusInfo(string loginUser)
        {
            BarcodeStatusInfo info = new BarcodeStatusInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///MODIFY_USER
            info.ModifyUser = null;
            ///MODIFY_DATE
            info.ModifyDate = null;
            return info;
        }
        /// <summary>
        /// BarcodeInfo -> BarcodeStatusInfo
        /// </summary>
        /// <param name="barcodeInfo"></param>
        /// <param name="barcodeStatusInfo"></param>
        public static void GetBarcodeStatusInfo(BarcodeInfo barcodeInfo, ref BarcodeStatusInfo barcodeStatusInfo)
        {
            if (barcodeInfo == null) return;
            ///BARCODE_FID
            barcodeStatusInfo.BarcodeFid = barcodeInfo.Fid;
            ///PART_NO
            barcodeStatusInfo.PartNo = barcodeInfo.PartNo;
            ///PART_CNAME
            barcodeStatusInfo.PartCname = barcodeInfo.PartCname;
            ///BARCODE_DATA
            barcodeStatusInfo.BarcodeData = barcodeInfo.BarcodeData;
            ///BARCODE_STATUS
            barcodeStatusInfo.BarcodeStatus = barcodeInfo.BarcodeStatus;
            ///PACKAGE_MODEL
            barcodeStatusInfo.PackageModel = barcodeInfo.PackageModel;
            ///PACKAGE
            barcodeStatusInfo.Package = barcodeInfo.Package;
            ///CURRENT_QTY
            barcodeStatusInfo.CurrentQty = barcodeInfo.CurrentQty;
            ///MEASURING_UNIT_NO
            barcodeStatusInfo.MeasuringUnitNo = barcodeInfo.MeasuringUnitNo;
            ///SUPPLIER_NUM
            barcodeStatusInfo.SupplierNum = barcodeInfo.SupplierNum;
            ///PLANT
            barcodeStatusInfo.Plant = barcodeInfo.Plant;
            ///ASSEMBLY_LINE
            barcodeStatusInfo.AssemblyLine = barcodeInfo.AssemblyLine;
            ///LOCATION
            barcodeStatusInfo.Location = barcodeInfo.Location;
            ///DOCK
            barcodeStatusInfo.Dock = barcodeInfo.Dock;
            ///WM_NO
            barcodeStatusInfo.WmNo = barcodeInfo.WmNo;
            ///ZONE_NO
            barcodeStatusInfo.ZoneNo = barcodeInfo.ZoneNo;
            ///DLOC
            barcodeStatusInfo.Dloc = barcodeInfo.Dloc;
            ///BATTH_NO
            barcodeStatusInfo.BatthNo = barcodeInfo.BatthNo;
            ///RFID_NO
            barcodeStatusInfo.RfidNo = barcodeInfo.RfidNo;
            ///ASN_RUNSHEET_NO
            barcodeStatusInfo.AsnRunsheetNo = barcodeInfo.AsnRunsheetNo;
            ///COMMENTS
            barcodeStatusInfo.Comments = barcodeInfo.Comments;
        }
        #endregion

    }
}

