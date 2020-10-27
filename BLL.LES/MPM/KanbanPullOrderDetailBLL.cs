using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    /// <summary>
    /// KanbanPullOrderDetailBLL
    /// </summary>
    public class KanbanPullOrderDetailBLL
    {
        #region Common
        /// <summary>
        /// KanbanPullOrderDetailDAL
        /// </summary>
        KanbanPullOrderDetailDAL dal = new KanbanPullOrderDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<KanbanPullOrderDetailInfo></returns>
        public List<KanbanPullOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KanbanPullOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(KanbanPullOrderDetailInfo info)
        {
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
        #endregion

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool BatchdeletingInfos(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0)
                throw new Exception("MC:0x00000053");///请选中行数据

            List<KanbanPullOrderDetailInfo> kanbanPullOrderDetailInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (kanbanPullOrderDetailInfos.Count == 0)
                throw new Exception("MC:0x00000053");///请选中行数据

            ///对应看板拉动单状态必须是10.已创建
            int cnt = new KanbanPullOrderDAL().GetCounts("[FID] in ('" + string.Join("','", kanbanPullOrderDetailInfos.Select(d => d.OrderFid.GetValueOrDefault())) + "') and [STATUS] <> " + (int)PullOrderStatusConstants.Created + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000037");///拉动单已创建状态才可以删除明细数据

            ///当同一张看板拉动单下所有明细都被删除时，更新拉动单状态为90.已作废
            StringBuilder sqlBilder = new StringBuilder();
            foreach (KanbanPullOrderDetailInfo kanbanPullOrderDetailInfo in kanbanPullOrderDetailInfos)
            {
                sqlBilder.AppendLine("IF NOT EXISTS (SELECT 1 FROM [LES].[TT_MPM_KANBAN_PULL_ORDER_DETAIL] with(nolock) WHERE [ID] <> " + kanbanPullOrderDetailInfo.Id + " and [VALID_FLAG] = 1 and [ORDER_FID] = N'" + kanbanPullOrderDetailInfo.OrderFid.GetValueOrDefault() + "')");
                sqlBilder.AppendLine("BEGIN");
                sqlBilder.AppendLine("    UPDATE [LES].[TT_MPM_KANBAN_PULL_ORDER] ");
                sqlBilder.AppendLine("    SET [STATUS] = " + (int)PullOrderStatusConstants.Invalid + ",");
                sqlBilder.AppendLine("           [MODIFY_USER] = N'" + loginUser + "',");
                sqlBilder.AppendLine("           [MODIFY_DATE] = GETDATE() ");
                sqlBilder.AppendLine("    WHERE [FID] = '" + kanbanPullOrderDetailInfo.Fid.GetValueOrDefault() + "';");
                sqlBilder.AppendLine("END");
                sqlBilder.AppendLine("UPDATE [LES].[TT_MPM_KANBAN_PULL_ORDER_DETAIL] ");
                sqlBilder.AppendLine("SET [VALID_FLAG] = 0,");
                sqlBilder.AppendLine("       [MODIFY_USER] = N'" + loginUser + "',");
                sqlBilder.AppendLine("       [MODIFY_DATE] = GETDATE() ");
                sqlBilder.AppendLine("WHERE [ID] = " + kanbanPullOrderDetailInfo.Id + ";");
            }

            using (TransactionScope trans = new TransactionScope())
            {
                if (sqlBilder.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(sqlBilder.ToString());
                trans.Complete();
            }

            return true;
        }
    }
}

