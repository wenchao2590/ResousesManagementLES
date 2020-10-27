using BLL.LES;
using BLL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace TEST.CreateSapProductOrder
{
    class Handle
    {
        public void Handler()
        {
            //随机抽取数据
            SapProductOrderInfo sapProductOrderInfo = new SapProductOrderBLL().GetInfoByRandom();
            if (sapProductOrderInfo == null)
                return;

            StringBuilder stringBuilder = new StringBuilder();
            SapProductOrderBomInfo sapProductOrderBomInfo = new SapProductOrderBomBLL().GetInfoByAufnr(sapProductOrderInfo.Aufnr);
            if (sapProductOrderBomInfo == null)
                return;
            //随机生成单号
            sapProductOrderInfo.Aufnr = new SeqDefineBLL().GetCurrentCode("SAP_PRODUCT_ORDER_NO");
            if (new SapProductOrderBLL().GetCounts("and [AUFNR] = N'" + sapProductOrderInfo.Aufnr + "'") > 0)
                return;
            //生成新数据
            Guid guid = Guid.NewGuid();
            //时间处理
            Random random = new Random();
            int intDays = (int)((0 - 9) * random.NextDouble() + 9) - 1;
            sapProductOrderInfo.OnlineDate = DateTime.Now.AddDays(intDays).Date;
            //插入主表
            stringBuilder.AppendFormat("insert into [LES].[TI_IFM_SAP_PRODUCT_ORDER]([FID],[MATNR],[DWERK],[KDAUF]," +
                "[KDPOS],[AUFNR],[LOCK_FLAG],[VERID],[PSMNG],[ONLINE_SEQ],[ONLINE_DATE],[OFFLINE_DATE],[SEQ],[NOTICE]," +
                "[CAR_COLOR],[PROCESS_FLAG],[PROCESS_TIME],[VALID_FLAG],[CREATE_USER],[CREATE_DATE]) " +
                "values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',10,'{15}',1,'{16}',getdate())",
                guid, sapProductOrderInfo.Matnr, sapProductOrderInfo.Dwerk, sapProductOrderInfo.Kdauf, sapProductOrderInfo.Kdpos, sapProductOrderInfo.Aufnr, sapProductOrderInfo.LockFlag.GetValueOrDefault() == false ? 0 : 1, sapProductOrderInfo.Verid,
                sapProductOrderInfo.Psmng, sapProductOrderInfo.OnlineSeq, sapProductOrderInfo.OnlineDate, sapProductOrderInfo.OfflineDate, sapProductOrderInfo.Seq, sapProductOrderInfo.Notice,
                sapProductOrderInfo.CarColor, sapProductOrderInfo.ProcessTime, sapProductOrderInfo.CreateUser);
            //插入明细表
            stringBuilder.AppendFormat("insert into [LES].[TI_IFM_SAP_PRODUCT_ORDER_BOM]([FID],[FMATNR],[DWERK],[AUFNR],[VERID],[ONLINE_DATE]," +
                "[OFFLINE_DATE],[MATNRS],[PROCESS_FLAG],[PROCESS_TIME],[VALID_FLAG],[CREATE_USER],[CREATE_DATE]) " +
                "values(newid(),'{0}','{1}','{2}','{3}','{5}','{6}','{7}',10,getdate(),1,'{8}',getdate())", sapProductOrderBomInfo.Fmatnr, sapProductOrderBomInfo.Dwerk, sapProductOrderInfo.Aufnr,
                sapProductOrderBomInfo.Verid, "", sapProductOrderInfo.OnlineDate, sapProductOrderBomInfo.OfflineDate, sapProductOrderBomInfo.Matnrs,
                sapProductOrderBomInfo.CreateUser);

            #region 执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (!BLL.LES.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString()))
                    throw new Exception("0x00000004");///TODO:写入数据库失败
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":"
                    + sapProductOrderInfo.Aufnr + "|"
                    + sapProductOrderInfo.OnlineDate.GetValueOrDefault().ToString("yyyy-MM-dd") + "|"
                    + sapProductOrderInfo.Verid);
                trans.Complete();
            }
            #endregion
        }
    }
}
