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

            //������δ���,��Ҫ���WCF����£�clientͨ��MessageHeader����ǰ�û������ݵ�Service
            string userName = Infrustructure.Service.ClientMessageInspector.GetHeadContent("UserName");

            if (string.IsNullOrEmpty(userName))
                throw new AuthenticationException("�û���ʱ,�����µ�½.");

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
    /// ��������WCF����,����service���յ�MessageHeader�Ժ󣬹���IUser��ʵ���࣬���ν�BLL����û����ʴ���
    /// </summary>
    public class AuthrationUser : IUser
    {
    }
}
