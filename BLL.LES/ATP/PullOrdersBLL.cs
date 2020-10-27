using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// PullOrdersBLL
    /// </summary>
    public class PullOrdersBLL
    {
        #region Common
        /// <summary>
        /// PullOrdersDAL
        /// </summary>
        PullOrdersDAL dal = new PullOrdersDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PullOrdersInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PullOrdersInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        /// <summary>
        /// 获取ordersList
        /// </summary>
        /// <param name="SendTime"></param>
        /// <param name="AssemblyLine"></param>
        /// <param name="Fid"></param>
        /// <returns></returns>
        public List<PullOrdersInfo> GetOrdersList(DateTime SendTime, string AssemblyLine, Guid Fid)
        {
            string sql = string.Format(@"select [ORDER_NO] from LES.TT_BAS_PULL_ORDERS  where [VALID_FLAG] = 1 and [PLAN_EXECUTE_TIME] <= N'{0}' and [ASSEMBLY_LINE] = N'{1}' and [ID] in (select [ORDER_ID] from LES.TT_MPM_PLAN_PULL_CREATE_STATUS where [VALID_FLAG] = 1 and [STATUS] = 10 and [PART_BOX_FID] = N'{2}')", SendTime, AssemblyLine, Fid);
            return dal.GetList(sql);
        }
        /// <summary>
        /// 获取IDList
        /// </summary>
        /// <param name="SumTime"></param>
        /// <param name="AssemblyLine"></param>
        /// <param name="Fid"></param>
        /// <returns></returns>
        public List<PullOrdersInfo> GetOrdersIdsList(int SumTime, string AssemblyLine, Guid Fid)
        {
            string sql = string.Format(@"select [ID] from LES.TT_BAS_PULL_ORDERS  where [VALID_FLAG] = 1 and (dateadd(minute,-{0},[ORDER_DATE]) >= [ORDER_DATE] and dateadd(minute,-{0},[ORDER_DATE])<dateadd(day,1,[ORDER_DATE])) and [ASSEMBLY_LINE] = N'{1}' and [ID] in (select [ORDER_ID] from LES.TT_MPM_PLAN_PULL_CREATE_STATUS where [VALID_FLAG] = 1 and [STATUS] = 10 and [PART_BOX_FID] = N'{2}')", SumTime, AssemblyLine, Fid);
            return dal.GetList(sql);
        }
        public List<PullOrdersInfo> GetList(string where, string order)
        {
            return dal.GetList(where, order);
        }

        /// <summary>
        /// GetDate
        /// </summary>
        /// <param name="ZordNo"></param>
        /// <returns></returns>
        public DateTime GetDate(string ZordNo)
        {
            return dal.GetDate(ZordNo);
        }
        /// <summary>
        /// GetInfoByOrderNo
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public PullOrdersInfo GetInfoByOrderNo(string orderNo)
        {
            return dal.GetInfoByOrderNo(orderNo);
        }

        #region Interface
        /// <summary>
        /// Create PullOrdersInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>PullOrdersInfo</returns>
        public static PullOrdersInfo CreatePullOrdersInfo(string loginUser)
        {
            PullOrdersInfo info = new PullOrdersInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,
            info.ValidFlag = true;
            ///CREATE_USER,COMMON_CREATE_USER
            info.CreateUser = loginUser;
            ///CREATE_DATE,COMMON_CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///VERSION,版本号
            info.Version = 1;

            ///MODEL,车型颜色代码描述
            info.Model = null;
            ///VIN,VIN
            info.Vin = null;
            ///VORSERIE,接口_VORSERIE
            info.Vorserie = null;
            ///SPJ,接口_SPJ
            info.Spj = null;
            ///KNR,接口_车辆识别号
            info.Knr = null;
            ///FARBAU,特征包代码
            info.Farbau = null;
            ///FARBIN,特征包代码描述
            info.Farbin = null;
            ///PNR_STRING,选项包代码
            info.PnrString = null;
            ///PNR_STRING_COMPUTE,选项包代码描述
            info.PnrStringCompute = null;
            ///DEAL_FLAG,处理标志
            info.DealFlag = null;
            ///STATUS_FLAG,状态标志
            info.StatusFlag = null;
            ///SIGNATURE,订单签名
            info.Signature = null;
            ///ORDER_FILE_NAME,订单文件名
            info.OrderFileName = null;
            ///ORDER_TYPE,订单类型
            info.OrderType = null;
            ///RECALCULATE_FLAG,重算标志
            info.RecalculateFlag = null;
            ///CHANGE_FLAG,变更标志
            info.ChangeFlag = null;
            ///PROCESS_LINE_SN,工艺路线编号
            info.ProcessLineSn = null;
            ///INIT_STSTUS,A00订单分解状态
            info.InitStstus = null;
            ///ORDER_STATUS,单据状态
            info.OrderStatus = null;
            ///QTY,物料数量
            info.Qty = null;
            ///MEASURING_UNIT,计量单位
            info.MeasuringUnit = null;
            ///ZCOLORI,内饰颜色代码
            info.Zcolori = null;
            ///ZCOLORI_D,内饰颜色描述
            info.ZcoloriD = null;
            ///PLAN_FLAG,计划标记
            info.PlanFlag = null;

            ///COMMENTS,COMMON_备注
            info.Comments = null;
            
            ///MODIFY_USER,COMMON_UPDATE_USER
            info.ModifyUser = null;
            ///MODIFY_DATE,COMMON_UPDATE_DATE
            info.ModifyDate = null;
            return info;
        }
        /// <summary>
        /// SapProductOrderInfo -> PullOrdersInfo
        /// </summary>
        /// <param name="sapProductOrderInfo"></param>
        /// <param name="pullOrdersInfo"></param>
        public static void GetPullOrdersInfo(SapProductOrderInfo sapProductOrderInfo, ref PullOrdersInfo info)
        {
            if (sapProductOrderInfo == null) return;
            ///ORDER_NO,订单号
            info.OrderNo = sapProductOrderInfo.Aufnr;
            ///MODEL_YEAR,车型颜色代码
            info.ModelYear = sapProductOrderInfo.CarColor;
            ///VEHICLE_ORDER,车辆顺序
            info.VehicleOrder = sapProductOrderInfo.OnlineSeq;
            ///PART_NO,物料号
            info.PartNo = sapProductOrderInfo.Matnr;
        }
        public static void GetPullOrdersInfo(ref PullOrdersInfo pullOrdersInfo)
        {

        }
        #endregion

    }
}

