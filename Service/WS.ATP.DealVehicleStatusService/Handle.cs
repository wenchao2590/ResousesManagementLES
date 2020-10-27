using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using BLL.LES;
using DM.LES;
using DM.SYS;
using BLL.SYS;
using System.Xml;
using Infrustructure.Utilities;
using Infrustructure.Logging;
using DAL.LES;

namespace WS.ATP.DealVehicleStatusService
{
    public class Handle
    {
        #region Variable

        /// <summary>
        /// 登录用户
        /// </summary>
        string loginUser = "DealVehicleStatusService";

        #endregion

        #region Handler

        public void Handler()
        {
            ///获取获取车辆过点扫描数据中状态为10.待处理的数据
            List<MesVehiclePointScanInfo> mesVehiclePointScanInfos = new MesVehiclePointScanBLL().GetList("[PROCESS_FLAG]=" + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (mesVehiclePointScanInfos.Count == 0) return;
            ///MES工厂集合
            List<PlantInfo> plantInfos = new PlantBLL().GetList("and [SAP_PLANT_CODE] in ('" + string.Join("','", mesVehiclePointScanInfos.Select(d => d.Enterprise).ToArray()) + "')", string.Empty);
            ///扫描点数据集合
            List<ScanPointInfo> scanPointInfos = new ScanPointBLL().GetList("and [PLANT] in ('" + string.Join("','", plantInfos.Select(d => d.Plant).ToArray()) + "')"
                + " and [MES_SCAN_POINT_CODE] in ('" + string.Join("','", mesVehiclePointScanInfos.Select(d => d.UnitNo).ToArray()) + "')", string.Empty);
            if (scanPointInfos.Count == 0) return;
            ///状态点集合
            List<StatusPointInfo> statusPointInfos = new StatusPointBLL().GetList(string.Empty, string.Empty);
            if (statusPointInfos.Count == 0) return;
            ///生产订单集合
            List<PullOrdersInfo> pullOrdersInfos = new PullOrdersBLL().GetList("and [ORDER_NO] in ('" + string.Join("','", mesVehiclePointScanInfos.Select(d => d.DmsNo).ToArray()) + "')", string.Empty);
            if (pullOrdersInfos.Count == 0) return;
            ///在系统配置中增加LES_VEHICLE_SEQ_STEP，默认为100，需要根据可在线车辆数量计算出这个值进行设定，在100%保障的情况下该值应被设定为最大可同时在线车辆数量
            string lesVehicleSeqStep = new ConfigBLL().GetValueByCode("LES_VEHICLE_SEQ_STEP");
            if (!int.TryParse(lesVehicleSeqStep, out int intLesVehicleSeqStep))
                intLesVehicleSeqStep = 100;
            ///逐条进行处理
            foreach (MesVehiclePointScanInfo mesVehiclePointScanInfo in mesVehiclePointScanInfos.OrderBy(d => d.Id).ToList())
            {
                ///sql语句
                StringBuilder stringBuilder = new StringBuilder();
                ///生产订单
                PullOrdersInfo pullOrdersInfo = pullOrdersInfos.Where(d => d.OrderNo == mesVehiclePointScanInfo.DmsNo).FirstOrDefault();
                if (pullOrdersInfo == null) continue;
                ///逐条处理车辆过点信息
                stringBuilder.AppendFormat(VehicleCrossingScanSql(mesVehiclePointScanInfo, scanPointInfos, statusPointInfos, intLesVehicleSeqStep, pullOrdersInfo, plantInfos));
                ///执行
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.AppendFormat(@"update [LES].[TI_IFM_MES_VEHICLE_POINT_SCAN] set [PROCESS_FLAG]=" + (int)ProcessFlagConstants.Processed + ",[PROCESS_TIME]=GETDATE(),[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE()");
                    using (TransactionScope trans = new TransactionScope())
                    {
                        BLL.LES.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                        trans.Complete();
                    }
                }
            }
        }

        #endregion

        #region Vehicle

