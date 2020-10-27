using System;
using System.Collections.Generic;
using System.Text;

using Infrustructure.Utilities;
using Infrustructure.Security;
using Infrustructure.Utilities.Exception;

namespace Infrustructure.BaseClass
{
    public class UserUtil
    {
        /// <summary>
        /// Get Current User
        /// </summary>
        /// <returns></returns>
        public static IUser GetCurrentUser()
        {
            if (System.Web.HttpContext.Current != null)
            {
                if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session[BasePage.SessionKey_CurrentUser] != null)
                {
                    return (IUser)(System.Web.HttpContext.Current.Session[BasePage.SessionKey_CurrentUser]);
                }
            }

            //下面这段代码,主要针对WCF情况下，client通过MessageHeader将当前用户名传递到Service
            string userName = Infrustructure.Service.ClientMessageInspector.GetHeadContent("UserName");

            if (string.IsNullOrEmpty(userName))
                throw new AuthenticationException("用户超时,请重新登陆.");

            IUser user = new AuthrationUser();
            user.UserLoginName = userName;

            return user;
        }

        public static string GetCurrentUserName()
        {
            try
            {
                IUser user = GetCurrentUser();
                if (user == null) return string.Empty;
                return user.UserLoginName;
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    public class AnonymousUser : IUser
    {
    }

    /// <summary>
    /// 该类用于WCF场景,用于service接收到MessageHeader以后，构造IUser的实现类，以衔接BLL层的用户访问代码
    /// </summary>
    public class AuthrationUser : IUser
    {
    }
}
