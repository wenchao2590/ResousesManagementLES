using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.SYS;

namespace BLL.LES
{
    public class BomRepleaceConditionBLL
    {
        #region Common
        BomRepleaceConditionDAL dal = new BomRepleaceConditionDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BomRepleaceConditionInfo></returns>
        public List<BomRepleaceConditionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public BomRepleaceConditionInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(BomRepleaceConditionInfo info)
        {
            info.ConditionCode = new SeqDefineDAL().GetCurrentCode("CONDITION_CODE");
            info.Status = (int)BreakPointOrderStatusConstants.Created;

            if ((info.NewPartQty != null && info.OldPartQty == null) || (info.NewPartQty == null && info.OldPartQty != null))
                throw new Exception("MC:0x00000492");///新旧物料用量必须同时填写或者同时不填

            if ((string.IsNullOrEmpty(info.NewPartNo) && !string.IsNullOrEmpty(info.OldPartNo)) || (!string.IsNullOrEmpty(info.NewPartNo) && string.IsNullOrEmpty(info.OldPartNo)))
                throw new Exception("MC:0x00000493");///新旧物料号必须同时填写或者同时不填

            if ((string.IsNullOrEmpty(info.NewSupplierNum) && !string.IsNullOrEmpty(info.OldSupplierNum)) || (!string.IsNullOrEmpty(info.NewSupplierNum) && string.IsNullOrEmpty(info.OldSupplierNum)))
                throw new Exception("MC:0x00000494");///新旧供应商必须同时填写或者同时不填

            if ((string.IsNullOrEmpty(info.NewLocation) && !string.IsNullOrEmpty(info.OldLocation)) || (!string.IsNullOrEmpty(info.NewLocation) && string.IsNullOrEmpty(info.OldLocation)))
                throw new Exception("MC:0x00000491");///新旧工位必须同时填写或者同时不填

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
            BomRepleaceConditionInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误
            if (info.Status != (int)BreakPointOrderStatusConstants.Created)
                throw new Exception("MC:0x00000415");///已创建状态才可进行删除
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            BomRepleaceConditionInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误
            if (info.Status != (int)BreakPointOrderStatusConstants.Created)
                throw new Exception("MC:0x00000084");///已创建状态才可进行修改

            string newLocation = CommonBLL.GetFieldValue(fields, "NEW_LOCATION");
            string oldLocation = CommonBLL.GetFieldValue(fields, "OLD_LOCATION");
            if ((string.IsNullOrEmpty(newLocation) && !string.IsNullOrEmpty(oldLocation)) || (!string.IsNullOrEmpty(newLocation) && string.IsNullOrEmpty(oldLocation)))
                throw new Exception("MC:0x00000491");///新旧工位必须同时填写或者同时不填

            string newPartQty = CommonBLL.GetFieldValue(fields, "NEW_PART_QTY");
            string oldPartQty = CommonBLL.GetFieldValue(fields, "OLD_PART_QTY");
            if ((newPartQty != "null" && oldPartQty == "null") || (newPartQty == "null" && oldPartQty != "null"))
                throw new Exception("MC:0x00000492");///新旧物料用量必须同时填写或者同时不填

            string newPartNo = CommonBLL.GetFieldValue(fields, "NEW_PART_NO");
            string oldPartNo = CommonBLL.GetFieldValue(fields, "OLD_PART_NO");
            if ((string.IsNullOrEmpty(newPartNo) && !string.IsNullOrEmpty(oldPartNo)) || (!string.IsNullOrEmpty(newPartNo) && string.IsNullOrEmpty(oldPartNo)))
                throw new Exception("MC:0x00000493");///新旧物料号必须同时填写或者同时不填

            string newSupplierNum = CommonBLL.GetFieldValue(fields, "NEW_SUPPLIER_NUM");
            string oldSupplierNum = CommonBLL.GetFieldValue(fields, "OLD_SUPPLIER_NUM");
            if ((string.IsNullOrEmpty(newSupplierNum) && !string.IsNullOrEmpty(oldSupplierNum)) || (!string.IsNullOrEmpty(newSupplierNum) && string.IsNullOrEmpty(oldSupplierNum)))
                throw new Exception("MC:0x00000494");///新旧供应商必须同时填写或者同时不填

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<BomRepleaceConditionInfo></returns>
        public List<BomRepleaceConditionInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        /// <summary>
        /// 发布按钮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BomRepleaceConditionInfo> conditionInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BreakPointOrderStatusConstants.Created, "[ID]");
            if (conditionInfos.Count() != rowsKeyValues.Count())
                throw new Exception("MC:0x00000495");///已创建状态才可进行发布
            string sql = "update [LES].[TT_BPM_BOM_REPLEACE_CONDITION] WITH(ROWLOCK) set [STATUS] = " + (int)BreakPointOrderStatusConstants.Published + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BomRepleaceConditionInfo> conditionInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BreakPointOrderStatusConstants.Published, string.Empty);
            if (conditionInfos.Count() != rowsKeyValues.Count())
                throw new Exception("MC:0x00000400");///已创建状态才可进行发布

