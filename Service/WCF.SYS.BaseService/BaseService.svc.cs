using BLL.SYS;
using DM.SYS;
using Infrustructure.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WCF.SYS.BaseService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    public class BaseService : IBaseService
    {
        public string DoFunction(string functionCode, string info)
        {
            if (string.IsNullOrEmpty(functionCode))
                functionCode = string.Empty;
            if (string.IsNullOrEmpty(info))
                info = string.Empty;
            BaseDataInfo basedata = JsonConvert.DeserializeObject<BaseDataInfo>(info);
            BaseDataInfo resultdata = new BaseDataInfo();
            switch (functionCode.ToLower())
            {
                case "login": resultdata = Login(basedata); break;
                case "getmenu": resultdata = GetMenu(basedata); break;
            }
            return JsonConvert.SerializeObject(resultdata);
        }
        /// <summary>
        /// 登录获取TOKEN
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo Login(BaseDataInfo info)
        {
            string userName = info.GetImportData("username");
            string passWord = info.GetImportData("password");
            UserInfo user = new UserBLL().Login(userName, passWord);
            if (user == null)
            {
                info.Result = false;
                info.ErrCode = "0x00000129";
                info.Msg = "登录失败";
            }
            else
            {
                info.Result = true;
                info.ErrCode = string.Empty;
                info.Msg = string.Empty;
                ///登录后的TOKEN
                info.Token = new UserTokenBLL().GetNewToken(user.Id);
            }
            return info;
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetMenu(BaseDataInfo info)
        {
            if (string.IsNullOrEmpty(info.Token))
            {
                info.Result = false;
                info.ErrCode = "0x00000000";
                info.Msg = "未登录或登录超时";
                return info;
            }
            long userId = new UserTokenBLL().GetUserFid(info.Token);
            if (userId == 0)
            {
                info.Result = false;
                info.ErrCode = "0x00000000";
                info.Msg = "未登录或登录超时";
                return info;
            }
            List<MenuInfo> menus = new MenuBLL().GetAppMenus(userId);
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("DIsplayOrder");
            dt.Columns.Add("MenuName");
            dt.Columns.Add("IconUrl");
            dt.Columns.Add("FunctionUrl");
            foreach (var menu in menus)
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = menu.Id;
                dr["DIsplayOrder"] = menu.DisplayOrder;
                if (info.Language.ToLower() == "zh-cn")
                    dr["MenuName"] = menu.MenuNameCn;
                else
                    dr["MenuName"] = menu.MenuName;
                dr["IconUrl"] = menu.FavoritePic;
                dr["FunctionUrl"] = menu.LinkUrl;
                dt.Rows.Add(dr);
            }
            info.Tables.Add("Menu", dt);
            return info;
        }
    }
}
