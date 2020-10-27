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
    /// LackOfMaterialDetailBLL
    /// </summary>
    public class LackOfMaterialDetailBLL
    {
        #region Common
        /// <summary>
        /// LackOfMaterialDetailDAL
        /// </summary>
        LackOfMaterialDetailDAL dal = new LackOfMaterialDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<LackOfMaterialDetailInfo></returns>
        public List<LackOfMaterialDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LackOfMaterialDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(LackOfMaterialDetailInfo info)
        {
            ///娿面中不允许进行添加
            throw new Exception("MC:0x00000255");///请勿新增数据
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
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
            ///反馈数量
            string feedbackLackQty = CommonBLL.GetFieldValue(fields, "FEEDBACK_LACK_QTY");
            if (string.IsNullOrEmpty(feedbackLackQty))
                throw new Exception("MC:0x00000124");///TODO:反馈数量未填写
            decimal decimalFeedbackLackQty = 0;
            if (!decimal.TryParse(feedbackLackQty, out decimalFeedbackLackQty))
                throw new Exception("MC:0x00000124");///TODO:反馈数量未填写
            if (decimalFeedbackLackQty == 0)
                throw new Exception("MC:0x00000124");///TODO:反馈数量未填写

            string lackOrderFid = CommonBLL.GetFieldValue(fields, "LACK_ORDER_FID");
            int cnt = new LackOfMaterialDAL().GetCounts("[FID] = N'" + lackOrderFid + "' and [STATUS] = " + (int)LackOfMaterialStatusConstants.Completed + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000084");///TODO:缺件表计算完成时才能进行反馈

            ///撤销反馈
            if (decimalFeedbackLackQty == -1)
                fields += ",[FEEDBACK_FLAG] = 0,[FEEDBACK_TIME] = NULL";
            else
                fields += ",[FEEDBACK_FLAG] = 1,[FEEDBACK_TIME] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        public List<LackOfMaterialDetailInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser, string whereText)
        {
            List<LackOfMaterialDetailInfo> lackOfMaterialDetailExcelInfos = CommonDAL.DatatableConvertToList<LackOfMaterialDetailInfo>(dataTable).ToList();
            if (lackOfMaterialDetailExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<LackOfMaterialDetailInfo> lackOfMaterialDetailInfos = dal.GetList(whereText, string.Empty);
            if (lackOfMaterialDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<LackOfMaterialInfo> lackOfMaterialInfos = new LackOfMaterialDAL().GetList("" +
                "[FID] in ('" + string.Join("','", lackOfMaterialDetailInfos.Select(d => d.LackOrderFid.GetValueOrDefault()).ToArray()) + "') and " +
                "[STATUS] = " + (int)LackOfMaterialStatusConstants.Completed + "", string.Empty);
            if (lackOfMaterialInfos.Count == 0)
                throw new Exception("MC:0x00000084");///TODO:缺件表计算完成时才能进行反馈

            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var lackOfMaterialDetailExcelInfo in lackOfMaterialDetailExcelInfos)
            {
                ///当前业务数据表中此工厂的该物流路线时需要新增
                LackOfMaterialDetailInfo LackOfMaterialDetailInfo = lackOfMaterialDetailInfos.FirstOrDefault(d =>
                d.Plant == lackOfMaterialDetailExcelInfo.Plant &&
                d.PartPurchaser == lackOfMaterialDetailExcelInfo.PartPurchaser &&
                d.SupplierNum == lackOfMaterialDetailExcelInfo.SupplierNum &&
                d.PartNo == lackOfMaterialDetailExcelInfo.PartNo);

                if (LackOfMaterialDetailInfo == null)
                    throw new Exception("MC:0x00000255");///请勿新增数据

                ///未填写数据的不标记已反馈，反馈数相同时不再标记已反馈
                if (lackOfMaterialDetailExcelInfo.FeedbackLackQty.GetValueOrDefault() == 0) continue;
                if (lackOfMaterialDetailExcelInfo.FeedbackLackQty.GetValueOrDefault() == LackOfMaterialDetailInfo.FeedbackLackQty.GetValueOrDefault()) continue;

                if (string.IsNullOrEmpty(lackOfMaterialDetailExcelInfo.PartNo)
                        || string.IsNullOrEmpty(lackOfMaterialDetailExcelInfo.SupplierNum)
                        || string.IsNullOrEmpty(lackOfMaterialDetailExcelInfo.Plant))
                    throw new Exception("MC:0x00000254");///供应商代码、工厂、物料号为必填项

                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<LackOfMaterialDetailInfo>(lackOfMaterialDetailExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TT_ATP_LACK_OF_MATERIAL_DETAIL] set "
                    + valueString + "" +
                    "[FEEDBACK_FLAG] = 1," +
                    "[FEEDBACK_TIME] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = " + LackOfMaterialDetailInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql))
                throw new Exception("MC:0x00000084");///TODO:没有可导入更新的数据

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}

