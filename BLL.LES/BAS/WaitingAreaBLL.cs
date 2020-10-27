
using BLL.LES;
using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class WaitingAreaBLL
    {
        #region Common
        WaitingAreaDAL dal = new WaitingAreaDAL();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<WaitingAreaInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public WaitingAreaInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 验证重复，添加实体
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(WaitingAreaInfo info)
        {
            // 等待区名称②不允许重复，必填项
            int waitingAreaCnt = dal.GetCounts("[AREA_NAME] = N'" + info.AreaName + "'");
            if (waitingAreaCnt > 0)
                throw new Exception("Err_:MC:0x00000685"); /// :等待区名称不允许重复 
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            ///若道口已配置过此等待区，则不允许删除
            int dockCnt = new DockDAL().GetCounts("[AREA_ID] = N'" + id + "' ");
            if (dockCnt > 0)
                throw new Exception("Err_:MC:0x00000694");///该等待区下有绑定道口信息,无法删除
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
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

