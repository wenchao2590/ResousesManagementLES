using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WS.SAP.InboundDataService
{
    public class BFDASapCommonBLL
    {
        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static DateTime TryParseDatetime(string dateStr, string dateFormat = "yyyyMMdd")
        {
            ///如果遇到小时数是240000 改成0,日期加1
            bool bo = false;
            if (string.IsNullOrEmpty(dateStr))
                throw new Exception("MC:0x00000393");///日期格式错误

            ///针对工作日历出现24时00分的时间转换.  2018
            if (dateFormat== "yyyyMMddHHmmss" && dateStr.Length == 14)
            {
                string dateDay = dateStr.Substring(8, 2);
                if (dateDay == "24")
                {
                    dateStr = dateStr.Replace("24", "00");
                }
                bo = true;
               
                
            }
            string[] format = { dateFormat };
            DateTime.TryParseExact(dateStr,
                                   format,
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out DateTime parsedate);
            ///如果Out日期格式是默认最小
            if (parsedate == DateTime.MinValue)
                throw new Exception("MC:0x00000393");///日期格式错误
            if (parsedate < new DateTime(1900, 1, 1))
                throw new Exception("MC:0x00000393");///日期格式错误

            if (bo) parsedate=parsedate.AddDays(1);
            return parsedate;
        }
    }
}