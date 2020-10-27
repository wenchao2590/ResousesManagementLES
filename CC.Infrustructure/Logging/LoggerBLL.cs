using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Infrustructure.BaseClass;
using Infrustructure.Configuration;
using Infrustructure.Data.Integration;
using Infrustructure.Event;
using Infrustructure.Logging.Proxy;

namespace Infrustructure.Logging
{
    public static class LoggerBLL
    {
        /// <summary>
        /// 写入数据库SysLog表 已知日志编码
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="msgCode"></param>
        public static void WriteDBSysLog(string plantCode, string moduleCode, string msgCode)
        {
            Log4NetHelper.Instance.Info(typeof(LoggerBLL), String.Format("{0}-{1}-{2}", plantCode, moduleCode, msgCode));
        }


        /// <summary>
        /// 错误日志 会写入数据库SysLog表，同时写入日志文件
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="exceptionCode">错误编号</param>
        /// <param name="message">异常信息</param>
        public static void LogError(string plantCode, string moduleCode, string exceptionCode, string message)
        {
            Log4NetHelper.Instance.LogError(plantCode, moduleCode, exceptionCode, message);
            Log4NetHelper.Instance.Error(typeof(LoggerBLL), String.Format("Module:{0};Code:{1};Msg:{2}", moduleCode, exceptionCode, message));
        }

        /// <summary>
        /// 错误日志 会写入数据库SysLog表，同时写入日志文件
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventLevel">事件级别</param>
        /// <param name="Message">异常消息</param>
        public static void LogError(string plantCode, string moduleCode, string eventType, EventLevel eventLevel, string Message)
        {
            Log4NetHelper.Instance.LogError(plantCode, moduleCode, eventType, eventLevel, Message);
            Log4NetHelper.Instance.Error(typeof(LoggerBLL), Message);
        }
        /// <summary>
        /// 错误日志 会写入数据库SysLog表，同时写入日志文件
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventLevel">错误级别</param>
        /// <param name="ex">异常详细信息</param>
        public static void LogError(string plantCode, string moduleCode, string eventType, EventLevel eventLevel, Exception ex)
        {
            Log4NetHelper.Instance.LogError(plantCode, moduleCode, eventType, eventLevel, ex);
            Error(eventType, ex);
        }

        /// <summary>
        /// 本地日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public static void Info(object sender, string message)
        {
            Log4NetHelper.Instance.Info(sender, message);
        }

        /// <summary>
        /// 本地跟踪
        /// </summary>
        /// <param name="message"></param>
        public static void Trace(string message)
        {
            Log4NetHelper.Instance.Trace(typeof(LoggerBLL), message);
        }

        /// <summary>
        /// 记录本地错误日志
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">信息详情</param>
        public static void Error(string message, Exception ex)
        {
            Log4NetHelper.Instance.Error(typeof(LoggerBLL), message, ex);
        }

        /// <summary>
        /// 记录本地错误日志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static void Error(string message)
        {
            Log4NetHelper.Instance.Error(typeof(LoggerBLL), message);
        }

        /// <summary>
        /// 记录本地错误日志
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="message">错误信息</param>
        public static void Error(Object sender, string message)
        {
            Log4NetHelper.Instance.Error(sender.GetType(), message);
        }



        /// <summary>
        /// 记录系统日志 写数据库
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="logMessage">日志信息</param>
        /// <param name="eventType">日志类型</param>
        public static void LogWindowsLog(string logName, string logMessage, System.Diagnostics.EventLogEntryType eventType)
        {
            Log4NetHelper.Instance.WriteWindowsLog(logName, logMessage, eventType);
        }

        /// <summary>
        /// 接口非异常日志
        /// </summary>
        /// <param name="outSysCode"></param>
        /// <param name="type"></param>
        /// <param name="level"></param>
        /// <param name="eventName"></param>
        /// <param name="desc"></param>
        public static void LogIOServiceLog(string outSysCode, IOEventType type, EventLevel level, string eventName, string desc)
        {
            Log4NetHelper.Instance.LogIOServiceLog(outSysCode, type, level, eventName, desc);
        }
        /// <summary>
        /// 接口异常日志
        /// </summary>
        /// <param name="outSysCode"></param>
        /// <param name="type"></param>
        /// <param name="level"></param>
        /// <param name="ex"></param>
        public static void LogIOServiceLog(string outSysCode, IOEventType type, EventLevel level, Exception ex)
        {
            Log4NetHelper.Instance.LogIOServiceLog(outSysCode, type, level, ex);
        }

        /// <summary>
        /// 用户操作日志 已知消息编码
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="msgCode"></param>
        /// <param name="userId"></param>
        public static void LogUserInfo(string plantCode, string moduleCode, string msgCode, string userId)
        {
            Log4NetHelper.Instance.LogUserInfo(plantCode, moduleCode, msgCode, userId);
        }

        /// <summary>
        /// </summary>
        /// <param name="plantCode">工厂编号</param>
        /// <param name="moduleCode">模块编号</param>
        /// <param name="msgCode"></param>
        /// <param name="userId">用户编号</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventLevel">事件级别</param>
        /// <param name="eventDescription">事件描述</param>
        public static void LogUserInfo(string plantCode, string moduleCode, string msgCode, string userId, string eventType, EventLevel eventLevel, string eventDescription)
        {
            Log4NetHelper.Instance.LogUserInfo(plantCode, moduleCode, msgCode, userId, eventType, eventLevel, eventDescription);
        }

