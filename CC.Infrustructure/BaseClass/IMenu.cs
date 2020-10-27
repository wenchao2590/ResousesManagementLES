using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.BaseClass
{
    public interface IMenu
    {
        /// <summary>
        /// 获取顶级菜单
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //List<MenuInfo> GetTopMenus(IUser user);

        /// <summary>
        /// 获取指定的子菜单
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parentMenuID"></param>
        /// <returns></returns>
        //List<MenuInfo> GetSubMenus(IUser user, int parentMenuID);
    }
}
