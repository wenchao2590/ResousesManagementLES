using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Infrustructure.Utilities;
using Infrustructure.Event;

namespace Infrustructure.Logging
{
    public static class EventLogger
    {
        private static readonly string TL_SYS_EVENT_LOG_INSERT =
@"INSERT INTO [MES].[TL_SYS_EVENT_LOG] 
           ([EVENT_TIME]
           ,[EVENT_ID]
           ,[EVENT_SOURCE]
           ,[EVENT_STATE]
           ,[EVENT_TYPE]
           ,[EVENT_LEVEL]
           ,[EVENT_DETAIL]
           ,[PARAMETER1]
           ,[PARAMETER2]
           ,[PARAMETER3]
           ,[PARAMETER4]
           ,[PARAMETER5]
           ,[PARAMETER6]
           ,[PARAMETER7]
           ,[PARAMETER8]
           ,[PARAMETER9]
           ,[PARAMETER10]
           ,[COMMENTS])
     VALUES
           (GetDate()
           ,@EVENT_ID
           ,@EVENT_SOURCE
           ,@EVENT_STATE
           ,@EVENT_TYPE
           ,@EVENT_LEVEL
           ,@EVENT_DETAIL
           ,@PARAMETER1
           ,@PARAMETER2
           ,@PARAMETER3
           ,@PARAMETER4
           ,@PARAMETER5
           ,@PARAMETER6
           ,@PARAMETER7
           ,@PARAMETER8
           ,@PARAMETER9
           ,@PARAMETER10
           ,@COMMENTS);";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventLog"></param>
        public static void LogEvent(EventLogInfo eventLog)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                DbCommand command = db.GetSqlStringCommand(TL_SYS_EVENT_LOG_INSERT);

