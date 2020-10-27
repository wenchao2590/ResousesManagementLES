using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Infrustructure.Logging
{
    public class Log
    {
        #region 用于直接写文件Log

        private static object syncRoot = new object();

        private static List<string> logs = new List<string>();

        private static Mutex mutex = new Mutex();
        public static void WriteInfo(string message)
        {
            lock (syncRoot)
            {
                logs.Add(message);
            }
        }

        /// <summary>
        /// 一次性写入到文件中。
        /// </summary>
        public static void Flush()
        {
            lock (syncRoot)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs = null;
                        foreach (var l in logs)
                        {
                            sw.WriteLine(l);

                        }
                        sw.Flush();
                    }
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Dispose();
                    }
                }

                logs.Clear();
            }
        }

        #endregion

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="filePath"></param>
        ///// <param name="fieldName"></param>
        //public static void WriteLogToFile(string message, string filePath, string fileName)
        //{
        //    ///如果没有该目录则新建
        //    if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        //    ///如果没有该文件则新建
        //    if (!File.Exists(filePath + @"\" + fileName + ".txt")) File.Create(filePath + @"\" + fileName + ".txt");

        //    FileStream fs = new FileStream(filePath + @"\" + fileName + ".txt", FileMode.Append);
        //    using (StreamWriter sw = new StreamWriter(fs))
        //    {
        //        sw.WriteLine(DateTime.Now + ":" + message);
        //        sw.Flush();
        //    }
        //    fs.Dispose();
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="filePath"></param>
        /// <param name="fieldName"></param>
        public static void WriteLogToFile(string message, string filePath, string fileName)
        {
            ///如果没有该目录则新建
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            ///如果没有该文件则新建
            if (!File.Exists(filePath + @"\" + fileName + ".txt")) File.Create(filePath + @"\" + fileName + ".txt").Close();

            mutex.WaitOne();

            File.AppendAllText(filePath + @"\" + fileName + ".txt", "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "\r\n" + message, Encoding.UTF8);
            mutex.ReleaseMutex();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="filePath"></param>
        /// <param name="fieldName"></param>
        public static void WriteLogToFile(string bo, string message, string filePath, string fileName)
        {
            if (bo.ToLower()=="true")
            {
                ///如果没有该目录则新建
                if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                ///如果没有该文件则新建
                if (!File.Exists(filePath + @"\" + fileName + ".txt")) File.Create(filePath + @"\" + fileName + ".txt").Close();

                mutex.WaitOne();

                File.AppendAllText(filePath + @"\" + fileName + ".txt", "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "\r\n\n" + message+"\r\n", Encoding.UTF8);
                mutex.ReleaseMutex();
            }            
        }
    }
}
