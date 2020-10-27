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
        /// ��ȡordersList
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
        /// ��ȡIDList
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
            ///VERSION,�汾��
            info.Version = 1;

            ///MODEL,������ɫ��������
            info.Model = null;
            ///VIN,VIN
            info.Vin = null;
            ///VORSERIE,�ӿ�_VORSERIE
            info.Vorserie = null;
            ///SPJ,�ӿ�_SPJ
            info.Spj = null;
            ///KNR,�ӿ�_����ʶ���
            info.Knr = null;
            ///FARBAU,����������
            info.Farbau = null;
            ///FARBIN,��������������
            info.Farbin = null;
            ///PNR_STRING,ѡ�������
            info.PnrString = null;
            ///PNR_STRING_COMPUTE,ѡ�����������
            info.PnrStringCompute = null;
            ///DEAL_FLAG,�����־
            info.DealFlag = null;
            ///STATUS_FLAG,״̬��־
            info.StatusFlag = null;
            ///SIGNATURE,����ǩ��
            info.Signature = null;
            ///ORDER_FILE_NAME,�����ļ���
            info.OrderFileName = null;
            ///ORDER_TYPE,��������
            info.OrderType = null;
            ///RECALCULATE_FLAG,�����־
            info.RecalculateFlag = null;
            ///CHANGE_FLAG,�����־
            info.ChangeFlag = null;
            ///PROCESS_LINE_SN,����·�߱��
            info.ProcessLineSn = null;
            ///INIT_STSTUS,A00�����ֽ�״̬
            info.InitStstus = null;
            ///ORDER_STATUS,����״̬
            info.OrderStatus = null;
            ///QTY,��������
            info.Qty = null;
            ///MEASURING_UNIT,������λ
            info.MeasuringUnit = null;
            ///ZCOLORI,������ɫ����
            info.Zcolori = null;
            ///ZCOLORI_D,������ɫ����
            info.ZcoloriD = null;
            ///PLAN_FLAG,�ƻ����
            info.PlanFlag = null;

            ///COMMENTS,COMMON_��ע
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
            ///ORDER_NO,������
            info.OrderNo = sapProductOrderInfo.Aufnr;
            ///MODEL_YEAR,������ɫ����
            info.ModelYear = sapProductOrderInfo.CarColor;
            ///VEHICLE_ORDER,����˳��
            info.VehicleOrder = sapProductOrderInfo.OnlineSeq;
            ///PART_NO,���Ϻ�
            info.PartNo = sapProductOrderInfo.Matnr;
        }
        public static void GetPullOrdersInfo(ref PullOrdersInfo pullOrdersInfo)
        {

        }
        #endregion

    }
}

