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
    public class LocationBLL
    {
        #region Common
        LocationDAL dal = new LocationDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<LocationInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LocationInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(LocationInfo info)
        {
            ///工位代码③不允许重复，单字段进行全表校验
            int locationCnt = dal.GetCounts("[LOCATION] = N'" + info.Location + "'");
            if (locationCnt > 0)
                throw new Exception("Err_:MC:0x00000163");
            ///工位名称④、顺序号⑥在同一工段中不允许重复
            int locationNameCnt = dal.GetCounts("[LOCATION_NAME] = N'" + info.LocationName + "' and [WORKSHOP_SECTION] = N'" + info.WorkshopSection + "'");
            if (locationNameCnt > 0)
                throw new Exception("Err_:MC:0x00000194");
            ///当顺序号非空时再进行判断
            if (info.SequenceNo.GetValueOrDefault() > 0)
            {
                int sequenceNoCnt = dal.GetCounts("[SEQUENCE_NO] = " + info.SequenceNo + " and [WORKSHOP_SECTION] = N'" + info.WorkshopSection + "'");
                if (sequenceNoCnt > 0)
                    throw new Exception("Err_:MC:0x00000195");
            }
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
            ///
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
            ///工位名称④、顺序号⑥在同一工段中不允许重复
            string locationName = CommonBLL.GetFieldValue(fields, "LOCATION_NAME");
            string workshopSection = CommonBLL.GetFieldValue(fields, "WORKSHOP_SECTION");
            if (string.IsNullOrEmpty(locationName))
                throw new Exception("MC:0x00000087");///工位名称不能为空
            if (string.IsNullOrEmpty(workshopSection))
                throw new Exception("MC:0x00000088");///工段代码不能为空
            int cnt = dal.GetCounts("[ID] <> " + id + " and [LOCATION_NAME] = N'" + locationName + "' and [WORKSHOP_SECTION] = N'" + workshopSection + "'");
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000194");
            ///顺序号可空，判断会复杂一些
            string sequenceNo = CommonBLL.GetFieldValue(fields, "SEQUENCE_NO");
            if (!string.IsNullOrEmpty(sequenceNo) && sequenceNo.Trim() != "null" && sequenceNo.Trim() != "0")
            {
                cnt = dal.GetCounts("[ID] <> " + id + " and [SEQUENCE_NO] = " + sequenceNo + " and [WORKSHOP_SECTION] = N'" + workshopSection + "'");
                if (cnt > 0)
                    throw new Exception("Err_:MC:0x00000195");
            }
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion

        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<LocationInfo> locationExcelInfos = CommonDAL.DatatableConvertToList<LocationInfo>(dataTable).ToList();
            if (locationExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<LocationInfo> routeInfos = new LocationDAL().GetListForInterfaceDataSync(locationExcelInfos.Select(d => d.Location).ToList());
            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var locationExcelInfo in locationExcelInfos)
            {
                ///
                LocationInfo locationInfo = routeInfos.FirstOrDefault(d => d.Location == locationExcelInfo.Location);
                if (locationInfo == null)
                {
                    if (string.IsNullOrEmpty(locationExcelInfo.Location)
                        || string.IsNullOrEmpty(locationExcelInfo.LocationName)
                        || string.IsNullOrEmpty(locationExcelInfo.Plant))
                        throw new Exception("MC:0x00000222");///工位代码、名称、对应工厂为必填项

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<LocationInfo>(locationExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from LES.TM_BAS_LOCATION with(nolock) where [LOCATION] = N'" + locationExcelInfo.Location + "' and [VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_LOCATION] ("
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
                ///
                if (string.IsNullOrEmpty(locationExcelInfo.LocationName)
                    || string.IsNullOrEmpty(locationExcelInfo.Plant))
                    throw new Exception("MC:0x00000222");///工位代码、名称、对应工厂为必填项

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<LocationInfo>(locationExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_LOCATION] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + locationInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }


        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>LocationInfo Collection </returns>
		public List<LocationInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
    }
}

