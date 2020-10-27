namespace WS.MES.InboundDataService
{
    using DAL.LES;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// MES-LES-001	过点信息
    /// </summary>
    public class BFDAMESVehiclePointScanBLL
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.MES.InboundDataService";
        /// <summary>
        /// ConversionToCentreInfo
        /// </summary>
        /// <param name="mesVehiclePointScanInfo"></param>
        /// <returns></returns>
        public MesVehiclePointScanInfo ConversionToCentreInfo(BFDAMesVehiclePointScanInfo mesVehiclePointScanInfo)
        {
            MesVehiclePointScanInfo vehiclePointScanInfo = new MesVehiclePointScanInfo();

            vehiclePointScanInfo.UnitNo = mesVehiclePointScanInfo.UNIT_NO;  ///采集点编号
            vehiclePointScanInfo.DmsSeq = mesVehiclePointScanInfo.DMS_SEQ;  ///过点顺序号
            vehiclePointScanInfo.DmsNo = mesVehiclePointScanInfo.DMS_NO;  ///计划订单号
            vehiclePointScanInfo.Vin = mesVehiclePointScanInfo.VIN;  ///VIN号
            vehiclePointScanInfo.PreviousDmsNo = mesVehiclePointScanInfo.PREVIOUS_DMS_NO;  ///前车订单号
            vehiclePointScanInfo.SendTime = mesVehiclePointScanInfo.SEND_TIME;  ///发送时间
            vehiclePointScanInfo.Falg = mesVehiclePointScanInfo.FALG;  ///标识

            return vehiclePointScanInfo;
        }
        /// <summary>
        /// ConversionToCentreList
        /// </summary>
        /// <param name="mesVehiclePointScanInfos"></param>
        /// <returns></returns>
        public List<MesVehiclePointScanInfo> ConversionToCentreList(List<BFDAMesVehiclePointScanInfo> mesVehiclePointScanInfos)
        {
            List<MesVehiclePointScanInfo> vehiclePointScanInfos = new List<MesVehiclePointScanInfo>();
            foreach (BFDAMesVehiclePointScanInfo mesVehiclePointScanInfo in mesVehiclePointScanInfos)
            {
                vehiclePointScanInfos.Add(ConversionToCentreInfo(mesVehiclePointScanInfo));
            }
            return vehiclePointScanInfos;
        }
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="mesVehiclePointScanInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAMesVehiclePointScanInfo mesVehiclePointScanInfo)
        {
            return mesVehiclePointScanInfo.UNIT_NO ;
        }
        /// <summary>
        /// GetKeyValues
        /// </summary>
        /// <param name="mesVehiclePointScanInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAMesVehiclePointScanInfo> mesVehiclePointScanInfos)
        {
            return string.Join(",", mesVehiclePointScanInfos.Select(d => d.UNIT_NO));
        }
        /// <summary>
        /// InsertInfoToCentreTable
        /// </summary>
        /// <param name="mesVehiclePointScanInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDAMesVehiclePointScanInfo mesVehiclePointScanInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// InsertListToCentreTable
        /// </summary>
        /// <param name="mesVehiclePointScanInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAMesVehiclePointScanInfo> mesVehiclePointScanInfos, Guid logFid, string logSql)
        {
            List<MesVehiclePointScanInfo> vehiclePointScanInfos = ConversionToCentreList(mesVehiclePointScanInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var vehiclePointScanInfo in vehiclePointScanInfos)
            {
                MesVehiclePointScanInfo mesVehiclePointScanInfo = new MesVehiclePointScanInfo();
                mesVehiclePointScanInfo.UnitNo = vehiclePointScanInfo.UnitNo;
                mesVehiclePointScanInfo.DmsSeq = vehiclePointScanInfo.DmsSeq.GetValueOrDefault();
                mesVehiclePointScanInfo.DmsNo = vehiclePointScanInfo.DmsNo;
                mesVehiclePointScanInfo.Vin = vehiclePointScanInfo.Vin;
                mesVehiclePointScanInfo.PreviousDmsNo = vehiclePointScanInfo.PreviousDmsNo;
                mesVehiclePointScanInfo.SendTime = vehiclePointScanInfo.SendTime.GetValueOrDefault();
                mesVehiclePointScanInfo.Falg = vehiclePointScanInfo.Falg;

                sqlSb.Append(MesVehiclePointScanDAL.GetInsertSql(mesVehiclePointScanInfo));
            }
            if (sqlSb.Length > 0)
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            return vehiclePointScanInfos.Count;
        }
    }
}