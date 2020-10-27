namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    public class JisCounterBLL
    {
        #region Common
        JisCounterDAL dal = new JisCounterDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<JisCounterInfo></returns>
        public List<JisCounterInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public JisCounterInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(JisCounterInfo info)
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
        /// <returns>List<JisCounterInfo></returns>
        public List<JisCounterInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Private
        /// <summary>
        /// 新建计数器对象
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static JisCounterInfo CreateJisCounterInfo(string loginUser)
        {
            JisCounterInfo jisCounterInfo = new JisCounterInfo();
            ///CreateUser
            jisCounterInfo.CreateUser = loginUser;
            ///CreateDate
            jisCounterInfo.CreateDate = DateTime.Now;
            ///ValidFlag
            jisCounterInfo.ValidFlag = true;
            ///Fid
            jisCounterInfo.Fid = Guid.NewGuid();
            ///STATUS
            jisCounterInfo.Status = (int)JisCounterStatusConstants.Accumulating;
            ///
            return jisCounterInfo;
        }
        /// <summary>
        /// JisPartBoxInfo -> JisCounterInfo
        /// </summary>
        /// <param name="jisPartBoxInfo"></param>
        /// <param name="jisCounterInfo"></param>
        public static void GetJisCounterInfo(JisPartBoxInfo jisPartBoxInfo, ref JisCounterInfo jisCounterInfo)
        {
            if (jisPartBoxInfo == null) return;
            ///PART_BOX_FID
            jisCounterInfo.PartBoxFid = jisPartBoxInfo.Fid;
            ///PART_BOX_CODE
            jisCounterInfo.PartBoxCode = jisPartBoxInfo.PartBoxCode;
            ///PLANT
            jisCounterInfo.Plant = jisPartBoxInfo.Plant;
            ///PLANT_ZONE
            jisCounterInfo.PlantZone = jisPartBoxInfo.PlantZone;
            ///WORKSHOP
            jisCounterInfo.Workshop = jisPartBoxInfo.Workshop;
            ///ASSEMBLY_LINE
            jisCounterInfo.AssemblyLine = jisPartBoxInfo.AssemblyLine;
            ///WORKSHOP_SECTION
            jisCounterInfo.WorkshopSection = jisPartBoxInfo.WorkshopSection;
            ///LOCATION
            jisCounterInfo.Location = jisPartBoxInfo.Location;
            ///SUPPLIER_NUM
            jisCounterInfo.SupplierNum = jisPartBoxInfo.SupplierNum;
            ///ACCUMULATIVE_TYPE
            jisCounterInfo.AccumulativeType = jisPartBoxInfo.AccumulativeType;
            ///ACCUMULATIVE_QTY
            jisCounterInfo.AccumulativeQty = jisPartBoxInfo.AccumulativeQty;
            ///PACKAGE_MODEL
            jisCounterInfo.PackageModel = jisPartBoxInfo.PackageModel;
            ///COMMENTS
            jisCounterInfo.Comments = jisPartBoxInfo.Comments;
        }
        /// <summary>
        /// 根据零件类外键获取计数器
        /// </summary>
        /// <param name="partBoxFid"></param>
        /// <returns></returns>
        public JisCounterInfo GetInfoByPartBoxFid(Guid partBoxFid)
        {
            return dal.GetInfoByPartBoxFid(partBoxFid);
        }
        #endregion
    }
}

