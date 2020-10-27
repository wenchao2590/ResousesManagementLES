using BLL.LES;
using DAL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// Sap下发的生产订单接收
    /// </summary>
    public class BFDASapProductOrderBLL : IBusiness<SapProductOrderInfo, BFDASapProductOrderInfo>
    {


        #region Common
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.SAP.InboundDataService";
        /// <summary>
        /// 是否写入日志
        /// </summary>
        string logFlag = ConfigurationManager.AppSettings["LogFlag"].ToLower();
        /// <summary>
        /// log日志名称
        /// </summary>
        string interfaceCode = "SAP-LES-006 SAP生产订单接收";
        #endregion

        /// <summary>
        /// 转换为中间对象
        /// </summary>
        /// <param name="sapProductOrderInfo"></param>
        /// <returns></returns>
        public SapProductOrderInfo ConversionToCentreInfo(BFDASapProductOrderInfo sapProductOrderInfo)
        {
            SapProductOrderInfo productOrderInfo = new SapProductOrderInfo();
            ///1 物料编号
            productOrderInfo.Matnr = sapProductOrderInfo.Matnr;
            ///2 工厂
            productOrderInfo.Dwerk = sapProductOrderInfo.Dwerk;
            ///3 销售订单
            productOrderInfo.Kdauf = sapProductOrderInfo.Kdauf;
            ///4 行项目
            productOrderInfo.Kdpos = sapProductOrderInfo.Kdpos;
            ///5 订单号
            productOrderInfo.Aufnr = sapProductOrderInfo.Aufnr;
            ///6 锁定标识
            productOrderInfo.LockFlag = sapProductOrderInfo.LockFlag == "10" ? false : true;
            ///7 生产版本
            productOrderInfo.Verid = sapProductOrderInfo.Verid;
            ///8 订单数量 
            productOrderInfo.Psmng = Convert.ToInt32(sapProductOrderInfo.Psmng);
            ///9 上线顺序	
            productOrderInfo.OnlineSeq = sapProductOrderInfo.OnlineSeq;
            ///10 上线日期	           
            productOrderInfo.OnlineDate = BFDASapCommonBLL.TryParseDatetime(sapProductOrderInfo.OnlineDate);
            ///11 下线日期	            
            productOrderInfo.OfflineDate = BFDASapCommonBLL.TryParseDatetime(sapProductOrderInfo.OfflineDate);
            ///13 公告编号	
            productOrderInfo.Notice = sapProductOrderInfo.Notice;
            ///14 整车颜色	 
            productOrderInfo.CarColor = sapProductOrderInfo.CarColor;
            ///删除标记
            productOrderInfo.Zsc = sapProductOrderInfo.Zsc;
            ///
            return productOrderInfo;
        }
        /// <summary>
        /// 转换为中间集合
        /// </summary>
        /// <param name="sapProductOrderInfos"></param>
        /// <returns></returns>
        public List<SapProductOrderInfo> ConversionToCentreList(List<BFDASapProductOrderInfo> sapProductOrderInfos)
        {
            List<SapProductOrderInfo> list = new List<SapProductOrderInfo>();
            foreach (BFDASapProductOrderInfo sapProductOrderInfo in sapProductOrderInfos)
            {
                list.Add(ConversionToCentreInfo(sapProductOrderInfo));
            }
            return list;
        }
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="sapProductOrderInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASapProductOrderInfo sapProductOrderInfo)
        {
            ///5 订单号
            return sapProductOrderInfo.Aufnr + "|" + sapProductOrderInfo.Dwerk;
        }
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="sapProductOrderInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASapProductOrderInfo> sapProductOrderInfos)
        {
            ///5 订单号
            return string.Join(",", sapProductOrderInfos.Select(d => d.Aufnr + "|" + d.Dwerk).ToArray());
        }
        /// <summary>
        /// 中间对象添加到数据库
        /// </summary>
        /// <param name="centreInfo"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public void InsertInfoToCentreTable(BFDASapProductOrderInfo sapProductOrderInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 中间集合添加到数据库
        /// </summary>
        /// <param name="sapProductOrderInfos"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASapProductOrderInfo> sapProductOrderInfos, Guid logFid, string logSql)
        {
            List<SapProductOrderInfo> productOrderInfos = ConversionToCentreList(sapProductOrderInfos);
            StringBuilder @string = new StringBuilder(logSql);
            foreach (var productOrderInfo in productOrderInfos)
            {
                ///FID
                productOrderInfo.Fid = Guid.NewGuid();
                ///LOG_FID
                productOrderInfo.LogFid = logFid;
                ///PROCESS_FLAG
                productOrderInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                ///CREATE_USER
                productOrderInfo.CreateUser = loginUser;
                ///
                @string.AppendLine(SapProductOrderDAL.GetInsertSql(productOrderInfo));
            }
            ///分布式事务 执行写入数据库操作. 
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return productOrderInfos.Count;
        }
    }
}