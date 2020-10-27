using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.SYS
{
    /// <summary>
    /// UserTokenBLL
    /// </summary>
    public class UserTokenBLL
    {
        /// <summary>
        /// UserTokenDAL
        /// </summary>
        UserTokenDAL dal = new UserTokenDAL();
        /// <summary>
        /// GetNewToken
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public string GetNewToken(Guid userFid)
        {
            ///TOKEN超时时间
            string userTokenExpireMinute = new ConfigDAL().GetValueByCode("USER_TOKEN_EXPIRE_MINUTE");
            if (!int.TryParse(userTokenExpireMinute, out int tokenExpireMinute))
                tokenExpireMinute = 120;
            ///获取有无未过期的TOKEN
            UserTokenInfo info = new UserTokenDAL().GetInfo(userFid);
            if (info == null)
            {
                info = new UserTokenInfo();
                info.Fid = Guid.NewGuid();
                
                ///GUID去掉-
                info.Token = info.Fid.GetValueOrDefault().ToString().Replace("-", string.Empty);
                ///
                info.DisableDate = DateTime.Now.AddMinutes(tokenExpireMinute);
                info.CreateDate = DateTime.Now;
                info.CreateUser = "#TOKEN_CREATER";
                info.ValidFlag = true;
                if (dal.Add(info) == 0)
                    return string.Empty;
                return info.Token;
            }
            ///
            dal.UpdateInfo("" +
                "[DISABLE_DATE] = N'" + DateTime.Now.AddMinutes(tokenExpireMinute) + "'," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'#TOKEN_CREATER'",
                info.Id);
            return info.Token;
        }
        /// <summary>
        /// 获取USER_FID
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Guid GetUserFid(string token)
        {
            ///获取有效令牌
            UserTokenInfo info = dal.GetInfo(token);
            if (info == null) return Guid.Empty;
            if (DateTime.Now < info.DisableDate.GetValueOrDefault())
                return info.Fid.GetValueOrDefault();
            dal.UpdateInfo("[VALID_FLAG] = 0,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'#VALID_TOKEN'", info.Id);
            throw new Exception("MC:0x00000092");///登录超时,请重新登录！
        }
        /// <summary>
        /// 是否超时
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsTimeOut(string token)
        {
            ///TOKEN超时时间
            int timeOutMinute = 10;
            UserTokenInfo info = dal.GetInfo(token);
            if (info == null) return true;
            TimeSpan ts = DateTime.Now - info.ModifyDate.GetValueOrDefault();
            ///已超时
            if (ts.TotalMinutes > timeOutMinute)
            {
                dal.UpdateInfo("[DISABLE_DATE]=GETDATE(),[VALID_FLAG]=0", info.Id);
                return true;
            }
            else
            {
                if (dal.UpdateInfo("[MODIFY_DATE]=GETDATE()", info.Id) == 0)
                    return true;
            }
            return false;
        }
    }
}
