#region Imported Namespace

using DM.LES;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

#endregion

namespace DAL.LES
{
    public partial class SapProductOrderDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public SapProductOrderInfo GetTopOneInfo(string textWhere, string textOrder)
        {
            #region where order 处理
            string query = string.Empty;
            if (string.IsNullOrEmpty(textWhere))
                query = string.Empty;
            else
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    query = textWhere;
                else
                    query = " and " + textWhere;
            }
            if (!string.IsNullOrEmpty(textOrder))
                query += " order by " + textOrder;
            #endregion

            string sql = string.Format(@"select top 1 * from [LES].[TI_IFM_SAP_PRODUCT_ORDER] with(nolock) where [VALID_FLAG] = 1 {0};", query);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateSapProductOrderInfo(dr);
            }
            return null;
        }
        public SapProductOrderInfo GetInfoByRandom()
        {
            string sql = "select top 1 * from [LES].[TI_IFM_SAP_PRODUCT_ORDER] with(nolock) where [VALID_FLAG] = 1 order by newid()";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateSapProductOrderInfo(dr);
            }
            return null;
        }


        #region Interface
        /// <summary>
        /// Create SapProductOrderInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>SapProductOrderInfo</returns>
        public static SapProductOrderInfo CreateSapProductOrderInfo(string loginUser)
        {
            SapProductOrderInfo info = new SapProductOrderInfo();
            ///ID,
            info.Id = 0;
            ///FID,
            info.Fid = Guid.NewGuid();
            ///LOG_FID,日志外键
            info.LogFid = null;
            ///MATNR,物料号
            info.Matnr = null;
            ///DWERK,工厂
            info.Dwerk = null;
            ///KDAUF,销售订单
            info.Kdauf = null;
            ///KDPOS,行项目
            info.Kdpos = null;
            ///AUFNR,订单号
            info.Aufnr = null;
            ///LOCK_FLAG,锁定标识
            info.LockFlag = null;
            ///VERID,生产版本
            info.Verid = null;
            ///PSMNG,订单数量
            info.Psmng = null;
            ///ONLINE_SEQ,上线顺序
            info.OnlineSeq = null;
            ///ONLINE_DATE,上线日期
            info.OnlineDate = null;
            ///OFFLINE_DATE,下线日期
            info.OfflineDate = null;
            ///SEQ,顺序号
            info.Seq = null;
            ///NOTICE,公告颜色
            info.Notice = null;
            ///CAR_COLOR,整车颜色
            info.CarColor = null;
            ///PROCESS_FLAG,处理状态
            info.ProcessFlag = null;
            ///PROCESS_TIME,最后处理时间
            info.ProcessTime = null;
            ///VALID_FLAG,是否删除
            info.ValidFlag = true;
            ///CREATE_USER,
            info.CreateUser = loginUser;
            ///CREATE_DATE,
            info.CreateDate = DateTime.Now;
            ///MODIFY_USER,
            info.ModifyUser = null;
            ///MODIFY_DATE,
            info.ModifyDate = null;
            return info;
        }
        #endregion
    }
}
