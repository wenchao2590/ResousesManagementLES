using DAL.SYS;
using DM.SYS;
using Infrustructure.BaseClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class ActionBLL
    {
        #region Common
        ActionDAL dal = new ActionDAL();
        public List<ActionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public long InsertInfo(ActionInfo info)
        {
            ///验证ACTION_NAME不能重复
            if (dal.IsExistActionName(info.ActionName))
                throw new Exception("MC:0x00000001"); ///Action Name is Existed!
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
        public ActionInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        #region MenuAction
        /// <summary>
        /// 根据菜单FID获取ACTION及配置信息
        /// 用于MENU_ACTION的维护
        /// </summary>
        /// <param name="menuFid"></param>
        /// <returns></returns>
        public List<ActionInfo> GetActionListByMenuFid(Guid menuFid, string textWhere, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            ///获取全部Action
            dataCount = dal.GetCounts("and [VALID_FLAG] = 1 " + textWhere);
            return dal.GetListByPage(menuFid, textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 菜单添加功能
        /// </summary>
        /// <param name="menuFid"></param>
        /// <param name="actionFid"></param>
        /// <param name="clientJs"></param>
        /// <param name="actionOrder"></param>
        /// <param name="needAuth"></param>
        /// <param name="detailFlag"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool SetMenuAction(Guid menuFid, Guid actionFid, string clientJs, int actionOrder, bool needAuth, bool detailFlag, string loginUser)
        {
            MenuActionDAL menuActionDal = new MenuActionDAL();
            ///查看关系是否已存在
            MenuActionInfo menuActionInfo = menuActionDal.GetInfo(menuFid, actionFid);
            if (menuActionInfo == null)
            {
                menuActionInfo = new MenuActionInfo();
                menuActionInfo.Fid = Guid.NewGuid();
                menuActionInfo.MenuFid = menuFid;
                menuActionInfo.ActionFid = actionFid;
                menuActionInfo.ClientJs = clientJs;
                menuActionInfo.ActionOrder = actionOrder;
                menuActionInfo.NeedAuth = needAuth;
                menuActionInfo.DetailFlag = detailFlag;
                menuActionInfo.ValidFlag = true;
                menuActionInfo.CreateDate = DateTime.Now;
                menuActionInfo.CreateUser = loginUser;
                return menuActionDal.Add(menuActionInfo) > 0 ? true : false;
            }
            menuActionInfo.ClientJs = clientJs;
            menuActionInfo.ActionOrder = actionOrder;
            menuActionInfo.NeedAuth = needAuth;
            menuActionInfo.DetailFlag = detailFlag;
            menuActionInfo.ModifyDate = DateTime.Now;
            menuActionInfo.ModifyUser = loginUser;
            return menuActionDal.Update(menuActionInfo) > 0 ? true : false;
            ///
        }
        /// <summary>
        /// 菜单上移除功能
        /// </summary>
        /// <param name="menuFid"></param>
        /// <param name="actionFid"></param>
        /// <returns></returns>
        public bool ClearMenuAction(Guid menuFid, Guid actionFid, string loginUser)
        {
            ///TODO:需要删除已授权的记录
            return new MenuActionDAL().LogicDelete(menuFid, actionFid, loginUser);
        }
        #endregion


        /// <summary>
        /// 获取客户端按钮根据菜单动作按钮
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public DataTable GetClientMenusActionByUser(Guid userFid)
        {
            ///授权
            List<Guid> sourceFids = new RoleAuthDAL().GetSourceFidsByUserFid(userFid, (int)AuthTypeConstants.MENU);
            if (sourceFids.Count == 0)
                return dal.GetMenuActionByUser("XC.[MENU_TYPE] = " + (int)MenuTypeConstants.ClientMenu + " and XC.[NEED_AUTH] = 0");
            return dal.GetMenuActionByUser("XC.[MENU_TYPE] = " + (int)MenuTypeConstants.ClientMenu + " and (XC.[FID] in ('" + string.Join("','", sourceFids.ToArray()) + "') or XC.[NEED_AUTH] = 0)");
        }
    }
}
