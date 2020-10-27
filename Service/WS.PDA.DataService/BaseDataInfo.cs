using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WS.PDA.BaseService
{
    public class BaseDataInfo
    {
        /// <summary>
        /// 传入传出参数
        /// </summary>
        public Dictionary<string, string> Import, Export;
        /// <summary>
        /// 单个或者多个列表临时雇员
        /// </summary>
        public Dictionary<string, string> Tables;
        /// <summary>
        /// 错误代码 错误信息 令牌 语言
        /// </summary>
        public string ErrCode, Msg, Token, Language;
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginUser;
        /// <summary>
        /// 角色外键
        /// </summary>
        public Guid RoleFid;
        /// <summary>
        /// 用户外键
        /// </summary>
        public Guid UserFid;
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool Result;
        /// <summary>
        /// 数据服务地址
        /// </summary>
        public string DataServiceUrl;
        /// <summary>
        /// 字段初始化
        /// </summary>
        public BaseDataInfo()
        {
            Import = new Dictionary<string, string>();
            Export = new Dictionary<string, string>();
            Tables = new Dictionary<string, string>();
            ErrCode = string.Empty;
            Msg = string.Empty;
            Token = string.Empty;
            Language = "zh-cn";
            Result = false;
            LoginUser = string.Empty;
            RoleFid = Guid.Empty;
            UserFid = Guid.Empty;
        }
        /// <summary>
        /// 获取输入参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetImportData(string key)
        {
            string importData = string.Empty;
            Import.TryGetValue(key, out importData);
            return (importData ?? string.Empty);
        }
        /// <summary>
        /// 获取DataTableJsonStr
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTablesData(string key)
        {
            string TablesData = string.Empty;
            Tables.TryGetValue(key, out TablesData);
            return (TablesData ?? string.Empty);
        }
    }
}