using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class BomChangeOrderBLL
    {
        #region Common
        BomChangeOrderDAL dal = new BomChangeOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BomChangeOrderInfo></returns>
        public List<BomChangeOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public BomChangeOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(BomChangeOrderInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="loginUser">用户</param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<BomChangeOrderInfo></returns>
        public List<BomChangeOrderInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion
        /// <summary>
        /// 过渡
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool TransitionInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BomChangeOrderInfo> outputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BreakPointOrderStatusConstants.Completed + " and [ORDER_TYPE] = " + (int)BreakPointOrderTypeConstants.OrderBreakpoint, "[ID]");
            if (outputInfos.Count() != rowsKeyValues.Count())
                throw new Exception("MC:0x00000489");///状态必须为已完成并且更改类型为订单断点才可以进行过度操作
            string sql = "update [LES].[TT_BPM_BOM_CHANGE_ORDER] set [ORDER_TYPE] = " + (int)BreakPointOrderTypeConstants.TransitionBreakpoint + ",[STATUS] = " + (int)BreakPointOrderStatusConstants.Created + " where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BomChangeOrderInfo> outputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BreakPointOrderStatusConstants.Created, "[ID]");
            if (outputInfos.Count() != rowsKeyValues.Count())
                throw new Exception("MC:0x00000683");///	将⑨状态为10.已创建更新为20.已发布
            string sql = "update [LES].[TT_BPM_BOM_CHANGE_ORDER] set [STATUS] = " + (int)BreakPointOrderStatusConstants.Published + " where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #region Interface
        /// <summary>
        /// Create BomChangeOrderInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>BomChangeOrderInfo</returns>
        public static BomChangeOrderInfo CreateBomChangeOrderInfo(string loginUser)
        {
            BomChangeOrderInfo info = new BomChangeOrderInfo();
            ///ID
            info.Id = new long();
            ///FID
            info.Fid = Guid.NewGuid();
            ///ORDER_CODE
            info.OrderCode = null;
            ///PLANT
            info.Plant = null;
            ///WORKSHOP
            info.Workshop = null;
            ///ASSEMBLY_LINE
            info.AssemblyLine = null;
            ///EFFECTIVE_DATE
            info.EffectiveDate = null;
            ///FAILURE_DATE
            info.FailureDate = null;
            ///ORDER_TYPE
            info.OrderType = null;
            ///TRANSITION_START_TIME
            info.TransitionStartTime = null;
            ///STATUS
            info.Status = null;
            ///COMMENTS
            info.Comments = null;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///MODIFY_USER
            info.ModifyUser = null;
            return info;
        }

        #endregion


        /// <summary>
        /// 更改单
        /// </summary>
        /// <param name="pullOrdersInfo"></param>
        public string TransitionBreakpoint(PullOrdersInfo pullOrdersInfo, string loginUser)
        {
            if (pullOrdersInfo.ChangeFlag == (int)ChangeFlagConstants.Replaced) return string.Empty;
            ///根据生产订单号获取其物料清单，作为后续匹配更改单的源数据
            List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomDAL().GetList("and [ORDERFID]='" + pullOrdersInfo.Fid + "'", string.Empty);
            if (pullOrderBomInfos.Count == 0) return string.Empty;
            ///根据生产订单对应的工厂、生产版本（生产线）获取状态为20.已发布且更改类型为20.过渡断点的更改单
            ///TT_BPM_BOM_CHANGE_ORDER 还需要校验过渡开始时间小于等于当前时间
            List<BomChangeOrderInfo> bomChangeOrderInfos = new BomChangeOrderDAL().GetList("" +
                " and [PLANT]=N'" + pullOrdersInfo.Werk + "'" +
                " and [ASSEMBLY_LINE]=N'" + pullOrdersInfo.AssemblyLine + "' " +
                " and [STATUS]=" + (int)BasicDataStatusConstants.Enable + "" +
                " and [ORDER_TYPE]=" + (int)BreakPointOrderTypeConstants.TransitionBreakpoint + "" +
                " and [TRANSITION_START_TIME] <=GETDATE()", string.Empty);
            if (bomChangeOrderInfos.Count == 0) return string.Empty;
            ///过滤其中旧物料库存数量大于等于旧物料已消耗数量的物料数据
            List<BomChangeOrderDetailInfo> bomChangeOrderDetailInfos = new BomChangeOrderDetailDAL().GetList("" +
                "and [ORDER_FID] in ('" + string.Join("','", bomChangeOrderInfos.Select(d => d.Fid).ToArray()) + "')", string.Empty);
            if (bomChangeOrderDetailInfos.Count == 0) return string.Empty;
            bomChangeOrderDetailInfos = bomChangeOrderDetailInfos.Where(d => d.OldPartStockQty >= d.OldPartConsumedQty).ToList();
            if (bomChangeOrderDetailInfos.Count == 0) return string.Empty;
            ///将物料清单与过渡更改单物料根据新物料号、工位过滤数据
            var replacedParts = (from p in pullOrderBomInfos
                                 join b in bomChangeOrderDetailInfos
                                 on new { PartNo = p.Zcomno, Location = p.Zloc } equals new { PartNo = b.NewPartNo, b.Location }
                                 select new
                                 {
                                     p.Id,
                                     p.SupplierNum,
                                     b.NewPartNo,
                                     b.OldPartNo,
                                     b.NewPartQty,
                                     b.Location,
                                     OldPartQty = 1//b.OldPartQty TODO:旧物料
                                 }).ToList();
            ///在更新生产订单物料清单信息（新换旧）
            if (replacedParts.Count == 0) return string.Empty;
            StringBuilder @string = new StringBuilder();
            foreach (var parts in replacedParts)
            {
                @string.AppendFormat("update [LES].[TT_BAS_PULL_ORDER_BOM] set " +
                    "[ZCOMNO]=N'" + parts.OldPartNo + "'," +
                    "[ZQTY]=" + parts.OldPartQty + "," +
                    "[MODIFY_DATE] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "'" +
                    "where [ID]=" + parts.Id + "");
                ///同时生成物料替换记录
                PorderBomRepleaceRecordInfo porderBomRepleaceRecordInfo = new PorderBomRepleaceRecordInfo();
                ///FID
                porderBomRepleaceRecordInfo.Fid = Guid.NewGuid();
                ///VALID_FLAG
                porderBomRepleaceRecordInfo.ValidFlag = true;
                ///CREATE_DATE
                porderBomRepleaceRecordInfo.CreateDate = DateTime.Now;
                ///CREATE_USER
                porderBomRepleaceRecordInfo.CreateUser = loginUser;
                ///生产订单号
                porderBomRepleaceRecordInfo.OrderNo = pullOrdersInfo.OrderNo;
                ///旧物料号
                porderBomRepleaceRecordInfo.OldPartNo = parts.OldPartNo;
                ///新物料号
                porderBomRepleaceRecordInfo.NewPartNo = parts.NewPartNo;
                ///旧供应商
                porderBomRepleaceRecordInfo.OldSupplierNum = parts.SupplierNum;
                ///新供应商
                porderBomRepleaceRecordInfo.NewSupplierNum = parts.SupplierNum;
                ///旧工位
                porderBomRepleaceRecordInfo.OldLocation = parts.Location;
                ///新工位
                porderBomRepleaceRecordInfo.NewLocation = parts.Location;
                ///旧物料用量
                porderBomRepleaceRecordInfo.OldPartQty = parts.OldPartQty;
                ///新物料用量
                porderBomRepleaceRecordInfo.NewPartQty = parts.NewPartQty;
                ///替换时间
                porderBomRepleaceRecordInfo.RepleaceTime = DateTime.Now;
                ///状态 TODO:暂时没有想好干嘛的
                porderBomRepleaceRecordInfo.Status = 10;
                ///同时生成物料替换记录
                @string.AppendFormat(PorderBomRepleaceRecordDAL.GetInsertSql(porderBomRepleaceRecordInfo));
            }
            if (@string.Length > 0)
            {
                ///在本更改单所有物料替换完成时还需生成一条状态为逆处理的生产订单中间表记录
                SapProductOrderInfo sapProductOrderInfo = SapProductOrderDAL.CreateSapProductOrderInfo(loginUser);
                ///订单号
                sapProductOrderInfo.Aufnr = pullOrdersInfo.OrderNo;
                ///处理状态
                sapProductOrderInfo.ProcessFlag = (int)ProcessFlagConstants.ConverseProgress;
                ///版本号
                sapProductOrderInfo.Verid = pullOrdersInfo.Version.ToString();
                @string.AppendFormat(SapProductOrderDAL.GetInsertSql(sapProductOrderInfo));
            }
            return @string.ToString();
        }
    }
}

