using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.BaseClass
{
    public interface IMenu
    {
        /// <summary>
        /// ��ȡ�����˵�
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //List<MenuInfo> GetTopMenus(IUser user);

        /// <summary>
        /// ��ȡָ�����Ӳ˵�
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parentMenuID"></param>
        /// <returns></returns>
        //List<MenuInfo> GetSubMenus(IUser user, int parentMenuID);
    }
}
