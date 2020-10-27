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
    /// 
    /// </summary>
    public partial class PartInspectionModeBLL
    {
        #region Common
        /// <summary>
        /// PartInspectionModeDAL
        /// </summary>
        PartInspectionModeDAL dal = new PartInspectionModeDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PartInspectionModeInfo></returns>
        public List<PartInspectionModeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<PartInspectionModeInfo> GetList(string whereText, string orderText)
        {
            return dal.GetList(whereText, orderText);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartInspectionModeInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PartInspectionModeInfo info)
        {
            int count = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "' and [SUPPLIER_NUM] = N'" + info.SupplierNum + "'");
            if (count > 0)
                throw new Exception("MC:0x00000247");/// 物料图号、供应商组合重复限制
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
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<PartInspectionModeInfo> partInspectionModeExcelInfos = CommonDAL.DatatableConvertToList<PartInspectionModeInfo>(dataTable).ToList();
            if (partInspectionModeExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<PartInspectionModeInfo> partInspectionModeInfos = new PartInspectionModeDAL().GetListForInterfaceDataSync();
            ///执行的SQL语句
            string sql = string.Empty;
            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var partInspectionModeExcelInfo in partInspectionModeExcelInfos)
            {
                PartInspectionModeInfo partInspectionModeInfo = partInspectionModeInfos.FirstOrDefault(d =>
                d.PartNo == partInspectionModeExcelInfo.PartNo &&
                d.SupplierNum == partInspectionModeExcelInfo.SupplierNum);
                if (partInspectionModeInfo == null)
                {
                    if (string.IsNullOrEmpty(partInspectionModeExcelInfo.SupplierNum)
                        || string.IsNullOrEmpty(partInspectionModeExcelInfo.PartNo)
                        || partInspectionModeExcelInfo.InspectionMode == null)
                        throw new Exception("MC:0x00000248");///物料号、供应商、检验模式为必填项

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<PartInspectionModeInfo>(partInspectionModeExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from LES.TM_BAS_PART_INSPECTION_MODE with(nolock) where " +
                        "[PART_NO] = N'" + partInspectionModeExcelInfo.PartNo + "' and " +
                        "[SUPPLIER_NUM] = N'" + partInspectionModeExcelInfo.SupplierNum + "' and " +
                        "[VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_PART_INSPECTION_MODE] ("
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
                if (string.IsNullOrEmpty(partInspectionModeExcelInfo.SupplierNum)
                    || string.IsNullOrEmpty(partInspectionModeExcelInfo.PartNo)
                    || partInspectionModeExcelInfo.InspectionMode == null)
                    throw new Exception("MC:0x00000248");///物料号、供应商、检验模式为必填项

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<PartInspectionModeInfo>(partInspectionModeExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_PART_INSPECTION_MODE] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + partInspectionModeInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion

        /// <summary>
        /// 物料检验模式匹配
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <param name="inspectionFlag"></param>
        /// <returns></returns>
        public static string LoadInspectionMode(ref ReceiveInfo receiveInfo, ref List<ReceiveDetailInfo> receiveDetailInfos, string loginUser)
        {
            ///是否启用质量系统接口 TODO:
            string enable_qmis_flag = new ConfigDAL().GetValueByCode("ENABLE_QMIS_FLAG");
            ///发布入库单时免检物料是否发送标记 TODO:
            string release_receive_exemption_part_send_flag = new ConfigDAL().GetValueByCode("RELEASE_RECEIVE_EXEMPTION_PART_SEND_FLAG");
            ///获取所有涉及的检验模式
            List<PartInspectionModeInfo> partInspectionModeInfos = new PartInspectionModeDAL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[SUPPLIER_NUM] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.SupplierNum).ToArray()) + "')",
                string.Empty);
            ///LOG_FID
            Guid logFid = Guid.NewGuid();
            StringBuilder @string = new StringBuilder();
            foreach (ReceiveDetailInfo receiveDetailInfo in receiveDetailInfos)
            {
                PartInspectionModeInfo partInspectionModeInfo = partInspectionModeInfos.FirstOrDefault(d => d.PartNo == receiveDetailInfo.PartNo && d.SupplierNum == receiveDetailInfo.SupplierNum);
                ///没有检验模式时如何处理，按照批检处理，TODO:增加系统配置
                if (partInspectionModeInfo == null)
                    receiveDetailInfo.InspectionMode = (int)InspectionModeConstants.BatchInspection;
                else
                    ///将当前检验模式写入入库单明细
                    receiveDetailInfo.InspectionMode = partInspectionModeInfo.InspectionMode;
                ///TODO:可以加入LES质量检验模块数据内容生成逻辑
                ///是否启用质量系统接口
                if (enable_qmis_flag.ToLower() != "true") continue;
                ///发布入库单时免检物料是否发送标记
                if (release_receive_exemption_part_send_flag.ToLower() != "true" && receiveDetailInfo.InspectionMode == (int)InspectionModeConstants.ExemptionInspection) continue;
                ///QMIS检验模式
                int qmisCheckMode = 0;
                switch (receiveDetailInfo.InspectionMode.GetValueOrDefault())
                {
                    case (int)InspectionModeConstants.ExemptionInspection: qmisCheckMode = (int)QmisInspectionModeConstants.Exemption; break;
                    case (int)InspectionModeConstants.SamplingInspection: qmisCheckMode = (int)QmisInspectionModeConstants.Sampling; break;
                    case (int)InspectionModeConstants.BatchInspection: qmisCheckMode = (int)QmisInspectionModeConstants.Batch; break;
                    default: continue;
                }
                ///
                QmisAsnPullSheetInfo qmisAsnPullSheetInfo = QmisAsnPullSheetBLL.CreateQmisAsnPullSheetInfo(loginUser);
                ///
                QmisAsnPullSheetBLL.GetQmisAsnPullSheetInfo(receiveDetailInfo, ref qmisAsnPullSheetInfo);
                ///LOG_FID,日志外键
                qmisAsnPullSheetInfo.LogFid = logFid;
                ///CHECK_MODE,检验模式
                qmisAsnPullSheetInfo.CheckMode = qmisCheckMode.ToString();
                ///TOTAL_NO,送检数量,TODO:送检数量即为需求数量？
                qmisAsnPullSheetInfo.TotalNo = Convert.ToInt32(receiveDetailInfo.RequiredQty.GetValueOrDefault());
                ///
                @string.AppendLine(QmisAsnPullSheetDAL.GetInsertSql(qmisAsnPullSheetInfo));
            }
            if (@string.Length > 0)
            {
                //receiveInfo.InspectionFlag = true;
                @string.AppendLine(CommonBLL.GetCreateOutboundLogSql("QMIS", logFid, "LES-QMIS-002", receiveInfo.ReceiveNo, loginUser));
            }
            return @string.ToString();
        }
        /// <summary>
        /// 入库免检物料重新校验检验模式
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <param name="inspectionFlag"></param>
        /// <returns></returns>
        public static string ReloadInspectionMode(ReceiveInfo receiveInfo, ref List<ReceiveDetailInfo> receiveDetailInfos, string loginUser)
        {
            ///是否启用质量系统接口
            string enableQmisFlag = new ConfigDAL().GetValueByCode("ENABLE_QMIS_FLAG");
            ///获取所有涉及的检验模式，只获取单据中免检物料
            List<PartInspectionModeInfo> partInspectionModeInfos = new PartInspectionModeDAL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", receiveDetailInfos.Where(d => d.InspectionMode.GetValueOrDefault() == (int)InspectionModeConstants.ExemptionInspection).Select(d => d.PartNo).ToArray()) + "') and " +
                "[SUPPLIER_NUM] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.SupplierNum).ToArray()) + "')",
                string.Empty);
            ///LOG_FID
            Guid logFid = Guid.NewGuid();
            StringBuilder @string = new StringBuilder();
            foreach (ReceiveDetailInfo receiveDetailInfo in receiveDetailInfos)
            {
                PartInspectionModeInfo partInspectionModeInfo = partInspectionModeInfos.FirstOrDefault(d => d.PartNo == receiveDetailInfo.PartNo && d.SupplierNum == receiveDetailInfo.SupplierNum);
                ///没有检验模式时如何处理，按照批检处理，TODO:增加系统配置
                if (partInspectionModeInfo == null)
                {
                    partInspectionModeInfo = new PartInspectionModeInfo();
                    partInspectionModeInfo.InspectionMode = (int)InspectionModeConstants.BatchInspection;
                }
                ///检验模式无变化时，不产生检验任务
                if (partInspectionModeInfo.InspectionMode == receiveDetailInfo.InspectionMode) continue;
                ///将当前检验模式写入入库单明细
                receiveDetailInfo.InspectionMode = partInspectionModeInfo.InspectionMode;
                ///TODO:可以加入LES质量检验模块数据内容生成逻辑
                ///是否启用质量系统接口
                if (enableQmisFlag.ToLower() != "true") continue;
                ///QMIS检验模式
                int qmisCheckMode = 0;
                switch (partInspectionModeInfo.InspectionMode.GetValueOrDefault())
                {
                    case (int)InspectionModeConstants.SamplingInspection: qmisCheckMode = (int)QmisInspectionModeConstants.Sampling; break;
                    case (int)InspectionModeConstants.BatchInspection: qmisCheckMode = (int)QmisInspectionModeConstants.Batch; break;
                    default: continue;
                }
                ///
                QmisAsnPullSheetInfo qmisAsnPullSheetInfo = QmisAsnPullSheetBLL.CreateQmisAsnPullSheetInfo(loginUser);
                ///
                QmisAsnPullSheetBLL.GetQmisAsnPullSheetInfo(receiveDetailInfo, ref qmisAsnPullSheetInfo);
                ///LOG_FID,日志外键
                qmisAsnPullSheetInfo.LogFid = logFid;
                ///CHECK_MODE,检验模式
                qmisAsnPullSheetInfo.CheckMode = qmisCheckMode.ToString();
                ///TOTAL_NO,送检数量,TODO:送检数量即为实收数量？
                qmisAsnPullSheetInfo.TotalNo = Convert.ToInt32(receiveDetailInfo.ActualQty.GetValueOrDefault());
                ///
                @string.AppendLine(QmisAsnPullSheetDAL.GetInsertSql(qmisAsnPullSheetInfo));
            }
            if (@string.Length > 0)
                @string.AppendLine(CommonBLL.GetCreateOutboundLogSql("QMIS", logFid, "LES-QMIS-002", receiveInfo.ReceiveNo, loginUser));
            return @string.ToString();
        }

    }

}

