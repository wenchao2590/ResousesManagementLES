using DAL.LES;
namespace BLL.LES
{
    using DAL.SYS;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;

    public partial class TwdCounterUpdateBLL
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="info"></param>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EntitySubmitInfos(TwdCounterUpdateInfo info, List<string> rowsKeyValues, string loginUser)
        {
            List<TwdCounterInfo> twdCounterInfos = new TwdCounterDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (twdCounterInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            string sql = string.Empty;
            foreach (var twdCounterInfo in twdCounterInfos)
            {
                if (twdCounterInfo.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Disabled)
                    throw new Exception("MC:0x00000455");///计数器已作废不能修改数量

                sql += "update [LES].[TT_MPM_TWD_COUNTER] set " +
                    "[CURRENT_QTY] = isnull([CURRENT_QTY],0) +" + info.SubmitQty.GetValueOrDefault() + "," +
                    "[MODIFY_DATE] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "' where " +
                    "[ID] = " + twdCounterInfo.Id + ";";
                ///根据计数器的物料拉动信息外键获取物料拉动信息
                MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = new MaintainInhouseLogisticStandardDAL().GetInfoByFid(twdCounterInfo.PartPullFid.GetValueOrDefault());
                if (maintainInhouseLogisticStandardInfo == null)
                    throw new Exception("MC:0x00000213");///物料拉动信息数据错误

                if (maintainInhouseLogisticStandardInfo.Status.GetValueOrDefault() != (int)BasicDataStatusConstants.Enable)
                    throw new Exception("MC:0x00000233");///没有已启用的物料拉动信息

                ///获取零件类信息
                TwdPartBoxInfo twdPartBoxInfo = new TwdPartBoxDAL().GetInfo(maintainInhouseLogisticStandardInfo.InhousePartClass);
                ///未能成功获取零件类信息
                if (twdPartBoxInfo == null)
                    throw new Exception("MC:0x00000225");///拉动零件类数据错误

                ///零件类未启用
                if (twdPartBoxInfo.Status.GetValueOrDefault() != (int)BasicDataStatusConstants.Enable)
                    throw new Exception("MC:0x00000456");///零件类未启用

                ///创建计数器日志
                TwdCounterLogInfo twdCounterLogInfo = TwdCounterLogBLL.CreateTwdCounterLogInfo(twdCounterInfo.Fid.GetValueOrDefault(), loginUser);
                ///以物料拉动信息填充计数器日志
                TwdCounterLogBLL.GetTwdCounterLogInfo(maintainInhouseLogisticStandardInfo, ref twdCounterLogInfo);
                ///以零件类信息填充计数器日志
                TwdCounterLogBLL.GetTwdCounterLogInfo(twdPartBoxInfo, ref twdCounterLogInfo);
                ///PART_QTY 
                twdCounterLogInfo.PartQty = info.SubmitQty.GetValueOrDefault();
                ///SOURCE_DATA_FID 
                twdCounterLogInfo.SourceDataFid = twdCounterInfo.Fid;
                ///SOURCE_DATA_TYPE 
                twdCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.Manual;
                ///SOURCE_DATA 
                twdCounterLogInfo.SourceData = twdCounterInfo.PartBoxCode;
                ///Comments
                twdCounterLogInfo.Comments = info.Comments;
                ///
                sql += TwdCounterLogDAL.GetInsertSql(twdCounterLogInfo);

                ///触发层级拉动
                sql += TwdCounterBLL.LevelPullRequirementCounter(
                    maintainInhouseLogisticStandardInfo,
                    info.SubmitQty.GetValueOrDefault(),
                    loginUser,
                    twdCounterInfo.Fid.GetValueOrDefault(),
                    twdCounterInfo.PartBoxCode);
            }
            ///
            using (var trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            ///
            return true;
        }
    }
}
