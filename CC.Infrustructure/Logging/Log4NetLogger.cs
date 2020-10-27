using System;
using log4net;
using System.Diagnostics;
using System.Reflection;

using log4net.Core;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Infrustructure.Logging
{
	public enum AppError
	{
		WARN = 0,
		EROR = 1,
		FATL = 2
	}
    
    // TODO: should be detached to a seperate project. by alex@20080827
    public class Log4NetLogger : ILogger
	{
		public const int InfoOrError_Info = 1;
		public const int InfoOrError_Error = 2;

		private static readonly ILog loginfo = LogManager.GetLogger("FotonMES.Logging.Info");
		private static readonly ILog logerror = LogManager.GetLogger("FotonMES.Logging.Error");
        private static readonly ILog logtrace = LogManager.GetLogger("FotonMES.Logging.Trace");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [Obsolete]
        public ILogger Error(object sender, string message)
        {
            this.Error(sender, string.Empty, message, null);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        /// <returns></returns>

        [Obsolete]
        public ILogger Error(object sender, Exception ex)
        {
            this.Error(sender, string.Empty, string.Empty, ex);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        [Obsolete]
        public ILogger Error(object sender, string message,Exception ex)
        {
            this.Error(sender, string.Empty, message, ex);

            return this;
        }

        /// <summary>
 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="functionName"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
		public ILogger Error(object sender, string functionName,string message,  Exception ex)
		{
            string referralMethod = GetReferralMethod();
            string msg = string.Format("{0}|当前方法:{1} | 详信息:{2} | 详细信息:{3}", DateTime.Now.ToString("yyyyMMddHHmmss"), referralMethod,message,ex);
            if (logerror != null && logerror.IsInfoEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(typeof(Log4NetLogger), logerror.Logger.Repository, logerror.Logger.Name, Level.Error, msg, ex);
                loggingEvent.Properties["catalog"] = functionName;
                logerror.Logger.Log(loggingEvent);

                //logerror.Error(string.Format("{0}|当前方法:{1} | 信息:{2} | 详细信息:{3}", DateTime.Now.ToString("yyyyMMddHHmmss"), referralMethod, msg, ex), ex);
            }

            //// 向LES系统报警
            //Database productionDb = DatabaseFactory.CreateDatabase();
            //// event_id=1501	历史数据归档异常
            //// event_state=0	未处理
            //// event_type=1601	历史数据归档异常
            //// event_level=1	错误
            //const string sql = "insert into dbo.TS_BAS_EventLog (event_time,event_id,event_state,event_type,event_level,event_source,event_brief,event_detail) values(getdate(),1501,0,1601,1,@source, @brief, @detail);";
            //DbCommand dbCommand = productionDb.GetSqlStringCommand(sql);
            //productionDb.AddInParameter(dbCommand, "source", DbType.AnsiString, referralMethod);
            //productionDb.AddInParameter(dbCommand, "brief", DbType.AnsiString, msg + ((ex == null) ? string.Empty : ex.Message));
            //productionDb.AddInParameter(dbCommand, "detail", DbType.AnsiString, (ex == null) ? string.Empty : ex.ToString());
            //productionDb.ExecuteNonQuery(dbCommand);
            return this;
		}

		public ILogger Info(object sender, string msg)
		{
            if (loginfo != null && loginfo.IsInfoEnabled)
                loginfo.Info(string.Format("{0}|当前方法:{1} | 信息:{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), GetReferralMethod(), msg));
			return this;
		}

		public ILogger Trace(object sender, string msg)
		{
            if (logtrace != null && logtrace.IsInfoEnabled)
                logtrace.Info(string.Format("{0}|当前方法:{1} | 信息:{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), GetReferralMethod(), msg));
            return this;
		}

		public ILogger Log(object sender, string msg)
		{
            return Info(sender, msg);
		}

		public static ILog GetLogger(string loggerName)
		{
			return LogManager.GetLogger(loggerName);
		}

        /// <summary>
        ///  获取被引用的方法
        /// </summary>
        /// <returns></returns>
        static string GetReferralMethod()
        {
            StackTrace st = new StackTrace(true);
            MethodBase m = st.GetFrame(2).GetMethod();
            return string.Format("{0}.{1}", m.DeclaringType, m.Name);
        }
	}
}
