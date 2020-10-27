using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// MaintainPartsBLL
    /// </summary>
    public class MaintainPartsBLL
    {
        #region Common
        /// <summary>
        /// MaintainPartsDAL
        /// </summary>
        MaintainPartsDAL dal = new MaintainPartsDAL();
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<MaintainPartsInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<MaintainPartsInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaintainPartsInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 验证-添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(MaintainPartsInfo info)
        {
            ///物料号①全表范围不允许重复
            int cnt = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000726"); ///物料号不允许重复 
            return dal.Add(info);
        }
        /// <summary>
        /// 验证有效物料仓库和物料拉动信息-进行删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            ///已有有效物料仓储信息或物料拉动信息的物料数据不允许进行删除操作
            int cnt = new MaintainInhouseLogisticStandardDAL().GetCounts("[PART_NO] in (select [PART_NO] from [LES].[TM_BAS_MAINTAIN_PARTS] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000727");///已有有效物料拉动信息,无法删除

            cnt = new PartsStockDAL().GetCounts("[PART_NO] in (select [PART_NO] from [LES].[TM_BAS_MAINTAIN_PARTS] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000728");///已有有效物料拉动信息,无法删除
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// 验证-修改
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
        /// 获取接口数据同步所需要的比较集合
        /// </summary>
        /// <returns></returns>
        public List<MaintainPartsInfo> GetListForInterfaceDataSync(List<string> partNos)
        {
            if (partNos.Count == 0)
                return new List<MaintainPartsInfo>();
            return dal.GetListForInterfaceDataSync(partNos);
        }
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<MaintainPartsInfo> maintainPartsExcelInfos = CommonDAL.DatatableConvertToList<MaintainPartsInfo>(dataTable).ToList();
            if (maintainPartsExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsDAL().GetListForInterfaceDataSync(maintainPartsExcelInfos.Select(d => d.PartNo).ToList());
            ///执行的SQL语句
            string sql = string.Empty;
            ///获取工厂信息
            List<PlantInfo> plantInfos = new PlantDAL().GetListForInterfaceDataSync();

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var maintainPartsExcelInfo in maintainPartsExcelInfos)
            {
                ///导入时需要填写LES的工厂编号
                PlantInfo plantInfo = plantInfos.FirstOrDefault(d => d.Plant == maintainPartsExcelInfo.Plant);
                if (plantInfo == null)
                    throw new Exception("MC:0x00000215");///工厂代码在系统中不存在

                ///当前业务数据表中此工厂的该物料信息时需要新增
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == maintainPartsExcelInfo.PartNo && d.Plant == maintainPartsExcelInfo.Plant);
                if (maintainPartsInfo == null)
                {
                    ///物料号①、物料中文名称②为必填项
                    if (string.IsNullOrEmpty(maintainPartsExcelInfo.PartCname) || string.IsNullOrEmpty(maintainPartsExcelInfo.PartNo))
                        throw new Exception("MC:3x00000020");///物料号、物料中文名称为必填项

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<MaintainPartsInfo>(maintainPartsExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from LES.TM_BAS_MAINTAIN_PARTS with(nolock) where [PART_NO] = N'" + maintainPartsExcelInfo.PartNo + "' and [PLANT] = N'" + maintainPartsExcelInfo.Plant + "' and [VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_MAINTAIN_PARTS] ("
                        + "[FID],"
                        + insertFieldString
                        + "[CREATE_USER],"
                        + "[CREATE_DATE],"
                        + "[VALID_FLAG]"
                        + ") values ("
                        + "NEWID(),"///FID
                        + insertValueString
                        + "N'" + loginUser + "',"///CREATE_USER
                        + "GETDATE(),"///CREATE_DATE
                        + "1"///VALID_FLAG
                        + ");";
                    continue;
                }
                ///物料中文名称②为必填项
                if (string.IsNullOrEmpty(maintainPartsExcelInfo.PartCname))
                    throw new Exception("MC:3x00000020");///物料号、物料中文名称为必填项

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<MaintainPartsInfo>(maintainPartsExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_MAINTAIN_PARTS] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + maintainPartsInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}

