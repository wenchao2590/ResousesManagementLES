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
    public class PlanPartBoxBLL
    {
        #region Common
        PlanPartBoxDAL dal = new PlanPartBoxDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PlanPartBoxInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<PlanPartBoxInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlanPartBoxInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PlanPartBoxInfo info)
        {
            ///零件类代码①、零件类名称②为必填项，各自校验在对应表字段内容不能重复
            int cnt = dal.GetCounts("[PART_BOX_CODE] = N'" + info.PartBoxCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000302");///零件类代码重复
            cnt = dal.GetCounts("[PART_BOX_NAME] = N'" + info.PartBoxName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000303");///零件类名称重复

            ///来源存储区⑧与目标存储区⑩不能为同一存储区
            if (info.SourceZoneNo == info.TargetZoneNo)
                throw new Exception("MC:0x00000402");///来源存储区与目标存储区不可一致，请重新选取
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            PlanPartBoxInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误

            ///零件类没有对应的物料拉动信息可用数据
            int cnt = new MaintainInhouseLogisticStandardDAL().GetCounts("[INHOUSE_PART_CLASS] = N'" + info.PartBoxCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000403");///该零件类下还有相关物料，不可删除

            ///零件类必须已创建状态
            if (info.Status != (int)BasicDataStatusConstants.Created)
                throw new Exception("MC:0x00000722");///零件类必须已创建状态
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            int cnt = dal.GetCounts("[ID] = " + id + " and [STATUS] = " + (int)BasicDataStatusConstants.Disabled + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000742");///已作废的零件类不能进行修改

            ///来源存储区⑧与目标存储区⑩不能为同一存储区
            string sourceZoneNo = CommonBLL.GetFieldValue(fields, "SOURCE_ZONE_NO");
            string targetZoneNo = CommonBLL.GetFieldValue(fields, "TARGET_ZONE_NO");
            if (sourceZoneNo == targetZoneNo)
                throw new Exception("MC:0x00000402");///来源存储区与目标存储区不可一致，请重新选取

            string partBoxCode = CommonBLL.GetFieldValue(fields, "PART_BOX_CODE");
            string plant = CommonBLL.GetFieldValue(fields, "PLANT");
            string workshop = CommonBLL.GetFieldValue(fields, "WORKSHOP");
            string assemblyLine = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE");
            PartsBoxInfo partsBoxInfo = new PartsBoxInfo();
            partsBoxInfo.PullMode = (int)PullModeConstants.Plan;
            partsBoxInfo.BoxParts = partBoxCode;
            partsBoxInfo.Plant = plant;
            partsBoxInfo.Workshop = workshop;
            partsBoxInfo.AssemblyLine = assemblyLine;
            using (var trans = new TransactionScope())
            {
                new MaintainInhouseLogisticStandardDAL().UpdatePartsBoxInfo(partsBoxInfo);
                if (dal.UpdateInfo(fields, id) == 0) return false;
                trans.Complete();
            }
            return true;
        }
        #endregion
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<PlanPartBoxInfo> planPartBoxes = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] <> " + (int)BasicDataStatusConstants.Disabled, "[ID]");
            if (planPartBoxes == null)
                throw new Exception("Err_:MC:0x00000084");

            ///零件类必须不为已作废状态
            if (planPartBoxes.Count != rowsKeyValues.Count)
                throw new Exception("Err_:MC:0x00000422");///零件类必须不为已作废状态下才可更新


            List<MaintainInhouseLogisticStandardInfo> maintainInhouses = new MaintainInhouseLogisticStandardDAL().GetList("[INHOUSE_PART_CLASS] in ('" + string.Join("','", planPartBoxes.Select(d => d.PartBoxCode).ToArray()) + "') and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "", "[ID]");
            if (maintainInhouses.Count > 0)
                throw new Exception("Err_:MC:0x00000423");///零件类下必须没有对应的已启用状态的零件拉动信息

            StringBuilder stringBuilder = new StringBuilder();
            ///操作完成时更新状态为已作废，同时将对应已创建状态零件拉动信息更新为已作废状态
            stringBuilder.Append("update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] ");
            stringBuilder.Append("set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() ");
            stringBuilder.Append("where [INHOUSE_PART_CLASS] in ('" + string.Join("','", planPartBoxes.Select(d => d.PartBoxCode).ToArray()) + "') and [STATUS] = " + (int)BasicDataStatusConstants.Created + ";");
            stringBuilder.Append("update [LES].[TM_MPM_PLAN_PART_BOX] WITH(ROWLOCK) set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")");
            return CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());

        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<PlanPartBoxInfo> planParts = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BasicDataStatusConstants.Created, "[ID]");
            if (planParts == null)
                throw new Exception("Err_:MC:0x00000084");

            ///零件类必须为已创建状态
            if (planParts.Count != rowsKeyValues.Count)
                throw new Exception("Err_:MC:0x00000683");///状态必须为已创建

            List<MaintainInhouseLogisticStandardInfo> maintainInhouses = new MaintainInhouseLogisticStandardDAL().GetList("[INHOUSE_PART_CLASS] in ('" + string.Join("','", planParts.Select(d => d.PartBoxCode).ToArray()) + "') and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "", "[ID]");
            if (maintainInhouses == null)
                throw new Exception("Err_:MC:0x00000084");
            foreach (var item in planParts)
            {
                if (maintainInhouses.Where(d => d.InhousePartClass == item.PartBoxCode).Count() == 0)
                    throw new Exception("Err_:MC:0x00000420");///零件类下必须有对应的已启用状态的零件拉动信息
            }


            string sql = "update [LES].[TM_MPM_PLAN_PART_BOX] WITH(ROWLOCK) set[STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);

        }
    }
}

