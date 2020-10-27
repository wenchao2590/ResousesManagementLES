using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PorderBomRepleaceRecordBLL
    {
        #region Common
        PorderBomRepleaceRecordDAL dal = new PorderBomRepleaceRecordDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PorderBomRepleaceRecordInfo></returns>
        public List<PorderBomRepleaceRecordInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public PorderBomRepleaceRecordInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(PorderBomRepleaceRecordInfo info)
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
        /// <returns>List<PorderBomRepleaceRecordInfo></returns>
        public List<PorderBomRepleaceRecordInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create PorderBomRepleaceRecordInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>PorderBomRepleaceRecordInfo</returns>
        public static PorderBomRepleaceRecordInfo CreatePorderBomRepleaceRecordInfo(string loginUser)
        {
            PorderBomRepleaceRecordInfo info = new PorderBomRepleaceRecordInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///ORDER_NO
            info.OrderNo = null;
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
            ///REPLEACE_TIME
            info.RepleaceTime = null;
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
        /// 在线替换
        /// </summary>
        /// <param name="bomRepleaceConditionInfos"></param>
        /// <param name="loginUser"></param>
        public void OnlineReplacement(List<BomRepleaceConditionInfo> bomRepleaceConditionInfos, string loginUser)
        {
            if (bomRepleaceConditionInfos.Count == 0) return;
            ///生产订单
            List<PullOrdersInfo> pullOrdersInfos = new PullOrdersDAL().GetList("" +
                    " and [CHANGE_FLAG]=" + (int)ChangeFlagConstants.NotReplaced + "", string.Empty);
            if (pullOrdersInfos.Count == 0) return;
            ///车辆状态点信息集合
            List<VehiclePointStatusInfo> vehiclePointStatusInfos = new VehiclePointStatusDAL().GetList("" +
                "and [ORDER_NO] in ('" + string.Join("','", bomRepleaceConditionInfos.Select(d => d.StartPorderCode).ToArray()) + "')", string.Empty);
            if (vehiclePointStatusInfos.Count == 0) return;
            ///状态点集合
            List<StatusPointInfo> statusPointInfos = new StatusPointDAL().GetList("" +
                "[STATUS_POINT_CODE] in ('" + string.Join("','", vehiclePointStatusInfos.Select(d => d.StatusPointCode).ToArray()) + "')", string.Empty);
            if (statusPointInfos.Count == 0) return;
            ///时间窗（过点累计方式）零件类
            List<TwdPartBoxInfo> twdPartBoxInfos = new TwdPartBoxDAL().GetList("" +
               " and [STATUS] =" + (int)BasicDataStatusConstants.Enable + " " +
               " and [REQUIREMENT_ACCUMULATE_MODE]=" + (int)RequirementAccumulateModeConstants.PassSpot + "" +
               " and [STATUS_POINT_CODE] in ('" + string.Join("','", statusPointInfos.Select(d => d.StatusPointCode).ToArray()) + "')", string.Empty);
            ///排序拉动方式 零件类
            List<JisPartBoxInfo> jisPartBoxInfos = new JisPartBoxDAL().GetList("" +
                " and [STATUS] =" + (int)BasicDataStatusConstants.Enable + "" +
                " and [STATUS_POINT_CODE] in ('" + string.Join("','", statusPointInfos.Select(d => d.StatusPointCode).ToArray()) + "')", string.Empty);
            ///相应的物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("" +
                " and [STATUS] =" + (int)BasicDataStatusConstants.Enable + "" +
                " (and [INHOUSE_PART_CLASS] in ('" + string.Join("','", twdPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "')" +
                " or [INHOUSE_PART_CLASS] in ('" + string.Join("','", jisPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "'))", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0) return;
            foreach (BomRepleaceConditionInfo bomRepleaceConditionInfo in bomRepleaceConditionInfos)
            {
                ///有效时间内
                if (!(bomRepleaceConditionInfo.ExecuteStartTime <= DateTime.Now) || !(DateTime.Now <= bomRepleaceConditionInfo.ExecuteEndTime)) continue;
                ///根据起始生产订单号，获取车辆状态点信息判断其是否在线，若未上线则不执行以下逻辑
                PullOrdersInfo pullOrdersInfo = pullOrdersInfos.FirstOrDefault(d => d.OrderNo == bomRepleaceConditionInfo.StartPorderCode);
                if (pullOrdersInfo == null || pullOrdersInfo.OrderStatus != (int)OrderStatusConstants.AlreadOnline) continue;
                ///若已上线或已下线则需要根据其获取顺序号之后的所有在线生产订单，依次循环进行逻辑处理    TODO:已下线的逻辑？
                ///同一起始生产订单号可能出现在多条生产线的状态点上，以下为单生产订单处理逻辑            TODO:多条生产线的逻辑？
                ///已上线的生产订单：
                ///本生产订单对应的车辆状态点信息
                List<VehiclePointStatusInfo> vehiclePointStatuss = vehiclePointStatusInfos.Where(d => d.OrderNo == pullOrdersInfo.OrderNo).ToList();
                if (vehiclePointStatuss.Count == 0) continue;
                ///当前车辆最大状态点信息
                VehiclePointStatusInfo vehiclePointStatusInfo = vehiclePointStatuss.Where(d => d.OrderNo == pullOrdersInfo.OrderNo).OrderByDescending(d => d.PassTime).FirstOrDefault();
                if (vehiclePointStatusInfo == null) continue;
                ///当前顺序号之后的所有的车辆状态点信息
                List<VehiclePointStatusInfo> vehiclePoints = vehiclePointStatusInfos.Where(d => d.SeqNo >= vehiclePointStatusInfo.SeqNo).ToList();
                ///当前顺序号之后的所有在线生产订单
                pullOrdersInfos = (from p in pullOrdersInfos
                                   join v in vehiclePoints
                                   on p.OrderNo equals v.OrderNo
                                   select p).Distinct().ToList();
                ///依次循环进行逻辑处理
                foreach (PullOrdersInfo pullOrder in pullOrdersInfos)
                {
                    ///根据生产订单号获取其物料清单，作为后续匹配更改单的源数据
                    List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomDAL().GetList("" +
                        "and [ORDERFID]='" + pullOrdersInfo.Fid + "'", string.Empty);
                    if (pullOrderBomInfos.Count == 0) continue;
                    ///本生产订单对应的车辆状态点信息
                    List<VehiclePointStatusInfo> vehicles = vehiclePointStatusInfos.Where(d => d.OrderNo == pullOrdersInfo.OrderNo).ToList();
                    if (vehicles.Count == 0) continue;
                    ///本产生订单对应的所有状态点信息
                    List<StatusPointInfo> statusPoints = statusPointInfos.Where(d => vehicles.Select(v => v.StatusPointCode).Contains(d.StatusPointCode)).ToList();
                    if (statusPoints.Count == 0) continue;
                    ///已在线的生产订单在此时，需要根据所在状态点位置
                    VehiclePointStatusInfo vehiclePointStatus = vehiclePointStatusInfos.Where(d => d.OrderNo == pullOrder.OrderNo).OrderByDescending(d => d.PassTime).FirstOrDefault();
                    if (vehiclePointStatus == null) continue;
                    StatusPointInfo statusPointInfo = statusPointInfos.FirstOrDefault(d => d.StatusPointCode == vehiclePointStatus.StatusPointCode);
                    if (statusPointInfo == null) continue;
                    ///将物料拉动的结果集分为三个部分，其一为未累计、其二为已累计未拉动、其三为已拉动    
                    ///该生产订单对应的其后状态点
                    List<StatusPointInfo> notStatusPoints = statusPoints.Where(d => d.StatusPointSeq > statusPointInfo.StatusPointSeq).ToList();
                    ///该生产订单对应的状态点及之前的状态点
                    List<StatusPointInfo> yesStatusPoints = statusPoints.Where(d => d.StatusPointSeq <= statusPointInfo.StatusPointSeq).ToList();
                    ///其后的状态点
                    if (notStatusPoints.Count > 0)
                    {
                        ///其一为当前状态点位置之后的状态点对应的时间窗（过点累计方式）、排序拉动方式相应的物料拉动信息物料、供应商、工位
                        ///该逻辑获取的数据在此不做后续处理，但此逻辑请事先在程序中实现，将会到离队归队时使用  
                        List<TwdPartBoxInfo> twdPartBoxs = twdPartBoxInfos.Where(d => notStatusPoints.Select(s => s.StatusPointCode).Contains(d.StatusPointCode)).ToList();
                        List<JisPartBoxInfo> jisPartBoxs = jisPartBoxInfos.Where(d => notStatusPoints.Select(s => s.StatusPointCode).Contains(d.StatusPointCode)).ToList();
                        ///零件类对应的物料拉动信息
                        if (twdPartBoxs.Count != 0 || jisPartBoxs.Count != 0)
                        {
                            ///生产订单的产线下的物料拉动信息
                            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = maintainInhouseLogisticStandardInfos.
                            Where(d => d.AssemblyLine == pullOrder.AssemblyLine).Where(d =>
                                (twdPartBoxs.Select(t => t.PartBoxCode).Contains(d.InhousePartClass)) ||
                                (jisPartBoxs.Select(j => j.PartBoxCode).Contains(d.InhousePartClass))).ToList();
                            if (maintainInhouseLogisticStandards.Count != 0)
                            {
                                ///拉动信息对应的Bom清单
                                List<PullOrderBomInfo> pullOrderBoms = pullOrderBomInfos.Where(d =>
                                 maintainInhouseLogisticStandards.Select(m => m.PartNo).Contains(d.Zcomno) &&
                                 maintainInhouseLogisticStandards.Select(m => m.SupplierNum).Contains(d.SupplierNum)).ToList();
                            }
                        }
                    }
                    ///其二、其三目前没有较理想的方式区分开，暂时以已拉动处理、当前状态点位置之前包括该状态点位置对应的拉动方式相关物料拉动信息
                    if (yesStatusPoints.Count > 0)
                    {
                        List<TwdPartBoxInfo> twdPartBoxs = twdPartBoxInfos.Where(d => yesStatusPoints.Select(s => s.StatusPointCode).Contains(d.StatusPointCode)).ToList();
                        List<JisPartBoxInfo> jisPartBoxs = jisPartBoxInfos.Where(d => yesStatusPoints.Select(s => s.StatusPointCode).Contains(d.StatusPointCode)).ToList();
                        ///零件类对应的物料拉动信息
                        if (twdPartBoxs.Count != 0 || jisPartBoxs.Count != 0)
                        {
                            ///生产订单的产线下的物料拉动信息
                            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = maintainInhouseLogisticStandardInfos.
                            Where(d => d.AssemblyLine == pullOrder.AssemblyLine).Where(d =>
                                (twdPartBoxs.Select(t => t.PartBoxCode).Contains(d.InhousePartClass)) ||
                                (jisPartBoxs.Select(j => j.PartBoxCode).Contains(d.InhousePartClass))).ToList();
                            maintainInhouseLogisticStandards= maintainInhouseLogisticStandards.Where(d=>d.PartNo== bomRepleaceConditionInfo.OldPartNo).ToList();
                            ///根据已拉动的物料拉动信息，与替换条件中的旧物料号对比
                            ///若不存在于已拉动物料中，则只需要执行生产订单物料清单替换逻辑即可
                            ///否则需要进行新物料号的自动紧急拉动且生成旧物料号的余料退库单（退库地点为物料拉动信息中的来源库存地点）
                            ///同时也需要执行生产订单物料清单替换逻辑 
                            if (maintainInhouseLogisticStandards.Count > 0)
                            {
                                ///拉动信息对应的Bom清单
                                List<PullOrderBomInfo> pullOrderBoms = pullOrderBomInfos.Where(d =>
                                 maintainInhouseLogisticStandards.Select(m => m.PartNo).Contains(d.Zcomno) &&
                                 maintainInhouseLogisticStandards.Select(m => m.SupplierNum).Contains(d.SupplierNum)).ToList();
                                foreach(PullOrderBomInfo pullOrderBom in pullOrderBoms)
                                {                            
                                    MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = maintainInhouseLogisticStandards.FirstOrDefault(d =>
                                     d.PartNo == pullOrderBom.Zcomno && d.SupplierNum == pullOrderBom.SupplierNum);
                                    if (maintainInhouseLogisticStandardInfo == null) continue;
                                    ///进行新物料号的自动紧急拉动
                                    
                                    ///生成旧物料号的余料退库单（退库地点为物料拉动信息中的来源库存地点）
                                }
                            }                         
                            new BomRepleaceConditionBLL().ReplacementCriteria(pullOrder, loginUser);
                        }
                    }
                }
            }
        }
    }
}

