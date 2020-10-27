using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
//using Infrustructure.Web;

namespace Infrustructure.Security
{
	public class MembershipManager
	{
		public const string SessionKey_CurrentUser = "SessionKey_CurrentUser";
		/// <summary>
		/// Get Current User
		/// </summary>
		/// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public static IUser CurrentUser
		{
			get
			{
				if (System.Web.HttpContext.Current == null)
				{
					return new AnonymousUser();
				}
				else
				{
					if (System.Web.HttpContext.Current.Session[SessionKey_CurrentUser] == null)
					{
						throw new AuthenticationException();
					}
					return (IUser)(System.Web.HttpContext.Current.Session[SessionKey_CurrentUser]);
				}
			}
		}

		public static int GetCurrentFirstMenuID()
		{
			// TODO:
			//IUser currentUser = UserUtil.GetCurrentUser();
			//if (currentUser == null) return -1;


			//List<MenuInfo> topMenuList = BLL.MenuManager.GetTopMenus(currentUser);
			//if (topMenuList == null || topMenuList.Count == 0)
			//{
			//    HttpContext.Current.Response.Write("<script language='javascript'>alert('你现在还没有任何访问权限。请与管理员联系。或按退出，用其它身份登录。');</script>");
			//    return -1;
			//}

			//return topMenuList[0].Menu_id;
			return -1;
		}
	}

	

}
