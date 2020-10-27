using DM.SYS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Transactions;

namespace DAL.SYS
{
    public partial class SeqDefineDAL
    {
        /// <summary>
        /// 类的对象锁
        /// </summary>
        static object lockObject = new object();
        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <param name="seqCode">序列号名称</param>
        /// <param name="manualParams">手工参数组</param>
        /// <returns>可用的序列号</returns>
        public string GetCurrentCode(string seqCode, params string[] manualParams)
        {
            string joinChar = GetJoinChar(seqCode);
            ///获取SEQ_CODE对应段规则列表
            IList<SeqSectionInfo> sectionlist = new SeqSectionDAL().GetList("[SEQ_CODE] = N'" + seqCode + "'", "[SECTION_SEQ] asc");
            //if (sectionlist.Count == 0)
            //    throw new Exception("No Section in " + seqCode);
            ///最终返回结果
            string result = string.Empty;
            ///种子字符串
            string seedValue = string.Empty;
            int manualCount = 0;
            using (var trans = new TransactionScope())
            {
                foreach (var sectioninfo in sectionlist)
                {
                    string subresult = GetSectionCurrentValue(sectioninfo, seedValue);
                    if (string.IsNullOrEmpty(subresult))
                    {
                        subresult = manualParams[manualCount];
                        manualCount++;
                    }
                    result += joinChar + subresult;
                    ///如果被设定为种子字符串，则累计到种子中
                    if (sectioninfo.IsSeedValue.GetValueOrDefault())
                        seedValue += subresult;
                }
                trans.Complete();
            }
            if (!string.IsNullOrEmpty(joinChar))
                result = result.Substring(1);
            return result;
        }
        private string GetSectionCurrentValue(SeqSectionInfo info, string queryValue)
        {
            switch (info.DataGenerateType.GetValueOrDefault())
            {
                case (int)DataGenerateTypeConstants.FixedValue:
                    string defaultValue = string.Empty;
                    string[] defaultValues = info.DefaultValue.Split(new string[] { "%" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in defaultValues)
                    {
                        switch (item.ToUpper())
                        {
                            case "RS": defaultValue += (char)30; break;
                            case "GS": defaultValue += (char)29; break;
                            case "EOT": defaultValue += (char)04; break;
                            default: defaultValue += item; break;
                        }
                    }
                    return defaultValue;
                case (int)DataGenerateTypeConstants.DynamicParam: return string.Empty;
                case (int)DataGenerateTypeConstants.DateValue:
                    switch (info.DefaultValue.ToLower())
                    {
                        case "yyyymmdd": return DateTime.Now.ToString("yyyyMMdd");
                        case "mmdd": return DateTime.Now.ToString("MMdd");
                        case "standardd": return GetYear() + GetMonth() + GetDay();
                        case "standardm": return GetYear() + GetMonth();
                        case "standardy": return GetYear();
                        case "yydayofyear": return DateTime.Now.ToString("yy") + DateTime.Now.DayOfYear.ToString().PadLeft(3, '0');
                        case "mm": return DateTime.Now.ToString("MM");
                        case "yy": return DateTime.Now.ToString("yy");
                        default: return DateTime.Now.ToString("yyMMdd");
                    }
            }
            if (info.DataGenerateType.GetValueOrDefault() == (int)DataGenerateTypeConstants.GrowthValue)
            {
                lock (lockObject)
                {
                    ///根据QUERY_VALUE获取当前值
                    IList<SeqCurrentValueInfo> currentValuelist
                        = new SeqCurrentValueDAL().GetList("and [SEQ_CODE] = '" + info.SeqCode + "' "
                        + "and [SEQ_SECTION_FID] = '" + info.Fid + "' "
                        + "and [QUERY_VALUE] = '" + queryValue + "' ", string.Empty);
                    SeqCurrentValueInfo currentValueinfo = null;
                    ///如果没有当前值，需要做首次的添加操作
                    if (currentValuelist.Count == 0)
                    {
                        currentValueinfo = new SeqCurrentValueInfo();
                        currentValueinfo.Fid = Guid.NewGuid();
                        currentValueinfo.SeqCode = info.SeqCode;
                        currentValueinfo.SeqSectionFid = info.Fid;
                        currentValueinfo.QueryValue = queryValue;
                        currentValueinfo.CurrentValue = info.MinValue;
                        currentValueinfo.ValidFlag = true;
                        currentValueinfo.CreateDate = DateTime.Now;
                        currentValueinfo.CreateUser = "";
                        new SeqCurrentValueDAL().Add(currentValueinfo);
                    }
                    else if (currentValuelist.Count == 1)
                    {
                        currentValueinfo = currentValuelist.FirstOrDefault().Clone();
                        ///按步长递增
                        currentValueinfo.CurrentValue += info.StepLength.GetValueOrDefault();
                        ///递增后大于设定的最大值时
                        if (currentValueinfo.CurrentValue.GetValueOrDefault()
                            > info.MaxValue.GetValueOrDefault())
                        {
                            if (info.IsCycle.GetValueOrDefault())
                                currentValueinfo.CurrentValue = info.MinValue;
                            else
                                throw new Exception("Current be Max Value");
                        }
                        if (currentValueinfo.CurrentValue.GetValueOrDefault()
                            < info.MinValue.GetValueOrDefault())
                            currentValueinfo.CurrentValue = info.MinValue;
                        currentValueinfo.ModifyDate = DateTime.Now;
                        currentValueinfo.ModifyUser = "";
                        new SeqCurrentValueDAL().Update(currentValueinfo);
                    }
                    else
                        throw new Exception("Current has error data");
                    if (!info.IsFixedLength.GetValueOrDefault())
                        return currentValueinfo.CurrentValue.ToString();
                    ///按长度填充
                    switch (info.FillType.GetValueOrDefault())
                    {
                        case 1: return currentValueinfo.CurrentValue.ToString().PadLeft(info.Length.GetValueOrDefault(), info.FillChar.ToCharArray()[0]);
                        case 2: return currentValueinfo.CurrentValue.ToString().PadRight(info.Length.GetValueOrDefault(), info.FillChar.ToCharArray()[0]);
                    }
                }
            }
            throw new Exception("Section has error config");
        }
        /// <summary>
        /// 年转换为字母
        /// </summary>
        /// <returns></returns>
        private string GetYear()
        {
            switch (DateTime.Now.Year)
            {
                case 2010: return "A";
                case 2011: return "B";
                case 2012: return "C";
                case 2013: return "D";
                case 2014: return "E";
                case 2015: return "F";
                case 2016: return "G";
                case 2017: return "H";
                case 2018: return "J";
                case 2019: return "K";
                case 2020: return "L";
                case 2021: return "M";
                case 2022: return "N";
                case 2023: return "P";
                case 2024: return "R";
                case 2025: return "S";
                case 2026: return "T";
                case 2027: return "V";
                case 2028: return "W";
                case 2029: return "X";
                case 2030: return "Y";
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取一位月份
        /// </summary>
        /// <returns></returns>
        private string GetMonth()
        {
            switch (DateTime.Now.Month)
            {
                case 10: return "A";
                case 11: return "B";
                case 12: return "C";
                default: return DateTime.Now.Month.ToString();
            }
        }
        /// <summary>
        /// 获取两位日
        /// </summary>
        /// <returns></returns>
        private string GetDay()
        {
            return DateTime.Now.Day.ToString().PadLeft(2, '0');
        }
        /// <summary>
        /// 根据外键获取代码
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string GetSeqCode(Guid fid)
        {
            string sql = "select [SEQ_CODE] from dbo.[TS_SYS_SEQ_DEFINE] with(nolock) where [FID] = @FID and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FID", DbType.Guid, fid);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
        /// <summary>
        /// 根据序列号规则获取段链接符
        /// </summary>
        /// <param name="seqCode"></param>
        /// <returns></returns>
        private string GetJoinChar(string seqCode)
        {
            string sql = "select [JOIN_CHAR] from dbo.[TS_SYS_SEQ_DEFINE] with(nolock) where [SEQ_CODE] = @SEQ_CODE and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SEQ_CODE", DbType.AnsiString, seqCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
    }
}
