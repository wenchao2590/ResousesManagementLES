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

namespace BLL.LES
{
    public class JisPartBoxBLL
    {
        #region Common
        JisPartBoxDAL dal = new JisPartBoxDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<JisPartBoxInfo></returns>
        public List<JisPartBoxInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public JisPartBoxInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(JisPartBoxInfo info)
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
            JisPartBoxInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (info.Status != (int)BasicDataStatusConstants.Created)
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
            JisPartBoxInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误
            if (info.Status != (int)BasicDataStatusConstants.Created)
                throw new Exception("MC:0x00000441");///已创建状态才可进行修改
            string partBoxName = CommonBLL.GetFieldValue(fields, "PART_BOX_NAME");
            int partBoxNameCnt = dal.GetCounts(" [PART_BOX_NAME] = N'" + partBoxName + "' and [ID] <> " + id + "");
            if (partBoxNameCnt > 0)
                throw new Exception("Err_:MC:0x00000303");///零件类名称重复

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<JisPartBoxInfo></returns>
        public List<JisPartBoxInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
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
            List<JisPartBoxInfo> jisPartBoxes = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] <> " + (int)BasicDataStatusConstants.Disabled, "[ID]");
            if (jisPartBoxes == null)
                throw new Exception("Err_:MC:0x00000084");////数据有误

            ///零件类必须不为已作废状态
            if (jisPartBoxes.Count != rowsKeyValues.Count)
                throw new Exception("Err_:MC:0x00000422");///零件类必须不为已作废状态下才可更新