        /// <summary>
        /// 用户操作日志 未知消息编码
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="msgCode"></param>
        /// <param name="userId"></param>
        /// <param name="eventType"></param>
        /// <param name="eventLevel"></param>
        /// <param name="eventDescription"></param>
        /// <param name="oldObj"></param>
        /// <param name="newObj"></param>
        public static void LogUserInfo(string plantCode, string moduleCode, string msgCode, string userId, string eventType, EventLevel eventLevel, string eventDescription, object oldObj, object newObj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(eventDescription);
            if (oldObj != null && newObj != null)
            {
                Type oldType = oldObj.GetType();
                Type newType = newObj.GetType();

                if (oldType != newType)
                {
                    sb.Append("exists object type:");
                    sb.Append(oldType.Name);
                    sb.Append(",new object type:");
                    sb.Append(newType.Name);
                }
                else
                {
                    PropertyInfo[] oldObjPropertyList = oldType.GetProperties();
                    foreach (PropertyInfo oldProperty in oldObjPropertyList)
                    {
                        if (oldProperty.PropertyType == typeof(DataItemField))
                            continue;

                        PropertyInfo newObjPropertuy = newType.GetProperty(oldProperty.Name);

                        object oldValue = GetValueObject(oldProperty, oldObj);
                        object newValue = GetValueObject(newObjPropertuy, newObj);
                        if (oldValue == null && newValue == null)
                            continue;

                        if (oldValue == null && newValue != null)
                        {
                            sb.Append("," + oldProperty.Name + ":null|" + newValue.ToString());
                            continue;
                        }

                        if (oldValue != null && newValue == null)
                        {
                            sb.Append("," + oldProperty.Name + ":" + oldValue.ToString() + "|null");
                            continue;
                        }

                        if (string.IsNullOrEmpty(oldValue.ToString()) && newValue.ToString().ToLower() == "null")
                            continue;

                        if (oldValue != null && newValue != null && oldValue.ToString() != newValue.ToString())
                        {
                            sb.Append("," + oldProperty.Name + ":" + oldValue.ToString() + "|" + newValue.ToString());
                        }
                    }
                }
            }

            Log4NetHelper.Instance.LogUserInfo(plantCode, moduleCode, msgCode, userId, eventType, eventLevel, sb.ToString());
        }

        private static object GetValueObject(PropertyInfo property, object obj)
        {
            try
            {
                return property.GetValue(obj, null);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 动态记录日志
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="typeName"></param>
        /// <param name="methodName"></param>
        /// <param name="dmlParams"></param>
        public static void LogUserInfo(string moduleCode, string typeName, string methodName, params object[] dmlParams)
        {
            try
            {
                //LoggerBLL.LogUserInfo(UserUtil.GetCurrentUser().PlantCode, moduleCode, UserUtil.GetCurrentUser().UserID.ToString(), "1", EventLevel.Normal, GetLogXmlContent(dmlParams));
                List<string> list = new List<string> { typeName };
                Log4NetHelper.Instance.LogUserInfo(null, UserUtil.GetCurrentUser().PlantCode, moduleCode, methodName, UserUtil.GetCurrentUser().UserID.ToString(), "1", EventLevel.Normal, GetLogXmlContent(dmlParams), list);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 动态记录日志
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="methodName"></param>
        /// <param name="dmlParams"></param>
        public static void LogBOOperate(string typeName, string methodName, params object[] dmlParams)
        {
            try
            {
                BOLogSettings boLogSettings = BOLogSettings.Instance;
                if (boLogSettings == null)
                    return;

                BOLog bolog = boLogSettings.BOLogs.GetBOLog(typeName);
                if (bolog == null)
                    return;

                string[] methods = bolog.Method.Split(',');
                if (methods.Contains(methodName))
                {
                    LoggerBLL.LogUserInfo(UserUtil.GetCurrentUser().PlantCode, bolog.Module, UserUtil.GetCurrentUser().UserID.ToString(), "1", EventLevel.Normal, GetLogXmlContent(dmlParams));
                    //Log4NetHelper.Instance.LogUserInfo(null, UserUtil.GetCurrentUser().PlantCode, bolog.Module, methodName, UserUtil.GetCurrentUser().UserID.ToString(), "1", EventLevel.Normal, GetLogXmlContent(dmlParams), null);
                }
            }
            catch
            {
            }
        }

        private static string GetLogContent(params object[] dmlParams)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dmlParams.Length; i++)
            {
                sb.Append("param" + (i + 1).ToString() + ":");
                PropertyInfo[] oldObjPropertyList = dmlParams[i].GetType().GetProperties();
                foreach (PropertyInfo oldProperty in oldObjPropertyList)
                {
                    object oldValue = GetValueObject(oldProperty, dmlParams[i]);
                    if (oldValue == null)
                        continue;

                    sb.Append("," + oldProperty.Name + ":" + oldValue.ToString());
                }
            }

            return sb.ToString();
        }

        private static string GetLogXmlContent(params object[] dmlParams)
        {
            if (dmlParams == null || dmlParams.Length == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            foreach (object t in dmlParams)
            {
                XElement xe = new XElement(t.GetType().Name);
                PropertyInfo[] oldObjPropertyList = t.GetType().GetProperties();
                foreach (PropertyInfo oldProperty in oldObjPropertyList)
                {
                    object oldValue = GetValueObject(oldProperty, t);
                    if (oldValue == null)
                        continue;
                    xe.Add(new XElement(oldProperty.Name, oldValue.ToString()));
                }

                sb.Append(xe.ToString());
            }

            return sb.ToString();
        }
    }
}
