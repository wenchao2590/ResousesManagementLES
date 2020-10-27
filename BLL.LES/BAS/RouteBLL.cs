using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    public class RouteBLL
    {
        #region Common
        RouteDAL dal = new RouteDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<RouteInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RouteInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(RouteInfo info)
        {
            ///路径代码、路径名称单字段全表范围不允许重复
            int routeCnt = dal.GetCounts("[ROUTE] = N'" + info.Route + "'");
            if (routeCnt > 0)
                throw new Exception("MC:0x00000666");///路径代码重复
            int routeNameCnt = dal.GetCounts("[ROUTE_NAME] = N'" + info.RouteName + "'");
            if (routeNameCnt > 0)
                throw new Exception("MC:0x00000312");///路径名称重复

            ///是否启用路径中途点配置
            string routeMidEnableFlag = new ConfigDAL().GetValueByCode("ROUTE_MID_ENABLE_FLAG");
            RouteMidInfo routeMidInfo = null;
            RouteMidInfo routeMid = null;
            if (routeMidEnableFlag.ToLower() == "true")
            {
                //if (info.SZoneNo == info.TZoneNo)
                //    throw new Exception("MC:0x00000323");///出发与目的不能是同一地点

                ///出发地
                routeMidInfo = new RouteMidInfo();
                //routeMidInfo.WmNo = info.SWmNo;
                //routeMidInfo.ZoneNo = info.SZoneNo;
                routeMidInfo.MidSeq = 0;
                routeMidInfo.ArriveTime = 0;
                routeMidInfo.RouteFid = info.Fid.GetValueOrDefault();
                routeMidInfo.ValidFlag = true;
                routeMidInfo.CreateUser = info.CreateUser;
                routeMidInfo.Fid = Guid.NewGuid();
                routeMidInfo.CreateDate = DateTime.Now;
                ///目的地
                routeMid = new RouteMidInfo();
                //routeMid.WmNo = info.wm;
                //routeMid.ZoneNo = info.TZoneNo;
                routeMid.MidSeq = 1000;
                routeMid.ArriveTime = 0;
                routeMid.RouteFid = info.Fid.GetValueOrDefault();
                routeMid.ValidFlag = true;
                routeMid.CreateUser = info.CreateUser;
                routeMid.Fid = Guid.NewGuid();
                routeMid.CreateDate = DateTime.Now;
            }

            using (var trans = new TransactionScope())
            {
                info.Id = dal.Add(info);
                if (info.Id == 0) return 0;
                if (routeMidInfo != null)
                {
                    routeMidInfo.Id = new RouteMidDAL().Add(routeMidInfo);
                    if (routeMidInfo.Id == 0) return 0;
                }
                if (routeMid != null)
                {
                    routeMid.Id = new RouteMidDAL().Add(routeMid);
                    if (routeMid.Id == 0) return 0;
                }
                trans.Complete();
            }
            return info.Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            ///是否启用路径中途点配置
            string routeMidEnableFlag = new ConfigDAL().GetValueByCode("ROUTE_MID_ENABLE_FLAG");
            if (routeMidEnableFlag.ToLower() == "true")
            {
                int cnt = new RouteMidDAL().GetCounts("[ROUTE_FID] in (select [FID] from LES.[TM_BAS_ROUTE] with(nolock) where [VALID_FLAG] = 1 and [ID] = " + id + ")");
                if (cnt > 0)
                    throw new Exception("MC:0x00000324");///路径有中途点不能删除
            }
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
            ///路径名称单字段全表范围不允许重复
            string routeName = CommonBLL.GetFieldValue(fields, "ROUTE_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [ROUTE_NAME] = N'" + routeName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000312");///路径名称重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
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
            List<RouteInfo> routeExcelInfos = CommonDAL.DatatableConvertToList<RouteInfo>(dataTable).ToList();
            if (routeExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<RouteInfo> routeInfos = new RouteDAL().GetListForInterfaceDataSync(routeExcelInfos.Select(d => d.Route).ToList());
            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var routeExcelInfo in routeExcelInfos)
            {
                ///当前业务数据表中此工厂的该物流路线时需要新增
                RouteInfo routeInfo = routeInfos.FirstOrDefault(d => d.Route == routeExcelInfo.Route);
                if (routeInfo == null)
                {
                    ///代码、名称、类型、工厂为必填项
                    if (string.IsNullOrEmpty(routeExcelInfo.Route) || string.IsNullOrEmpty(routeExcelInfo.RouteName) || routeExcelInfo.RouteType.GetValueOrDefault() == 0)
                        throw new Exception("MC:0x00000216");///路径代码、名称、类型、对应工厂为必填项

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<RouteInfo>(routeExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from LES.TM_BAS_ROUTE with(nolock) where [ROUTE] = N'" + routeExcelInfo.Route + "' and [VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_ROUTE] ("
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
                ///代码、名称、类型、工厂为必填项
                if (string.IsNullOrEmpty(routeExcelInfo.RouteName) || routeExcelInfo.RouteType.GetValueOrDefault() == 0)
                    throw new Exception("MC:0x00000216");///路径代码、名称、类型、对应工厂为必填项

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<RouteInfo>(routeExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_ROUTE] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + routeInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }

    }
}

