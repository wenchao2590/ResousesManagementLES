using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class SrmPullingOrderDetailBLL
    {
        #region Common
        SrmPullingOrderDetailDAL dal = new SrmPullingOrderDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<SrmPullingOrderDetailInfo></returns>
        public List<SrmPullingOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public SrmPullingOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(SrmPullingOrderDetailInfo info)
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
        /// <returns>List<SrmPullingOrderDetailInfo></returns>
        public List<SrmPullingOrderDetailInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create SrmPullingOrderDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>SrmPullingOrderDetailInfo</returns>
        public static SrmPullingOrderDetailInfo CreateSrmPullingOrderDetailInfo(string loginUser)
        {
            SrmPullingOrderDetailInfo info = new SrmPullingOrderDetailInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///ORDER_FID
            info.OrderFid = null;
            ///TARGET_SLCODE
            info.TargetSlcode = null;
            ///PART_NO
            info.PartNo = null;
            ///PART_CNAME
            info.PartCname = null;
            ///SNP
            info.Snp = null;
            ///PART_QTY
            info.PartQty = null;
            ///PACKAGE_MODEL
            info.PackageModel = null;
            ///REMARK
            info.Remark = null;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///MODIFY_USER
            info.ModifyUser = null;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///COMMENTS
            info.Comments = null;
            return info;
        }

        #endregion
    }
}

