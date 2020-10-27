using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class MenuActionBLL
    {
        MenuActionDAL dal = new MenuActionDAL();
        public long InsertInfo(MenuActionInfo info)
        {
            info.ValidFlag = true;
            return dal.Add(info);
        }
        public List<ActionInfo> GetActionByPageUrl(string pageUrl, Guid roleFid)
        {
            Guid menuFid = new MenuDAL().GetFid(pageUrl);
            if (menuFid == Guid.Empty) return new List<ActionInfo>();
            if (roleFid == Guid.Empty)
                return GetActionsByMenuRoleFid(menuFid);
            return GetActionsByMenuRoleFid(menuFid, roleFid);
        }

        public List<ActionInfo> GetActionsByMenuRoleFid(Guid menuFid)
        {
            List<MenuActionInfo> list = dal.GetList("and [VALID_FLAG] = 1 and [MENU_FID] = '" + menuFid + "'", string.Empty);
            if (list.Count == 0) return new List<ActionInfo>();
            List<ActionInfo> actionList = new ActionDAL().GetList("and [VALID_FLAG] = 1 "
                + "and [FID] in ('" + string.Join("','", list.Select(d => d.ActionFid.GetValueOrDefault()).ToArray()) + "')"
                , string.Empty);
            foreach (var actionInfo in actionList)
            {
                var info = list.SingleOrDefault(d => d.ActionFid == actionInfo.Fid);
                if (info == null) continue;
                actionInfo.DisplayOrder = info.ActionOrder.GetValueOrDefault();
                actionInfo.ClientJs = info.ClientJs;
                actionInfo.Fid = info.Fid;
            }
            return actionList.OrderBy(d => d.DisplayOrder).ToList();
        }

        public List<ActionInfo> GetActionsByMenuRoleFid(Guid menuFid, Guid roleFid)
        {
            List<MenuActionInfo> list = dal.GetList("and [MENU_FID] = '" + menuFid + "'", string.Empty);
            if (list.Count == 0) return new List<ActionInfo>();
            List<Guid> notNeedAuthFids = list.Where(d => !d.NeedAuth.GetValueOrDefault()).Select(d => d.ActionFid.GetValueOrDefault()).ToList();
            ///权限
            List<Guid> sourceFids
                = new RoleAuthDAL().GetAuthSourceFidsByRoleFidAuthTypeInSourceFids(roleFid
                , 2
                , "'" + string.Join("','", list.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "'");
            string actionFidCondition = string.Empty;
            if (sourceFids.Count > 0)
            {
                List<Guid> actionFids = list.Where(d => sourceFids.Contains(d.Fid.GetValueOrDefault())).Select(d => d.ActionFid.GetValueOrDefault()).ToList();
                if (actionFids.Count > 0)
                    actionFidCondition += ",'" + string.Join("','", actionFids.ToArray()) + "'";
            }
            if (notNeedAuthFids.Count > 0)
                actionFidCondition += ",'" + string.Join("','", notNeedAuthFids.ToArray()) + "'";
            if (string.IsNullOrEmpty(actionFidCondition))
                return new List<ActionInfo>();
            List<ActionInfo> actionList = new ActionDAL().GetList("and [FID] in (" + actionFidCondition.Substring(1) + ")"
                , string.Empty);
            foreach (var actionInfo in actionList)
            {
                var info = list.SingleOrDefault(d => d.ActionFid == actionInfo.Fid);
                if (info == null) continue;
                actionInfo.DisplayOrder = info.ActionOrder.GetValueOrDefault();
                actionInfo.ClientJs = info.ClientJs;
                actionInfo.Fid = info.Fid;
            }
            return actionList.OrderBy(d => d.DisplayOrder).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuFid">LIST菜单的FID</param>
        /// <param name="roleFid">角色FID</param>
        /// <param name="entityName">实体名，可能为FORM的实体名，用于定位弹出窗体的菜单项</param>
        /// <param name="formEditWidth">FORM的宽度</param>
        /// <param name="formEditHeight">FORM的高度</param>
        /// <param name="formUrl">FORM的URL</param>
        /// <returns></returns>
        public List<ActionInfo> GetActionByMenuFid(Guid menuFid, Guid roleFid, string entityName, out int formEditWidth, out int formEditHeight, out string formUrl)
        {
            ///out默认值
            formEditWidth = 800;
            formEditHeight = 480;
            formUrl = string.Empty;
            ///LIST菜单
            MenuInfo menuinfo = new MenuDAL().GetInfo(menuFid);
            if (menuinfo == null)
                throw new Exception("MC:0x00000028");///菜单数据错误

            ///弹出窗体，在菜单管理中限制同级菜单名称不能重复
            MenuInfo formMenuInfo = new MenuDAL().GetInfo(entityName, menuFid);
            if (formMenuInfo != null)
            {
                formEditWidth = formMenuInfo.EditFormWidth.GetValueOrDefault();
                formEditHeight = formMenuInfo.EditFormHeight.GetValueOrDefault();
                formUrl = formMenuInfo.LinkUrl;
            }
            ///所有按钮，后期按MENU_FID区分
            List<MenuActionInfo> menuAtions = dal.GetList("and [MENU_FID] in ('" + menuFid + "'" + (formMenuInfo == null ? string.Empty : ",'" + formMenuInfo.Fid.GetValueOrDefault() + "'") + ")", string.Empty);
            ///无按钮
            if (menuAtions.Count == 0) return new List<ActionInfo>();
            ///不需要授权页面按钮的FID
            List<Guid> menuActionFids = menuAtions.Where(d => !d.NeedAuth.GetValueOrDefault()).Select(d => d.Fid.GetValueOrDefault()).ToList();
            ///获取已授权的按钮GUID
            List<Guid> sourceFids = new RoleAuthDAL().GetAuthSourceFidsByRoleFidAuthTypeInSourceFids(roleFid
                , (int)AuthTypeConstants.ACTION///授权类型为按钮
                , menuAtions.Count(d => d.NeedAuth.GetValueOrDefault()) == 0 ? string.Empty : "'" + string.Join("','", menuAtions.Where(d => d.NeedAuth.GetValueOrDefault()).Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "'");
            ///可以显示的页面按钮
            menuActionFids.AddRange(sourceFids);
            if (menuActionFids.Count == 0) return new List<ActionInfo>();
            ///获取所有的动作按钮
            List<ActionInfo> actionList = new ActionDAL().GetList("[FID] in ('" + string.Join("','", menuAtions.Where(d => menuActionFids.Contains(d.Fid.GetValueOrDefault())).Select(d => d.ActionFid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            List<ActionInfo> actionInfos = new List<ActionInfo>();
            foreach (var menuActionFid in menuActionFids)
            {
                MenuActionInfo menuActionInfo = menuAtions.FirstOrDefault(d => d.Fid.GetValueOrDefault() == menuActionFid);
                if (menuActionInfo == null) continue;
                ActionInfo actionInfo = actionList.FirstOrDefault(d => d.Fid.GetValueOrDefault() == menuActionInfo.ActionFid.GetValueOrDefault()).Clone();
                if (actionInfo == null) continue;
                ///是否LIST的ACTION
                if (menuActionInfo.MenuFid.GetValueOrDefault() == menuFid) actionInfo.IsListAction = true;
                else actionInfo.IsListAction = false;
                actionInfo.DisplayOrder = menuActionInfo.ActionOrder.GetValueOrDefault();
                actionInfo.ClientJs = menuActionInfo.ClientJs;
                actionInfo.Fid = menuActionInfo.Fid;
                actionInfo.DetailFlag = menuActionInfo.DetailFlag.GetValueOrDefault();
                actionInfos.Add(actionInfo);
            }
            return actionInfos.OrderBy(d => d.DisplayOrder).ToList();
        }
        /// <summary>
        /// 只获取弹出窗体的授权动作
        /// </summary>
        /// <param name="menuFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="entityName"></param>
        /// <param name="formEditWidth"></param>
        /// <param name="formEditHeight"></param>
        /// <param name="formUrl"></param>
        /// <returns></returns>
        public List<ActionInfo> GetFormActions(Guid menuFid, Guid roleFid, string entityName, out int formEditWidth, out int formEditHeight, out string formUrl)
        {
            ///out默认值
            formEditWidth = 800;
            formEditHeight = 480;
            formUrl = string.Empty;
            ///弹出窗体，在菜单管理中限制同级菜单名称不能重复
            MenuInfo formMenuInfo = new MenuDAL().GetInfo(entityName, menuFid);
            if (formMenuInfo == null)
                throw new Exception("MC:0x00000028");///菜单数据错误

            ///窗体属性
            formEditWidth = formMenuInfo.EditFormWidth.GetValueOrDefault();
            formEditHeight = formMenuInfo.EditFormHeight.GetValueOrDefault();
            formUrl = formMenuInfo.LinkUrl;

            ///所有按钮，后期按MENU_FID区分
            List<MenuActionInfo> menuAtions = dal.GetList("[MENU_FID] = N'" + formMenuInfo.Fid.GetValueOrDefault() + "'", string.Empty);
            ///无按钮
            if (menuAtions.Count == 0) return new List<ActionInfo>();
            ///不需要授权页面按钮的FID
            List<Guid> menuActionFids = menuAtions.Where(d => !d.NeedAuth.GetValueOrDefault()).Select(d => d.Fid.GetValueOrDefault()).ToList();
            ///获取已授权的按钮GUID
            List<Guid> sourceFids = new RoleAuthDAL().GetAuthSourceFidsByRoleFidAuthTypeInSourceFids(roleFid
                , (int)AuthTypeConstants.ACTION///授权类型为按钮
                , menuAtions.Count(d => d.NeedAuth.GetValueOrDefault()) == 0 ? string.Empty : "'" + string.Join("','", menuAtions.Where(d => d.NeedAuth.GetValueOrDefault()).Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "'");
            ///可以显示的页面按钮
            menuActionFids.AddRange(sourceFids);
            if (menuActionFids.Count == 0) return new List<ActionInfo>();
            ///获取所有的动作按钮
            List<ActionInfo> actionList = new ActionDAL().GetList("[FID] in ('" + string.Join("','", menuAtions.Where(d => menuActionFids.Contains(d.Fid.GetValueOrDefault())).Select(d => d.ActionFid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            List<ActionInfo> actionInfos = new List<ActionInfo>();
            foreach (var menuActionFid in menuActionFids)
            {
                MenuActionInfo menuActionInfo = menuAtions.FirstOrDefault(d => d.Fid.GetValueOrDefault() == menuActionFid);
                if (menuActionInfo == null) continue;
                ActionInfo actionInfo = actionList.FirstOrDefault(d => d.Fid.GetValueOrDefault() == menuActionInfo.ActionFid.GetValueOrDefault()).Clone();
                if (actionInfo == null) continue;
                actionInfo.IsListAction = false;
                actionInfo.DisplayOrder = menuActionInfo.ActionOrder.GetValueOrDefault();
                actionInfo.ClientJs = menuActionInfo.ClientJs;
                actionInfo.Fid = menuActionInfo.Fid;
                actionInfo.DetailFlag = menuActionInfo.DetailFlag.GetValueOrDefault();
                actionInfos.Add(actionInfo);
            }
            return actionInfos.OrderBy(d => d.DisplayOrder).ToList();
        }

        public List<ActionInfo> GetCommonEditActionByMenuName(string menuName, Guid roleFid)
        {
            Guid menuFid = new MenuDAL().GetFidByMenuNameInType2(menuName);
            if (menuFid == Guid.Empty) return new List<ActionInfo>();
            return GetActionsByMenuRoleFid(menuFid, roleFid);
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<MenuActionInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
    }
}
