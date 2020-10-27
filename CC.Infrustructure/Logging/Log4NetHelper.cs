using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Infrustructure.Event;
using Infrustructure.Logging.Proxy;
using Infrustructure.Utilities;
using log4net;
using log4net.Core;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Infrustructure.Logging
{
    public class Log4NetHelper
    {
        private readonly ILog _fileLog;

        private readonly ILog _sysLog;
        private readonly ILog _userLog;
        private readonly ILog _interfaceLog;

        /// <summary>
        /// 获取实例
        /// </summary>
        public static Log4NetHelper Instance { get; private set; }

        private Log4NetHelper()
        {
            _fileLog = LogManager.GetLogger("YFV.MES.Logging.FileLog");

            _sysLog = LogManager.GetLogger("YFV.MES.Log.SysLog");
            _userLog = LogManager.GetLogger("YFV.MES.Log.UserLog");

            _interfaceLog = LogManager.GetLogger("YFV.MES.Log.interfaceLog");
        }

        static Log4NetHelper()
        {
            Instance = new Log4NetHelper();
        }

        /// <summary>
        /// 错误日志 已知错误编码
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="exceptionCode">错误编号</param>
        /// <param name="message">异常信息</param>
        public void LogError(string plantCode, string moduleCode, string exceptionCode, string message)
        {
            this.LogError(this, plantCode, moduleCode, exceptionCode, string.Empty, EventLevel.Error, string.Empty, string.Empty, null, message, null);
        }
        /// <summary>
        /// 错误日志 处理无法预知的异常
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventLevel">事件级别</param>
        /// <param name="message">异常消息</param>
        public void LogError(string plantCode, string moduleCode, string eventType, EventLevel eventLevel, string message)
        {
            this.LogError(this, plantCode, moduleCode, string.Empty, eventType, eventLevel, message, string.Empty, null, message, null);
        }
        /// <summary>
        /// 错误日志 处理无法预知的异常 
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventLevel">错误级别</param>
        /// <param name="ex">异常详细信息</param>
        public void LogError(string plantCode, string moduleCode, string eventType, EventLevel eventLevel, Exception ex)
        {
            this.LogError(this, plantCode, moduleCode, string.Empty, eventType, eventLevel, ex.Message, string.Empty, null, ex.Message, ex);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="plantCode">工厂编号</param>
        /// <param name="moduleCode">模块编号</param>
        /// <param name="exceptionCode">消息编码</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventLevel">日志级别</param>
        /// <param name="eventName">日志名称</param>
        /// <param name="eventDescription">日志描述</param>
        /// <param name="listExternParas">扩展参数 最多三个</param>
        /// <param name="message">异常消息</param>
        /// <param name="ex">异常详细信息</param>
        public void LogError(object sender, string plantCode, string moduleCode, string exceptionCode, string eventType, EventLevel eventLevel, string eventName, string eventDescription, List<string> listExternParas, string message, Exception ex)
        {
            var model = new SysInfoProxy
            {
                Logid = Guid.NewGuid(),
                Plantcode = plantCode,
                OccurTime = DateTime.Now,
                Modulecode = moduleCode
            };
            if (string.IsNullOrWhiteSpace(exceptionCode))
            {
                string msg;
                if (ex == null)
                {
                    msg = string.IsNullOrWhiteSpace(message) ? eventDescription : message;
                }
                else
                {
                    msg = ex.StackTrace;
                }
                model.Eventlevel = eventLevel.ToString();
                model.Eventdescription = msg;
                model.Eventname = eventName;
                model.Eventtype = eventType;
            }
            else
            {
                model.Exceptioncode = exceptionCode;
                model.Eventname = exceptionCode;
            }
            if (listExternParas != null)
            {
                if (listExternParas.Count == 1)
                    model.Para1 = listExternParas[0];
                if (listExternParas.Count == 2)
                {
                    model.Para1 = listExternParas[1];
                    model.Para2 = listExternParas[2];

                }
                if (listExternParas.Count == 3)
                {
                    model.Para1 = listExternParas[0];
                    model.Para2 = listExternParas[1];
                    model.Para3 = listExternParas[2];
                }
            }
            WriteSysLog(model);
            //非异常日志 不记录在本地
            if (string.IsNullOrWhiteSpace(message) && ex == null)
                this.Error(this, message, ex);
        }

        /// <summary>
        /// 用户操作日志 已知消息编码
        /// </summary>
        /// <param name="plantCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="msgCode"></param>
        /// <param name="userId"></param>
        public void LogUserInfo(string plantCode, string moduleCode, string msgCode, string userId)
        {
            LogUserInfo(this, plantCode, moduleCode, msgCode, userId, null, EventLevel.Normal, null, null);
        }

        /// <summary>
        /// /用户操作日志 未知消息编码
        /// </summary>
        /// <param name="plantCode">工厂编号</param>
        /// <param name="moduleCode">模块编号</param>
        /// <param name="msgCode"></param>
        /// <param name="userId">用户编号</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventLevel">事件级别</param>
        /// <param name="eventDescription">事件描述</param>
        public void LogUserInfo(string plantCode, string moduleCode, string msgCode, string userId, string eventType, EventLevel eventLevel, string eventDescription)
        {
            LogUserInfo(this, plantCode, moduleCode, msgCode, userId, eventType, eventLevel, eventDescription, null);
        }
        /// <summary>
        /// 用户操作日志
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="plantCode">工厂编号</param>
        /// <param name="moduleCode">模块编号</param>
        /// <param name="msgCode">消息编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventLevel">事件级别</param>
        /// <param name="eventDescription">事件描述</param>
        /// <param name="listExternParas">扩展参数</param>
        public void LogUserInfo(object sender, string plantCode, string moduleCode, string msgCode, string userId, string eventType, EventLevel eventLevel, string eventDescription, List<string> listExternParas)
        {
            try
            {
                UserLogInfoProxy model = new UserLogInfoProxy
                {
                    Logid = Guid.NewGuid(),
                    Plantcode = plantCode,
                    OccurTime = DateTime.Now,
                    Modulecode = moduleCode,
                    Userid = userId
                };
                if (string.IsNullOrWhiteSpace(msgCode))
                {
                    model.Eventlevel = eventLevel.ToString();
                    model.Eventtype = eventType;
                }
                else
                {
                    model.Eventlevel = eventLevel.ToString();
                    model.Eventtype = eventType;
                    model.Msgcode = msgCode;
                }
                model.Eventdescription = eventDescription;
                if (listExternParas != null)
                {
                    if (listExternParas.Count == 1)
                        model.Para1 = listExternParas[0];
                    if (listExternParas.Count == 2)
                    {
                        model.Para1 = listExternParas[1];
                        model.Para2 = listExternParas[2];

                    }
                    if (listExternParas.Count == 3)
                    {
                        model.Para1 = listExternParas[0];
                        model.Para2 = listExternParas[1];
                        model.Para3 = listExternParas[2];
                    }
                }
                WriteOperateLog(model);
                //this.Info(this, EventDescription);
            }
            catch (Exception ex)
            {
                this.LogError(plantCode, moduleCode, string.Empty, eventLevel, ex);
                throw;
            }
        }
        /// <summary>
        /// 接口非异常日志
        /// </summary>
        /// <param name="outSysCode"></param>
        /// <param name="type"></param>
        /// <param name="level"></param>
        /// <param name="eventName"></param>
        /// <param name="desc"></param>
        public void LogIOServiceLog(string outSysCode, IOEventType type, EventLevel level, string eventName, string desc)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(outSysCode))
                {
                    var info = new InterfaceLogInfoProxy
                    {
                        CreateTime = DateTime.Now,
                        Creator = "MES System",
                        EventDescription = desc,
                        EventLevel = level.ToString(),
                        EventName = eventName,
                        EventType = type.ToString(),
                        LogID = Guid.NewGuid(),
                        OutSysCode = Guid.Parse(outSysCode)
                    };

                    WriteInterfaceServiceLog(info);
                }
            }
            catch (Exception exNew)
            {
                this.LogError(null, null, string.Empty, level, exNew);
                throw;
            }
        }
        /// <summary>
        /// 接口异常日志
        /// </summary>
        /// <param name="outSysCode"></param>
        /// <param name="type"></param>
        /// <param name="level"></param>
        /// <param name="ex"></param>
        public void LogIOServiceLog(string outSysCode, IOEventType type, EventLevel level, Exception ex)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(outSysCode))
                {
                    InterfaceLogInfoProxy info = new InterfaceLogInfoProxy
                    {
                        CreateTime = DateTime.Now,
                        Creator = "MES System",
                        EventDescription = ex.StackTrace,
                        EventLevel = level.ToString(),
                        EventName = ex.Message,
                        EventType = type.ToString(),
                        LogID = Guid.NewGuid(),
                        OutSysCode = Guid.Parse(outSysCode)
                    };

                    WriteInterfaceServiceLog(info);
                }
            }
            catch (Exception exNew)
            {
                this.LogError(null, null, string.Empty, level, exNew);
                throw;
            }
        }

        /// <summary>
        /// 本地日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void Info(object sender, string message)
        {
            if (_fileLog != null && _fileLog.IsInfoEnabled)
                _fileLog.Info(string.Format("{0}|信息:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
        }

        /// <summary>
        /// 本地跟踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void Trace(object sender, string message)
        {
            if (_fileLog != null && _fileLog.IsDebugEnabled)
                _fileLog.Info(string.Format("{0}|跟踪:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
        }
        /// <summary>
        /// 记录本地错误日志
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="message">错误信息</param>
        /// <param name="ex">信息详情</param>
        public void Error(object sender, string message, Exception ex)
        {
            string msg = string.Format("{0}|错误信息:{1} | 错误详细信息:{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message, ex);
            if (_fileLog != null && _fileLog.IsErrorEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(typeof(Log4NetHelper), _fileLog.Logger.Repository, _fileLog.Logger.Name, Level.Error, msg, ex);
                //loggingEvent.Properties["catalog"] = functionName;
                _fileLog.Logger.Log(loggingEvent);
            }
        }

        /// <summary>
        /// 记录本地错误日志
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="message">错误信息</param>
        public void Error(object sender, string message)
        {
            string msg = string.Format("{0}|错误信息:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
            if (_fileLog != null && _fileLog.IsErrorEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(typeof(Log4NetHelper), _fileLog.Logger.Repository, _fileLog.Logger.Name, Level.Error, msg, null);
                //loggingEvent.Properties["catalog"] = functionName;
                _fileLog.Logger.Log(loggingEvent);
            }
        }

        /// <summary>
        /// 记录windows日志 
        /// </summary>
        /// <param name="logName">日志名</param>
        /// <param name="logMessage">日志信息</param>
        /// <param name="eventType">日志类型</param>
        public void WriteWindowsLog(string logName, string logMessage, System.Diagnostics.EventLogEntryType eventType)
        {
            if (!System.Diagnostics.EventLog.SourceExists(logName))
            {
                System.Diagnostics.EventLog.CreateEventSource(logName, logName);
            }

            System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
            eventLog.Source = logName;
            eventLog.WriteEntry(logMessage, eventType);
        }

        /// <summary>
        /// 记录系统日志 写数据库
        /// </summary>
        /// <param name="model"></param>
        public void WriteSysLog(SysInfoProxy model)
        {
            if (!string.IsNullOrEmpty(model.Exceptioncode))
            {
                var msgInfo = GetInfo(model.Exceptioncode);
                if (msgInfo != null)
                {
                    model.Eventtype = msgInfo.Msgtype;
                    model.Eventlevel = msgInfo.Defaultlevel;
                    model.Eventdescription = msgInfo.Chncontent;
                    model.Para1 = msgInfo.Parameter1;
                    model.Para2 = msgInfo.Parameter2;
                    model.Para3 = msgInfo.Parameter3;
                }
            }
            if (_sysLog != null && _sysLog.IsInfoEnabled)
                _sysLog.Info(model);
        }
        /// <summary>
        /// 用户操作日志
        /// </summary>
        public void WriteOperateLog(UserLogInfoProxy model)
        {
            if (!string.IsNullOrEmpty(model.Msgcode))
            {
                var msgInfo = GetInfo(model.Msgcode);
                if (msgInfo != null)
                {
                    model.Eventtype = msgInfo.Msgtype;
                    model.Eventlevel = msgInfo.Defaultlevel;
                    model.Eventdescription = msgInfo.Chncontent;
                    model.Para1 = msgInfo.Parameter1;
                    model.Para2 = msgInfo.Parameter2;
                    model.Para3 = msgInfo.Parameter3;
                }
            }
            if (_userLog != null && _userLog.IsInfoEnabled)
                _userLog.Info(model);
        }
        /// <summary>
        /// 记录接口日志
        /// </summary>
        /// <param name="model"></param>
        public void WriteInterfaceServiceLog(InterfaceLogInfoProxy model)
        {
            if (_interfaceLog != null && _interfaceLog.IsInfoEnabled)
                _interfaceLog.Info(model);
        }

        #region 数据库操作方法

        private static MessageCodeInfoProxy GetInfo(string aMsgcode)
        {
            string MessageCode_SELECT_BY_ID =
            @"SELECT  MsgCode,
				ModuleCode,
				MsgType,
				DefaultLevel,
				CHNContent,
				EngContent,
				Parameter1,
				Parameter2,
				Parameter3,
				Creator,
				CreateTime,
				Modifier,
				ModifyTIme				 
			FROM [ADM].[MessageCode] WITH (NOLOCK)  
			 WHERE MsgCode=@MsgCode  ;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(MessageCode_SELECT_BY_ID);
            db.AddInParameter(dbCommand, "@MsgCode", DbType.String, aMsgcode);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateMessageCodeInfo(dr);
            }
            return null;
        }
        private static MessageCodeInfoProxy CreateMessageCodeInfo(IDataReader rdr)
        {
            var info = new MessageCodeInfoProxy
            {
                Msgcode = DBConvert.GetString(rdr, rdr.GetOrdinal("MsgCode")),
                Modulecode = DBConvert.GetString(rdr, rdr.GetOrdinal("ModuleCode")),
                Msgtype = DBConvert.GetString(rdr, rdr.GetOrdinal("MsgType")),
                Defaultlevel = DBConvert.GetString(rdr, rdr.GetOrdinal("DefaultLevel")),
                Chncontent = DBConvert.GetString(rdr, rdr.GetOrdinal("CHNContent")),
                Engcontent = DBConvert.GetString(rdr, rdr.GetOrdinal("EngContent")),
                Parameter1 = DBConvert.GetString(rdr, rdr.GetOrdinal("Parameter1")),
                Parameter2 = DBConvert.GetString(rdr, rdr.GetOrdinal("Parameter2")),
                Parameter3 = DBConvert.GetString(rdr, rdr.GetOrdinal("Parameter3")),
                Creator = DBConvert.GetString(rdr, rdr.GetOrdinal("Creator")),
                Createtime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CreateTime")),
                Lastmodifier = DBConvert.GetString(rdr, rdr.GetOrdinal("Modifier")),
                Lastmodifytime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("ModifyTIme"))
            };


            return info;
        }
        #endregion
    }
}