            string sql = "update [LES].[TT_BPM_BOM_REPLEACE_CONDITION] WITH(ROWLOCK) set [STATUS] = " + (int)BreakPointOrderStatusConstants.Invalid + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }

        #region Interface
        /// <summary>
        /// Create BomRepleaceConditionInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>BomRepleaceConditionInfo</returns>
        public static BomRepleaceConditionInfo CreateBomRepleaceConditionInfo(string loginUser)
        {
            BomRepleaceConditionInfo info = new BomRepleaceConditionInfo();
            ///ID
            info.Id = new long();
            ///FID
            info.Fid = Guid.NewGuid();
            ///CONDITION_CODE
            info.ConditionCode = null;
            ///OLD_PART_NO
            info.OldPartNo = null;
            ///NEW_PART_NO
            info.NewPartNo = null;
            ///OLD_SUPPLIER_NUM
            info.OldSupplierNum = null;
            ///NEW_SUPPLIER_NUM
            info.NewSupplierNum = null;
            ///OLD_LOCATION
            info.OldLocation = null;
            ///NEW_LOCATION
            info.NewLocation = null;
            ///OLD_PART_VERSION
            info.OldPartVersion = null;
            ///NEW_PART_VERSION
            info.NewPartVersion = null;
            ///OLD_PART_QTY
            info.OldPartQty = null;
            ///NEW_PART_QTY
            info.NewPartQty = null;
            ///START_PORDER_CODE
            info.StartPorderCode = null;
            ///PORDER_START_TIME
            info.PorderStartTime = null;
            ///PORDER_END_TIME
            info.PorderEndTime = null;
            ///EXECUTE_START_TIME
            info.ExecuteStartTime = null;
            ///EXECUTE_END_TIME
            info.ExecuteEndTime = null;
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
        /// 替换条件
        /// </summary>
        /// <param name="pullOrdersInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string ReplacementCriteria(PullOrdersInfo pullOrdersInfo, string loginUser)
        {
            ///车辆过点时根据替换条件更新物料清单中的物料号、供应商、工位、用量信息，同时产生替换记录
            if (pullOrdersInfo.ChangeFlag == (int)ChangeFlagConstants.Replaced) return string.Empty;
            ///根据生产订单号获取其物料清单，作为后续匹配更改单的源数据
            List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomDAL().GetList("" +
                "and [ORDERFID]='" + pullOrdersInfo.Fid + "'", string.Empty);
            if (pullOrderBomInfos.Count == 0) return string.Empty;
            ///TT_BPM_BOM_REPLEACE_CONDITION 替换条件信息
            List<BomRepleaceConditionInfo> bomRepleaceConditionInfos = new BomRepleaceConditionDAL().GetList("" +
                " and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "" +
                " and [OLD_PART_NO] in ('" + string.Join("','", pullOrderBomInfos.Select(d => d.Zcomno).ToArray()) + "')" +
                " and GETDATE() between [EXECUTE_START_TIME] and [EXECUTE_END_TIME]", string.Empty);
            if (bomRepleaceConditionInfos.Count == 0) return string.Empty;

            ///车型信息 TT_BPM_BOM_REPLEACE_CONDITION_VEHICLE
            List<BomRepleaceConditionVehicleInfo> bomRepleaceConditionVehicleInfos = new BomRepleaceConditionVehicleBLL().GetList("" +
                " and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "" +
                " and [CONDITION_FID] in ('" + string.Join("','", bomRepleaceConditionInfos.Select(d => d.Fid).ToArray()) + "')" +
                " and [PART_NO]=N'"+ pullOrdersInfo.PartNo+ "'" +
                " and [MODEL_YEAR]=N'"+ pullOrdersInfo.ModelYear+ "'" +
                " and [FARBAU]=N'"+ pullOrdersInfo.Farbau+ "'" +
                " and [PNR_STRING]=N'"+ pullOrdersInfo.PnrString+ "'" +
                " and [ZCOLORI]=N'"+ pullOrdersInfo.Zcolori + "'" , string.Empty);
            StringBuilder @string = new StringBuilder();
            foreach (BomRepleaceConditionInfo bomRepleaceConditionInfo in bomRepleaceConditionInfos)
            {
                ///物料号
                List<PullOrderBomInfo> pullOrderBoms = pullOrderBomInfos.Where(d => d.Zcomno == bomRepleaceConditionInfo.OldPartNo).ToList();
                if (pullOrderBoms.Count == 0) continue;
                ///工位
                if (!string.IsNullOrEmpty(bomRepleaceConditionInfo.OldLocation))
                    pullOrderBoms = pullOrderBoms.Where(d => d.Zloc == bomRepleaceConditionInfo.OldLocation).ToList();
                ///供应商
                if (!string.IsNullOrEmpty(bomRepleaceConditionInfo.OldSupplierNum))
                    pullOrderBoms = pullOrderBoms.Where(d => d.SupplierNum == bomRepleaceConditionInfo.OldSupplierNum).ToList();
                if (pullOrderBoms.Count == 0) continue;
                ///车型
                BomRepleaceConditionVehicleInfo bomRepleaceConditionVehicleInfo = bomRepleaceConditionVehicleInfos.FirstOrDefault(d=>d.ConditionFid== bomRepleaceConditionInfo.Fid);
                foreach (PullOrderBomInfo pullOrderBom in pullOrderBoms)
                {
                    ///更新物料清单
                    @string.AppendFormat("update [LES].[TT_BAS_PULL_ORDER_BOM] set " +
                    "[ZCOMNO]=N'" + bomRepleaceConditionInfo.NewPartNo + "',");
                    if (bomRepleaceConditionInfo.OldPartQty.GetValueOrDefault()!=0)
                        @string.AppendFormat("[ZQTY]=" + bomRepleaceConditionInfo.NewPartQty.GetValueOrDefault() + ",");
                    if(!string.IsNullOrEmpty(bomRepleaceConditionInfo.OldLocation))
                        @string.AppendFormat("[ZLOC]=N'" + bomRepleaceConditionInfo.NewLocation + "',");
                    if (!string.IsNullOrEmpty(bomRepleaceConditionInfo.OldSupplierNum))
                        @string.AppendFormat("[SUPPLIER_NUM]=N'" + bomRepleaceConditionInfo.NewSupplierNum + "',");
                    @string.AppendFormat("[MODIFY_DATE] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "'" +
                    "where [ID]=" + pullOrderBom.Id + "");
                    ///旧物料多的情况
                    if(pullOrderBom.Zqty> bomRepleaceConditionInfo.NewPartQty)
                    {
                        PullOrderBomInfo info = new PullOrderBomInfo();
                        ///FID,
                        info.Fid = Guid.NewGuid();
                        ///ORDERFID,订单外键
                        info.Orderfid = pullOrdersInfo.Fid;
                        ///ZORDNO,订单号
                        info.Zordno = pullOrdersInfo.OrderNo;
                        ///ZKWERK,工厂
                        info.Zkwerk = pullOrderBom.Zkwerk;
                        ///ZBOMID,MBOM项目号
                        info.Zbomid = pullOrderBom.Zbomid;
                        ///ZCOMNO,零件号
                        info.Zcomno = bomRepleaceConditionInfo.NewPartNo;
                        ///ZCOMDS,零件描述
                        info.Zcomds = pullOrderBom.Zcomds;
                        ///ZVIN,ZVIN
                        info.Zvin = pullOrderBom.Zvin;
                        ///ZQTY,数量
                        info.Zqty =pullOrderBom.Zqty.GetValueOrDefault() -Convert.ToInt32(bomRepleaceConditionInfo.NewPartQty.GetValueOrDefault());
                        ///ZDATE,计划下线日期
                        info.Zdate = pullOrderBom.Zdate;
                        ///ZLOC,工位
                        info.Zloc = bomRepleaceConditionInfo.OldLocation == null ? pullOrderBom.Zloc : bomRepleaceConditionInfo.NewLocation;
                        ///ZST,操作状态
                        info.Zst = pullOrderBom.Zst;
                        ///ZMEMO,备注
                        info.Zmemo = pullOrderBom.Zmemo;
                        ///ZMEINS,单位
                        info.Zmeins = pullOrderBom.Zmeins;
                        ///SUPPLIER_NUM,供应商
                        info.SupplierNum = bomRepleaceConditionInfo.OldSupplierNum == null ? pullOrderBom.SupplierNum : bomRepleaceConditionInfo.NewSupplierNum;
                        ///PLATFORM,平台
                        info.Platform = pullOrderBom.Platform;
                        ///VALID_FLAG,
                        info.ValidFlag = true;
                        ///CREATE_USER,COMMON_CREATE_USER
                        info.CreateUser = loginUser;
                        ///CREATE_DATE,COMMON_CREATE_DATE
                        info.CreateDate = DateTime.Now;
                        ///MODIFY_USER,COMMON_UPDATE_USER
                        info.ModifyUser = null;
                        ///MODIFY_DATE,COMMON_UPDATE_DATE
                        info.ModifyDate = null;
                        @string.AppendFormat(PullOrderBomDAL.GetInsertSql(info));
                    }
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
                    porderBomRepleaceRecordInfo.OldPartNo = bomRepleaceConditionInfo.OldPartNo;
                    ///新物料号
                    porderBomRepleaceRecordInfo.NewPartNo = bomRepleaceConditionInfo.NewPartNo;
                    ///旧供应商
                    porderBomRepleaceRecordInfo.OldSupplierNum = bomRepleaceConditionInfo.OldSupplierNum;
                    ///新供应商
                    porderBomRepleaceRecordInfo.NewSupplierNum = bomRepleaceConditionInfo.NewSupplierNum;
                    ///旧工位
                    porderBomRepleaceRecordInfo.OldLocation = bomRepleaceConditionInfo.OldLocation;
                    ///新工位
                    porderBomRepleaceRecordInfo.NewLocation = bomRepleaceConditionInfo.NewLocation;
                    ///旧物料版本
                    porderBomRepleaceRecordInfo.OldPartVersion = bomRepleaceConditionInfo.OldPartVersion;
                    ///新物料版本
                    porderBomRepleaceRecordInfo.NewPartVersion = bomRepleaceConditionInfo.NewPartVersion;
                    ///旧物料用量
                    porderBomRepleaceRecordInfo.OldPartQty = bomRepleaceConditionInfo.OldPartQty;
                    ///新物料用量
                    porderBomRepleaceRecordInfo.NewPartQty = bomRepleaceConditionInfo.NewPartQty;
                    ///替换时间
                    porderBomRepleaceRecordInfo.RepleaceTime = DateTime.Now;
                    ///状态 TODO:暂时没有想好干嘛的
                    porderBomRepleaceRecordInfo.Status = 10;
                    ///同时生成物料替换记录
                    @string.AppendFormat(PorderBomRepleaceRecordDAL.GetInsertSql(porderBomRepleaceRecordInfo));
                }
                //车型不为空
                if (bomRepleaceConditionVehicleInfo != null && @string.Length > 0)
                {
                    @string.AppendFormat("update [LES].[TT_BPM_BOM_REPLEACE_CONDITION_VEHICLE] set " +
                        "[REPLEACED_VEHICLE_QTY]=isnull([REPLEACED_VEHICLE_QTY],0)+" + pullOrderBoms.Count + "," +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "'" +
                        "where [ID]=" + bomRepleaceConditionVehicleInfo.Id + "");
                }    
            }
            if (@string.Length > 0 && bomRepleaceConditionInfos.Where(d=>d.NewPartNo != d.OldPartNo || d.OldSupplierNum !=d.NewSupplierNum || d.NewPartQty!=d.OldPartQty).ToList().Count>0)
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
