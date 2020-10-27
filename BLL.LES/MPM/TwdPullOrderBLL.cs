using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public partial class TwdPullOrderBLL
    {
        #region Common
        TwdPullOrderDAL dal = new TwdPullOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<TwdPullOrderInfo></returns>
        public List<TwdPullOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public TwdPullOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(TwdPullOrderInfo info)
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
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<TwdPullOrderInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        #endregion

        #region 打印获取数据源方法
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            //根据预设看板拉动单格式进行打印，格式等待业务部门提供
            //打印成功后记录最后打印时间⑯、最后打印用户⑰、累计打印次数⑮
            List<TwdPullOrderInfo> list = new TwdPullOrderDAL().GetList(string.Format("[ID] IN ({0})", string.Join(",", rowsKeyValues.ToArray())), string.Empty);
            if (list.Count == 0)
                throw new Exception("MC:0x00000072");//没有打印文件生成
            string sql = "select c.[ITEM_NAME] as ORDER_TYPE,r.*"
                + "from LES.TT_MPM_TWD_PULL_ORDER r with(nolock) "
                + "left join TS_SYS_CODE_ITEM c with(nolock) on c.[ITEM_VALUE] = r.[ORDER_TYPE] and c.[CODE_FID] = N'4afa543d-4455-4e54-868e-f36474e21cf6' and c.[VALID_FLAG] = 1 "
                + "where r.[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  and r.[VALID_FLAG] = 1;"
                + "select * from LES.TT_MPM_TWD_PULL_ORDER_DETAIL with(nolock) "
                + "where [ORDER_FID] in (select [FID] from LES.TT_MPM_TWD_PULL_ORDER with(nolock) "
                + "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  and [VALID_FLAG] = 1 ) and [VALID_FLAG] = 1;";

            return DAL.SYS.CommonDAL.ExecuteDataSetBySql(sql);
        }
        public void GetPrintCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = string.Empty;
            DataTable dt = DAL.SYS.CommonDAL.ExecuteDataTableBySql("select * from [TS_SYS_PRINT_CONFIG] where [VALID_FLAG] = 1 and [PRINT_CONFIG_CODE] = 'BFDA_TWD_PULL_ORDER'");
            sql += "update [LES].[TT_MPM_TWD_PULL_ORDER] set [LAST_PRINT_DATE] =  GETDATE(),[PRINT_TIMES] =isnull([PRINT_TIMES],0)+" + dt.Rows[0]["PRINT_COPIES"] + ",[LAST_PRINT_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues) + ")";
            DAL.SYS.CommonDAL.ExecuteScalar(sql);
        }
        #endregion

        #region Interface
        /// <summary>
        /// Create TwdPullOrderInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>TwdPullOrderInfo</returns>
        public static TwdPullOrderInfo CreateTwdPullOrderInfo(string loginUser)
        {
            TwdPullOrderInfo info = new TwdPullOrderInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = true;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;
            ///CREATE_USER,创建用户
            info.CreateUser = loginUser;
            ///ORDER_TYPE,单据类型(10.正常拉动单、20.紧急拉动单)
            info.OrderType = (int)PullOrderTypeConstants.Pulling;
            ///ORDER_STATUS,拉动单状态
            info.OrderStatus = (int)PullOrderStatusConstants.Released;
            ///ORDER_CODE,拉动单号
            info.OrderCode = new SeqDefineDAL().GetCurrentCode("TWD_PULL_ORDER_CODE");
            ///PUBLISH_TIME,发布时间
            info.PublishTime = DateTime.Now;
            ///KEEPER,保管员
            info.Keeper = null;
            return info;
        }
        /// <summary>
        /// TwdPartBoxInfo -> TwdPullOrderInfo
        /// </summary>
        /// <param name="twdPartBoxInfo"></param>
        /// <param name="info"></param>
        public static void GetTwdPullOrderInfo(TwdPartBoxInfo twdPartBoxInfo, ref TwdPullOrderInfo info)
        {
            if (twdPartBoxInfo == null) return;
            ///PART_BOX_CODE,零件类代码
            info.PartBoxCode = twdPartBoxInfo.PartBoxCode;
            ///PART_BOX_NAME,零件类名称
            info.PartBoxName = twdPartBoxInfo.PartBoxName;
            ///PLANT,工厂代码
            info.Plant = twdPartBoxInfo.Plant;
            ///WORKSHOP,车间代码
            info.Workshop = twdPartBoxInfo.Workshop;
            ///ASSEMBLY_LINE,生产线代码
            info.AssemblyLine = twdPartBoxInfo.AssemblyLine;
            ///ROUTE_CODE,物流路径
            info.RouteCode = twdPartBoxInfo.RouteCode;
            ///SUPPLIER_NUM,供应商代码
            info.SupplierNum = twdPartBoxInfo.SupplierNum;
            ///S_ZONE_NO,来源存储区
            info.SZoneNo = twdPartBoxInfo.SZoneNo;
            ///S_WM_NO,来源仓库
            info.SWmNo = twdPartBoxInfo.SWmNo;
            ///T_ZONE_NO,目标存储区
            info.TZoneNo = twdPartBoxInfo.TZoneNo;
            ///T_WM_NO,目标仓库
            info.TWmNo = twdPartBoxInfo.TWmNo;
            ///DOCK,道口代码
            info.Dock = twdPartBoxInfo.Dock;
            ///PLAN_SHIPPING_TIME,预计发货时间
            info.PlanShippingTime = info.PlanDeliveryTime.GetValueOrDefault().AddMinutes(0 -
                twdPartBoxInfo.TransportTime.GetValueOrDefault() -
                twdPartBoxInfo.LoadTime.GetValueOrDefault());
        }
        /// <summary>
        /// SupplierInfo -> TwdPullOrderInfo
        /// </summary>
        /// <param name="supplierInfo"></param>
        /// <param name="info"></param>
        public static void GetTwdPullOrderInfo(SupplierInfo supplierInfo, ref TwdPullOrderInfo info)
        {
            if (supplierInfo == null) return;
            ///ASN_FLAG,是否允许编辑ASN
            if (supplierInfo.SupplierType.GetValueOrDefault() == (int)SupplierTypeConstants.MaterialSupplier)
                info.AsnFlag = supplierInfo.AsnFlag;
        }
        /// <summary>
        /// TwdWindowTimeInfo -> TwdPullOrderInfo
        /// </summary>
        /// <param name="twdWindowTimeInfo"></param>
        /// <param name="info"></param>
        public static void GetTwdPullOrderInfo(TwdWindowTimeInfo twdWindowTimeInfo, ref TwdPullOrderInfo info)
        {
            if (twdWindowTimeInfo == null) return;
            ///PLAN_DELIVERY_TIME,预计到厂时间
            info.PlanDeliveryTime = twdWindowTimeInfo.WindowTime;
            ///TIME_ZONE,时区
            info.TimeZone = twdWindowTimeInfo.TimeZone;
        }
        public static void GetTwdPullOrderInfo(ref TwdPullOrderInfo info)
        {

        }
        #endregion

    }
}

