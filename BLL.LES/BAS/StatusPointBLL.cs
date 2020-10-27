using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL.SYS;

namespace BLL.LES
{
    /// <summary>
    /// StatusPointBLL
    /// </summary>
    public partial class StatusPointBLL
    {
        #region Common
        /// <summary>
        /// StatusPointDAL
        /// </summary>
        StatusPointDAL dal = new StatusPointDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<StatusPointInfo></returns>
        public List<StatusPointInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public List<StatusPointInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StatusPointInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(StatusPointInfo info)
        {
            int cnt = dal.GetCounts("[STATUS_POINT_CODE] = N'" + info.StatusPointCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000285");///状态点代码重复

            cnt = dal.GetCounts("[STATUS_POINT_NAME] = N'" + info.StatusPointName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000286");///状态点名称重复

            ///TODO:工位⑤在同一生产线⑦下的状态点不能重复设置

            cnt = dal.GetCounts("[ASSEMBLY_LINE] = N'" + info.AssemblyLine + "' and [STATUS_POINT_SEQ] = N'" + info.StatusPointSeq.GetValueOrDefault() + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000287");///同一生产线范围内不能重复填写状态点顺序
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
            ///预留一个判断依据，在该状态点已被零件类使用的情况下不能进行删除，如何判断后期再增加
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string statusPointName = CommonBLL.GetFieldValue(fields, "STATUS_POINT_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [STATUS_POINT_NAME] = N'" + statusPointName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000286");///状态点名称重复

            string statusPointSeq = CommonBLL.GetFieldValue(fields, "STATUS_POINT_SEQ");
            if (string.IsNullOrEmpty(statusPointSeq))
                throw new Exception("MC:0x00000341");///状态点顺序为必填项
            string assemblyLine = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE");
            cnt = dal.GetCounts("[ID] <> " + id + " and [ASSEMBLY_LINE] = N'" + assemblyLine + "' and [STATUS_POINT_SEQ] = N'" + statusPointSeq + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000287");///同一生产线范围内不能重复填写状态点顺序

            ///TODO:工位⑤在同一生产线⑦下的状态点不能重复设置

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        #region Private 
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<StatusPointInfo> statusPointExcelInfos = CommonDAL.DatatableConvertToList<StatusPointInfo>(dataTable).ToList();
            if (statusPointExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<StatusPointInfo> statusPointInfos = dal.GetList("[STATUS_POINT_CODE] in ('" + string.Join("','", statusPointExcelInfos.Select(d => d.StatusPointCode).ToArray()) + "') ", string.Empty);
            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var statusPointExcelInfo in statusPointExcelInfos)
            {
                ///当前业务数据表中此工厂的该物流路线时需要新增
                StatusPointInfo statusPointInfo = statusPointInfos.FirstOrDefault(d => d.StatusPointCode == statusPointExcelInfo.StatusPointCode);
                if (statusPointInfo == null)
                {
                    if (string.IsNullOrEmpty(statusPointExcelInfo.StatusPointCode)
                        || string.IsNullOrEmpty(statusPointExcelInfo.StatusPointName)
                        || statusPointExcelInfo.StatusPointSeq != null
                        || string.IsNullOrEmpty(statusPointExcelInfo.AssemblyLine)
                        || string.IsNullOrEmpty(statusPointExcelInfo.Workshop)
                        || string.IsNullOrEmpty(statusPointExcelInfo.Plant))
                        throw new Exception("MC:0x00000368");///状态点代码、名称、顺序、工厂、车间、生产线不能为空

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<StatusPointInfo>(statusPointExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///判断业务主键是否重复，以防止EXCEL中有重复数据，适用于基础数据导入
                    sql += "if not exists (select * from LES.TM_BAS_STATUS_POINT with(nolock) where [STATUS_POINT_CODE] = N'" + statusPointExcelInfo.StatusPointCode + "' and [VALID_FLAG] = 1)"
                        + " insert into [LES].[TM_BAS_STATUS_POINT] ("
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
                    ///为防止EXCEL中数据有重复
                    statusPointInfo = new StatusPointInfo();
                    statusPointInfo.StatusPointCode = statusPointExcelInfo.StatusPointCode;
                    statusPointInfos.Add(statusPointInfo);
                    ///
                    continue;
                }

                if (string.IsNullOrEmpty(statusPointExcelInfo.StatusPointCode)
                        || string.IsNullOrEmpty(statusPointExcelInfo.StatusPointName)
                        || statusPointExcelInfo.StatusPointSeq != null
                        || string.IsNullOrEmpty(statusPointExcelInfo.AssemblyLine)
                        || string.IsNullOrEmpty(statusPointExcelInfo.Workshop)
                        || string.IsNullOrEmpty(statusPointExcelInfo.Plant))
                    throw new Exception("MC:0x00000368");///状态点代码、名称、顺序、工厂、车间、生产线不能为空

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<StatusPointInfo>(statusPointExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_STATUS_POINT] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + statusPointInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql))
                throw new Exception("MC:0x00000283");///:没有可导入更新的数据

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion
    }
}

