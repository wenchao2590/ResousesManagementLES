using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LES_PDA
{
    public class AuthorityUser
    {
        private string mPassWord;
        public string MPassWord
        {
            get { return mPassWord; }
            set { mPassWord = value; }
        }
         
        /// <summary>
        /// 用户名
        /// </summary>
        internal string mUserName;
     
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get { return mUserName; } }

        /// <summary>
        /// 失败原因
        /// </summary>
        internal string mNote;

        /// <summary>
        /// 失败原因
        /// </summary>
        public string Note { get { return mNote; } }


        /// <summary>
        /// 用户状态1/0
        /// </summary>
        internal int mStatus;
        /// <summary>
        /// 用户状态1/0
        /// </summary>
        public int Status { get { return mStatus; } }

        /// <summary>
        /// 对应资源值名称
        /// </summary>
        internal List<string> mResource;
        /// <summary>
        /// 对应资源值名称
        /// </summary>
        public List<string> Resource { get { return mResource; } }
        

        ////调用web 传入用户名，密码
        //public static AuthorityUser login(string userName, string password, out string message)
        //{
        //    message = "";
        //    AuthorityUser user = null;
        //    try
        //    {
        //        //开始调用web
        //        BC_PT_PDA.RPdaLogin.PublishLoginnfo rpdalogin = new BC_PT_PDA.RPdaLogin.PublishLoginnfo();
        //        rpdalogin.USERNAME = userName;
        //        rpdalogin.PASSWORD = password;

        //        BC_PT_PDA.RPdaLogin.ReturnPdaPublishLoginnfo[] returnLogin;

        //        BC_PT_PDA.RPdaLogin.RPdaMesLoginService rpdaloginSerice = new BC_PT_PDA.RPdaLogin.RPdaMesLoginService();

        //        returnLogin = rpdaloginSerice.PublishLogin(rpdalogin);

        //        user = new AuthorityUser();

        //        List<string> li = new List<string>();
        //        foreach (BC_PT_PDA.RPdaLogin.ReturnPdaPublishLoginnfo us in returnLogin)
        //        {
        //            user.mUserName = us.USERNAME;
        //            if (us.STATUS) { user.mStatus = 1; } else { user.mStatus = 0; }
        //            user.mNote = us.NOTE;
        //            if (us.RESOURCE != "")
        //            {
        //                li.Add(us.RESOURCE);
        //            }
        //        }
        //        //li.Add("冲压出库拣货$Cyckjh");
        //        user.mResource = li;
        //        message = user.Note;

        //        if (message != "") { return null; }
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //        return null;
        //    }
        //    return user;
        //}
    }
}
