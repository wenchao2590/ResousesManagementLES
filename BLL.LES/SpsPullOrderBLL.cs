using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class SpsPullOrderBLL
    {
        #region Common
        SpsPullOrderDAL dal = new SpsPullOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<SpsPullOrderInfo></returns>
        public List<SpsPullOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public SpsPullOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(SpsPullOrderInfo info)
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
        /// <returns>List<SpsPullOrderInfo></returns>
        public List<SpsPullOrderInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion
        #region 打印获取数据源方法
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            //根据预设看板拉动单格式进行打印，格式等待业务部门提供
            //打印成功后记录最后打印时间⑯、最后打印用户⑰、累计打印次数⑮
            List<SpsPullOrderInfo> list = dal.GetList(string.Format("[ID] IN ({0})", string.Join(",", rowsKeyValues.ToArray())), string.Empty);
            if (list.Count == 0)
                throw new Exception("MC:0x00000072");//没有打印文件生成
            string sql = "select c.[ITEM_NAME] as ORDER_TYPE,r.*"
                + "from LES.TT_MPM_SPS_PULL_ORDER r with(nolock) "
                + "left join TS_SYS_CODE_ITEM c with(nolock) on c.[ITEM_VALUE] = r.[ORDER_TYPE] and c.[CODE_FID] = N'4afa543d-4455-4e54-868e-f36474e21cf6' and c.[VALID_FLAG] = 1 "
                + "where r.[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  and r.[VALID_FLAG] = 1;"
                + "select * from LES.TT_MPM_SPS_PULL_ORDER_DETAIL with(nolock) "
                + "where [ORDER_FID] in (select [FID] from LES.TT_MPM_SPS_PULL_ORDER with(nolock) "
                + "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  and [VALID_FLAG] = 1 ) and [VALID_FLAG] = 1;";

            return DAL.SYS.CommonDAL.ExecuteDataSetBySql(sql);
        }
        public void GetPrintCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = string.Empty;
            DataTable dt = DAL.SYS.CommonDAL.ExecuteDataTableBySql("select * from [TS_SYS_PRINT_CONFIG] where [VALID_FLAG] = 1 and [PRINT_CONFIG_CODE] = 'BFDA_PCS_PULL_ORDER'");
            sql += "update [LES].[TT_MPM_SPS_PULL_ORDER] set [LAST_PRINT_DATE] =  GETDATE(),[PRINT_TIMES] =isnull([PRINT_TIMES],0)+" + dt.Rows[0]["PRINT_COPIES"] + ",[LAST_PRINT_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues) + ")";
            DAL.SYS.CommonDAL.ExecuteScalar(sql);
        }
        #endregion
    }
}

