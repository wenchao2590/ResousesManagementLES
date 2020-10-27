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
            ///·�����롢·�����Ƶ��ֶ�ȫ��Χ�������ظ�
            int routeCnt = dal.GetCounts("[ROUTE] = N'" + info.Route + "'");
            if (routeCnt > 0)
                throw new Exception("MC:0x00000666");///·�������ظ�
            int routeNameCnt = dal.GetCounts("[ROUTE_NAME] = N'" + info.RouteName + "'");
            if (routeNameCnt > 0)
                throw new Exception("MC:0x00000312");///·�������ظ�

            ///�Ƿ�����·����;������
            string routeMidEnableFlag = new ConfigDAL().GetValueByCode("ROUTE_MID_ENABLE_FLAG");
            RouteMidInfo routeMidInfo = null;
            RouteMidInfo routeMid = null;
            if (routeMidEnableFlag.ToLower() == "true")
            {
                //if (info.SZoneNo == info.TZoneNo)
                //    throw new Exception("MC:0x00000323");///������Ŀ�Ĳ�����ͬһ�ص�

                ///������
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
                ///Ŀ�ĵ�
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
            ///�Ƿ�����·����;������
            string routeMidEnableFlag = new ConfigDAL().GetValueByCode("ROUTE_MID_ENABLE_FLAG");
            if (routeMidEnableFlag.ToLower() == "true")
            {
                int cnt = new RouteMidDAL().GetCounts("[ROUTE_FID] in (select [FID] from LES.[TM_BAS_ROUTE] with(nolock) where [VALID_FLAG] = 1 and [ID] = " + id + ")");
                if (cnt > 0)
                    throw new Exception("MC:0x00000324");///·������;�㲻��ɾ��
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
            ///·�����Ƶ��ֶ�ȫ��Χ�������ظ�
            string routeName = CommonBLL.GetFieldValue(fields, "ROUTE_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [ROUTE_NAME] = N'" + routeName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000312");///·�������ظ�
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// ִ�е���EXCEL����
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<RouteInfo> routeExcelInfos = CommonDAL.DatatableConvertToList<RouteInfo>(dataTable).ToList();
            if (routeExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

            ///��ȡҵ�����Ҫ��������ݼ���,׼���Ա�
            List<RouteInfo> routeInfos = new RouteDAL().GetListForInterfaceDataSync(routeExcelInfos.Select(d => d.Route).ToList());
            ///ִ�е�SQL���
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///���������м������
            foreach (var routeExcelInfo in routeExcelInfos)
            {
                ///��ǰҵ�����ݱ��д˹����ĸ�����·��ʱ��Ҫ����
                RouteInfo routeInfo = routeInfos.FirstOrDefault(d => d.Route == routeExcelInfo.Route);
                if (routeInfo == null)
                {
                    ///���롢���ơ����͡�����Ϊ������
                    if (string.IsNullOrEmpty(routeExcelInfo.Route) || string.IsNullOrEmpty(routeExcelInfo.RouteName) || routeExcelInfo.RouteType.GetValueOrDefault() == 0)
                        throw new Exception("MC:0x00000216");///·�����롢���ơ����͡���Ӧ����Ϊ������

                    ///�ֶ�
                    string insertFieldString = string.Empty;
                    ///ֵ
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<RouteInfo>(routeExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶
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
                ///���롢���ơ����͡�����Ϊ������
                if (string.IsNullOrEmpty(routeExcelInfo.RouteName) || routeExcelInfo.RouteType.GetValueOrDefault() == 0)
                    throw new Exception("MC:0x00000216");///·�����롢���ơ����͡���Ӧ����Ϊ������

                ///ֵ
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<RouteInfo>(routeExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

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

