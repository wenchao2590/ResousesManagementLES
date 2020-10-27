using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    /// <summary>
    /// StocksBLL
    /// </summary>
    public class StocksBLL
    {
        #region Common
        /// <summary>
        /// StocksDAL
        /// </summary>
        StocksDAL dal = new StocksDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<StocksInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<StocksInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StocksInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(StocksInfo info)
        {
            return dal.Add(info);
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
        /// ���ݸ���������ȡ�ϼƿ��
        /// �������������Ϻ�+��λ+�洢��+�ֿ�
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <param name="dloc"></param>
        /// <returns></returns>
        public decimal GetPartStocks(string partNo, string wmNo, string zoneNo, string dloc)
        {
            return dal.GetPartStocks(partNo, wmNo, zoneNo, dloc);
        }
        /// <summary>
        /// ����������(��ʼ��)
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public StocksInfo CreateStocksInfo(string loginUser)
        {
            StocksInfo stocksInfo = new StocksInfo();

            ///CreateUser
            stocksInfo.CreateUser = loginUser;
            ///CreateDate
            stocksInfo.CreateDate = DateTime.Now;
            ///ValidFlag
            stocksInfo.ValidFlag = true; 
            ///Fid
            //stocksInfo.Fid = Guid.NewGuid();
            return stocksInfo;
        }
        /// <summary>
        /// �������ϻ�������
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="plant"></param>
        /// <param name="stocksInfo"></param>
        public void UpdateMaintainPartsInfo(MaintainPartsInfo maintainPartsInfo, ref StocksInfo stocksInfo)
        {
            if (maintainPartsInfo == null) return;
            ///��ϢԱ
            stocksInfo.Informationer = maintainPartsInfo.InfoPerson;
        }
        /// <summary>
        /// ���ݽ��׼�¼��ȡ��Դ������
        /// TranDetailsInfo=>StocksInfo
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        /// <param name="stocksInfo"></param>
        public void GetSourceStocksInfo(TranDetailsInfo tranDetailsInfo, ref StocksInfo stocksInfo)
        {
            ///����
            stocksInfo.Plant = tranDetailsInfo.Plant;
            ///��Ӧ��
            stocksInfo.SupplierNum = tranDetailsInfo.SupplierNum;
            ///�����
            stocksInfo.PartNo = tranDetailsInfo.PartNo;
            ///���������
            stocksInfo.PartCname = tranDetailsInfo.PartCname;
            ///����س�
            stocksInfo.PartNickname = tranDetailsInfo.PartNickname;
            ///�����λ
            stocksInfo.PartUnits = tranDetailsInfo.MeasuringUnitNo;
            ///��׼��װ�ͺ�
            stocksInfo.PackageModel = tranDetailsInfo.PackageModel;
            ///��׼��װ����
            stocksInfo.Package = Convert.ToInt32( tranDetailsInfo.Package.GetValueOrDefault());
            ///����������
            stocksInfo.ZoneNo = tranDetailsInfo.ZoneNo;
            ///�ֿ����
            stocksInfo.WmNo = tranDetailsInfo.WmNo;
            ///��λ
            stocksInfo.Dloc = tranDetailsInfo.Dloc;
            ///Max
            stocksInfo.Max = tranDetailsInfo.Max;
            ///Min
            stocksInfo.Min = tranDetailsInfo.Min;
            ///���
            stocksInfo.Stocks = tranDetailsInfo.ActualPackageQty;
            ///�Ƿ����ι���
            stocksInfo.IsBatch = tranDetailsInfo.IsBatch;
            ///�����������
            stocksInfo.StocksNum = tranDetailsInfo.ActualQty;
            ///������
            stocksInfo.PartCls = tranDetailsInfo.PartCls;
            ///����
            stocksInfo.BatchNo = tranDetailsInfo.BatchNo;
            ///����
            stocksInfo.BarcodeData = tranDetailsInfo.BarcodeData;
            ///��������
            stocksInfo.BarcodeType = tranDetailsInfo.BarcodeType;
            ///����
            //stocksInfo.OriginPlace = tranDetailsInfo.OriginPlace;
            /////�ɹ����Ͻ��
            //stocksInfo.PurchasePartPrice = tranDetailsInfo.PartPrice;
            /////�������Ͻ��
            //stocksInfo.SalePartPrice = tranDetailsInfo.PartPrice;
            /////�ɱ�����
            //stocksInfo.CostCenter = tranDetailsInfo.CostCenter;
            ///����Ա
            stocksInfo.Keeper = tranDetailsInfo.Keeper;
            ///�Ƿ��ѽ���
            stocksInfo.SettledFlag = tranDetailsInfo.SettledFlag.GetValueOrDefault();
        }

        /// <summary>
        /// ���ݲִ���Ϣ��ȡ������
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="stocksInfo"></param>
        public void GetPartsStocksInfo(PartsStockInfo partsStockInfo, ref StocksInfo stocksInfo)
        {
            ///����
            stocksInfo.Plant = partsStockInfo.Plant;
            ///��Ӧ��
            stocksInfo.SupplierNum = partsStockInfo.SupplierNum;
            ///�����
            stocksInfo.PartNo = partsStockInfo.PartNo;
            ///���������
            stocksInfo.PartCname = partsStockInfo.PartCname;
            ///����س�
            stocksInfo.PartNickname = partsStockInfo.PartNickname;
            ///�����λ
            stocksInfo.PartUnits = partsStockInfo.PartUnits;
            ///��׼��װ�ͺ�
            stocksInfo.PackageModel = partsStockInfo.PackageModel;
            ///��׼��װ����
            stocksInfo.Package = partsStockInfo.Package;
            ///����������
            stocksInfo.ZoneNo = partsStockInfo.ZoneNo;
            ///�ֿ����
            stocksInfo.WmNo = partsStockInfo.WmNo;
            ///��λ
            stocksInfo.Dloc = partsStockInfo.Dloc;
            ///Max
            stocksInfo.Max = partsStockInfo.Max;
            ///Min
            stocksInfo.Min = partsStockInfo.Min;
            ///���
            stocksInfo.Stocks = 0;
            ///�Ƿ����ι���
            stocksInfo.IsBatch = partsStockInfo.IsBatch;
            ///�����������
            stocksInfo.StocksNum = 0;
            ///������
            stocksInfo.PartCls = partsStockInfo.PartCls;
            ///����
            stocksInfo.BatchNo = null;
            ///����
            stocksInfo.BarcodeData = null;
            ///��������
            stocksInfo.BarcodeType = null;
            ///����
            //stocksInfo.OriginPlace = null;
            ///�ɹ����Ͻ��
            //stocksInfo.PurchasePartPrice = null;
            /////�������Ͻ��
            //stocksInfo.SalePartPrice = null;
            /////�ɱ�����
            //stocksInfo.CostCenter = null;
            ///����Ա
            stocksInfo.Keeper = partsStockInfo.Keeper;
            ///�Ƿ��ѽ���
            stocksInfo.SettledFlag = false;
        }
        /// <summary>
        /// ���ݽ��׼�¼��ȡĿ�������
        /// TranDetailsInfo=>StocksInfo
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        /// <param name="stocksInfo"></param>
        public void GetTargetStocksInfo(TranDetailsInfo tranDetailsInfo, ZonesInfo zonesInfo, ref StocksInfo stocksInfo)
        {
            ///����
            stocksInfo.Plant = tranDetailsInfo.Plant;
            ///��Ӧ��
            stocksInfo.SupplierNum = tranDetailsInfo.SupplierNum;
            ///�����
            stocksInfo.PartNo = tranDetailsInfo.PartNo;
            ///���������
            stocksInfo.PartCname = tranDetailsInfo.PartCname;
            ///����س�
            stocksInfo.PartNickname = tranDetailsInfo.PartNickname;
            ///�����λ
            stocksInfo.PartUnits = tranDetailsInfo.MeasuringUnitNo;
            ///��׼��װ�ͺ�
            stocksInfo.PackageModel = tranDetailsInfo.PackageModel;
            ///��׼��װ����
            stocksInfo.Package = Convert.ToInt32( tranDetailsInfo.Package);
            ///����������
            stocksInfo.ZoneNo = tranDetailsInfo.TargetZone;
            ///�ֿ����
            stocksInfo.WmNo = tranDetailsInfo.TargetWm;
            ///��λ
            stocksInfo.Dloc = tranDetailsInfo.TargetDloc;
            ///Max
            stocksInfo.Max = tranDetailsInfo.Max;
            ///Min
            stocksInfo.Min = tranDetailsInfo.Min;
            ///���
            stocksInfo.Stocks = tranDetailsInfo.ActualPackageQty;
            ///�Ƿ����ι���
            stocksInfo.IsBatch = tranDetailsInfo.IsBatch;
            ///�����������
            stocksInfo.StocksNum = tranDetailsInfo.ActualQty;
            ///������
            stocksInfo.PartCls = tranDetailsInfo.PartCls;
            ///����
            stocksInfo.BatchNo = tranDetailsInfo.BatchNo;
            ///����
            stocksInfo.BarcodeData = tranDetailsInfo.BarcodeData;
            ///��������
            stocksInfo.BarcodeType = tranDetailsInfo.BarcodeType;
            ///����
            //stocksInfo.OriginPlace = tranDetailsInfo.OriginPlace;
            /////�ɹ����Ͻ��
            //stocksInfo.PurchasePartPrice = tranDetailsInfo.PartPrice;
            /////�������Ͻ��
            //stocksInfo.SalePartPrice = tranDetailsInfo.PartPrice;
            /////�ɱ�����
            //stocksInfo.CostCenter = tranDetailsInfo.CostCenter;
            ///����Ա
            stocksInfo.Keeper = tranDetailsInfo.Keeper;
            ///�Ƿ��ѽ���
            stocksInfo.SettledFlag = tranDetailsInfo.SettledFlag.GetValueOrDefault();
            if (zonesInfo == null) return;
            //if (zonesInfo.SettlementFlag.GetValueOrDefault() && !tranDetailsInfo.SettledFlag.GetValueOrDefault())
            //    stocksInfo.SettledFlag = true;
        }
        /// <summary>
        /// ɢװ������������
        /// </summary>
        /// <param name="stocksInfo"></param>
        public void StockCalculation(ref StocksInfo stocksInfo)
        {
            if (stocksInfo.Stocks.GetValueOrDefault() == 0)
                stocksInfo.FragmentNum = stocksInfo.StocksNum;
            else
            {
                stocksInfo.FragmentNum///ɢװ��������
                    = stocksInfo.Stocks.GetValueOrDefault()///��װ��
                    * (stocksInfo.Package.GetValueOrDefault() == 0 ? 1 : stocksInfo.Package.GetValueOrDefault())///����װ��
                    - stocksInfo.StocksNum.GetValueOrDefault();///��������
            }
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public long GetStocksId(StocksInfo stocksInfo, string textWhere)
        {
            long id = dal.GetStocksId(textWhere);
            if (id == 0) id = dal.InitStockInfo(stocksInfo);
            return id;
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public long GetStocksId(Guid fid)
        {
            return dal.GetStocksId(fid);
        }

        #region ������
        /// <summary>
        /// ���ӿ��ÿ��
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string StocksRaiseSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[STOCKS] = isnull([STOCKS],0) + " + stocksInfo.Stocks.GetValueOrDefault() + "," +
                "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) + " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[STOCKS_NUM] = isnull([STOCKS_NUM],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        /// <summary>
        /// ���ٿ��ÿ��
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string StocksReduceSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[STOCKS] = isnull([STOCKS],0) - " + stocksInfo.Stocks.GetValueOrDefault() + "," +
                "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) - " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[STOCKS_NUM] = isnull([STOCKS_NUM],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        /// <summary>
        /// ���Ӷ�����
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string FrozenRaiseSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[STOCKS] = isnull([STOCKS],0) + " + stocksInfo.Stocks.GetValueOrDefault() + "," +
                "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) + " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[STOCKS_NUM] = isnull([STOCKS_NUM],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        /// <summary>
        /// ���ٶ�����
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string FrozenReduceSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[STOCKS] = isnull([STOCKS],0) - " + stocksInfo.Stocks.GetValueOrDefault() + "," +
                "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) - " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[STOCKS_NUM] = isnull([STOCKS_NUM],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        #endregion

        /// <summary>
        /// ��ȡ��潻������״̬�������
        /// </summary>
        /// <param name="tranDetailsId"></param>
        /// <param name="tranState"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetTranDetailsStatusUpdateSql(long tranDetailsId, int tranState, string loginUser)
        {
            return "update [LES].[TT_WMM_TRAN_DETAILS] set " +
                "[TRAN_STATE] = " + (int)WmmTranStateConstants.Done + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + tranDetailsId + ";";
        }

        /// <summary>
        /// ��ⶳ��
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksFrozen(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return dal.StocksFrozen(stocksInfo, tranDetailsId, loginUser);
        }

        /// <summary>
        /// ����ⶳ
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksThaw(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return dal.StocksThaw(stocksInfo, tranDetailsId, loginUser);
        }
        public string StocksThawSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] "
                + "set [PACKAGE] = [PACKAGE] - " + stocksInfo.Package.GetValueOrDefault() + ""
                + ",[FROZEN_STOCKS] = [FROZEN_STOCKS] - " + stocksInfo.FrozenStocks.GetValueOrDefault() + ""
                + ",[FRAGMENT_NUM] = [FRAGMENT_NUM] - " + stocksInfo.FragmentNum.GetValueOrDefault() + " "
                + ",[STOCKS_NUM] = [STOCKS_NUM] - " + stocksInfo.StocksNum.GetValueOrDefault() + " "
                //+ ",[SALE_PART_PRICE] = [SALE_PART_PRICE] -" + stocksInfo.SalePartPrice.GetValueOrDefault() + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] =" + stocksInfo.Id + ";"
               + "update [LES].[TT_WMM_TRAN_DETAILS] set " +
               "[TRAN_STATE] = " + (int)WmmTranStateConstants.Done + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] = " + tranDetailsId + ";";
        }
        /// <summary>
        /// ԭ�ⶳ��
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksStatusFrozen(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return dal.StocksStatusFrozen(stocksInfo, tranDetailsId, loginUser);
        }
        /// <summary>
        /// ״̬����
        /// ԭ�ص�Ŀ��ñ�ɶ���
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string StocksStateFreezingSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) - " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] =" + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        /// <summary>
        /// ԭ��ⶳ
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksStatusThaw(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return dal.StocksStatusThaw(stocksInfo, tranDetailsId, loginUser);
        }
        public string StocksStatusThawSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] "
                + ",[AVAILBLE_STOCKS] = [AVAILBLE_STOCKS] + " + stocksInfo.AvailbleStocks.GetValueOrDefault() + ""
                + ",[FROZEN_STOCKS] = [FROZEN_STOCKS] - " + stocksInfo.FrozenStocks.GetValueOrDefault() + ""
                + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] =" + stocksInfo.Id + ";"
               + "update [LES].[TT_WMM_TRAN_DETAILS] "
               + "set [TRAN_STATE] = " + (int)WmmTranStateConstants.Done + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] = " + tranDetailsId + ";";
        }

        #region �������
        /// <summary>
        /// �ṩ���������������Ͻ����Ų飨�����������������������ʱ�������
        /// </summary>
        /// <param name="productOrderNo"></param>
        /// <param name="assemblyLine"></param>
        /// <param name="loginUser"></param>
        /// <param name="forcePassFlag"></param>
        /// <returns></returns>
        public bool CheckMaterialStock(string productOrderNo, string assemblyLine, string loginUser, bool forcePassFlag = true)
        {
            ///ȱ����ʶ��Ĭ��Ϊ��ȱ��
            bool lackFlag = false;
            ///ִ�����
            StringBuilder stringBuilder = new StringBuilder();
            ///���ݲ������������Ż�ȡ��ȡ��������
            PullOrdersInfo pullOrdersInfo = new PullOrdersDAL().GetInfoByOrderNo(productOrderNo);
            if (pullOrdersInfo == null)
                throw new Exception("MC:0x00000447");///�����������ݴ���

            ///���ݲ������������Ż�ȡTT_BAS_PULL_ORDER_BOM�е������嵥��Ϊ�˼ӿ��ȡ���ݵ�Ч�������ȡ�����߼�������ֶ�����           
            List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomDAL().GetPartList(productOrderNo);
            if (pullOrderBomInfos.Count == 0)
                throw new Exception("MC:0x00000448");///�������������嵥���ݴ���

            ///���飬����ͬһ���������µ������嵥�����Ϻ�+��Ӧ�̻��ж�������
            var pullBoms = pullOrderBomInfos.GroupBy(p => new { p.Zcomno, p.SupplierNum }).Select(p => new { p.Key.Zcomno, p.Key.SupplierNum, Zqty = p.Sum(x => x.Zqty.GetValueOrDefault()) }).ToList();
            ///���ݲ��������߻�ȡ����������Ϣ�������߶�Ӧ����Ϣ�������Ƿ�������Ͽ���Ų��ǽ������ݹ���
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetCheckMaterialStockList(assemblyLine, pullBoms.Select(d => d.Zcomno).ToList());
            ///û����Ҫ��������ʱֱ�ӷ���true
            if (maintainInhouseLogisticStandardInfos.Count == 0)
                return true;
            ///������������Ϣ�������嵥ƥ�������Ϻš���Ӧ�̣�����ƥ��õ����ݼ��϶�������������Ϣ�е���Դ�洢���ĸù�Ӧ�����Ͻ��п��ÿ����
            var query = (from p in pullBoms
                         join m in maintainInhouseLogisticStandardInfos
                         on new { PartNo = p.Zcomno, p.SupplierNum } equals new { m.PartNo, m.SupplierNum }
                         select new { PartNo = p.Zcomno, p.SupplierNum, p.Zqty, m.SZoneNo, m.SWmNo }).ToList();
            ///δ������Ҫ��������ʱ������true
            if (query.Count == 0) return true;
            ///��Ӧ����Ϣ
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetList("[SUPPLIER_NUM] in ('" + string.Join("','", query.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
            ///��ȡ�������
            ///TODO:���ٻ�ȡ���ֶ������Ч��
            List<StocksInfo> stocksInfos = dal.GetList("" +
                "[PART_NO] in ('" + string.Join("','", query.Select(d => d.PartNo).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", query.Select(d => d.SWmNo).ToArray()) + "') and " +
                "[SUPPLIER_NUM] in ('" + string.Join("','", query.Select(d => d.SupplierNum).ToArray()) + "') and " +
                "[ZONE_NO] in ('" + string.Join("','", query.Select(d => d.SZoneNo).ToArray()) + "')", string.Empty);

            ///���ϲִ���Ϣ TM_BAS_PARTS_STOCK
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", stocksInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[SUPPLIER_NUM] in ('" + string.Join("','", stocksInfos.Select(d => d.SupplierNum).ToArray()) + "') and " +
                "[ZONE_NO] in ('" + string.Join("','", stocksInfos.Select(d => d.ZoneNo).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", stocksInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);

            ///�ظ�ǰ��Ҫ�����ÿ���������嵥�������������пۼ�������������ۼ�
            List<StocksInfo> lockStocksInfos = new List<StocksInfo>();
            ///ȱ����������
            PorderLackMaterialInfo porderLackMaterialInfo = new PorderLackMaterialDAL().GetInfo(productOrderNo);
            if (porderLackMaterialInfo == null)
            {
                porderLackMaterialInfo = new PorderLackMaterialInfo();
                porderLackMaterialInfo.CreateUser = loginUser;
                porderLackMaterialInfo.Fid = Guid.NewGuid();
                porderLackMaterialInfo.ValidFlag = true;
                porderLackMaterialInfo.CreateDate = DateTime.Now;
                porderLackMaterialInfo.ProductionOrderFid = pullOrdersInfo.Fid;
                porderLackMaterialInfo.ProductionOrderNo = pullOrdersInfo.OrderNo;
                porderLackMaterialInfo.Plant = pullOrdersInfo.Werk;
                porderLackMaterialInfo.AssemblyLine = pullOrdersInfo.AssemblyLine;
                porderLackMaterialInfo.LackFlag = false;
                porderLackMaterialInfo.OrderDate = pullOrdersInfo.OrderDate;
                porderLackMaterialInfo.Status = null;///TODO:״̬�ֶε�����?
                porderLackMaterialInfo.Id = new PorderLackMaterialDAL().Add(porderLackMaterialInfo);
                if (porderLackMaterialInfo.Id == 0)
                    throw new Exception("MC:0x00000450");///ȱ��������������ʧ��
            }
            ///
            foreach (var q in query)
            {
                ///��Ӧ�Ŀ���¼
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d => d.PartNo == q.PartNo && d.SupplierNum == q.SupplierNum && d.WmNo == q.SWmNo && d.ZoneNo == q.SZoneNo);
                if (partsStockInfo == null)
                    throw new Exception("MC:0x00000451");///���ϲִ���Ϣ����
                List<StocksInfo> stocks = stocksInfos.Where(d => d.PartNo == q.PartNo && d.SupplierNum == q.SupplierNum && d.WmNo == q.SWmNo && d.ZoneNo == q.SZoneNo).ToList();
                ///���ô洢���Ŀ�淢�ֶ���������������������������Ȳ����ѽ���Ŀ������
                ///���߼���Ҫ�������������ϲִ���Ϣ��<�Ƿ�����ʹ���ѽ�������>
                decimal partRequireQty = q.Zqty;
                //if (partsStockInfo.PriorityUseFlag.GetValueOrDefault())
                //    stocks = stocks.OrderByDescending(d => d.SettledFlag.GetValueOrDefault()).ToList();
                //else
                //    stocks = stocks.OrderBy(d => d.SettledFlag.GetValueOrDefault()).ToList();
                ///���һ���ۼ����
                StocksInfo lastStock = new StocksInfo();
                while (partRequireQty > 0)
                {
                    StocksInfo stock = stocks.FirstOrDefault();
                    ///��һ�β�ѯ ��û�п��
                    if (stock == null)
                    {
                        if (lastStock == null)
                        {
                            lastStock = CreateStocksInfo(loginUser);
                            ///��Ӧ��
                            lastStock.SupplierNum = q.SupplierNum;
                            ///�����
                            lastStock.PartNo = q.PartNo;
                            ///����������
                            lastStock.ZoneNo = q.SZoneNo;
                            ///�ֿ����
                            lastStock.WmNo = q.SWmNo;
                            ///id
                            lastStock.Id = GetStocksId(lastStock, string.Empty);
                        }
                        ///MatchedQty
                        lastStock.MatchedQty = partRequireQty;
                        lockStocksInfos.Add(lastStock);
                        break;
                    }
                    ///����
                    if (partRequireQty <= stock.AvailbleStocks.GetValueOrDefault())
                    {
                        stock.MatchedQty = partRequireQty;
                        lockStocksInfos.Add(stock);
                        stocks.Remove(stock);
                        partRequireQty = 0;
                        break;
                    }
                    ///������
                    if (partRequireQty > stock.AvailbleStocks.GetValueOrDefault())
                    {
                        stock.MatchedQty = stock.AvailbleStocks.GetValueOrDefault();
                        lockStocksInfos.Add(stock);
                        stocks.Remove(stock);
                        partRequireQty -= stock.AvailbleStocks.GetValueOrDefault();
                        lastStock = stock;
                    }
                }
                ///�����������
                if (partRequireQty > 0)
                {
                    PorderLackMaterialDetailInfo porderLackMaterialDetailInfo = new PorderLackMaterialDetailInfo();
                    porderLackMaterialDetailInfo.CreateUser = loginUser;
                    porderLackMaterialDetailInfo.LackPorderFid = porderLackMaterialInfo.Fid;
                    porderLackMaterialDetailInfo.ProductionOrderFid = pullOrdersInfo.Fid;
                    porderLackMaterialDetailInfo.PartNo = q.PartNo;
                    porderLackMaterialDetailInfo.SupplierNum = q.SupplierNum;
                    porderLackMaterialDetailInfo.Plant = pullOrdersInfo.Werk;
                    porderLackMaterialDetailInfo.RequireQty = q.Zqty;
                    porderLackMaterialDetailInfo.LackQty = partRequireQty;
                    porderLackMaterialDetailInfo.PartPurchaser = string.Empty;
                    porderLackMaterialDetailInfo.ProductionOrderNo = pullOrdersInfo.OrderNo;
                    porderLackMaterialDetailInfo.AssemblyLine = pullOrdersInfo.AssemblyLine;
                    porderLackMaterialDetailInfo.OrderDate = pullOrdersInfo.OrderDate;
                    stringBuilder.AppendLine(PorderLackMaterialDetailDAL.GetInsertSql(porderLackMaterialDetailInfo));
                    ///���ȱ��
                    lackFlag = true;
                }
            }
            ///ȱ��
            if (lackFlag)
                stringBuilder.AppendLine("update [LES].[TT_ATP_PORDER_LACK_MATERIAL] set " +
                    "[LACK_FLAG] = 1," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = " + porderLackMaterialInfo.Id + ";");

            ///ȱ���ҷ�ǿ��
            if (lackFlag && !forcePassFlag)
            {
                ///�����ȫ����������ʱ�����ظ�true
                using (var trans = new TransactionScope())
                {
                    if (stringBuilder.Length > 0)
                        CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
                    trans.Complete();
                }
                return false;
            }
            ///�������ݴ���������Ҫ�ⲿϵͳ�ӿ�ͬ�����ã����μ����ʱ������2����
            ///���Ͽ�涳������ԵĲֿ�洢��ΪVMI���ͣ�����Ҫͬʱ����VMI�Ŀ�������м������
            ///������������׼�¼

            ///ǿ��or��ȱ��
            if (forcePassFlag || !lackFlag)
            {
                ///ʵ�ִ��߼���Ҫ��ϵͳ���������ӿ���STOCK_CHECK_LOCK_MATERIAL_SYNC_UPDATE_PART_STOCK_FLAG,����Ų����������Ƿ�ͬ�����¿��, Ĭ��Ϊtrue
                string stockCheckLockFlag = new ConfigDAL().GetValueByCode("STOCK_CHECK_LOCK_MATERIAL_SYNC_UPDATE_PART_STOCK_FLAG");
                ///�ֿ�
                List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetListForInterfaceDataSync(stocksInfos.Select(d => d.WmNo).ToList());
                List<WmsVmiPartStockLockInfo> wmsVmiPartStockLockInfos = new List<WmsVmiPartStockLockInfo>();
                foreach (StocksInfo lockStocksInfo in lockStocksInfos)
                {
                    SupplierInfo supplierInfo = supplierInfos.Where(d => d.SupplierNum == lockStocksInfo.SupplierNum).FirstOrDefault();
                    WarehouseInfo warehouseInfo = warehouseInfos.Where(d => d.Warehouse == lockStocksInfo.WmNo).FirstOrDefault();
                    int tranState = (int)WmmTranStateConstants.Created;




                    ///�������
                    if (!string.IsNullOrEmpty(stockCheckLockFlag) && stockCheckLockFlag.ToLower() == "true")
                    {
                        tranState = (int)WmmTranStateConstants.Done;
                        stringBuilder.AppendFormat("update [LES].[TT_WMM_STOCKS] set " +
                            "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) - " + lockStocksInfo.MatchedQty.GetValueOrDefault() + "," +
                            "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) + " + lockStocksInfo.MatchedQty.GetValueOrDefault() + "," +
                            "[PRIOR_USE_FLAG] = " + (lockStocksInfo.AvailbleStocks.GetValueOrDefault() == lockStocksInfo.MatchedQty.GetValueOrDefault() ? "0" : "[PRIOR_USE_FLAG]") + "," +
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() where " +
                            "[ID] =" + lockStocksInfo.Id + ";");
                    }

                    ///�Ƿ�����WMSϵͳ���
                    string enable_vmi_flag = new ConfigDAL().GetValueByCode("ENABLE_VMI_FLAG");

                    ///��������м��
                    if (!warehouseInfo.VmiEnable.GetValueOrDefault() && !string.IsNullOrEmpty(enable_vmi_flag) && enable_vmi_flag.ToLower() == "true")
                    {

                        WmsVmiPartStockLockInfo wmsVmiPartStockLockInfo = new WmsVmiPartStockLockInfo();
                        wmsVmiPartStockLockInfo.Werks = lockStocksInfo.Plant;
                        wmsVmiPartStockLockInfo.Partno = lockStocksInfo.PartNo;
                        wmsVmiPartStockLockInfo.Suppliercode = lockStocksInfo.SupplierNum;
                        wmsVmiPartStockLockInfo.Vmiwarehousecode = lockStocksInfo.ZoneNo;
                        wmsVmiPartStockLockInfo.Partqty = lockStocksInfo.MatchedQty;
                        wmsVmiPartStockLockInfo.Orilockstatus = "FXZ";
                        wmsVmiPartStockLockInfo.Targetlockstatus = "XZ";
                        wmsVmiPartStockLockInfo.Invstatus = "SC";
                        wmsVmiPartStockLockInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                        wmsVmiPartStockLockInfo.CreateUser = loginUser;
                        wmsVmiPartStockLockInfos.Add(wmsVmiPartStockLockInfo);
                    }
                    ///��������
                    TranDetailsInfo tranDetailsInfo = TranDetailsBLL.CreateTranDetailsInfo((int)WmmTranTypeConstants.StateFreezing, tranState, loginUser);
                    ///�Կ��������佻�׼�¼
                    TranDetailsBLL.GetTranDetailsInfo2(lockStocksInfo, ref tranDetailsInfo);
                    ///�Թ�Ӧ����Ϣ��佻�׼�¼
                    TranDetailsBLL.GetTranDetailsInfo(supplierInfo, ref tranDetailsInfo);
                    ///����ʱ��
                    tranDetailsInfo.TranDate = DateTime.Now;
                    ///���ױ��
                    tranDetailsInfo.TranNo = productOrderNo;
                    ///��װ����
                    tranDetailsInfo.ActualPackageQty = Convert.ToInt32(Math.Ceiling(lockStocksInfo.MatchedQty.GetValueOrDefault() / lockStocksInfo.Package.GetValueOrDefault()));
                    ///��������
                    tranDetailsInfo.ActualQty = lockStocksInfo.MatchedQty;
                    ///
                    stringBuilder.AppendLine(TranDetailsDAL.GetInsertSql(tranDetailsInfo));
                }
                if (wmsVmiPartStockLockInfos.Count > 0)
                {
                    Guid logFid = Guid.NewGuid();
                    string targetSystem = "VMI";
                    string methodCode = "LES-WMS-010";
                    string keyValue = productOrderNo;
                    stringBuilder.AppendLine(CommonBLL.GetCreateOutboundLogSql(targetSystem, logFid, methodCode, keyValue, loginUser));
                    foreach (var wmsVmiPartStockLockInfo in wmsVmiPartStockLockInfos)
                    {
                        wmsVmiPartStockLockInfo.LogFid = logFid;
                        stringBuilder.AppendLine(WmsVmiPartStockLockDAL.GetInsertSql(wmsVmiPartStockLockInfo));
                    }
                }
            }
            ///�����ȫ����������ʱ�����ظ�true
            using (var trans = new TransactionScope())
            {
                if (stringBuilder.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
                trans.Complete();
            }
            return true;
        }

        #endregion

        /// <summary>
        /// �������У��
        /// </summary>
        public List<StocksInfo> GetValidStocksInfo(List<StocksInfo> stocksInfos, List<BarcodeInfo> barcodeInfos,
            string partNo, string wmNo, string zoneNo, string dloc, decimal partQty, int wmmTranType,
            string supplierNum, string plant, string packageModel, string partCls, string originPlace, string costCenter, bool? settledFlag)
        {
            #region ��ȡ���ά��
            bool supplierFlag = false;
            bool plantFlag = false;
            bool packageModelFlag = false;
            bool partClsFlag = false;
            bool batchFlag = false;
            bool barcodeFlag = false;
            bool originPlaceFlag = false;
            bool costCenterFlag = false;
            bool settlementFlag = false;
            GetStockDimensionFlag(
                ref supplierFlag,
                ref plantFlag,
                ref packageModelFlag,
                ref partClsFlag,
                ref batchFlag,
                ref barcodeFlag,
                ref originPlaceFlag,
                ref costCenterFlag,
                ref settlementFlag);
            #endregion
            ///�����ϡ��ֿ⡢�洢������λ���й���
            List<StocksInfo> stocks = new List<StocksInfo>();
            ///��û��ָ����λ����Ҫ���˿�λ���ڴ˺�������ʱ�Ŀ����������Ӧ�Ŀ�λ��Ϊ���γ���Ŀ�λ
            if (string.IsNullOrEmpty(dloc))
                stocks = stocksInfos.Where(d => d.PartNo == partNo && d.WmNo == wmNo && d.ZoneNo == zoneNo).ToList();
            else
                stocks = stocksInfos.Where(d => d.PartNo == partNo && d.WmNo == wmNo && d.ZoneNo == zoneNo && d.Dloc == dloc).ToList();
            ///��Ӧ��
            if (supplierFlag && !string.IsNullOrEmpty(supplierNum)) stocks = stocks.Where(d => d.SupplierNum == supplierNum).ToList();
            ///����
            if (plantFlag && !string.IsNullOrEmpty(plant)) stocks = stocks.Where(d => d.Plant == plant).ToList();
            ///��װ�ͺţ�TODO:����ʱ?
            if (packageModelFlag && !string.IsNullOrEmpty(packageModel)) stocks = stocks.Where(d => d.PackageModel == packageModel).ToList();
            ///���Ϸ���
            if (partClsFlag && !string.IsNullOrEmpty(partCls)) stocks = stocks.Where(d => d.PartCls == partCls).ToList();
            ///����,TODO:��Ҫ��ϵͳ�����Ƽ�
            if (batchFlag) stocks = stocks.Where(d => d.BatchNo == string.Empty).ToList();
            ///����
            if (barcodeFlag) stocks = stocks.Where(d => barcodeInfos.Select(b => b.BarcodeData).Contains(d.BarcodeData)).ToList();
            ///����
            //if (originPlaceFlag && !string.IsNullOrEmpty(originPlace)) stocks = stocks.Where(d => d.OriginPlace == originPlace).ToList();
            ///�ɱ�����
            //if (costCenterFlag && !string.IsNullOrEmpty(costCenter)) stocks = stocks.Where(d => d.CostCenter == costCenter).ToList();
            ///������
            ///settledFlagΪnullʱ��ʶ���β�������ע�Ƿ����
            if (settlementFlag && settledFlag != null) stocks = stocks.Where(d => d.SettledFlag == settledFlag).ToList();
            ///
            if (stocks.Count == 0) return null;
            ///���ض�����Ч�Ŀ������
            List<StocksInfo> list = new List<StocksInfo>();
            ///TODO:���ݹ���stocks��������
            stocks = stocks.OrderByDescending(d => d.SettledFlag.GetValueOrDefault()).ToList();
            ///
            while (partQty > 0)
            {
                ///����ж��������¼�ٴ�ȷ���߼������һ���ļ�¼�Ƿ�����
                StocksInfo info = stocks.FirstOrDefault();
                if (info == null) break;
                ///TODO:�������������㣬�ϼ���������ʱ������������ݣ�
                ///���⡢���϶��ᡢ״̬����
                StocksInfo sInfo = info.Clone();
                switch (wmmTranType)
                {
                    case (int)WmmTranTypeConstants.Outbound:
                    case (int)WmmTranTypeConstants.Movement:
                    case (int)WmmTranTypeConstants.MaterialFreezing:
                    case (int)WmmTranTypeConstants.StateFreezing:
                        if (info.AvailbleStocks.GetValueOrDefault() >= partQty)
                        {
                            sInfo.MatchedQty = partQty;
                            list.Add(sInfo);
                            partQty = 0;
                            break;
                        }
                        sInfo.MatchedQty = info.AvailbleStocks.GetValueOrDefault();
                        list.Add(sInfo);
                        partQty -= info.AvailbleStocks.GetValueOrDefault();
                        break;
                    case (int)WmmTranTypeConstants.MaterialThawing:
                    case (int)WmmTranTypeConstants.FrozenMovement:
                    case (int)WmmTranTypeConstants.StateThawing:
                    case (int)WmmTranTypeConstants.FrozenOutbound:
                        if (info.FrozenStocks.GetValueOrDefault() >= partQty)
                        {
                            sInfo.MatchedQty = partQty;
                            list.Add(sInfo);
                            partQty = 0;
                            break;
                        }
                        sInfo.MatchedQty = info.FrozenStocks.GetValueOrDefault();
                        list.Add(sInfo);
                        partQty -= info.FrozenStocks.GetValueOrDefault();
                        break;
                }
                stocks.Remove(info);
            }
            ///���������޷�����
            if (partQty > 0) return new List<StocksInfo>();
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stocksInfos"></param>
        /// <param name="outputDetailInfo"></param>
        /// <param name="batchNo"></param>
        /// <param name="settledFlag"></param>
        /// <param name="barcodeInfos"></param>
        /// <returns></returns>
        public List<StocksInfo> GetValidStocksInfo(OutputDetailInfo outputDetailInfo, int wmmTranType,
            List<StocksInfo> stocksInfos, List<OutputDetailInfo> outputDetailInfos, List<BarcodeInfo> barcodeInfos,
            bool? settledFlag)
        {
            #region ��ȡ���ά��
            bool supplierFlag = false;
            bool plantFlag = false;///
            bool packageModelFlag = false;
            bool partClsFlag = false;///
            bool batchFlag = false;
            bool barcodeFlag = false;
            bool originPlaceFlag = false;
            bool costCenterFlag = false;
            bool settlementFlag = false;
            GetStockDimensionFlag(
                ref supplierFlag,
                ref plantFlag,
                ref packageModelFlag,
                ref partClsFlag,
                ref batchFlag,
                ref barcodeFlag,
                ref originPlaceFlag,
                ref costCenterFlag,
                ref settlementFlag);
            #endregion

            List<OutputDetailInfo> outputDetails = outputDetailInfos;
            ///��Ӧ��
            if (supplierFlag) outputDetails = outputDetails.Where(d => d.SupplierNum == outputDetailInfo.SupplierNum).ToList();
            ///����
            if (plantFlag) outputDetails = outputDetails.Where(d => d.Plant == outputDetailInfo.Plant).ToList();
            ///��װ�ͺţ�TODO:����ʱ?
            if (packageModelFlag) outputDetails = outputDetails.Where(d => d.PackageModel == outputDetailInfo.PackageModel).ToList();
            ///���Ϸ���
            if (partClsFlag) outputDetails = outputDetails.Where(d => d.PartCls == outputDetailInfo.PartCls).ToList();
            ///����
            if (originPlaceFlag) outputDetails = outputDetails.Where(d => d.OriginPlace == outputDetailInfo.OriginPlace).ToList();
            ///�ɱ�����
            if (costCenterFlag) outputDetails = outputDetails.Where(d => d.CostCenter == outputDetailInfo.CostCenter).ToList();
            ///�ϼ���������
            decimal partQty = outputDetails.Sum(d => d.RequiredQty.GetValueOrDefault());

            return GetValidStocksInfo(
                stocksInfos,
                barcodeInfos,
                outputDetailInfo.PartNo,
                outputDetailInfo.WmNo,
                outputDetailInfo.ZoneNo,
                outputDetailInfo.Dloc,
                partQty,
                wmmTranType,
                outputDetailInfo.SupplierNum,
                outputDetailInfo.Plant,
                outputDetailInfo.PackageModel,
                outputDetailInfo.PartCls,
                outputDetailInfo.OriginPlace,
                outputDetailInfo.CostCenter,
                settledFlag);
        }

        /// <summary>
        /// ��ʼ�����ά�ȱ��
        /// </summary>
        /// <param name="supplierFlag">��湩Ӧ��ά�ȱ��</param>
        /// <param name="plantFlag">��湤��ά�ȱ��</param>
        /// <param name="packageModelFlag">����װ�ͺ�ά�ȱ��</param>
        /// <param name="partClsFlag">����������ά�ȱ��</param>
        /// <param name="batchFlag">�������ά�ȱ��</param>
        /// <param name="barcodeFlag">����ǩά�ȱ��</param>
        /// <param name="originPlaceFlag">������ά�ȱ��</param>
        /// <param name="costCenterFlag">���ɱ�����ά�ȱ��</param>
        /// <param name="settlementFlag">������ά�ȱ��</param>
        public void GetStockDimensionFlag(ref bool supplierFlag
            , ref bool plantFlag
            , ref bool packageModelFlag
            , ref bool partClsFlag
            , ref bool batchFlag
            , ref bool barcodeFlag
            , ref bool originPlaceFlag
            , ref bool costCenterFlag
            , ref bool settlementFlag)
        {
            Dictionary<string, string> keyValues = new ConfigDAL().GetValuesByCodes(new string[] {
                "SUPPLIER_STOCKS_DIMENSION",
                "PLANT_STOCKS_DIMENSION",
                "PACKAGE_MODEL_STOCKS_DIMENSION",
                "PART_CLS_STOCKS_DIMENSION",
                "BATCH_STOCKS_DIMENSION",
                "BARCODE_STOCKS_DIMENSION",
                "ORIGIN_PLACE_STOCKS_DIMENSION",
                "COST_CENTER_STOCKS_DIMENSION",
                "SETTLEMENT_STOCKS_DIMENSION" });
            ///��湩Ӧ��ά�ȱ��
            keyValues.TryGetValue("SUPPLIER_STOCKS_DIMENSION", out string supplierStocksDimension);
            bool.TryParse(supplierStocksDimension, out supplierFlag);
            ///��湤��ά�ȱ��
            keyValues.TryGetValue("PLANT_STOCKS_DIMENSION", out string plantStocksDimension);
            bool.TryParse(plantStocksDimension, out plantFlag);
            ///����װ�ͺ�ά�ȱ��
            keyValues.TryGetValue("PACKAGE_MODEL_STOCKS_DIMENSION", out string packageModelStocksDimension);
            bool.TryParse(packageModelStocksDimension, out packageModelFlag);
            ///����������ά�ȱ��
            keyValues.TryGetValue("PART_CLS_STOCKS_DIMENSION", out string partClsStocksDimension);
            bool.TryParse(partClsStocksDimension, out partClsFlag);
            ///�������ά�ȱ��
            keyValues.TryGetValue("BATCH_STOCKS_DIMENSION", out string batchStocksDimension);
            bool.TryParse(batchStocksDimension, out batchFlag);
            ///����ǩά�ȱ��
            keyValues.TryGetValue("BARCODE_STOCKS_DIMENSION", out string barcodeStocksDimension);
            bool.TryParse(barcodeStocksDimension, out barcodeFlag);
            ///������ά�ȱ��
            keyValues.TryGetValue("ORIGIN_PLACE_STOCKS_DIMENSION", out string originPlaceStocksDimension);
            bool.TryParse(originPlaceStocksDimension, out originPlaceFlag);
            ///���ɱ�����ά�ȱ��
            keyValues.TryGetValue("COST_CENTER_STOCKS_DIMENSION", out string costCenterStocksDimension);
            bool.TryParse(costCenterStocksDimension, out costCenterFlag);
            ///������ά�ȱ��
            keyValues.TryGetValue("SETTLEMENT_STOCKS_DIMENSION", out string settlementStocksDimension);
            bool.TryParse(settlementStocksDimension, out settlementFlag);
        }
        /// <summary>
        /// ��ȡ���ÿ��
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <param name="supplierNum"></param>
        /// <returns></returns>
        public decimal GetAvailbleQty(string partNo, string wmNo, string zoneNo, string supplierNum = "")
        {
            return dal.GetAvailbleQty(partNo, wmNo, zoneNo, supplierNum);
        }


        /// <summary>
        /// ����ʹ��
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool PrioruseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<StocksInfo> stocksInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (stocksInfos.Count == 0)
                throw new Exception("MC:0x00000084");///���ݴ���
            StringBuilder @string = new StringBuilder();
            foreach (var stocksInfo in stocksInfos)
            {
                if (!stocksInfo.SettledFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000525");///���ѽ������ϲ��ܱ��Ϊ����ʹ��
                //if (stocksInfo.PriorUseFlag.GetValueOrDefault()) continue;
                int cnt = dal.GetCounts("" +
                    "[ID] <> " + stocksInfo.Id + " and " +
                    "[PRIOR_USE_FLAG] = 1 and " +
                    "[PART_NO] = N'" + stocksInfo.PartNo + "' and " +
                    "[SUPPLIER_NUM] = N'" + stocksInfo.SupplierNum + "'");
                if (cnt > 0)
                    throw new Exception("MC:0x00000526");///�ѽ�������ֻ���ڵ�һ�洢���ڱ����Ϊ����ʹ��
                @string.AppendLine("update [LES].[TT_WMM_STOCKS] " +
                    "set [PRIOR_USE_FLAG] = 1," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + stocksInfo.Id + ";");
            }

            return true;
        }
    }
}