                db.AddInParameter(command, "@EVENT_ID", DbType.Int32, DBConvert.ToDBValue((int)eventLog.EventId));
                db.AddInParameter(command, "@EVENT_SOURCE", DbType.AnsiString, DBConvert.ToDBValue(eventLog.EventSource));
                db.AddInParameter(command, "@EVENT_STATE", DbType.Int32, DBConvert.ToDBValue((int)eventLog.EventState));
                db.AddInParameter(command, "@EVENT_TYPE", DbType.Int32, DBConvert.ToDBValue((int)eventLog.EventType));
                db.AddInParameter(command, "@EVENT_LEVEL", DbType.Int32, DBConvert.ToDBValue((int)eventLog.EventLevel));
                db.AddInParameter(command, "@EVENT_DETAIL", DbType.AnsiString, DBConvert.ToDBValue(eventLog.EventDetail));
                db.AddInParameter(command, "@PARAMETER1", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter1));
                db.AddInParameter(command, "@PARAMETER2", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter2));
                db.AddInParameter(command, "@PARAMETER3", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter3));
                db.AddInParameter(command, "@PARAMETER4", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter4));
                db.AddInParameter(command, "@PARAMETER5", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter5));
                db.AddInParameter(command, "@PARAMETER6", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter6));
                db.AddInParameter(command, "@PARAMETER7", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter7));
                db.AddInParameter(command, "@PARAMETER8", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter8));
                db.AddInParameter(command, "@PARAMETER9", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter9));
                db.AddInParameter(command, "@PARAMETER10", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter10));
                db.AddInParameter(command, "@COMMENTS", DbType.AnsiString, DBConvert.ToDBValue(eventLog.Parameter10));

                db.ExecuteNonQuery(command);

            }
            catch (System.Exception ex)
            {
                Logger.Instance.Error(typeof(EventLogger), "事件日管理", "写事件日志失败, 相关参数" + eventLog.ToString(), ex);
                //Logger.Instance.Error(typeof(EventLogger), "写事件日志失败, 相关参数" + eventLog.ToString() + "错误信息:"+ ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventType"></param>
        /// <param name="eventLevel"></param>
        /// <param name="eventState"></param>
        /// <param name="eventSource"></param>
        /// <param name="eventBrief"></param>
        /// <param name="eventDetail"></param>
        /// <param name="eventTime"></param>
        public static void LogEvent(EventID eventId, EventType eventType, EventLevel eventLevel,
                                    EventState eventState, string eventSource, string eventBrief,
                                    string eventDetail, DateTime eventTime)
        {
            EventLogInfo eventInfo = new EventLogInfo();

            eventInfo.EventId = eventId;
            eventInfo.EventState = eventState;
            eventInfo.EventType = eventType;
            eventInfo.EventSource = eventSource;
            eventInfo.EventDetail = eventDetail;
            eventInfo.EventTime = eventTime;

            LogEvent(eventInfo);
        }

        #region 过期
        //        public static void LogOperation(OperationlogInfo info)
        //        {
        //            const string sql = @"INSERT INTO [TS_BAS_OperationLog] WITH(ROWLOCK) (
        //                [operation_time],
        //                [action_id],
        //                [operator],
        //                [operation_detail],
        //                [operation_parameter1],
        //                [operation_parameter2],
        //                [operation_parameter3],
        //                [operation_parameter4],
        //                [operation_parameter5],
        //                [operation_parameter6],
        //                [operation_parameter7],
        //                [operation_parameter8],
        //                [operation_parameter9],
        //                [operation_parameter10]
        //            ) VALUES (
        //                GETDATE(),
        //                @action_id,
        //                @operator,
        //                @operation_detail,
        //                @operation_parameter1,
        //                @operation_parameter2,
        //                @operation_parameter3,
        //                @operation_parameter4,
        //                @operation_parameter5,
        //                @operation_parameter6,
        //                @operation_parameter7,
        //                @operation_parameter8,
        //                @operation_parameter9,
        //                @operation_parameter10
        //            );select isnull(scope_identity(),-1);";
        //            try
        //            {
        //                using (SqlConnection connection = new SqlConnection(_connectionstring))
        //                {
        //                    SqlCommand command = connection.CreateCommand();
        //                    command.CommandText = sql;
        //                    db.AddInParameter(command,"@action_id", DbType.Int32).Value = (int)info.ActionId;
        //                    db.AddInParameter(command,"@operator", DbType.AnsiString).Value = DBConvert.ToDBValue(info.Operator);
        //                    db.AddInParameter(command,"@operation_detail", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationDetail);
        //                    db.AddInParameter(command,"@operation_parameter1", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter1);
        //                    db.AddInParameter(command,"@operation_parameter2", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter2);
        //                    db.AddInParameter(command,"@operation_parameter3", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter3);
        //                    db.AddInParameter(command,"@operation_parameter4", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter4);
        //                    db.AddInParameter(command,"@operation_parameter5", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter5);
        //                    db.AddInParameter(command,"@operation_parameter6", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter6);
        //                    db.AddInParameter(command,"@operation_parameter7", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter7);
        //                    db.AddInParameter(command,"@operation_parameter8", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter8);
        //                    db.AddInParameter(command,"@operation_parameter9", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter9);
        //                    db.AddInParameter(command,"@operation_parameter10", DbType.AnsiString).Value = DBConvert.ToDBValue(info.OperationParameter10);

        //                    connection.Open();
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //            catch (System.Exception ex)
        //            {
        //                Logger.Default.Error(null, string.Format("写操作日志失败, 相关参数:" + info.ToString()), ex);
        //            }
        //        }

        //        public static void LogMessage(InfomationanderrorsInfo info)
        //        {
        //            const string sql = @"INSERT INTO [TS_BAS_InfomationAndErrors] (
        //			[time_stamp],
        //						[application],
        //						[function_name],
        //						[class],
        //						[information_or_error],
        //						[exception_message],
        //						[error_code]
        //			) VALUES (
        //			GETDATE(),
        //						@application,
        //						@function_name,
        //						@class,
        //						@information_or_error,
        //						@exception_message,
        //						@error_code
        //			);select isnull(scope_identity(),-1);";
        //            try
        //            {
        //                using (SqlConnection connection = new SqlConnection(_connectionstring))
        //                {
        //                    SqlCommand command = connection.CreateCommand();
        //                    command.CommandText = sql;

        //                    db.AddInParameter(command,"@application", DbType.AnsiString).Value = DBConvert.ToDBValue(info.Application);
        //                    db.AddInParameter(command,"@function_name", DbType.AnsiString).Value = DBConvert.ToDBValue(info.FunctionName);
        //                    db.AddInParameter(command,"@class", DbType.AnsiString).Value = DBConvert.ToDBValue(info.Class);
        //                    db.AddInParameter(command,"@information_or_error", DbType.StringFixedLength).Value = DBConvert.ToDBValue(info.InformationOrError);
        //                    db.AddInParameter(command,"@exception_message", DbType.AnsiString).Value = DBConvert.ToDBValue(info.ExceptionMessage);
        //                    db.AddInParameter(command,"@error_code", DbType.AnsiString).Value = DBConvert.ToDBValue(info.ErrorCode);

        //                    connection.Open();
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //            catch (System.Exception ex)
        //            {
        //                Logger.Default.Error(null, string.Format("写消息日志失败, 相关参数:" + info.ToString()), ex);
        //            }
        //        }
        #endregion
    }
}
