using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DM.SYS;
using Infrustructure.Utilities;
using Infrustructure.Logging;

namespace WS.VMI.InboundDataService
{

    /// <summary>
    /// LES-SRM-006	物料送货单
    /// </summary>
    public class BFDAVmiAsnRunsheetBLL : IBusiness<WmsVmiAsnRunsheetInfo, BFDAVmiAsnRunsheetInfo >
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.VMI.InboundDataService";

        public WmsVmiAsnRunsheetInfo ConversionToCentreInfo(BFDAVmiAsnRunsheetInfo interfaceInfo)
        {
            WmsVmiAsnRunsheetInfo vmiAsnRunsheetInfo = new WmsVmiAsnRunsheetInfo();
            vmiAsnRunsheetInfo.Ordercode = interfaceInfo.OrderCode;//送货单号	 
            vmiAsnRunsheetInfo.Plant = interfaceInfo.Plant;//送货单号	 
            int orderType = 0;
            int.TryParse(interfaceInfo.SourceOrderType, out orderType);
            vmiAsnRunsheetInfo.Sourceordertype = orderType;//原始单据类型 
            

            vmiAsnRunsheetInfo.Dock = interfaceInfo.Dock;//道口nvarchar(8)
            DateTime onDate = default(DateTime);          
            DateTime.TryParse(interfaceInfo.PublishTime, out onDate);
            if (onDate == DateTime.MinValue)
                onDate = onDate.AddYears(1900);

            interfaceInfo.PublishTime = onDate.ToString();   //发单时间	datetime
            vmiAsnRunsheetInfo.Suppliercode = interfaceInfo.SupplierCode;//供应商代码 nvarchar(8)
            vmiAsnRunsheetInfo.Suppliername = interfaceInfo.SupplierName;//供应商名称 nvarchar(64)
            vmiAsnRunsheetInfo.Sourcezoneno = interfaceInfo.SourceZoneNo;//来源存储区代码	nvarchar(8)
            vmiAsnRunsheetInfo.Targetzoneno = interfaceInfo.TargetZoneNo;//目标存储区代码	nvarchar(8)            
            vmiAsnRunsheetInfo.Keeper = interfaceInfo.Keeper;//保管员 nvarchar(8)

            DateTime PlanTime = default(DateTime);            
            DateTime.TryParse(interfaceInfo.PlanShippingTime, out PlanTime);
            if (PlanTime == DateTime.MinValue)
                PlanTime = PlanTime.AddYears(1900);

            vmiAsnRunsheetInfo.Planshippingtime = PlanTime;//预计发货时间	datetime

            DateTime pDatetime = default(DateTime);            
            DateTime.TryParse(interfaceInfo.PlanDeliveryTime, out pDatetime);
            if (pDatetime == DateTime.MinValue)
                pDatetime = pDatetime.AddYears(1900);

            vmiAsnRunsheetInfo.Plandeliverytime = pDatetime; //预计到货时间    datetime           
            vmiAsnRunsheetInfo.Remark = interfaceInfo.Remark;//备注 nvarchar(64)
            vmiAsnRunsheetInfo.Emergencyflag = interfaceInfo.EmergencyFlag== "1" ? true : false; ;//是否紧急 	bit
            vmiAsnRunsheetInfo.Deleteflag = interfaceInfo.DeleteFlag == "1" ? true : false;//删除标记 	bit
            vmiAsnRunsheetInfo.RunsheetDetail = new List<WmsVmiAsnRunsheetDetailInfo>();
            if (interfaceInfo.DetailInfo.Count > 0)
            {
                foreach (BFDAVmiAsnRunsheetDetailInfo item in interfaceInfo.DetailInfo)
                {
                    WmsVmiAsnRunsheetDetailInfo detailinfo = new WmsVmiAsnRunsheetDetailInfo();
                    detailinfo.Sourceordercode = item.SourceOrderCode;
                    detailinfo.Partno = item.PartNo;
                    detailinfo.Partcname = item.PartCName;
                    decimal decQty = default(decimal);
                    decimal.TryParse(item.PartQty, out decQty);
                  
                    detailinfo.Partqty = decQty;
                    
                    detailinfo.Targetslcode = item.TargetSLCode;
                    detailinfo.Packagecode = item.PackageCode;
                    decimal decSnp = default(decimal);
                    decimal.TryParse(item.SNP, out decSnp);                    
                    detailinfo.Snp = decSnp;

                    detailinfo.Wmssourcekey = item.Wmssourcekey;
                    detailinfo.Wmslinenumber = item.Wmslinenumber;
                    detailinfo.Remark = item.Remark;
                    vmiAsnRunsheetInfo.RunsheetDetail.Add(detailinfo);
                }
            }
            return vmiAsnRunsheetInfo;
        }
      