            List<MaintainInhouseLogisticStandardInfo> maintainInhouses = new MaintainInhouseLogisticStandardDAL().GetList("[INHOUSE_PART_CLASS] in ('" + string.Join("','", jisPartBoxes.Select(d => d.PartBoxCode).ToArray()) + "') and [INHOUSE_SYSTEM_MODE] = " + (int)PullModeConstants.Jis + " and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "", "[ID]");
            if (maintainInhouses.Count > 0)
                throw new Exception("Err_:MC:0x00000423");///零件类下必须没有对应的已启用状态的零件拉动信息


            StringBuilder stringBuilder = new StringBuilder();
            ///操作完成时更新状态为已作废，同时将对应已创建状态零件拉动信息更新为已作废状态
            stringBuilder.Append("update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] ");
            stringBuilder.Append("set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() ");
            stringBuilder.Append("where [INHOUSE_PART_CLASS] in ('" + string.Join("','", jisPartBoxes.Select(d => d.PartBoxCode).ToArray()) + "') and [INHOUSE_SYSTEM_MODE] = " + (int)PullModeConstants.Jis + " and [STATUS] = " + (int)BasicDataStatusConstants.Created + ";");
            stringBuilder.Append("update [LES].[TM_MPM_JIS_PART_BOX] WITH(ROWLOCK) set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")");
            return CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());


        }


        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<JisPartBoxInfo> jisPartBoxes = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BasicDataStatusConstants.Created, "[ID]");
            if (jisPartBoxes == null)
                throw new Exception("Err_:MC:0x00000084");

            ///零件类必须为已创建状态
            if (jisPartBoxes.Count != rowsKeyValues.Count)
                throw new Exception("Err_:MC:0x00000683");///状态必须为已创建

            List<MaintainInhouseLogisticStandardInfo> maintainInhouses = new MaintainInhouseLogisticStandardDAL().GetList("[INHOUSE_PART_CLASS] in ('" + string.Join("','", jisPartBoxes.Select(d => d.PartBoxCode).ToArray()) + "') and [STATUS] = " + (int)BasicDataStatusConstants.Enable + " and  [INHOUSE_SYSTEM_MODE] = " + (int)PullModeConstants.Jis, "[ID]");
            if (maintainInhouses == null)
                throw new Exception("Err_:MC:0x00000084");
            foreach (var item in jisPartBoxes)
            {
                if (maintainInhouses.Where(d => d.InhousePartClass == item.PartBoxCode).Count() == 0)
                    throw new Exception("Err_:MC:0x00000420");///零件类下必须有对应的已启用状态的零件拉动信息
            }

            string sql = "update [LES].[TM_MPM_JIS_PART_BOX] WITH(ROWLOCK) set[STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
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
            List<JisPartBoxInfo> jisPartBoxes = CommonDAL.DatatableConvertToList<JisPartBoxInfo>(dataTable).ToList();
            if (jisPartBoxes.Count == 0 || jisPartBoxes == null)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<JisPartBoxInfo> jisParts = new JisPartBoxDAL().GetList(" [PART_BOX_CODE] in ('" + string.Join("', '", jisPartBoxes.Select(d => d.PartBoxCode).ToList().ToArray()) + "')", "");
            List<JisPartBoxInfo> boxInfos = new JisPartBoxDAL().GetList(" [PART_BOX_NAME] in ('" + string.Join("', '", jisPartBoxes.Select(d => d.PartBoxName).ToList().ToArray()) + "')", "");
            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var item in jisPartBoxes)
            {
                JisPartBoxInfo info = jisParts.FirstOrDefault(d => d.PartBoxCode == item.PartBoxCode);
                if (info == null)
                {
                    if (string.IsNullOrEmpty(item.PartBoxCode)
                        || string.IsNullOrEmpty(item.PartBoxName)
                        || string.IsNullOrEmpty(item.Plant)
                        || string.IsNullOrEmpty(item.Workshop)
                        || string.IsNullOrEmpty(item.AssemblyLine)
                        || string.IsNullOrEmpty(item.StatusPointCode)
                        || string.IsNullOrEmpty(item.TWmNo)
                        || string.IsNullOrEmpty(item.TZoneNo))
                        throw new Exception("MC:0x00000452");///零件类代码、零件类名称、工厂、车间、产线、状态点、目标仓库存储区为必填项

                    JisPartBoxInfo jisPartBoxInfo = boxInfos.FirstOrDefault(d => d.PartBoxName == item.PartBoxName);
                    if (jisPartBoxInfo != null)
                        throw new Exception("MC:0x00000303");///零件类名称重复

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;

                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<JisPartBoxInfo>(item, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///判断业务主键是否重复，以防止EXCEL中有重复数据，适用于基础数据导入
                    sql += "if not exists (select * from LES.TM_MPM_TWD_PART_BOX with(nolock) where [PART_BOX_CODE] = N'" + item.PartBoxCode + "' and [VALID_FLAG] = 1)"
                        + " insert into [LES].[TM_MPM_JIS_PART_BOX] ("
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
                        + ");";
                    jisParts.Add(item);
                    continue;
                }
                if (info.Status == (int)BasicDataStatusConstants.Created)
                {

                    if (string.IsNullOrEmpty(item.PartBoxName)
                       || string.IsNullOrEmpty(item.Plant)
                       || string.IsNullOrEmpty(item.Workshop)
                       || string.IsNullOrEmpty(item.AssemblyLine)
                       || string.IsNullOrEmpty(item.StatusPointCode)
                       || string.IsNullOrEmpty(item.TWmNo)
                       || string.IsNullOrEmpty(item.TZoneNo))
                        throw new Exception("MC:0x00000452");///零件类代码、零件类名称、工厂、车间、产线、状态点、目标仓库存储区为必填项

                    JisPartBoxInfo jisPartBoxInfo = boxInfos.FirstOrDefault(d => d.PartBoxName == item.PartBoxName && d.Id != info.Id);
                    if (jisPartBoxInfo != null)
                        throw new Exception("MC:0x00000303");///零件类名称重复

                    ///值
                    string valueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<JisPartBoxInfo>(item, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                        valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                    }
                    sql += "update [LES].[TM_MPM_JIS_PART_BOX] set "
                        + valueString
                        + "[MODIFY_USER] = N'" + loginUser + "',"
                        + "[MODIFY_DATE] = GETDATE() "
                        + "where [ID] = " + info.Id + ";";
                }
            }

            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }

    }
}

