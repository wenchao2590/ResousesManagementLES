using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class DeliveryRunsheetBarcodeBLL
    {
        #region Common
        DeliveryRunsheetBarcodeDAL dal = new DeliveryRunsheetBarcodeDAL();
        public List<DeliveryRunsheetBarcodeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public DeliveryRunsheetBarcodeInfo SelectInfo(int barcodeDetailId)
        {
            return dal.GetInfo(barcodeDetailId);
        }

        public int InsertInfo(DeliveryRunsheetBarcodeInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(DeliveryRunsheetBarcodeInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int barcodeDetailId)
        {
            return dal.Delete(barcodeDetailId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int barcodeDetailId)
        {
            return dal.UpdateInfo(fields, barcodeDetailId) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}

