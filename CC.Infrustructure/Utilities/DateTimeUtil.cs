using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.Utilities
{
    public class DateTimeUtil
    {
        /// <summary>
        /// 线程同步锁对象
        /// </summary>
        private static object syncRoot = new object();
        /// <summary>
        /// 周，日期缓存
        /// </summary>
        private static Dictionary<string, DateTime> weekDayCache = new Dictionary<string, DateTime>();

        //stupid but can used
        /// 把德国时间转换为中国时间
        /// 如：2004，347:意为2004年34周第7天
        /// E.G.   Utility.ConvertDateFromGermToCn("2004","347")
        /// 
        /// <param name="mYear">所在年</param>
        /// <param name="mWeekDay">所在周、日</param>
        public static DateTime ConvertDateFromGermToCn(string mYear, string mWeekDay)
        {
            DateTime dtYearStart1 = new DateTime(Convert.ToInt32(mYear), 1, 1);
            int preyear = dtYearStart1.Year - 1;
            int week = Int32.Parse(mWeekDay.Substring(0, 2));//周数
            int day = Int32.Parse(mWeekDay.Substring(2, 1));//该周的那一天
            //DateTime dtReturn;

            double intervalDay = (week - 1) * 7 + day - 1;

            switch (dtYearStart1.DayOfWeek)
            {
                case DayOfWeek.Monday:
                default:
                    //dtYearStart2=dtYearStart1;//DateTime.Parse("");
                    break;
                case DayOfWeek.Tuesday:
                    //temp=preyear.ToString()+"-12-31";
                    //dtYearStart2=DateTime.Parse(temp);
                    intervalDay -= 1;
                    break;
                case DayOfWeek.Wednesday:
                    //temp=preyear.ToString()+"-12-30";
                    //dtYearStart2=DateTime.Parse(temp);
                    intervalDay -= 2;
                    break;
                case DayOfWeek.Thursday:
                    //temp=preyear.ToString()+"-12-29";
                    //dtYearStart2=DateTime.Parse(temp);
                    intervalDay -= 3;
                    break;
                case DayOfWeek.Friday:
                    //temp=mYear+"-01-04";
                    //dtYearStart2=DateTime.Parse(temp);
                    intervalDay += 3;
                    break;
                case DayOfWeek.Saturday:
                    //temp=mYear+"-01-03";
                    //dtYearStart2=DateTime.Parse(temp);
                    intervalDay += 2;
                    break;
                case DayOfWeek.Sunday:
                    //temp=mYear+"-01-02";
                    //dtYearStart2=DateTime.Parse(temp);
                    intervalDay += 1;
                    break;

            }

            return dtYearStart1.AddDays(intervalDay);
        }

        /// <summary>
        /// 获取时间，从周时间中获取，例如 113 表示第11周的第三天
        /// </summary>
        /// <param name="mYear"></param>
        /// <param name="mWeekDay"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromWeekDay(string mYear, string mWeekDay)
        {
            string key = mYear + mWeekDay;
            if (!weekDayCache.ContainsKey(key))
            {
                lock (syncRoot)
                {
                    weekDayCache[key] = ConvertDateFromGermToCn(mYear, mWeekDay);
                }
            }

            return weekDayCache[key];
        }
    }
}