        /// <summary>
        /// 车辆过点信息处理
        /// </summary>
        /// <param name="mesVehiclePointScanInfo"></param>
        /// <param name="scanPointInfos"></param>
        /// <param name="statusPointInfos"></param>
        /// <param name="intLesVehicleSeqStep"></param>
        /// <returns></returns>
        public string VehicleCrossingScanSql(MesVehiclePointScanInfo mesVehiclePointScanInfo, List<ScanPointInfo> scanPointInfos, List<StatusPointInfo> statusPointInfos, int intLesVehicleSeqStep, PullOrdersInfo pullOrdersInfo, List<PlantInfo> plantInfos)
        {
            ///sql
            StringBuilder stringBuilder = new StringBuilder();
            ///根据采集点编号④ = MES扫描点③获取到唯一的扫描点代码①，此处需要匹配工厂编号① = 工厂④
            PlantInfo plantInfo = plantInfos.Where(d => d.SapPlantCode == mesVehiclePointScanInfo.Enterprise).First();
            ScanPointInfo scanPointInfo = scanPointInfos.Where(d => d.MesScanPointCode == mesVehiclePointScanInfo.UnitNo && d.Plant == plantInfo.Plant).FirstOrDefault();
            if (scanPointInfo == null) return string.Empty;
            ///并根据唯一的扫描点代码①=④从状态点表中获取对应的状态点集合
            List<StatusPointInfo> statusPointByscanPointInfos = statusPointInfos.Where(d => d.ScanPointCode == scanPointInfo.ScanPointCode).ToList();
            if (statusPointByscanPointInfos.Count == 0) return string.Empty;
            ///处理状态点集合
            foreach (StatusPointInfo statusPointInfo in statusPointByscanPointInfos)
            {
                ///当状态点数据中是否匹配生产线⑪=true时、需要根据计划订单号匹配生产订单⑥中对应的生产线、若是否匹配生产线⑪=false则无需进行匹配
                string assemblyLine = statusPointInfo.AssemblyLineFixedFlag.GetValueOrDefault() ? statusPointInfo.AssemblyLine : string.Empty;
                ///车辆状态点信息
                List<VehiclePointStatusInfo> vehiclePointStatusInfos = new VehiclePointStatusBLL().GetList("" +
                    "[PLANT] = N'" + statusPointInfo.Plant + "' and " +
                    "[WORKSHOP] = N'" + statusPointInfo.Workshop + "' and " +
                    (string.IsNullOrEmpty(assemblyLine) ? string.Empty : "[ASSEMBLY_LINE] = N'" + assemblyLine + "' and ") +
                    "[ORDER_NO] = N'" + mesVehiclePointScanInfo.DmsNo + "'", string.Empty);
                ///扫描点基础数据
                List<StatusPointInfo> statusPoints = new StatusPointBLL().GetList("" +
                    "[PLANT] = N'" + statusPointInfo.Plant + "' and " +
                    (string.IsNullOrEmpty(assemblyLine) ? string.Empty : "[ASSEMBLY_LINE] = N'" + assemblyLine + "' and ") +
                    "[WORKSHOP] = N'" + statusPointInfo.Workshop + "'", string.Empty);
                ///若没有数据则需要根据该维度获取状态点基础数据中对应的数据集合
                ///并以此集合为基础将信息匹配完成后批量写入车辆状态点信息
                long currentSeqNo = 0;

                #region 横向维度
                if (vehiclePointStatusInfos.Count == 0)
                {
                    ///首次批量插入一个生产订单在工厂⑨车间⑧或工厂⑨车间⑧生产线⑦维度范围内数据时                
                    foreach (var statusPoint in statusPoints.OrderBy(d => d.StatusPointSeq.GetValueOrDefault()).ToList())
                    {
                        ///StatusPointInfo ->VehiclePointStatusInfo
                        VehiclePointStatusInfo pointStatusInfo = new VehiclePointStatusInfo();
                        ///ORDER_NO,生产订单号
                        pointStatusInfo.OrderNo = mesVehiclePointScanInfo.DmsNo;
                        ///STATUS_POINT_CODE,状态点代码
                        pointStatusInfo.StatusPointCode = statusPoint.StatusPointCode;
                        ///PLANT
                        pointStatusInfo.Plant = statusPoint.Plant;
                        ///Workshop
                        pointStatusInfo.Workshop = statusPoint.Workshop;
                        ///ASSEMBLY_LINE
                        pointStatusInfo.AssemblyLine = statusPoint.AssemblyLine;
                        ///SPJ
                        pointStatusInfo.Spj = pullOrdersInfo.Spj;
                        ///KNR
                        pointStatusInfo.Knr = pullOrdersInfo.Knr;
                        ///Schicht
                        pointStatusInfo.Schicht = null;
                        ///Shift
                        pointStatusInfo.Shift = null;
                        ///Vin
                        pointStatusInfo.Vin = pullOrdersInfo.Vin;
                        ///RunningNo
                        pointStatusInfo.RunningNo = mesVehiclePointScanInfo.DmsSeq.ToString();
                        ///PassTime，过点时间
                        pointStatusInfo.PassTime = null;
                        ///VehicleStatus，初始化
                        pointStatusInfo.VehicleStatus = (int)VehicleStatusTypeConstants.Initializtion;
                        ///ProcessFlag
                        pointStatusInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                        ///SeqNo，LES序号
                        pointStatusInfo.SeqNo = GetcurrentSeqNo(statusPoints, statusPointInfo, intLesVehicleSeqStep);
                        stringBuilder.AppendLine(VehiclePointStatusDAL.GetInsertSql(pointStatusInfo));
                    }
                    ///LES排序号
                    currentSeqNo = GetcurrentSeqNo(statusPoints, statusPointInfo, intLesVehicleSeqStep);
                    ///更新生产订单状态为已上线
                    stringBuilder.AppendFormat("update [LES].[TT_BAS_PULL_ORDERS] set " +
                        "[ORDER_STATUS]="+(int)OrderStatusConstants.AlreadOnline+ "" +
                        ",[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE() " +
                        "where [ID] = " + pullOrdersInfo.Id + ";");
                }
                ///若已存在数据则首先需要将本采集点编号④对应的状态点代码②数据的过点时间⑦=⑦、车辆状态⑨更新
                ///车辆状态的更新目前已知的类型有初始化、正常过点、校验补入、车辆离队、车辆归队，需要枚举项VEHICLE_STATUS_TYPE支持
                else
                {
                    VehiclePointStatusInfo pointStatusInfo = vehiclePointStatusInfos.FirstOrDefault(d => d.StatusPointCode == statusPointInfo.StatusPointCode);
                    if (pointStatusInfo == null) continue;
                    ///当车辆离队时，更新LES排序号⑬为零，车辆归队时重新计算车辆所在位置对应的LES排序号⑬，规则依照之前逻辑
                    ///离队标记
                    bool leaveFlag = false;
                    if (leaveFlag)
                    {
                        List<StatusPointInfo> statuses = statusPoints.Where(d => d.StatusPointSeq.GetValueOrDefault() >= statusPointInfo.StatusPointSeq.GetValueOrDefault()).ToList();
                        List<VehiclePointStatusInfo> infos = vehiclePointStatusInfos.Where(d => statuses.Select(s => s.StatusPointCode).Contains(d.StatusPointCode)).ToList();
                        foreach (var info in infos)
                        {
                            stringBuilder.AppendLine("update [LES].[TT_BAS_VEHICLE_POINT_STATUS] " +
                                "set [VEHICLE_STATUS] = " + (int)VehicleStatusTypeConstants.VehicleLeave + ",[SEQ_NO] = 0,[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE() " +
                                "where [ID] = " + info.Id + ";");
                        }
                        ///更新生产订单状态为已离队
                        stringBuilder.AppendFormat("update [LES].[TT_BAS_PULL_ORDERS] set " +
                            "[ORDER_STATUS]=" + (int)OrderStatusConstants.Bryan + "" +
                            ",[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE() " +
                            "where [ID] = " + pullOrdersInfo.Id + ";");
                        continue;
                    }
                    ///车辆归队
                    if (pointStatusInfo.VehicleStatus == (int)VehicleStatusTypeConstants.VehicleLeave)
                    {

                        stringBuilder.AppendLine("update [LES].[TT_BAS_VEHICLE_POINT_STATUS] " +
                            "set [PASS_TIME] = N'" + mesVehiclePointScanInfo.SendTime.GetValueOrDefault() + "'," +
                            "[VEHICLE_STATUS] = " + (int)VehicleStatusTypeConstants.VehicleReturn + " ," +
                            "[SEQ_NO] = " + GetcurrentSeqNo(statusPoints, statusPointInfo, intLesVehicleSeqStep) + ",[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE()" +
                            "where [ID] = " + pointStatusInfo.Id + ";");
                        List<StatusPointInfo> statuses = statusPoints.Where(d => d.StatusPointSeq.GetValueOrDefault() > statusPointInfo.StatusPointSeq.GetValueOrDefault()).ToList();
                        List<VehiclePointStatusInfo> infos = vehiclePointStatusInfos.Where(d => statuses.Select(s => s.StatusPointCode).Contains(d.StatusPointCode)).ToList();
                        foreach (var info in infos)
                        {
                            stringBuilder.AppendLine("update [LES].[TT_BAS_VEHICLE_POINT_STATUS] " +
                                "set [VEHICLE_STATUS] = " + (int)VehicleStatusTypeConstants.Initializtion + " ," +
                                "[SEQ_NO] = " + GetcurrentSeqNo(statuses, statusPointInfo, intLesVehicleSeqStep) + " ,[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE()" +
                                "where [ID] = " + info.Id + ";");
                        }
                        ///更新生产订单状态为已归队
                        stringBuilder.AppendFormat("update [LES].[TT_BAS_PULL_ORDERS] set " +
                            "[ORDER_STATUS]=" + (int)OrderStatusConstants.ComeBack + "" +
                            ",[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE() " +
                            "where [ID] = " + pullOrdersInfo.Id + ";");
                    }
                    ///LES序号
                    currentSeqNo = pointStatusInfo.SeqNo.GetValueOrDefault();
                    DateTime currentTime = mesVehiclePointScanInfo.SendTime.GetValueOrDefault();
                    stringBuilder.AppendLine("update [LES].[TT_BAS_VEHICLE_POINT_STATUS] " +
                        "set [PASS_TIME] = N'" + currentTime + "',[VEHICLE_STATUS] = " + (int)VehicleStatusTypeConstants.NormalPoint + " ,[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE()" +
                        "where [ID] = " + pointStatusInfo.Id + ";");
                    ///对于状态点顺序在本次更新的车辆状态点信息之前的且车辆状态为初始化的数据需要更新其过点时间⑦、车辆状态⑨为校验补入
                    ///过点时间根据获取最近一条有过点时间⑦到本条车辆状态数据过点时间⑦的时间差按车辆状态⑨初始化的状态点数量平均分配计算赋值
                    List<VehiclePointStatusInfo> vehiclePointStatuses = vehiclePointStatusInfos.
                        Where(d => d.VehicleStatus == (int)VehicleStatusTypeConstants.VehicleLeave && d.StatusPointCode != statusPointInfo.StatusPointCode).
                        OrderByDescending(d => d.Id).
                        ToList();
                    if (vehiclePointStatuses.Count == 0) continue;
                    DateTime? lastTime = new VehiclePointStatusDAL().GetLastTime(
                           statusPointInfo.Plant,
                           statusPointInfo.Workshop,
                           statusPointInfo.StatusPointCode,
                           statusPointInfo.AssemblyLine);
                    TimeSpan timeSpan = mesVehiclePointScanInfo.SendTime.GetValueOrDefault() - lastTime.GetValueOrDefault();
                    int deductionSecondsPer = Convert.ToInt32(timeSpan.TotalSeconds) / (vehiclePointStatuses.Count - 1);
                    foreach (var vehiclePointStatuse in vehiclePointStatuses)
                    {
                        currentTime = currentTime.AddSeconds(0 - deductionSecondsPer);
                        stringBuilder.AppendLine("update [LES].[TT_BAS_VEHICLE_POINT_STATUS] set " +
                            "[PASS_TIME] = N'" + currentTime + "'," +
                            "[VEHICLE_STATUS] = " + (int)VehicleStatusTypeConstants.CheckAndFill + ",[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE() " +
                            "where [ID] = " + vehiclePointStatuse.Id + ";");
                    }
                    ///TODO:
                    ///当前过点类型为正常过点、车辆离队时触发以上逻辑
                    ///过点类型目前接口中未体现，后期需要增加，预先考虑逻辑放置位置，类型为车辆归队时只需更新本条记录即可
                    ///若接口中车辆归队与正常过点无法区分，则需要在车辆离队时将其状态标记在生产订单上，以便区别正常过点与车辆归队
                }
                ///以上为横向维度（同生产订单号）的处理逻辑
                #endregion

                ///以下为纵向维度（同状态点）的处理逻辑
                stringBuilder.AppendFormat(LongitudinalDimension(statusPoints, statusPointInfo, mesVehiclePointScanInfo, assemblyLine, currentSeqNo));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 纵向维度
        /// </summary>
        /// <param name="statusPoints"></param>
        /// <param name="statusPointInfo"></param>
        /// <param name="mesVehiclePointScanInfo"></param>
        /// <param name="assemblyLine"></param>
        /// <param name="currentSeqNo"></param>
        /// <returns></returns>
        public string LongitudinalDimension(List<StatusPointInfo> statusPoints, StatusPointInfo statusPointInfo, MesVehiclePointScanInfo mesVehiclePointScanInfo, string assemblyLine, long currentSeqNo)
        {
            ///sql
            StringBuilder stringBuilder = new StringBuilder();
            ///以下开始为纵向维度（同状态点）的处理逻辑
            ///以本次采集点编号④对应状态点代码①的状态点顺序③为基准
            List<StatusPointInfo> pointInfos = statusPoints.
                Where(d => d.StatusPointSeq.GetValueOrDefault() > statusPointInfo.StatusPointSeq.GetValueOrDefault()).
                OrderBy(d => d.StatusPointSeq.GetValueOrDefault()).
                ToList();
            List<VehiclePointStatusInfo> pointStatusInfos = new VehiclePointStatusBLL().GetList("" +
                "[PLANT] = N'" + statusPointInfo.Plant + "' and " +
                "[WORKSHOP] = N'" + statusPointInfo.Workshop + "' and " +
                (string.IsNullOrEmpty(assemblyLine) ? string.Empty : "[ASSEMBLY_LINE] = N'" + assemblyLine + "' and ") +
                "[SEQ_NO] < " + currentSeqNo + " and " +
                "[VEHICLE_STATUS] =" + (int)VehicleStatusTypeConstants.VehicleLeave + "", string.Empty);
            ///依次将状态点顺序③比参照数据大的数据  且非本次计划订单号⑥中车辆状态⑨为初始化数据的过点时间⑦、车辆状态⑨进行更新
            ///直至状态点顺序③最大值，理论上当状态点顺序号③逐渐变大时更新数据的数量始终唯一，过点时间⑦按本次发送时间⑦进行更新，车辆状态⑨为正常过点
            foreach (var pointInfo in pointInfos)
            {
                VehiclePointStatusInfo pointStatusInfo = pointStatusInfos.
                    Where(d => d.StatusPointCode == pointInfo.StatusPointCode).
                    OrderByDescending(d => d.SeqNo.GetValueOrDefault()).
                    FirstOrDefault();
                if (pointStatusInfo == null) continue;
                DateTime currentTime = mesVehiclePointScanInfo.SendTime.GetValueOrDefault();
                stringBuilder.AppendLine("update [LES].[TT_BAS_VEHICLE_POINT_STATUS] " +
                    "set [PASS_TIME] = N'" + currentTime + "',[VEHICLE_STATUS] = " + (int)VehicleStatusTypeConstants.NormalPoint + ",[MODIFY_USER]='" + loginUser + "',[MODIFY_DATE]=GETDATE()" +
                    "where [ID] = " + pointStatusInfo.Id + ";");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 计算排序号
        /// </summary>
        /// <param name="currentSeqNo"></param>
        /// <param name="statusPoints"></param>
        /// <param name="statusPointInfo"></param>
        /// <param name="intLesVehicleSeqStep"></param>
        /// <returns></returns>
        public long GetcurrentSeqNo(List<StatusPointInfo> statusPoints, StatusPointInfo statusPointInfo, int intLesVehicleSeqStep)
        {
            long currentSeqNo = 0;
            ///若本次采集点编号④对应的状态点代码②是该维度下状态点顺序号③最小的一个
            if (statusPoints.Min(d => d.StatusPointSeq.GetValueOrDefault()) == statusPointInfo.StatusPointSeq.GetValueOrDefault())
            {
                ///则根据当前该维度下最大的LES排序号⑬
                ///系统配置步长作为本次的LES排序号⑬
                currentSeqNo = new VehiclePointStatusDAL().GetMaxSeqNo(
                    statusPointInfo.Plant,
                    statusPointInfo.Workshop,
                    statusPointInfo.StatusPointCode,
                    statusPointInfo.AssemblyLine) + intLesVehicleSeqStep;
            }
            else
            {
                ///否则需要获取状态点代码①过点时间距离当前时间最近的前辆车以及状态点顺序③距离当前最近后辆车的LES排序号⑬
                currentSeqNo = new VehiclePointStatusDAL().GetLastTimeSeqNo(
                    statusPointInfo.Plant,
                    statusPointInfo.Workshop,
                    statusPointInfo.StatusPointCode,
                    statusPointInfo.AssemblyLine);

                List<StatusPointInfo> statuses = statusPoints.
                    Where(d => d.StatusPointSeq.GetValueOrDefault() < statusPointInfo.StatusPointSeq.GetValueOrDefault()).
                    OrderByDescending(d => d.StatusPointSeq.GetValueOrDefault()).
                    ToList();
                long nextSeqNo = 0;
                foreach (var statuse in statuses)
                {
                    nextSeqNo = new VehiclePointStatusDAL().GetLastTimeSeqNo(
                       statuse.Plant,
                       statuse.Workshop,
                       statuse.StatusPointCode,
                       statuse.AssemblyLine);
                    if (nextSeqNo > 0) break;
                }
                ///求其平均值作为本次的LES排序号⑬
                currentSeqNo += nextSeqNo == 0 ? 100 : nextSeqNo;
                currentSeqNo = nextSeqNo == 0 ? currentSeqNo : currentSeqNo / 2;
            }
            return currentSeqNo;
        }
        #endregion
    }
}