        public List<WmsVmiAsnRunsheetInfo> ConversionToCentreList(List<BFDAVmiAsnRunsheetInfo>  bFDAVmiAsnRunsheetInfos)
        {
            List<WmsVmiAsnRunsheetInfo> list = new List<WmsVmiAsnRunsheetInfo>();
            //List<SrmAsnRunsheetDetailInfo> DetailList = new List<SrmAsnRunsheetDetailInfo>();
            foreach (BFDAVmiAsnRunsheetInfo interfaceInfo in bFDAVmiAsnRunsheetInfos)
            {
                list.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return list;
        }

       

        public string GetKeyValue(BFDAVmiAsnRunsheetInfo interfaceInfo)
        {
            return interfaceInfo.OrderCode;
        }

        public string GetKeyValues(List<BFDAVmiAsnRunsheetInfo>  bFDAVmiAsnRunsheetInfos)
        {
            return string.Join(",", bFDAVmiAsnRunsheetInfos.Select(d => d.OrderCode).ToArray());
        }

        public void InsertInfoToCentreTable(BFDAVmiAsnRunsheetInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }

        public int InsertListToCentreTable(List<BFDAVmiAsnRunsheetInfo>  bFDAVmiAsnRunsheetInfos, Guid logFid, string logSql)
        {
            List<WmsVmiAsnRunsheetInfo> WmsVmiAsnRunsheetInfos = ConversionToCentreList(bFDAVmiAsnRunsheetInfos);
            StringBuilder sqlsb = new StringBuilder(logSql);
            sqlsb.AppendLine();
            foreach (var srmRunsheetinfo in WmsVmiAsnRunsheetInfos)
            {
                sqlsb.AppendFormat("insert into [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET]"
              + "( [FID] ,"                  ///
              + "[LOG_FID] ,"                /// 0
              + "[ORDERCODE],"               /// 1
              + "[SOURCEORDERTYPE] ,"        /// 2
              + "[DOCK] ,"                   /// 3
              + "[PUBLISHTIME] ,"            /// 4
              + "[SUPPLIERCODE] ,"           /// 5
              + "[SUPPLIERNAME] ,"           /// 6
              + "[SOURCEZONENO] ,"           /// 7
              + "[TARGETZONENO] ,"           /// 8
              + "[KEEPER] ,"                 /// 9
              + "[PLANSHIPPINGTIME] ,"       /// 10
              + "[PLANDELIVERYTIME] ,"       /// 11
              + "[REMARK] ,"                 /// 12
              + "[EMERGENCYFLAG] ,"          /// 13
              + "[DELETEFLAG] ,"             /// 14
              + "[PROCESS_FLAG] ,"           /// 15
              + "[PLANT] ,"                  /// 16
              + "[CREATE_USER]  ,"           /// 17
              + "[VALID_FLAG] ,"             /// 18
              + "[PROCESS_TIME] ,"           /// 19
              + "[CREATE_DATE] ) values  ("
                + "NEWID(),'{0}', N'{1}', {2}, N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',1, GETDATE(), GETDATE());",
                logFid,srmRunsheetinfo.Ordercode, srmRunsheetinfo.Sourceordertype, srmRunsheetinfo.Dock,  ///0123
                srmRunsheetinfo.Publishtime, srmRunsheetinfo.Suppliercode, srmRunsheetinfo.Suppliername, srmRunsheetinfo.Sourcezoneno,  ///4 5 6 7
                srmRunsheetinfo.Targetzoneno, srmRunsheetinfo.Keeper, srmRunsheetinfo.Planshippingtime, srmRunsheetinfo.Plandeliverytime, srmRunsheetinfo.Remark, ///8 9 10 11 12
                srmRunsheetinfo.Emergencyflag, srmRunsheetinfo.Deleteflag, (int)ProcessFlagConstants.Untreated, srmRunsheetinfo.Plant, loginUser);  /// 13 14 15 16 17
                sqlsb.AppendLine();
                
                if (srmRunsheetinfo.RunsheetDetail.Count > 0)
                {                    
                    foreach (WmsVmiAsnRunsheetDetailInfo  detailInfo in srmRunsheetinfo.RunsheetDetail)
                    {
                        sqlsb.AppendFormat("insert into [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET_DETAIL]"
                        + "( [FID] ,"
                        + "[ORDER_FID] ,"
                        + "[SOURCEORDERCODE] ,"
                        + "[PARTNO] ,"
                        + "[PARTCNAME] ,"
                        + "[PARTQTY] ,"
                        + "[TARGETSLCODE] ,"
                        + "[PACKAGECODE] ,"
                        + "[SNP] ,"
                        + "[REMARK] ,"
                        + "[WMSSOURCEKEY] ,"
                        + "[WMSLINENUMBER] ,"
                        + "[PROCESS_FLAG] ,"
                        + "[VALID_FLAG] ,"
                        + "[CREATE_USER] ,"
                        + "[PROCESS_TIME] ,"
                        + "[CREATE_DATE] )"
                        + " VALUES  ( NEWID() ,"       /// FID - uniqueidentifier
		                + "N'{0}' ,"       /// LOG_FID - uniqueidentifier
		                + "N'{1}' ,"       /// SOURCEORDERCODE - nvarchar(32)
		                + "N'{2}' ,"       /// PARTNO - nvarchar(32)
		                + "N'{3}' ,"       /// PARTCNAME - nvarchar(128)
		                + "{4} ,"          /// PARTQTY - decimal(18," 4)
		                + "N'{5}' ,"       /// TARGETSLCODE - nvarchar(16)
		                + "N'{6}' ,"       /// PACKAGECODE - nvarchar(16)
		                + "{7} ,"          /// SNP - decimal(18," 4)
		                + "N'{8}' ,"       /// REMARK - nvarchar(64)
                        + "N'{9}' ,"       ///  WMSSOURCEKEY
                        + "N'{10}' ,"      ///  WMSLINENUMBER
		                + "{11} ,"          /// PROCESS_FLAG - int
		                + "{12} ,"         /// VALID_FLAG - bit
		                + "N'{13}' ,"      /// CREATE_USER - nvarchar(32)
		                + "GETDATE() ,"    /// PROCESS_TIME - datetime           
		                + "GETDATE() );"   /// CREATE_DATE - datetime      
                          ,logFid, detailInfo.Sourceordercode, detailInfo.Partno, detailInfo.Partcname, detailInfo.Partqty, 
                          detailInfo.Targetslcode, detailInfo.Packagecode,
                          detailInfo.Snp, detailInfo.Remark,detailInfo.Wmssourcekey, detailInfo.Wmslinenumber, (int)ProcessFlagConstants.Untreated,1, loginUser                           
                          );
                        sqlsb.AppendLine();
                    }
                }
            }
            if (sqlsb.Length > 0)
            {
                Log.WriteLogToFile(sqlsb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\SQL-Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlsb.ToString());
            }
            return WmsVmiAsnRunsheetInfos.Count;
        }
    }
}