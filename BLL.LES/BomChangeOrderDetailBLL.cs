using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class BomChangeOrderDetailBLL
    {
        #region Common
        BomChangeOrderDetailDAL dal = new BomChangeOrderDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BomChangeOrderDetailInfo></returns>
        public List<BomChangeOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public BomChangeOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(BomChangeOrderDetailInfo info)
        {
            if (info != null)
                throw new Exception("MC:0x00000255");///不可新增数据

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
            BomChangeOrderDetailInfo info = dal.GetInfo(id);

            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误

            BomChangeOrderInfo orderInfo = new BomChangeOrderDAL().GetList(" [FID] = N'" + info.OrderFid + "'", string.Empty).FirstOrDefault();

            if (orderInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (orderInfo.Status != (int)BreakPointOrderStatusConstants.Created)
                throw new Exception("MC:0x00000441");///需要校验单据状态⑨为10.已创建

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<BomChangeOrderDetailInfo></returns>
        public List<BomChangeOrderDetailInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create BomChangeOrderDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>BomChangeOrderDetailInfo</returns>
        public static BomChangeOrderDetailInfo CreateBomChangeOrderDetailInfo(string loginUser)
        {
            BomChangeOrderDetailInfo info = new BomChangeOrderDetailInfo();
            ///ID
            info.Id = new long();
            ///FID
            info.Fid = Guid.NewGuid();
            ///ORDER_FID
            info.OrderFid = null;
            ///ORDER_CODE
            info.OrderCode = null;
            ///PARENT_PART_NO
            info.ParentPartNo = null;
            ///CHANGE_FLAG
            info.ChangeFlag = null;
            ///OLD_PART_NO
            info.OldPartNo = null;
            ///OLD_PART_STOCK_QTY
            info.OldPartStockQty = null;
            ///OLD_PART_CONSUMED_QTY
            info.OldPartConsumedQty = null;
            ///NEW_PART_NO
            info.NewPartNo = null;
            ///NEW_PART_QTY
            info.NewPartQty = null;
            ///WORKSHOP_SECTION
            info.WorkshopSection = null;
            ///LOCATION
            info.Location = null;
            ///COMMENTS
            info.Comments = null;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///MODIFY_USER
            info.ModifyUser = null;
            return info;
        }

        #endregion
    }
}

