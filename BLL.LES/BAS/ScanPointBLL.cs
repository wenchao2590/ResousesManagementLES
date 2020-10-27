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
    /// <summary>
    /// ScanPointBLL
    /// </summary>
    public partial class ScanPointBLL
    {
        #region Common
        /// <summary>
        /// ScanPointDAL
        /// </summary>
        ScanPointDAL dal = new ScanPointDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<ScanPointInfo></returns>
        public List<ScanPointInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public List<ScanPointInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ScanPointInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(ScanPointInfo info)
        {
            int cnt = dal.GetCounts("[SCAN_POINT_CODE] = N'" + info.ScanPointCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000280");///扫描点代码不能重复
            cnt = dal.GetCounts("[SCAN_POINT_NAME] = N'" + info.ScanPointName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000281");///扫描点名称不能重复
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
            ScanPointInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误
            int cnt = new StatusPointDAL().GetCounts("[SCAN_POINT_CODE] = N'" + info.ScanPointCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000284");///扫描点已被状态点设置，不允许进行删除
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
            string scanPointName = CommonBLL.GetFieldValue(fields, "SCAN_POINT_NAME");
            int scanPointNameCnt = dal.GetCounts("[ID] <> " + id + " and [SCAN_POINT_NAME] = N'" + scanPointName + "'");
            if (scanPointNameCnt > 0)
                throw new Exception("MC:0x00000281");///扫描点名称不能重复
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
            List<ScanPointInfo> scanPointExcelInfos = CommonDAL.DatatableConvertToList<ScanPointInfo>(dataTable).ToList();
            if (scanPointExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<ScanPointInfo> scanPointInfos = dal.GetList("[SCAN_POINT_CODE] in ('" + string.Join("','", scanPointExcelInfos.Select(d => d.ScanPointCode).ToArray()) + "') ", string.Empty);
            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var scanPointExcelInfo in scanPointExcelInfos)
            {
                ///当前业务数据表中此工厂的该物流路线时需要新增
                ScanPointInfo scanPointInfo = scanPointInfos.FirstOrDefault(d => d.ScanPointCode == scanPointExcelInfo.ScanPointCode);
                if (scanPointInfo == null)
                {
                    if (string.IsNullOrEmpty(scanPointExcelInfo.ScanPointCode)
                        || string.IsNullOrEmpty(scanPointExcelInfo.ScanPointName)
                        || string.IsNullOrEmpty(scanPointExcelInfo.Plant)
                        || string.IsNullOrEmpty(scanPointExcelInfo.Workshop)
                        || string.IsNullOrEmpty(scanPointExcelInfo.AssemblyLine))
                        throw new Exception("MC:0x00000282");///扫描点代码、扫描点名称、工厂、车间、生产线不能为空

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<ScanPointInfo>(scanPointExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///判断业务主键是否重复，以防止EXCEL中有重复数据，适用于基础数据导入
                    sql += "if not exists (select * from LES.TM_BAS_SCAN_POINT with(nolock) where [SCAN_POINT_CODE] = N'" + scanPointExcelInfo.ScanPointCode + "' and [VALID_FLAG] = 1)"
                        + " insert into [LES].[TM_BAS_SCAN_POINT] ("
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
                    scanPointInfo = new ScanPointInfo();
                    scanPointInfo.ScanPointCode = scanPointExcelInfo.ScanPointCode;
                    scanPointInfos.Add(scanPointInfo);
                    ///
                    continue;
                }

                if (string.IsNullOrEmpty(scanPointExcelInfo.ScanPointCode)
                        || string.IsNullOrEmpty(scanPointExcelInfo.ScanPointName)
                        || string.IsNullOrEmpty(scanPointExcelInfo.Plant)
                        || string.IsNullOrEmpty(scanPointExcelInfo.Workshop)
                        || string.IsNullOrEmpty(scanPointExcelInfo.AssemblyLine))
                    throw new Exception("MC:0x00000282");///扫描点代码、扫描点名称、工厂、车间、生产线不能为空

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<ScanPointInfo>(scanPointExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_SCAN_POINT] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + scanPointInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql))
                throw new Exception("MC:0x00000283");///:没有可导入更新的数据

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion
    }
}

