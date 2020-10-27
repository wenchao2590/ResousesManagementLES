//using Contract.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.SYS;
using DAL.SYS;
using System.Data;

namespace BLL.SYS
{
    /// <summary>
    /// MenuBLL
    /// </summary>
    public partial class MenuBLL
    {
        #region Common
        /// <summary>
        /// MenuDAL
        /// </summary>
        MenuDAL dal = new MenuDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<MenuInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public long InsertInfo(MenuInfo info)
        {
            info.ValidFlag = true;
            return dal.Add(info);
        }
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        public MenuInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        /// <summary>
        /// 获取客户端菜单
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public List<MenuInfo> GetClientMenus(Guid userFid)
        {
            ///授权
            List<Guid> sourceFids = new RoleAuthDAL().GetSourceFidsByUserFid(userFid, (int)AuthTypeConstants.MENU);
            if (sourceFids.Count == 0)
                return dal.GetList("[MENU_TYPE] = " + (int)MenuTypeConstants.ClientMenu + " and [NEED_AUTH] = 0", "[DISPLAY_ORDER]");
            return dal.GetList("[MENU_TYPE] = " + (int)MenuTypeConstants.ClientMenu + " and ([FID] in ('" + string.Join("','", sourceFids.ToArray()) + "') or [NEED_AUTH] = 0)", "[DISPLAY_ORDER]");
        }
        /// <summary>
        /// 根据角色获取菜单项
        /// </summary>
        /// <param name="roleFid"></param>
        /// <returns></returns>
        public List<MenuInfo> GetMenusByRoleFid(Guid roleFid)
        {
            ///权限
            List<Guid> sourceFids = new RoleAuthDAL().GetSourceFidsByRoleFid(roleFid, (int)AuthTypeConstants.MENU);
            if (sourceFids.Count == 0)
                return dal.GetList("[MENU_TYPE] in (" + (int)MenuTypeConstants.WebModule + "," + (int)MenuTypeConstants.WebMenu + ") and [NEED_AUTH] = 0", "[DISPLAY_ORDER]");
            return dal.GetList("[MENU_TYPE] in (" + (int)MenuTypeConstants.WebModule + "," + (int)MenuTypeConstants.WebMenu + ") and ([FID] in ('" + string.Join("','", sourceFids.ToArray()) + "') or [NEED_AUTH] = 0)", "[DISPLAY_ORDER]");
        }

        public List<MenuInfo> GetClientMenusByRoleFid(Guid roleFid)
        {
            ///权限
            List<Guid> sourceFids = new RoleAuthDAL().GetSourceFidsByRoleFid(roleFid, 1);
            if (sourceFids.Count == 0) return new List<MenuInfo>();
            ///仅获取WEB菜单
            return dal.GetList("and [VALID_FLAG] <> 0 and [MENU_TYPE] in (30) "
                + "and [FID] in ('" + string.Join("','", sourceFids.ToArray()) + "')", "[DISPLAY_ORDER]");
        }

        public MenuInfo GetInfo(Guid fid)
        {
            return dal.GetInfo(fid);
        }
        /// <summary>
        /// GetInfoByLinkUrl
        /// </summary>
        /// <param name="linkUrl"></param>
        /// <returns></returns>
        public MenuInfo GetInfo(string linkUrl)
        {
            return dal.GetInfo(linkUrl);
        }

        public MenuInfo GetInfo(string menuName, Guid parentMenuFid)
        {
            return dal.GetInfo(menuName, parentMenuFid);
        }
    }
}