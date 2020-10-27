using BLL.SYS;
using DM.SYS;
using BLL.LES;
using DM.LES;
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
using Infrustructure.Utilities;
using System.Net;
using System.IO;
using System.Configuration;

namespace WS.PDA.BaseService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    public class BaseService : IBaseService
    {
        /// <summary>
        /// 是否数据中心部署方式
        /// </summary>
        private string dataCenterFlag = ConfigurationManager.AppSettings["dataCenter"];
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetRequest(string url)
        {
            CookieContainer cc = new CookieContainer();
            //制定编码格式
            var encoding = Encoding.GetEncoding("utf-8");
            //url的设置
            var request = (HttpWebRequest)WebRequest.Create(url);
            //设置请求的方式
            request.Method = "GET";
            request.CookieContainer = cc;
            request.Timeout = 30000;
            //设置Content-Type 的值
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();
            var srContent = new StreamReader(response.GetResponseStream(), encoding);
            //获取抓取下来的页面内容
            var strPage = srContent.ReadToEnd();
            response.Close();
            srContent.Close();
            return strPage;
        }

        #region 入口方法
        public string DoFunction(string functionCode, string info)
        {
            if (string.IsNullOrEmpty(functionCode))
                functionCode = string.Empty;
            if (string.IsNullOrEmpty(info))
                info = string.Empty;
            BaseDataInfo basedata = null;
            BaseDataInfo resultdata = null;
            try
            {
                basedata = JsonConvert.DeserializeObject<BaseDataInfo>(info);
            }
            catch
            {
                basedata.Msg = GetMessage(basedata.Language, basedata.ErrCode = "3x00000001");///传入参数序列化失败
                return JsonConvert.SerializeObject(basedata);
            }

            string dataServiceUrl = string.Empty;
            ///如果是数据中心部署方式
            if (dataCenterFlag.ToLower() == "true")
            {
                ///如果是登录
                if (functionCode.ToLower() == "login" || functionCode.ToLower() == "mobileregister")
                {
                    resultdata = LoginDataCenter(basedata);
                    if (!resultdata.Result)
                        return JsonConvert.SerializeObject(resultdata);
                    dataServiceUrl = resultdata.DataServiceUrl + "/DoFunction?functionCode=" + functionCode + "&info=" + info;
                }
                else
                    dataServiceUrl = basedata.DataServiceUrl + "/DoFunction?functionCode=" + functionCode + "&info=" + info;
                return GetRequest(dataServiceUrl);
            }

            switch (functionCode.ToLower())
            {
                case "login": resultdata = Login(basedata); break;
                case "mobileregister": resultdata = MobileRegister(basedata); break;
                case "getkanbancard": resultdata = GetKanbanCard(basedata); break;
                case "getmessageforpda": resultdata = GetMessageForPDA(basedata); break;
                case "createkanbanpullorder": resultdata = CreateKanbanPullOrder(basedata); break;
                case "getbarcode": resultdata = GetBarcode(basedata); break;
                case "getreceiveandoutorder": resultdata = GetReceiveAndOutOrder(basedata); break;
                case "submitwarehouseorder": resultdata = SubmitWarehouseOrder(basedata); break;
                case "getoutorder": resultdata = GetOutOrder(basedata); break;
                case "getreceiveorder": resultdata = GetReceiveOrder(basedata); break;
                case "finishreceive": resultdata = FinishReceive(basedata); break;
                case "pickupbarcode": resultdata = PickupBarcode(basedata); break;
                case "finishpickup": resultdata = FinishPickup(basedata); break;
                case "getcodeitems": resultdata = GetCodeItems(basedata); break;
                case "getwarehouses": resultdata = GetWarehouses(basedata); break;
                case "getzones": resultdata = GetZones(basedata); break;
                case "getbarcodestatus": resultdata = GetBarcodeStatus(basedata); break;
                case "getrunsheetstatus": resultdata = GetRunsheetStatus(basedata); break;
                case "undobarcodestatus": resultdata = UndoBarcodeStatus(basedata); break;
                case "batchdeletingbarcode": resultdata = BatchDeletingBarcode(basedata); break;
                case "getvmipullorderbyordercode": resultdata = GetVmiPullOrderByOrderCode(basedata); break;
                case "addintoshippingpart": resultdata = AddIntoShippingPart(basedata); break;
                case "getvmishippingpartinfosbyloginuser": resultdata = GetVmiShippingPartInfosByLoginUser(basedata); break;
                case "batchdeletingbyids": resultdata = BatchDeletingByIds(basedata); break;
                case "predelivery": resultdata = PreDelivery(basedata); break;//2018/07/09新增
                case "submitpickupdata": resultdata = SubmitPickupData(basedata); break;


                default: resultdata = GetNoFindFunction(basedata); break;
            }
            return JsonConvert.SerializeObject(resultdata);
        }

        /// <summary>
        /// 未找到执行方法默认调用
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetNoFindFunction(BaseDataInfo info)
        {
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000002");///未找到处理方法
            return info;
        }
        #endregion

        #region 获取系统配置提示信息
        private List<MessageInfo> msglist = null;
        private List<ConfigInfo> configlist = null;
        public BaseService()
        {
            msglist = new MessageBLL().GetList(string.Empty, string.Empty);
            if (msglist == null) msglist = new List<MessageInfo>();
            configlist = new ConfigBLL().GetList(string.Empty, string.Empty);
            if (configlist == null) configlist = new List<ConfigInfo>();
        }
        /// <summary>
        /// 根据信息提示代码获取信息文本
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessage(string lang, string code)
        {
            if (code.StartsWith("Err_:"))
                code = code.Replace("Err_:", string.Empty);
            if (code.StartsWith("MC:"))
                code = code.Replace("MC:", string.Empty);
            MessageInfo messageInfo = msglist.FirstOrDefault(m => m.MessageCode == code);
            if (messageInfo == null)
                return code;
            if (lang.ToLower() == "en-us")
                return messageInfo.MessageEn;
            return messageInfo.MessageCn;
        }
        /// <summary>
        /// 根据配置代码 获取代码对应值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetConfigInfoConfigValueByCode(string code)
        {
            ConfigInfo info = configlist.FirstOrDefault(w => w.Code == code);
            if (info == null)
                return string.Empty;
            else
                return info.ConfigValue;
        }
        /// <summary>
        ///获取所有PDA提示消息
        /// </summary>
        /// <param name="info"></param>
        /// <returns>返回listjson字符串</returns>
        public BaseDataInfo GetMessageForPDA(BaseDataInfo info)
        {
            string jsonData = new BLL.SYS.MessageBLL().GetListJsonMessageForPDA();
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");
            info.Export.Add("Info", jsonData);

            return info;
        }
        /// <summary>
        /// 获取系统代码项描述
        /// </summary>
        /// <param name="codeName"></param>
        /// <param name="codeItemValue"></param>
        /// <param name="codeItemInfos"></param>
        /// <returns></returns>
        private string GetCodeItemName(string codeName, int codeItemValue, List<CodeItemInfo> codeItemInfos)
        {
            CodeItemInfo codeItemInfo = codeItemInfos.FirstOrDefault(d => d.CodeName == codeName && d.ItemValue.GetValueOrDefault() == codeItemValue);
            if (codeItemInfo == null) return codeItemValue.ToString();
            return codeItemInfo.ItemName;
        }
        #endregion

        #region 登录和获取菜单
        /// <summary>
        /// 设备注册
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo MobileRegister(BaseDataInfo info)
        {
            string loginUser = info.LoginUser;
            string imei = info.GetImportData("imei");
            string imsi = info.GetImportData("imsi");
            string model = info.GetImportData("model");
            string vendor = info.GetImportData("vendor");
            string uuid = info.GetImportData("uuid");
            string os_language = info.GetImportData("os_language");
            string os_version = info.GetImportData("os_version");
            string os_name = info.GetImportData("os_name");
            string os_vendor = info.GetImportData("os_vendor");
            UserMobileInfo userMobileInfo = new UserMobileInfo();
            userMobileInfo.Fid = Guid.NewGuid();
            userMobileInfo.UserFid = new UserBLL().GetFid(loginUser);
            userMobileInfo.UserName = loginUser;
            userMobileInfo.Imei = imei;
            userMobileInfo.Imsi = imsi;
            userMobileInfo.Model = model;
            userMobileInfo.Vendor = vendor;
            userMobileInfo.Uuid = uuid;
            userMobileInfo.OsLanguage = os_language;
            userMobileInfo.OsVersion = os_version;
            userMobileInfo.OsName = os_name;
            userMobileInfo.OsVendor = os_vendor;
            userMobileInfo.Status = 10;
            userMobileInfo.ValidFlag = true;
            userMobileInfo.CreateUser = loginUser;
            userMobileInfo.CreateDate = DateTime.Now;
            try
            {
                new UserMobileBLL().InsertInfo(userMobileInfo);
            }
            catch (Exception ex)
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = ex.Message);
                return info;
            }
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");///操作成功
            return info;
        }
        /// <summary>
        /// 登录数据中心
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo LoginDataCenter(BaseDataInfo info)
        {
            ///用户名密码
            string userName = info.LoginUser;
            string passWord = info.GetImportData("password");
            UserInfo user = null;
            try
            {
                user = new UserBLL().Login(userName, passWord);
                info.DataServiceUrl = user.DataServiceUrl;
            }
            catch (Exception ex)
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = ex.Message);///登录失败
                return info;
            }
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "1x00000032");///登录成功
            return info;
        }
        /// <summary>
        /// 登录获取TOKEN
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo Login(BaseDataInfo info)
        {
            ///用户名密码
            string userName = info.LoginUser;
            string passWord = info.GetImportData("password");
            string uuid = info.GetImportData("uuid");
            UserInfo user = null;
            try
            {
                user = new UserBLL().Login(userName, passWord);
                info.DataServiceUrl = user.DataServiceUrl;
            }
            catch (Exception ex)
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = ex.Message);///登录失败
                return info;
            }
            if (user == null)
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000129");///登录失败
                return info;
            }
            try
            {
                int cnt = new UserMobileBLL().GetCounts("[UUID] = N'" + uuid + "' and [USER_FID] = N'" + user.Fid.GetValueOrDefault() + "' and [STATUS] = 20");
                if (cnt == 0)
                {
                    info.Result = false;
                    info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000025");///该设备未注册或审批未通过
                    return info;
                }
            }
            catch (Exception ex)
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = ex.Message);///登录失败
                return info;
            }
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "1x00000032");///登录成功

            ///登录后的TOKEN
            info.Token = new UserTokenBLL().GetNewToken(user.Fid.GetValueOrDefault());
            if (string.IsNullOrEmpty(info.Token))
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000129");///登录失败
                return info;
            }
            ///获取菜单项
            info.Tables.Add("Menu", JsonHelper.DataTableToJson(GetMenu(user.Fid.GetValueOrDefault())));
            ///获取动作
            info.Tables.Add("Action", JsonHelper.DataTableToJson(GetAction(user.Fid.GetValueOrDefault())));
            ///获取角色
            info.Tables.Add("Role", JsonHelper.DataTableToJson(GetRoles(user.Fid.GetValueOrDefault())));
            ///传出Token令牌
            info.Export.Add("Token", info.Token);
            ///传出UserFid
            info.Export.Add("UserId", user.Fid.GetValueOrDefault().ToString());

            info.RoleFid = Guid.Parse(GetRoles(user.Fid.GetValueOrDefault()).Rows[0][0].ToString());
            info.UserFid = (Guid)user.Fid.GetValueOrDefault();

            return info;
        }
        /// <summary>
        /// 校验TOKEN
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool ValidToken(ref BaseDataInfo info)
        {
            info.Result = true;
            ///TOKEN空
            if (string.IsNullOrEmpty(info.Token))
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000092");///登录超时,请重新登录！
            }
            ///根据TOKEN获取USER_ID
            info.UserFid = new UserTokenBLL().GetUserFid(info.Token);
            if (info.UserFid == Guid.Empty)
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000092");///登录超时,请重新登录！
            }
            return info.Result;
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private DataTable GetMenu(Guid userFid)
        {
            List<MenuInfo> menus = new MenuBLL().GetClientMenus(userFid);
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("DIsplayOrder");
            dt.Columns.Add("MenuNameCn");
            dt.Columns.Add("MenuNameEn");
            dt.Columns.Add("IconUrl");
            dt.Columns.Add("FunctionUrl");
            foreach (var menu in menus)
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = menu.Id;
                dr["DIsplayOrder"] = menu.DisplayOrder.GetValueOrDefault();
                dr["MenuNameCn"] = menu.MenuNameCn;
                dr["MenuNameEn"] = menu.MenuName;
                dr["IconUrl"] = menu.FavoritePic;
                dr["FunctionUrl"] = menu.LinkUrl;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        private DataTable GetRoles(Guid userFid)
        {
            DataTable dt = new DataTable();
            ///用户角色
            List<UserRoleInfo> userRoleInfos = new UserBLL().GetRolesByUser(userFid, string.Empty, 1, int.MaxValue, out int dataCnt);
            if (userRoleInfos.Count == 0) return dt;
            ///获取角色
            List<RoleInfo> roleInfos = new RoleBLL().GetList("[FID] in ('" + string.Join("','", userRoleInfos.Select(d => d.RoleFid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (roleInfos.Count == 0) return dt;
            ///
            dt.Columns.Add("RoleFid");
            dt.Columns.Add("RoleName");
            foreach (var roleInfo in roleInfos)
            {
                DataRow dr = dt.NewRow();
                dr["RoleFid"] = roleInfo.Fid.GetValueOrDefault();
                dr["RoleName"] = roleInfo.RoleName;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 获取功能
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        private DataTable GetAction(Guid userFid)
        {
            return new ActionBLL().GetClientMenusActionByUser(userFid);
        }
        /// <summary>
        /// 获取枚举项
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetCodeItems(BaseDataInfo info)
        {
            string codeName = info.GetImportData("codeName");
            List<string> codeNames = new List<string>() { codeName };
            List<CodeItemInfo> codeItemInfos = new CodeItemBLL().GetListByCodeNames(codeNames);
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");
            info.Tables.Add("CodeItems", JsonHelper.ToJson(codeItemInfos));
            return info;
        }
        #endregion

        #region MPM-004 看板卡发料中心
        /// <summary>
        /// MPM-005 获取看板卡信息
        /// </summary>
        /// <param name="CardNo">看板号</param>
        /// <returns>返回 KanbanCardInfo 对象</returns>
        private BaseDataInfo GetKanbanCard(BaseDataInfo info)
        {
            /// TOKEN校验
            if (!ValidToken(ref info)) return info;
            ///获取传入参数中的看板卡号
            string cardNo = info.GetImportData("cardno");
            string jsonData = string.Empty;
            KanbanCardInfo kanbanCardInfo = null;
            try
            {
                kanbanCardInfo = new KanbanCardBLL().SelectInfoByCardNo(cardNo, info.LoginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }

            jsonData = JsonConvert.SerializeObject(kanbanCardInfo);
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000003");///扫描成功
            info.Export.Add("Info", jsonData);
            return info;
        }
        /// <summary>
        /// MPM-006 看板拉动单生成
        /// </summary>
        public BaseDataInfo CreateKanbanPullOrder(BaseDataInfo info)
        {
            /// TOKEN校验 每个方法必须调用
            if (!ValidToken(ref info)) return info;
            ///方法体调用时已try-catch 包裹
            try
            {
                string cardNos = info.GetImportData("cardnos");
                List<string> cardNoList = JsonConvert.DeserializeObject<List<string>>(cardNos);
                info.Result = new KanbanPullOrderBLL().CreateKanbanPullOrder(cardNoList, info.LoginUser, true);
                if (info.Result)
                    info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");
                else
                    info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000173");
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = exMsg;
            }
            ///最后返回已赋值的 BaseDataInfo 
            return info;
        }
        #endregion

        #region WMM-003/WMM-021 物料收货/物料交接
        /// <summary>
        /// 收货完成
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo FinishReceive(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string receiveId = info.GetImportData("orderid");
            ///1入库单2出库单
            string inoutFlag = info.GetImportData("inoutflag");
            string loginUser = info.LoginUser;
            try
            {
                if (inoutFlag == "1")
                    new ReceiveBLL().CompleteInfos(new List<string>() { receiveId }, loginUser);
                if (inoutFlag == "2")
                    new OutputBLL().CloseInfos(new List<string>() { receiveId }, loginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");///操作成功
            return info;
        }
        /// <summary>
        /// 扫描入库单获取入库单信息
        /// 同时支持出库单扫描获取信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetReceiveOrder(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string jsonData = string.Empty;
            string orderNo = info.GetImportData("orderno");
            ///首先根据单号获取出库单
            OutputInfo outputInfo = new OutputBLL().GetInfo(orderNo);
            ReceiveInfo receiveInfo = null;
            ///
            if (outputInfo == null)
            {
                ///出库单没有被发现时获取入库单
                receiveInfo = new ReceiveBLL().GetInfo(orderNo);
                ///
                if (receiveInfo == null)
                {
                    info.Result = false;
                    info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000012");///单号不存在或单号状态不为已发布
                    return info;
                }
                ///
                if (receiveInfo.Status.GetValueOrDefault() != (int)ReceiveOrderStatusConstants.PUBLISHED)
                {
                    info.Result = false;
                    info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000735");///入库单必须为已发布状态
                    return info;
                }
                ///1标识入库单
                receiveInfo.InoutFlag = 1;
            }
            else
            {
                if (outputInfo.Status.GetValueOrDefault() != (int)OutputOrderStatusConstants.Published)
                {
                    info.Result = false;
                    info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000735");///入库单必须为已发布状态
                    return info;
                }
                receiveInfo = new ReceiveBLL().GetReceiveInfoByOutputInfo(outputInfo);
                ///2标识出库单
                receiveInfo.InoutFlag = 2;
            }
            ///
            List<string> codeNames = new List<string>() { "INBOUND_TYPE", "INSPECTION_MODE", "OUTBOUND_TYPE" };
            List<CodeItemInfo> codeItemInfos = new CodeItemBLL().GetListByCodeNames(codeNames);
            if (receiveInfo.InoutFlag == 1)
                receiveInfo.ReceiveTypeName = GetCodeItemName("INBOUND_TYPE", receiveInfo.ReceiveType.GetValueOrDefault(), codeItemInfos);
            if (receiveInfo.InoutFlag == 2)
                receiveInfo.ReceiveTypeName = GetCodeItemName("OUTBOUND_TYPE", receiveInfo.ReceiveType.GetValueOrDefault(), codeItemInfos);
            if (receiveInfo.InspectionMode != null)
                receiveInfo.InspectionModeName = GetCodeItemName("INSPECTION_MODE", receiveInfo.InspectionMode.GetValueOrDefault(), codeItemInfos);
            receiveInfo.OrganizationName = new OrganizationBLL().GetNameByFid(receiveInfo.OrganizationFid.GetValueOrDefault());
            ///WAREHOUSE
            List<string> wmNos = new List<string>() { receiveInfo.WmNo, receiveInfo.SourceWmNo };
            List<WarehouseInfo> warehouseInfos = new WarehouseBLL().GetListForInterfaceDataSync(wmNos);
            WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == receiveInfo.WmNo);
            receiveInfo.WmName = warehouseInfo == null ? string.Empty : warehouseInfo.WarehouseName;
            warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == receiveInfo.SourceWmNo);
            receiveInfo.SourceWmName = warehouseInfo == null ? string.Empty : warehouseInfo.WarehouseName;
            ///ZONE
            List<ZonesInfo> zonesInfos = new ZonesBLL().GetList("[ZONE_NO] in ('" + receiveInfo.ZoneNo + "','" + receiveInfo.SourceZoneNo + "')", string.Empty);
            ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == receiveInfo.ZoneNo);
            receiveInfo.ZoneName = zonesInfo == null ? string.Empty : zonesInfo.ZoneName;
            zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == receiveInfo.SourceZoneNo);
            receiveInfo.SourceZoneName = zonesInfo == null ? string.Empty : zonesInfo.ZoneName;

            jsonData = JsonHelper.ToJson(receiveInfo);
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");///执行成功！
            info.Export.Add("ReceiveOrder", jsonData);
            return info;
        }

        /// <summary>
        /// WMM-022 获取单据信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo GetReceiveAndOutOrder(BaseDataInfo info)
        {
            ///TOKEN校验
            //if (!ValidToken(ref info)) return info;
            string jsonData = "[]";
            string orderNo = info.GetImportData("orderno");
            List<ReceiveAndOutputInfo> orderList = new ReceiveAndOutputBLL().GetReceiveAndOutOrder(orderNo);
            if (orderList.Count() > 0)
            {
                string OperationType = orderList.FirstOrDefault().OperationType.ToString();
                if (OperationType == "100") ///TODO:使用枚举项
                {
                    #region 检验
                    /*0:免检；1：抽检；2：批检*/
                    if (orderList.Where(w => w.OperationType == 10 && w.CheckMode != 0 && w.CheckStatus == 0).Count() > 0)
                    {
                        info.Result = false;
                        info.ErrCode = "3x00000028";
                        info.Msg = "入库单物料检验未完成或检验不合格";
                        return info;
                    }

                    if (orderList.Where(w => w.OperationType == 10 && w.CheckMode == 1 && w.UnqualifiedQty > 0).Count() > 0)
                    {
                        info.Result = false;
                        info.ErrCode = "0x00000392";
                        info.Msg = "入库单存在抽检不合格的物料";
                        return info;
                    }
                    #endregion
                }
                else if (OperationType == "20")
                {
                    #region 校验

                    #endregion
                }

                jsonData = JsonHelper.ToJson(orderList);
                info.Export.Add("operationType", OperationType);
                info.Result = true;
                info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");
                info.Tables.Add("List", jsonData);
                return info;
            }
            else
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000012");
                info.Tables.Add("List", jsonData);
                return info;
            }
        }
        /// <summary>
        /// WMM-006 获取标签信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetBarcode(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string barcodeData = info.GetImportData("barcodedata");
            ///单号
            string orderNo = info.GetImportData("orderno");
            ///1入库单2出库单
            string inoutFlag = info.GetImportData("inoutflag");
            string jsonData = string.Empty;
            ///客户端操作用户
            string loginUser = info.LoginUser;
            BarcodeInfo barcodeInfo = null;
            try
            {
                if (inoutFlag == "1")
                    barcodeInfo = new BarcodeBLL().GetBarcode(barcodeData, orderNo, BarcodeStatusConstants.Scaned, loginUser);
                if (inoutFlag == "2")
                    barcodeInfo = new BarcodeBLL().GetBarcode(barcodeData, orderNo, BarcodeStatusConstants.Scaned, loginUser, 2);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                jsonData = string.Empty;
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }
            jsonData = JsonHelper.ToJson(barcodeInfo);
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000003");///扫描成功
            info.Export.Add("Info", jsonData);
            return info;
        }
        /// <summary>
        /// 撤销条码状态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo UndoBarcodeStatus(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string barcodeData = info.GetImportData("barcodedata");
            string loginUser = info.LoginUser;
            try
            {
                new BarcodeBLL().UndoBarcodeStatus(barcodeData, loginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");///操作成功
            return info;
        }
        /// <summary>
        /// 根据条码List批量还原条码
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo BatchDeletingBarcode(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string barcodeDatas = info.GetImportData("barcodedatas");
            string loginUser = info.LoginUser;
            List<string> barcodeDatasList = JsonHelper.FormJson<List<string>>(barcodeDatas);
            try
            {
                foreach (string barcodeData in barcodeDatasList)
                {
                    new BarcodeBLL().UndoBarcodeStatus(barcodeData, loginUser);
                }
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }

            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");///操作成功
            return info;
        }

        /// <summary>
        /// WMM-024 单据提交
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo SubmitWarehouseOrder(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string orderJsonStr = info.GetImportData("orderJsonStr");
            string barJsonStr = info.GetImportData("barJsonStr");
            string emergencyFlag = info.GetImportData("emergencyFlag");//0 不产生拉动 1 产生拉动
            string operationType = info.GetImportData("operationType");//10 入库单 20 出库单
            string loginUser = info.LoginUser;
            List<ReceiveAndOutputInfo> ReceiveAndOutputInfoList = JsonHelper.FormJson<List<ReceiveAndOutputInfo>>(orderJsonStr) ?? new List<ReceiveAndOutputInfo>();
            List<BarcodeInfo> BarcodeInfoList = JsonHelper.FormJson<List<BarcodeInfo>>(barJsonStr) ?? new List<BarcodeInfo>();
            List<string> rowsKeyValues = ReceiveAndOutputInfoList.GroupBy(w => w.OrderId).Select(w => w.Key.Value.ToString()).ToList<string>();
            ///TODO:枚举
            if (operationType == "10")
            {
                info.Result = new ReceiveAndOutputBLL().UpdateTheActualNumberForReceive(ReceiveAndOutputInfoList, loginUser);
                if (info.Result)
                {
                    new ReceiveBLL().CompleteInfos(rowsKeyValues, loginUser);
                }
            }
            else if (operationType == "20")
            {
                info.Result = new ReceiveAndOutputBLL().UpdateTheActualNumberForOutPut(ReceiveAndOutputInfoList, loginUser);
                if (info.Result)
                {
                    info.Result = new OutputBLL().CloseInfos(rowsKeyValues, loginUser);
                }
            }

            if (info.Result)
            {
                info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");
            }
            else
            {
                info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000012");
            }

            return info;
        }
        #endregion

        #region WMM-027 物料拣配
        /// <summary>
        /// 获取仓库信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetWarehouses(BaseDataInfo info)
        {
            List<WarehouseInfo> warehouseInfos = new WarehouseBLL().GetList(string.Empty, string.Empty);
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");///操作成功
            info.Tables.Add("Warehouse", JsonHelper.ToJson(warehouseInfos));
            return info;
        }
        /// <summary>
        /// 获取存储区信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetZones(BaseDataInfo info)
        {
            string wmNo = info.GetImportData("wmNo");
            List<ZonesInfo> zonesInfos = new ZonesBLL().GetZonesInfos(wmNo);
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");///操作成功
            info.Tables.Add("Zone", JsonHelper.ToJson(zonesInfos));
            return info;
        }
        /// <summary>
        /// 扫码拣配
        /// </summary>
        /// <param name="info">条码</param>
        /// <returns></returns>
        private BaseDataInfo PickupBarcode(BaseDataInfo info)
        {
            /////TOKEN校验
            if (!ValidToken(ref info)) return info;
            string barcodeData = info.GetImportData("barcodedata");
            string jsonData = string.Empty;
            string loginUser = info.LoginUser;
            BarcodeInfo barcodeInfo = null;
            try
            {
                barcodeInfo = new BarcodeBLL().GetBarcode(barcodeData, string.Empty, BarcodeStatusConstants.PickedUp, loginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                jsonData = string.Empty;
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }
            jsonData = JsonHelper.ToJson(barcodeInfo);
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000003");///扫描成功
            info.Export.Add("Info", jsonData);
            return info;
        }
        /// <summary>
        /// 拣配完成
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo FinishPickup(BaseDataInfo info)
        {
            /////TOKEN校验
            if (!ValidToken(ref info)) return info;
            string receiveNos = info.GetImportData("receiveNos");
            string outputType = info.GetImportData("outputType");
            Guid organizationFid = new UserRoleBLL().GetOrganizationFid(info.UserFid, info.RoleFid);
            string planShippingTime = info.GetImportData("planShippingTime");
            string planDeliveryTime = info.GetImportData("planDeliveryTime");
            string conveyance = info.GetImportData("conveyance");
            string carrierTel = info.GetImportData("carrierTel");
            string wmNo = info.GetImportData("wmNo");
            string zoneNo = info.GetImportData("zoneNo");
            string tWmNo = info.GetImportData("tWmNo");
            string tZoneNo = info.GetImportData("tZoneNo");
            string loginUser = info.LoginUser;
            try
            {
                new OutputBLL().CreateOutputByPickupedBarcodes(receiveNos
                    , int.Parse(outputType)
                    , organizationFid
                    , DateTime.Parse(planShippingTime)
                    , DateTime.Parse(planDeliveryTime)
                    , conveyance
                    , carrierTel
                    , wmNo, zoneNo, tWmNo, tZoneNo
                    , loginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");///操作成功
            return info;
        }
        /// <summary>
        /// WMM-027 获取出库单号
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetOutOrder(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string jsonData = "[]";
            string orderNo = info.GetImportData("orderno");
            List<ReceiveAndOutputInfo> orderList = new ReceiveAndOutputBLL().GetOutOrder(orderNo);
            if (orderList != null && orderList.Count() > 0)
            {
                jsonData = JsonHelper.ToJson(orderList);
                info.Result = true;
                info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");
                info.Tables.Add("List", jsonData);
            }
            else
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000012");
                info.Tables.Add("List", jsonData);
            }

            return info;
        }
        /// <summary>
        /// WMM-028 拣配提交
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo SubmitPickupData(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;

            string orderJsonStr = info.GetImportData("orderJsonStr");
            string barJsonStr = info.GetImportData("barJsonStr");
            string loginUser = info.LoginUser;
            List<ReceiveAndOutputInfo> ReceiveAndOutputInfoList = JsonHelper.FormJson<List<ReceiveAndOutputInfo>>(orderJsonStr) ?? new List<ReceiveAndOutputInfo>();
            List<long> BarcodeInfoList = JsonHelper.FormJson<List<long>>(barJsonStr) ?? new List<long>();
            List<string> rowsKeyValues = ReceiveAndOutputInfoList.GroupBy(w => w.OrderId).Select(w => w.Key.Value.ToString()).ToList<string>();

            try
            {
                new ReceiveAndOutputBLL().UpdateTheActualNumberForOutPut(ReceiveAndOutputInfoList, loginUser);
                new OutputBLL().CompleteInfos(rowsKeyValues, loginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }

            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");
            return info;
        }
        #endregion

        #region 状态查询
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetBarcodeStatus(BaseDataInfo info)
        {
            string barcodeData = info.GetImportData("barcodedata");
            List<BarcodeStatusInfo> codeItemInfos = new BarcodeStatusBLL().GetBarcodeStatusInfos(barcodeData);
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");
            info.Tables.Add("BarcodeStatus", JsonHelper.ToJson(codeItemInfos));
            return info;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BaseDataInfo GetRunsheetStatus(BaseDataInfo info)
        {
            ///拉动单号、委托编号
            string runsheetNo = info.GetImportData("runsheetno");
            List<ReceiveInfo> receiveInfos = new ReceiveBLL().GetList("(charindex(N'" + runsheetNo + "',[RUNSHEET_NO]) > 0 or charindex(N'" + runsheetNo + "',[ASN_NO]) > 0) and " +
                "[STATUS] in (" + (int)ReceiveOrderStatusConstants.PUBLISHED + "," + (int)ReceiveOrderStatusConstants.COMPLETED + ")", "[TRAN_TIME] asc");
            if (receiveInfos.Count == 0)
            {
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000279");///暂无相关的物流信息
                return info;
            }
            ///存储区
            List<ZonesInfo> zonesInfos = new ZonesBLL().GetList("", "");
            List<LogisticsInfo> logisticsInfos = new List<LogisticsInfo>();
            ///
            string orderNo = string.Empty;
            string custTrustNo = string.Empty;
            foreach (var receiveInfo in receiveInfos)
            {
                orderNo = receiveInfo.RunsheetNo;
                custTrustNo = receiveInfo.AsnNo;
                LogisticsInfo logisticsInfo = new LogisticsInfo();
                string operationName = string.Empty;
                ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == receiveInfo.SourceZoneNo);
                if (zonesInfo == null) zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == receiveInfo.ZoneNo);
                if (zonesInfo == null) continue;
                string zoneName = zonesInfo.ZoneName;
                logisticsInfo.TranTime = receiveInfo.TrustTime.GetValueOrDefault();
                operationName = "已接单";
                logisticsInfo.Comments = "业务编号 " + receiveInfo.ReceiveNo + "[" + operationName + "]";
                logisticsInfos.Add(logisticsInfo);
                if (receiveInfo.Status.GetValueOrDefault() == (int)ReceiveOrderStatusConstants.COMPLETED)
                {
                    LogisticsInfo logisticsInfo1 = new LogisticsInfo();
                    logisticsInfo1.TranTime = receiveInfo.TranTime.GetValueOrDefault();
                    operationName = "已入库";
                    logisticsInfo1.Comments = zoneName + "[" + operationName + "]" + "，业务编号 " + receiveInfo.ReceiveNo;
                    logisticsInfos.Add(logisticsInfo1);
                }

            }
            List<OutputInfo> outputInfos = new OutputBLL().GetList("(charindex(N'" + runsheetNo + "',[RUNSHEET_NO]) > 0 or charindex(N'" + runsheetNo + "',[ASN_NO]) > 0) and " +
                "[STATUS] in (" + (int)OutputOrderStatusConstants.Published + "," + (int)OutputOrderStatusConstants.Completed + "," + (int)OutputOrderStatusConstants.Closed + ")", "[TRAN_TIME] asc");
            foreach (var outputInfo in outputInfos)
            {
                orderNo = outputInfo.RunsheetNo;
                custTrustNo = outputInfo.AsnNo;
                LogisticsInfo logisticsInfo = new LogisticsInfo();
                string operationName = string.Empty;
                ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == outputInfo.ZoneNo);
                if (zonesInfo == null) continue;
                string zoneName = zonesInfo.ZoneName;
                logisticsInfo.TranTime = outputInfo.SendTime.GetValueOrDefault();
                operationName = "装车完成";
                logisticsInfo.Comments =
                    zoneName +
                    "[" + operationName + "]" +
                    "，运单号：" + outputInfo.OutputNo +
                    "，箱数：" + outputInfo.SumPackageQty.GetValueOrDefault().ToString("F0") + "";
                logisticsInfos.Add(logisticsInfo);
                if (outputInfo.Status.GetValueOrDefault() == (int)ReceiveOrderStatusConstants.COMPLETED
                    || outputInfo.Status.GetValueOrDefault() == (int)ReceiveOrderStatusConstants.CLOSED)
                {
                    LogisticsInfo logisticsInfo1 = new LogisticsInfo();
                    zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == outputInfo.TZoneNo);
                    if (zonesInfo == null) continue;
                    zoneName = zonesInfo.ZoneName;
                    logisticsInfo1.TranTime = outputInfo.ConfirmDate.GetValueOrDefault();
                    operationName = "发货完成";
                    logisticsInfo1.Comments =
                        zoneName +
                        "[" + operationName + "]" +
                        "，运单号：" + outputInfo.OutputNo +
                        "，数量：" + outputInfo.SumPartQty.GetValueOrDefault().ToString("F0") + "";
                    logisticsInfos.Add(logisticsInfo1);
                }
                if (outputInfo.Status.GetValueOrDefault() == (int)ReceiveOrderStatusConstants.CLOSED)
                {
                    LogisticsInfo logisticsInfo1 = new LogisticsInfo();
                    zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == outputInfo.TZoneNo);
                    if (zonesInfo == null) continue;
                    zoneName = zonesInfo.ZoneName;
                    logisticsInfo1.TranTime = outputInfo.TranTime.GetValueOrDefault();
                    operationName = "报关完成";
                    logisticsInfo1.Comments =
                        zoneName +
                        "[" + operationName + "]" +
                        "，运单号：" + outputInfo.OutputNo +
                        "，数量：" + outputInfo.SumPartQty.GetValueOrDefault().ToString("F0") + "";
                    logisticsInfos.Add(logisticsInfo1);
                }
            }
            List<BusinessFollowInfo> businessFollowInfos = new BusinessFollowBLL().GetList("charindex(N'" + orderNo + "',[RUNSHEET_NO]) > 0", string.Empty);
            foreach (var businessFollowInfo in businessFollowInfos)
            {
                LogisticsInfo logisticsInfo = new LogisticsInfo();
                logisticsInfo.TranTime = businessFollowInfo.TranTime.GetValueOrDefault();
                logisticsInfo.Comments = businessFollowInfo.TranContent;
                logisticsInfos.Add(logisticsInfo);
            }
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "0x00000069");
            info.Tables.Add("LogisticsInfo", JsonHelper.ToJson(logisticsInfos.OrderByDescending(d => d.TranTime).ToList()));
            return info;
        }
        #endregion

        #region VMI拉动单
        /// <summary>
        /// 根据拉动单号获取拉动单
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo GetVmiPullOrderByOrderCode(BaseDataInfo info)
        {
            if (!ValidToken(ref info)) return info;/// TOKEN校验
            string partNo = info.GetImportData("partNo");/// 拉动单号
            string jsonData = string.Empty;
            try
            {
                List<VmiPullOrderDetailViewInfo> vmipullorderdetailinfos = new VmiPullOrderDetailBLL().GetVmiPullOrderDetailListByPartNo(partNo);
                jsonData = JsonHelper.ToJson(vmipullorderdetailinfos);
                info.Export.Add("List", jsonData);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }
            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000003");///扫描成功
            return info;
        }
        /// <summary>
        /// 根据拉动单明细ID加入购物车
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo AddIntoShippingPart(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string listStr = info.GetImportData("listStr");
            List<VmiPullOrderDetailInfo> vmipullorderdetailinfos = JsonConvert.DeserializeObject<List<VmiPullOrderDetailInfo>>(listStr);
            try
            {
                VmiShippingPartBLL.AddCartVmiShippingPartInfo(vmipullorderdetailinfos, info.LoginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }

            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");///扫描成功
            return info;
        }
        /// <summary>
        /// 根据ID集合批量删除购物车删除
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo BatchDeletingByIds(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;

            string listStr = info.GetImportData("listStr");
            List<string> ids = JsonConvert.DeserializeObject<List<string>>(listStr);
            try
            {
                new VmiShippingPartBLL().BatchdeletingInfos(ids, info.LoginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }

            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");///执行成功
            return info;
        }
        /// <summary>
        /// 根据用户名获取购物车列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo GetVmiShippingPartInfosByLoginUser(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            try
            {
                string jsonData = string.Empty;
                List<VmiShippingPartInfo> vmiShippingPartInfos = new VmiShippingPartBLL().GetVmiShippingPartInfosByLoginUser(info.LoginUser);

                jsonData = JsonHelper.ToJson(vmiShippingPartInfos);
                info.Export.Add("List", jsonData);
                info.Export.Add("ListCount", vmiShippingPartInfos.Count().ToString());
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }

            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");///执行成功
            return info;
        }

        /// <summary>
        /// 根据购物车明细ID，进行批量预发货操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public BaseDataInfo PreDelivery(BaseDataInfo info)
        {
            ///TOKEN校验
            if (!ValidToken(ref info)) return info;
            string IdArray = info.GetImportData("IdArray");

            List<string> rowsKeyValues = JsonHelper.FormJson<List<string>>(IdArray);

            try
            {
                new VmiShippingPartBLL().ReleaseInfos(rowsKeyValues, info.LoginUser);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message.Replace("MC:", string.Empty);
                info.Result = false;
                info.Msg = GetMessage(info.Language, info.ErrCode = exMsg);
                return info;
            }

            info.Result = true;
            info.Msg = GetMessage(info.Language, info.ErrCode = "3x00000005");///执行成功
            return info;
        }
        #endregion
    }
}
