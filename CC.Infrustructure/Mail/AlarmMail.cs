using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infrustructure.Logging;

namespace Infrustructure.Mail
{
    //1014 M1重复过点
    //1010 接口文件大小异常  
    //1009 接口文件处理错误
    //1008 接口文件超时未生成  
    //1007 接口文件积压     
    public enum AlarmMailConfig
    {
        M100RePass = 1014,
        M100JumpRunningNo = 1015,
        FileTooSmall= 1010,
        InvalidFile=1009,
        FileTimeout=1008,
        TooManyFile=1007,
        NoOrder = 1013
    }

    public class AlarmMail
    {
        /// <summary>
        /// M100重复过点 Hashtable [createuser,orderno]
        /// </summary>
        /// <param name="htParams"></param>
        public static void AddM100RePassAlarmmail(Hashtable htParams)
        {
            string body = @"From：{0} \r\n
                            订单号：{1}，M100重复过点";
            body = string.Format(body,
                htParams["createuser"] != null ? htParams["createuser"].ToString() : "",
                htParams["orderno"] != null ? htParams["orderno"].ToString() : "");

            AddAlarmMail(AlarmMailConfig.M100RePass,
                "M100重复过点",
                body,
               htParams);
        }

        /// <summary>
        /// M100重复过点 Hashtable [createuser,plant,assemblyline,runningno,orderno]
        /// </summary>
        /// <param name="htParams"></param>
        public static void AddM100JumpRunningNoAlarmmail(Hashtable htParams)
        {
            string body = @"From：{0} \r\n
                            {1} {2}M1跳号，流水号{3}，订单号：{4}";
            body = string.Format(body,
                htParams["createuser"] != null ? htParams["createuser"].ToString() : "",
                htParams["plant"] != null ? htParams["plant"].ToString() : "",
                htParams["assemblyline"] != null ? htParams["assemblyline"].ToString() : "",
                htParams["runningno"] != null ? htParams["runningno"].ToString() : "",
                htParams["orderno"] != null ? htParams["orderno"].ToString() : "");

            AddAlarmMail(AlarmMailConfig.M100JumpRunningNo,
                "M100跳号",
                body,
               htParams);
        }

        /// <summary>
        /// 接口文件大小异常 
        /// ConfigurationManager.AppSettings["CheckFileSize"] 默认大小20M
        /// Hashtable [createuser]
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="fileSize"></param>
        /// <param name="htParams"></param>
        public static void CheckFileTooSmallAlarmmail(FileInfo fileInfo, Hashtable htParams)
        {
            int fileSize = 0;
            if (ConfigurationManager.AppSettings["CheckFileSize"] != null)
            {
                int.TryParse(ConfigurationManager.AppSettings["CheckFileSize"].ToString(), out fileSize);
            }

            //默认文件大小20M
            int checkFileSize = 20*1024*1024;
            if (fileSize > 0)
            {
                checkFileSize = fileSize * 1024 * 1024;
            }
            if (fileInfo.Length < checkFileSize)
            {
                string body = @"From：{0} \r\n
                                文件{1}大小为{2}M小于限定值{3}M";

                body = string.Format(body,
                    htParams["createuser"] != null ? htParams["createuser"].ToString() : "",
                    fileInfo.FullName,
                    fileInfo.Length/1024 /1024,
                    checkFileSize/1024/1024);

                AddAlarmMail(AlarmMailConfig.FileTooSmall,
                    "接口文件大小异常",
                    body,
                   htParams);
            }
        }

        /// <summary>
        /// 接口文件超时未生成 Hashtable [createuser]
        /// ConfigurationManager.AppSettings["FileTimeOut"] 默认40分钟
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="fileSize"></param>
        /// <param name="htParams"></param>
        public static void CheckFileTimeOutAlarmmail(string filePath, Hashtable htParams)
        {
            string fileExtension = "checkfile.time";
            string currentPath=Application.StartupPath ;
            string timeFilePath = Path.Combine(currentPath, fileExtension);
            FileInfo timeFile = new FileInfo(timeFilePath);
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

            int checkTime = 0;
            if (ConfigurationManager.AppSettings["FileTimeOut"] != null)
            {
                int.TryParse(ConfigurationManager.AppSettings["FileTimeOut"].ToString(), out checkTime);
            }

            //默认文件超时时间
            int configCheckFileTime = 40;
            if (checkTime > 0)
            {
                configCheckFileTime = checkTime;
            }

            if (!timeFile.Exists)//不存在，生成文件时间以一个文本文件保存在程序运行目录下
            {
                SaveFile(timeFilePath, DateTime.Now.ToString());
            }
            else//存在
            {
                if (directoryInfo.GetFiles().Count() == 0)//没有接口文件，检查上次生成时间
                {
                    DateTime dtLastCheckTime = Convert.ToDateTime(ReadFile(timeFilePath));
                    if (dtLastCheckTime.AddMinutes(configCheckFileTime) < DateTime.Now)
                    {
                        string body = @"From：{0}
                                        接口目录{1} 超过{2}分钟未收到新的接口文件";

                        body = string.Format(body,
                            htParams["createuser"] != null ? htParams["createuser"].ToString() : "",
                            filePath,
                            checkTime);

                        AddAlarmMail(AlarmMailConfig.FileTimeout,
                            "接口文件超时未生成",
                            body,
                           htParams);
                    }
                }
                else//文件数目大于0，更新检查时间
                {
                    SaveFile(timeFilePath, DateTime.Now.ToString());
                }
            }

        }

