using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Infrustructure.Logging;

namespace Infrustructure.Utilities
{
    public class FileUtil
    {
        #region 备份文件
        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="targetDirectory">目标文件夹</param>
        /// <returns></returns>
        public static  bool BakupFile(string sourceFilePath, string targetDirectory)
        {
            try
            {
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                if (File.Exists(Path.Combine(targetDirectory, Path.GetFileName(sourceFilePath))))
                {
                    File.Delete(Path.Combine(targetDirectory, Path.GetFileName(sourceFilePath)));
                }

                File.Copy(sourceFilePath, Path.Combine(targetDirectory, Path.GetFileName(sourceFilePath)));

                File.Delete(sourceFilePath);

                return true;
            }
            catch (IOException)
            {
                throw ;
            }
        }

        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="functionName">功能模块</param>
        /// <param name="sourceFilePath">目标文件</param>
        /// <param name="targetDirectory">目标文件夹</param>
        /// <returns></returns>
        public static bool BakupFile(string functionName,string sourceFilePath, string targetDirectory)
        {
            try
            {
                Logger.Instance.Info(typeof(FileUtil), string.Format("开始备份【{0}】文件【{1}】,目标文件夹【{2}】", functionName, sourceFilePath, targetDirectory));

                BakupFile(sourceFilePath, targetDirectory);

                Logger.Instance.Info(typeof(FileUtil), string.Format("备份【{0}】文件【{1}】成功...", functionName,sourceFilePath));

                return true;

            }
            catch (System.Exception ex)
            {
                Logger.Instance.Error(typeof(FileUtil), ex);
                Logger.Instance.Info(typeof(FileUtil), string.Format("备份【{0}】文件【{1}】失败,失败原因【{2}】",functionName, sourceFilePath, ex.Message));
            }

            return false;
        }

        #endregion

        #region 线程阻塞

        private static object syncRoot = new object();

        /// <summary>
        /// 读取文件时，当遇到该文件会被其他进程占用时，需要等待该文件解琐。
        /// </summary>
        /// <param name="filePath"></param>
        public static void ThreadBlock(string filePath)
        {
            while (true)
            {
                try
                {
                    lock (syncRoot)
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                        }
                        break;
                    }
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(5000);
                    continue;
                }
            }
        }

        #endregion
    }
}
