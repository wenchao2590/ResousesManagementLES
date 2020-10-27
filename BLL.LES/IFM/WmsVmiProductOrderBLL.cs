using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class WmsVmiProductOrderBLL
    {
        #region Common
        WmsVmiProductOrderDAL dal = new WmsVmiProductOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<WmsVmiProductOrderInfo></returns>
        public List<WmsVmiProductOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public WmsVmiProductOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WmsVmiProductOrderInfo Collection </returns>
		public List<WmsVmiProductOrderInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }

        public long InsertInfo(WmsVmiProductOrderInfo info)
        {
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<WmsVmiProductOrderInfo> wmsVmis = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated, string.Empty);
            if (wmsVmis.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000523");///仅同步状态为待处理时可以更新已取消
            string sql = "update [LES].[TI_IFM_WMS_VMI_PRODUCT_ORDER] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Cancel + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonBLL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 重发
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ResendInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<WmsVmiProductOrderInfo> wmsVmis = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend, string.Empty);
            if (wmsVmis.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000524");///仅同步状态为挂起时可以更新已重发

            //List<SapMaterialReservationInfo> sapMaterialReservations = dal.GetList("[MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "')) and [MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "'))", string.Empty);

            string sql = "update [LES].[TI_IFM_WMS_VMI_PRODUCT_ORDER] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Resend + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonBLL.ExecuteNonQueryBySql(sql);
        }

        #region Interface
        /// <summary>
        /// Create WmsVmiProductOrderInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>WmsVmiProductOrderInfo</returns>
        public static WmsVmiProductOrderInfo CreateWmsVmiProductOrderInfo(string loginUser)
        {
            WmsVmiProductOrderInfo info = new WmsVmiProductOrderInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///LOG_FID,
            info.LogFid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除状态
            info.ValidFlag = true;
            ///CREATE_USER,
            info.CreateUser = loginUser;
            ///CREATE_DATE,
            info.CreateDate = DateTime.Now;
            ///PROCESS_FLAG,处理状态
            info.ProcessFlag = (int)ProcessFlagConstants.Untreated;


            ///ONLINE_TIME,上线时间
            info.OnlineTime = null;
            ///DOWN_LINE_TIME,下线时间
            info.DownLineTime = null;
            ///LOCK_FLAG,锁定标识
            info.LockFlag = null;
            ///PROCESS_TIME,处理时间
            info.ProcessTime = null;
            ///COMMENTS,
            info.Comments = null;
            ///MODIFY_USER,
            info.ModifyUser = null;
            ///MODIFY_DATE,
            info.ModifyDate = null;
            return info;
        }
        /// <summary>
        /// -> WmsVmiProductOrderInfo
        /// </summary>
        /// <param name="wmsVmiProductOrderInfo"></param>
        public static void GetWmsVmiProductOrderInfo(PullOrdersInfo pullOrdersInfo, ref WmsVmiProductOrderInfo info)
        {
            if (pullOrdersInfo == null) return;
            ///ORDER_NO,订单编号
            info.OrderNo = pullOrdersInfo.OrderNo;
            ///PART_NO,物料编号
            info.PartNo = pullOrdersInfo.PartNo;
            ///ORDER_DATE,订单日期
            info.OrderDate = pullOrdersInfo.OrderDate;
            ///ASSEMBLY_LINE,生产线
            info.AssemblyLine = pullOrdersInfo.AssemblyLine;
            ///QTY,数量
            info.Qty = 1;
            ///MODEL_YEAR,整车颜色
            info.ModelYear = pullOrdersInfo.ModelYear;
            ///SEQ,顺序
            info.Seq = Convert.ToInt32(pullOrdersInfo.VehicleOrder);
            ///WERKS,工厂代码
            info.Werks = pullOrdersInfo.Werk;
        }
        /// <summary>
        /// -> WmsVmiProductOrderInfo
        /// </summary>
        /// <param name="wmsVmiProductOrderInfo"></param>
        public static void GetWmsVmiProductOrderInfo(ref WmsVmiProductOrderInfo wmsVmiProductOrderInfo)
        {

        }
        #endregion

    }
}

