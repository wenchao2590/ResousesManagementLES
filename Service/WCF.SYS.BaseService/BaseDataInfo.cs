using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WCF.SYS.BaseService
{
    public class BaseDataInfo
    {
        public Dictionary<string, string> Import, Export;
        public Dictionary<string, DataTable> Tables;
        public string ErrCode, Msg, Token, Language;
        public bool Result;
        public BaseDataInfo()
        {
            Import = new Dictionary<string, string>();
            Export = new Dictionary<string, string>();
            Tables = new Dictionary<string, DataTable>();
            ErrCode = string.Empty;
            Msg = string.Empty;
            Token = string.Empty;
            Language = "zh-cn";
            Result = false;
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
            return importData;
        }
    }
}