using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class MeasuringUnitBLL
    {
        #region Common
        MeasuringUnitDAL dal = new MeasuringUnitDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<MeasuringUnitInfo></returns>
        public List<MeasuringUnitInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MeasuringUnitInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(MeasuringUnitInfo info)
        {
            int cnt = dal.GetCounts("[MEASURING_UNIT_NO] = N'" + info.MeasuringUnitNo + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000106");///计量单位编码不允许重复

            //cnt = dal.GetCounts("[MEASURING_UNIT_NAME] = N'" + info.MeasuringUnitName + "'");
            //if (cnt > 0)
            //    throw new Exception("MC:0x00000107");///计量单位名称不允许重复
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
            int cnt = new MaintainPartsDAL().GetCounts("[PART_UNITS] in (select [MEASURING_UNIT_NO] from LES.[TM_BAS_MEASURING_UNIT] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000108");///计量单位已在材料档案中使用，不能删除
            return dal.Delete(id) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string measuringUnitNo = CommonBLL.GetFieldValue(fields, "MEASURING_UNIT_NO");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [MEASURING_UNIT_NO] = N'" + measuringUnitNo + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000106");///计量单位编码不允许重复

            //string measuringUnitName = CommonBLL.GetFieldValue(fields, "MEASURING_UNIT_NAME");
            //cnt = dal.GetCounts("[ID] <> " + id + " and [MEASURING_UNIT_NAME] = N'" + measuringUnitName + "'");
            //if (cnt > 0)
            //    throw new Exception("MC:0x00000107");///计量单位名称不允许重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

