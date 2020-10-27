using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LES_PDA
{
    public class Log
    {
        public enum logType
        { ERRORLOG, SYSTEM, Log,USER }

        private static string StartPath = "C\\LOG";//pda

        //private static string StartPath = ".\\LOG";

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        public static void WriteTxt(logType type, string content)
        {
            try
            {
                string time = System.DateTime.Now.ToString();
                string fileName = "";
                if (type == logType.SYSTEM)
                    fileName = "SYSTEM_" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (type == logType.ERRORLOG)
                    fileName = "ERRORLOG_" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (type == logType.Log)
                    fileName = "1.txt";
                if (type == logType.USER)
                    fileName = "USER.txt";
                //指定路径
                //如果文件a.txt存在就打开，不存在就新建 .append 是追加写 
                if (!Directory.Exists(StartPath))
                {
                    Directory.CreateDirectory(StartPath);
                }
                FileStream fst;
                if (type == logType.Log)
                {
                    fst = new FileStream(StartPath + "\\" + fileName, FileMode.Create);
                }
                else if (type == logType.USER) { fst = new FileStream(StartPath + "\\" + fileName, FileMode.Create); }
                else
                {
                    fst = new FileStream(StartPath + "\\" + fileName, FileMode.Append);
                }
                //写数据到a.txt格式 
                StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                //写入 
                //swt.WriteLine("");
                if (type == logType.Log)
                {
                    swt.Write(content);
                }
                else if (type == logType.USER) { swt.Write(content); }
                else
                {
                    swt.Write("[" + time + "] " + content);
                }
                swt.WriteLine();
                swt.Close();
                fst.Close();
            }
            catch
            {
                
            }
        }

        public static string RedTxt()
        {
            //指定路径
            //如果文件a.txt存在就打开，不存在就新建 .append 是追加写 
            try
            {
                if (!Directory.Exists(StartPath))
                {
                    Directory.CreateDirectory(StartPath);
                }
                FileStream fst;

                fst = new FileStream(StartPath + "\\1.txt", FileMode.Open);
                StreamReader red = new StreamReader(fst, System.Text.Encoding.GetEncoding("utf-8"));
                string s = red.ReadToEnd();
                red.Close();
                fst.Close();
                return s;
            }
            catch
            {
                Log.WriteTxt(logType.ERRORLOG, "该1.txt不存在");
                return null;
            }
        }

        /// <summary>
        /// 读取用户
        /// </summary>
        /// <returns></returns>
        public static string RadUSER()
        {
            try
            {
                if (!Directory.Exists(StartPath))
                {
                    Directory.CreateDirectory(StartPath);
                }
                FileStream fst;

                fst = new FileStream(StartPath + "\\USER.txt", FileMode.Open);
                StreamReader red = new StreamReader(fst, System.Text.Encoding.GetEncoding("utf-8"));
                string s = red.ReadToEnd();
                red.Close();
                fst.Close();
                return s;
            }
            catch
            {
                Log.WriteTxt(logType.ERRORLOG, "该USER.txt不存在");
                return null;
            }
        }
    }
}