        /// <summary>
        /// 接口文件处理错误 Hashtable [createuser]
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="fileSize"></param>
        /// <param name="htParams"></param>
        public static void CheckInvalidFileAlarmmail(string invalidFilePath, Hashtable htParams)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(invalidFilePath);
            StringBuilder sb = new StringBuilder();
            if (directoryInfo.Exists)
            {
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    sb.Append(fileInfo.Name);
                    sb.Append("\r\n");
                }

                string invalidFiles = sb.ToString();

                if (!string.IsNullOrEmpty(invalidFiles))
                {
                    string body = @"From：{0} \r\n
                                目录：{1} \r\n
                                存在文件:{2}";

                    body = string.Format(body,
                        htParams["createuser"] != null ? htParams["createuser"].ToString() : "",
                        invalidFilePath,
                        invalidFiles);

                    AddAlarmMail(AlarmMailConfig.InvalidFile,
                        "接口文件处理错误",
                        body,
                       htParams);
                }
            }
            else
            {
                Logger.Instance.Error("CheckInvalidFileAlarmmail",
                  string.Format("错误文件目录不存在，【{0}】", invalidFilePath));           
            }
        }

        /// <summary>
        /// 接口文件积压 Hashtable [createuser]
        /// ConfigurationManager.AppSettings["FileCreateTime"] 默认文件超时时间10分钟
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="fileSize"></param>
        /// <param name="htParams"></param>
        public static void CheckTooManyFileAlarmmail(string filePath, Hashtable htParams)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            StringBuilder sb = new StringBuilder();

            int fileTime = 0;
            if (ConfigurationManager.AppSettings["FileCreateTime"] != null)
            {
                int.TryParse(ConfigurationManager.AppSettings["FileCreateTime"].ToString(), out fileTime);
            }
            //默认文件超时时间
            int configCheckFileTime = 10;
            if (fileTime > 0)
            {
                configCheckFileTime = fileTime;
            }

            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                if (fileInfo.CreationTime.AddMinutes(configCheckFileTime) < DateTime.Now)
                {
                    sb.Append(fileInfo.Name);
                    sb.Append("\r\n");
                }
            }

            string files = sb.ToString();

            if (!string.IsNullOrEmpty(files))
            {
                string body = @"From：{0} \r\n
                                接口目录:{1} \r\n
                                存在{2}分钟未处理的文件 :{3}";

                body = string.Format(body,
                    htParams["createuser"] != null ? htParams["createuser"].ToString() : "",
                    filePath,
                    configCheckFileTime,
                    files);

                AddAlarmMail(AlarmMailConfig.TooManyFile,
                    "接口文件积压",
                    body,
                   htParams);
            }
        }

        #region 文件读取
        protected static string ReadFile(string timeFilePath)
        {
            try
            {
                FileStream fs = new FileStream(timeFilePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);  //使用StreamReader类来读取文件
                string line = sr.ReadLine();
                sr.Close();
                fs.Close();

                return line;
            }
            catch(Exception e)
            {
                Logger.Instance.Error("ReadFile",
                   string.Format("读取文件出错，【{0}】",timeFilePath), e);
                return DateTime.Now.ToString();
            }
        }

        protected static void SaveFile(string timeFilePath, string content)
        {
            try
            {
                FileStream fst = new FileStream(timeFilePath, FileMode.Create);
                //写数据到txt
                StreamWriter swt = new StreamWriter(fst);
                //写入
                swt.WriteLine(content);
                swt.Close();
                fst.Close();
            }
            catch (Exception e)
            {
                Logger.Instance.Error("ReadFile",
                   string.Format("写文件出错，文件【{0}】内容【{1}】", timeFilePath, content), e);
            }
        }
        #endregion

        public static void AddAlarmMail(AlarmMailConfig mailConfig,
            string subject,
            string body,
            Hashtable htParams
            )
        {
            MailSendListInfo info = new MailSendListInfo();
            try
            {
                AlartMailDAL dal = new AlartMailDAL();

                info.AlarmName = subject;
                info.AlarmSubject = subject;
                info.MailBody = body;
                info.SysId = (int)mailConfig;
                info.CreateUser = htParams["createuser"] != null ? htParams["createuser"].ToString() : "";
                info.CreateDate = DateTime.Now;
                info.SendStatus = 0;
                dal.Add(info);
            }
            catch (Exception e)
            {
                Logger.Instance.Error("AddAlarmMail",
                    string.Format("添加警告邮件出错，主题【{0}】内容【{1}】创建者【{2}】",
                    info.AlarmSubject,
                info.MailBody,
                info.CreateDate), e);
            }
        }
    }
}
