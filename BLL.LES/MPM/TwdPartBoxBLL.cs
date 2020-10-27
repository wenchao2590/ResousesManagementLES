namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Transactions;

    /// <summary>
    /// TwdPartBoxBLL
    /// </summary>
    public partial class TwdPartBoxBLL
    {
        #region Common
        /// <summary>
        /// TwdPartBoxDAL
        /// </summary>
        TwdPartBoxDAL dal = new TwdPartBoxDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<TwdPartBoxInfo></returns>
        public List<TwdPartBoxInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
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
        public List<TwdPartBoxInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TwdPartBoxInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(TwdPartBoxInfo info)
        {
            ValidTwdPartBoxInfo(info);
            info.Status = (int)BasicDataStatusConstants.Created;
            return dal.Add(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            TwdPartBoxInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据有误

            if (info.Status != (int)BasicDataStatusConstants.Created)
                throw new Exception("MC:0x00000415");///已创建状态才可进行删除

            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("" +
                "[INHOUSE_PART_CLASS] = N'" + info.PartBoxCode + "' and " +
                "[INHOUSE_SYSTEM_MODE] in ('" + (int)PullModeConstants.Twd + "','" + (int)PullModeConstants.Pcs + "')", string.Empty);

            StringBuilder @string = new StringBuilder();
            @string.AppendLine("update [LES].[TM_MPM_TWD_PART_BOX] " +
                    "set [VALID_FLAG] = 0," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + info.Id + ";");
            foreach (var maintainInhouseLogisticStandardInfo in maintainInhouseLogisticStandardInfos)
            {
                if (maintainInhouseLogisticStandardInfo.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Enable)
                    throw new Exception("MC:0x00000423");///零件类下存在已启用状态的物料拉动信息

                @string.AppendLine("update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] " +
                "set [VALID_FLAG] = 0," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() " +
                "where [ID] = " + maintainInhouseLogisticStandardInfo.Id + ";");
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            TwdPartBoxInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误

            string partBoxName = CommonBLL.GetFieldValue(fields, "PART_BOX_NAME");
            info.PartBoxName = partBoxName;

            string sZoneNo = CommonBLL.GetFieldValue(fields, "S_ZONE_NO");
            string tZoneNo = CommonBLL.GetFieldValue(fields, "T_ZONE_NO");
            string prevSZoneNo = info.SZoneNo;
            string prevTZoneNo = info.TZoneNo;
            info.SZoneNo = sZoneNo;
            info.TZoneNo = tZoneNo;

            string requirement_accumulate_time = CommonBLL.GetFieldValue(fields, "REQUIREMENT_ACCUMULATE_TIME");
            int.TryParse(requirement_accumulate_time, out int requirementAccumulateTime);
            info.RequirementAccumulateTime = requirementAccumulateTime;

            string load_time = CommonBLL.GetFieldValue(fields, "LOAD_TIME");
            int.TryParse(load_time, out int loadTime);
            info.LoadTime = loadTime;

            string transport_time = CommonBLL.GetFieldValue(fields, "TRANSPORT_TIME");
            int.TryParse(transport_time, out int transportTime);
            info.TransportTime = transportTime;

            string unload_time = CommonBLL.GetFieldValue(fields, "UNLOAD_TIME");
            int.TryParse(unload_time, out int unloadTime);
            info.UnloadTime = unloadTime;

            string delay_time = CommonBLL.GetFieldValue(fields, "DELAY_TIME");
            int.TryParse(delay_time, out int delayTime);
            info.DelayTime = delayTime;

            string online_time = CommonBLL.GetFieldValue(fields, "ONLINE_TIME");
            int.TryParse(online_time, out int onlineTime);
            info.OnlineTime = onlineTime;

            string requirement_accumulate_mode = CommonBLL.GetFieldValue(fields, "REQUIREMENT_ACCUMULATE_MODE");
            int.TryParse(requirement_accumulate_mode, out int requirementAccumulateMode);
            info.RequirementAccumulateMode = requirementAccumulateMode;
            string status_point_code = CommonBLL.GetFieldValue(fields, "STATUS_POINT_CODE");
            info.StatusPointCode = status_point_code;

            string twd_pull_mode = CommonBLL.GetFieldValue(fields, "TWD_PULL_MODE");
            int.TryParse(twd_pull_mode, out int twdPullMode);
            int prevTwdPullMode = info.TwdPullMode.GetValueOrDefault();
            info.TwdPullMode = twdPullMode;

            string roundness_mode = CommonBLL.GetFieldValue(fields, "ROUNDNESS_MODE");
            int.TryParse(roundness_mode, out int roundnessMode);
            info.RoundnessMode = roundnessMode;

            ValidTwdPartBoxInfo(info);
            ///校验通过后将拉动模式\地点回复
            info.TwdPullMode = prevTwdPullMode;
            info.SZoneNo = prevSZoneNo;
            info.TZoneNo = prevTZoneNo;

            if (info.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Disabled)
                throw new Exception("MC:0x00000422");///已作废状态的零件类不能修改

            string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
            info.ModifyUser = loginUser;
            info.ModifyDate = DateTime.Now;
            ///已发布状态的零件类只能更新部分信息
            if (info.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Enable)
                return dal.Update(info) > 0 ? true : false;

            StringBuilder @string = new StringBuilder();
            ///是否更新了拉动模式,需要同步更新物料拉动信息中的拉动模式
            if (info.TwdPullMode.GetValueOrDefault() != twdPullMode)
                @string.AppendLine("update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] " +
                        "set [INHOUSE_SYSTEM_MODE] = N'" + twdPullMode + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                        "where [INHOUSE_SYSTEM_MODE] = N'" + info.TwdPullMode.GetValueOrDefault() + "' and [INHOUSE_PART_CLASS] = N'" + info.PartBoxCode + "' and [VALID_FLAG] = 1;");
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (dal.UpdateInfo(fields, id) == 0) return false;
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Private
        /// <summary>
        /// 校验规则
        /// </summary>
        /// <param name="info"></param>
        private void ValidTwdPartBoxInfo(TwdPartBoxInfo info)
        {
            int cnt = 0;
            ///PART_BOX_CODE
            if (info.Id == 0)
                cnt = dal.GetCounts("[PART_BOX_CODE] = N'" + info.PartBoxCode + "'");
            else
                cnt = dal.GetCounts("[PART_BOX_CODE] = N'" + info.PartBoxCode + "' and [ID] <> " + info.Id + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000302");///零件类代码重复

            if (string.IsNullOrEmpty(info.PartBoxName))
                throw new Exception("MC:0x00000299");///零件类名称不可为空

            ///PART_BOX_NAME
            if (info.Id == 0)
                cnt = dal.GetCounts("[PART_BOX_NAME] = N'" + info.PartBoxName + "'");
            else
                cnt = dal.GetCounts("[PART_BOX_NAME] = N'" + info.PartBoxName + "' and [ID] <> " + info.Id + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000303");///零件类名称重复

            if (string.IsNullOrEmpty(info.TWmNo))
                throw new Exception("MC:0x00000514");///目标仓库不能为空
            if (string.IsNullOrEmpty(info.TZoneNo))
                throw new Exception("MC:0x00000515");///目标存储区不能为空

            if (info.SZoneNo == info.TZoneNo)
                throw new Exception("MC:0x00000293");///来源存储区与目标存储区不能一致

            if (info.RequirementAccumulateTime.GetValueOrDefault() < 0)
                throw new Exception("MC:0x00000314");///需求累积时间必须大于等于零

            if (info.LoadTime.GetValueOrDefault() < 0)
                throw new Exception("MC:0x00000315");///装货时间必须大于等于零

            if (info.TransportTime.GetValueOrDefault() < 0)
                throw new Exception("MC:0x00000316");///运输时间必须大于等于零

            if (info.UnloadTime.GetValueOrDefault() < 0)
                throw new Exception("MC:0x00000317");///卸货时间必须大于等于零

            if (info.DelayTime.GetValueOrDefault() < 0)
                throw new Exception("MC:0x00000725");///延迟时间必须大于等于零

            if (info.OnlineTime.GetValueOrDefault() < 0)
                throw new Exception("MC:0x00000319");///取货时间必须大于等于零

            if (info.RequirementAccumulateMode == null)
                throw new Exception("MC:0x00000516");///需求累计为必选项

            if (info.RequirementAccumulateMode == (int)RequirementAccumulateModeConstants.PassSpot)
            {
                if (string.IsNullOrEmpty(info.StatusPointCode))
                    throw new Exception("MC:0x00000294");///当需求累计方式为过点时状态点不能为空
            }

            if (info.TwdPullMode == null)
                throw new Exception("MC:0x00000517");///拉动模式为必选项

            if (info.TwdPullMode.GetValueOrDefault() == (int)TwdPullModeConstants.Pcs)
            {
                if (info.RoundnessMode.GetValueOrDefault() != (int)RoundnessModeConstants.Downward)
                    throw new Exception("MC:0x00000512");///整包消耗拉动时圆整方式只能选择向下圆整
            }
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<TwdPartBoxInfo> twdPartBoxInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (twdPartBoxInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据有误

            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("" +
                "[INHOUSE_PART_CLASS] in ('" + string.Join("','", twdPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "') and " +
                "[INHOUSE_SYSTEM_MODE] in ('" + (int)PullModeConstants.Twd + "','" + (int)PullModeConstants.Pcs + "')", string.Empty);
            ///语句
            StringBuilder @string = new StringBuilder();
            foreach (var twdPartBoxInfo in twdPartBoxInfos)
            {
                if (twdPartBoxInfo.Status.GetValueOrDefault() != (int)BasicDataStatusConstants.Enable)
                    throw new Exception("MC:0x00000513");///仅已启用状态可变更为已作废状态

                @string.AppendLine("update [LES].[TM_MPM_TWD_PART_BOX] " +
                    "set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + twdPartBoxInfo.Id + ";");

                ///将已启用的物料拉动信息作废
                foreach (MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo in maintainInhouseLogisticStandardInfos.Where(d =>
                d.InhousePartClass == twdPartBoxInfo.PartBoxCode && d.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Enable).ToList())
                {
                    @string.AppendLine("update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] " +
                    "set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + maintainInhouseLogisticStandardInfo.Id + ";");
                }
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<TwdPartBoxInfo> twdPartBoxInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (twdPartBoxInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据有误

            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("" +
                "[INHOUSE_PART_CLASS] in ('" + string.Join("','", twdPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "') and " +
                "[INHOUSE_SYSTEM_MODE] in ('" + (int)PullModeConstants.Twd + "','" + (int)PullModeConstants.Pcs + "')", string.Empty);

            foreach (var twdPartBoxInfo in twdPartBoxInfos)
            {
                if (twdPartBoxInfo.Status.GetValueOrDefault() != (int)BasicDataStatusConstants.Created)
                    throw new Exception("MC:0x00000421");///仅已创建状态可变更已启用状态

                int cnt = maintainInhouseLogisticStandardInfos.Count(d => d.InhousePartClass == twdPartBoxInfo.PartBoxCode && d.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Enable);
                if (cnt == 0)
                    throw new Exception("MC:0x00000420");///零件类下必须有对应的已启用状态的物料拉动信息
            }
            string sql = "update [LES].[TM_MPM_TWD_PART_BOX] " +
                "set [STATUS] = " + (int)BasicDataStatusConstants.Enable + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() " +
                "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<TwdPartBoxInfo> twdPartBoxExcelInfos = CommonDAL.DatatableConvertToList<TwdPartBoxInfo>(dataTable).ToList();
            if (twdPartBoxExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<TwdPartBoxInfo> twdPartBoxInfos = new TwdPartBoxDAL().GetList("[PART_BOX_CODE] in ('" + string.Join("', '", twdPartBoxExcelInfos.Select(d => d.PartBoxCode).ToList().ToArray()) + "')", "");

            ///执行的SQL语句
            StringBuilder @string = new StringBuilder();
            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var twdPartBoxExcelInfo in twdPartBoxExcelInfos)
            {
                TwdPartBoxInfo twdPartBoxInfo = twdPartBoxInfos.FirstOrDefault(d => d.PartBoxCode == twdPartBoxExcelInfo.PartBoxCode);
                ///需要新增
                if (twdPartBoxInfo == null)
                {
                    ///校验
                    ValidTwdPartBoxInfo(twdPartBoxExcelInfo);
                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;

                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<TwdPartBoxInfo>(twdPartBoxExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///判断业务主键是否重复，以防止EXCEL中有重复数据，适用于基础数据导入
                    @string.AppendLine("if not exists (select * from LES.TM_MPM_TWD_PART_BOX with(nolock) " +
                        "where [PART_BOX_CODE] = N'" + twdPartBoxExcelInfo.PartBoxCode + "' and [VALID_FLAG] = 1)"
                        + " insert into [LES].[TM_MPM_TWD_PART_BOX] ("
                        + "[FID],"
                        + insertFieldString
                        + "[STATUS],"
                        + "[CREATE_USER],"
                        + "[CREATE_DATE],"
                        + "[VALID_FLAG]"
                        + ") values ("
                        + "NEWID(),"///FID
                        + insertValueString
                        + (int)BasicDataStatusConstants.Created + ","
                        + "N'" + loginUser + "',"///CREATE_USER
                        + "GETDATE(),"///CREATE_DATE
                        + "1"///VALID_FLAG
                        + ");");
                    ///防止EXCEL中有重复项
                    twdPartBoxInfos.Add(twdPartBoxExcelInfo);
                    continue;
                }
                ///
                if (twdPartBoxInfo.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Disabled) continue;
                ///
                twdPartBoxExcelInfo.Id = twdPartBoxInfo.Id;
                ///校验
                ValidTwdPartBoxInfo(twdPartBoxExcelInfo);
                ///是否更新了拉动模式,需要同步更新物料拉动信息中的拉动模式
                if (twdPartBoxInfo.TwdPullMode.GetValueOrDefault() != twdPartBoxExcelInfo.TwdPullMode.GetValueOrDefault())
                    @string.AppendLine("update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] " +
                            "set [INHOUSE_SYSTEM_MODE] = N'" + twdPartBoxExcelInfo.TwdPullMode.GetValueOrDefault() + "'," +
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() " +
                            "where [INHOUSE_SYSTEM_MODE] = N'" + twdPartBoxInfo.TwdPullMode.GetValueOrDefault() + "' and " +
                            "[INHOUSE_PART_CLASS] = N'" + twdPartBoxExcelInfo.PartBoxCode + "' and " +
                            "[VALID_FLAG] = 1;");
                ///已创建状态的数据可以全信息更新
                if (twdPartBoxInfo.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Created)
                {
                    ///值
                    string valueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<TwdPartBoxInfo>(twdPartBoxExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                        valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                    }
                    @string.AppendLine("update [LES].[TM_MPM_TWD_PART_BOX] set "
                        + valueString
                        + "[MODIFY_USER] = N'" + loginUser + "',"
                        + "[MODIFY_DATE] = GETDATE() "
                        + "where [ID] = " + twdPartBoxInfo.Id + ";");
                    continue;
                }
                ///
                @string.AppendLine("update [LES].[TM_MPM_TWD_PART_BOX] " +
                    "set [PART_BOX_NAME] = N'" + twdPartBoxExcelInfo.PartBoxName + "'," +
                    "[REQUIREMENT_ACCUMULATE_TIME] = " + (twdPartBoxExcelInfo.RequirementAccumulateTime == null ? "NULL" : "" + twdPartBoxExcelInfo.RequirementAccumulateTime.GetValueOrDefault() + "") + "," +
                    "[LOAD_TIME] = " + (twdPartBoxExcelInfo.LoadTime == null ? "NULL" : "" + twdPartBoxExcelInfo.LoadTime.GetValueOrDefault() + "") + "," +
                    "[TRANSPORT_TIME] = " + (twdPartBoxExcelInfo.TransportTime == null ? "NULL" : "" + twdPartBoxExcelInfo.TransportTime.GetValueOrDefault() + "") + "," +
                    "[UNLOAD_TIME] = " + (twdPartBoxExcelInfo.UnloadTime == null ? "NULL" : "" + twdPartBoxExcelInfo.UnloadTime.GetValueOrDefault() + "") + "," +
                    "[DELAY_TIME] = " + (twdPartBoxExcelInfo.DelayTime == null ? "NULL" : "" + twdPartBoxExcelInfo.DelayTime.GetValueOrDefault() + "") + "," +
                    "[ONLINE_TIME] = " + (twdPartBoxExcelInfo.OnlineTime == null ? "NULL" : "" + twdPartBoxExcelInfo.OnlineTime.GetValueOrDefault() + "") + "," +
                    "[REQUIREMENT_ACCUMULATE_MODE] = " + twdPartBoxExcelInfo.RequirementAccumulateMode.GetValueOrDefault() + "," +
                    "[STATUS_POINT_CODE] = N'" + twdPartBoxExcelInfo.StatusPointCode + "'," +
                    "[ROUNDNESS_MODE] = " + twdPartBoxExcelInfo.RoundnessMode.GetValueOrDefault() + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + twdPartBoxInfo.Id + ";");
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        #endregion
    }
}

