using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
//using BC_PT_PDA.NewCodeText;
using System.Data;
using System.Text.RegularExpressions;

namespace LES_PDA
{
    public class API
    {
        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="dt">时间设定</param>
        public static void SetDate(DateTime dt)
        {
            SYSTEMTIME st;
            st.year = (short)dt.Year;
            st.month = (short)dt.Month;
            st.dayOfWeek = (short)dt.DayOfWeek;
            st.day = (short)dt.Day;
            st.hour = (short)dt.Hour;
            st.minute = (short)dt.Minute;
            st.second = (short)dt.Second;
            st.milliseconds = (short)dt.Millisecond;
            SetLocalTime(ref st);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME time);


        /// <summary>
        /// 截取
        /// </summary>
        /// <param name="no">必须是XX-XXX格式</param>
        /// <returns>返回的是XX</returns>
        public static string Cmbstring(string no)
        {
            string[] notypes = no.Split('-');
            return notypes[0];
        }

        /// <summary>
        /// 清空DataTable
        /// </summary>
        /// <param name="dt"></param>
        public static void ClearDataTable(DataTable dt)
        {
            while (dt.Rows.Count > 0)
            {
                dt.Rows[0].Delete();
            }
        }

        /// <summary>
        /// 验证字符是否是数字
        /// </summary>
        /// <param name="txtNum">字符文本</param>
        /// <returns></returns>
        public static bool AllowNumbers(string txtNum)
        {
            string pat = "^([0-9]*)$";
            Regex rg = new Regex(pat);
            Match mt = rg.Match(txtNum);
            if (!mt.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 刷新dt
        /// </summary>
        /// <param name="dt">dt</param>
        /// <param name="RowColumn">第几列数</param>
        public static void ShowDataTable(DataTable dt,int RowColumn)
        {
            //更新序号
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][RowColumn] = i + 1;
            }
        }
        /// <summary>
        /// 只能输入英文字母和数字,不能输入中文
        /// </summary>
        /// <param name="txtNum">文本</param>
        /// <returns></returns>
        public static bool RegexNumber(string txtNum)
        {
            Regex rx = new Regex("^[A-Za-z0-9]+$");
            if (rx.IsMatch(txtNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
