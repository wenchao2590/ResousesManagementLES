using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.Utilities
{
	[Obsolete("Never be used.")]
    class EventOperationWrite
    {


        #region EventLog
        /// <summary>
        /// 写报警信息到EventLog表中
        /// </summary>
        /// <param name="EventID">事件号，系统定义，具有实际意义</param>
        /// <param name="EventState">事件状态,共有未处理，已处理，已接受，查阅四种状态，请参考枚举定义</param>
        /// <param name="EventType">事件类型，系统定义，如eps，Eps Massage,JIT错误,JIT信息及登录和登出，请参考枚举定义</param>
        /// <param name="EventLevel">事件级别,系统定义有 正常，错误，警告，信息，成功审核,失败审核等，请参考枚举定义</param>
        /// <param name="EventSource">事件来源</param>
        /// <param name="EventBrief">事件简述</param>
        /// <param name="detail">事件详述</param>
        /// <param name="disposal">事件处理描述</param>
        public static void WriteEvent(int EventID, int EventState, int EventType, int EventLevel, string EventSource,
            string EventBrief, string detail, string disposal)
        {
            WriteEvent(EventID, EventState, EventType, EventLevel, EventSource, EventBrief, detail, disposal, "", DateTime.MinValue, "", "", "", "", "", "", "", "", "","");
        }


        /// <summary>
        /// 写报警信息到EventLog表中
        /// </summary>
        /// <param name="EventID">事件号，系统定义，具有实际意义</param>
        /// <param name="EventState">事件状态,共有未处理，已处理，已接受，查阅四种状态，请参考枚举定义</param>
        /// <param name="EventType">事件类型，系统定义，如eps，Eps Massage,JIT错误,JIT信息及登录和登出，请参考枚举定义</param>
        /// <param name="EventLevel">事件级别,系统定义有 正常，错误，警告，信息，成功审核,失败审核等，请参考枚举定义</param>
        /// <param name="EventSource">事件来源</param>
        /// <param name="EventBrief">事件简述</param>
        /// <param name="detail">事件详述</param>
        /// <param name="disposal">事件处理描述</param>
        /// <param name="DealName">处理人</param>
        /// <param name="DealDate">处理时间</param>
        public static void WriteEvent(int EventID, int EventState, int EventType, int EventLevel, string EventSource,
            string EventBrief, string detail, string disposal,string DealName,DateTime DealDate)
        {
            WriteEvent(EventID, EventState, EventType, EventLevel, EventSource, EventBrief, detail, disposal, DealName, DealDate,"","","","","","","","","","");
        }


        /// <summary>
        /// 写报警信息到EventLog表中
        /// </summary>
        /// <param name="EventID">事件号，系统定义，具有实际意义</param>
        /// <param name="EventState">事件状态,共有未处理，已处理，已接受，查阅四种状态，请参考枚举定义</param>
        /// <param name="EventType">事件类型，系统定义，如eps，Eps Massage,JIT错误,JIT信息及登录和登出，请参考枚举定义</param>
        /// <param name="EventLevel">事件级别,系统定义有 正常，错误，警告，信息，成功审核,失败审核等，请参考枚举定义</param>
        /// <param name="EventSource">事件来源</param>
        /// <param name="EventBrief">事件简述</param>
        /// <param name="detail">事件详述</param>
        /// <param name="disposal">事件处理描述</param>
        /// <param name="DealName">处理人</param>
        /// <param name="DealDate">处理时间</param>
        /// <param name="Param1">备用参数1</param>
        /// <param name="Param2">备用参数2</param>
        /// <param name="Param3">备用参数3</param>
        /// <param name="Param4">备用参数4</param>
        /// <param name="Param5">备用参数5</param>
        /// <param name="Param6">备用参数6</param>
        /// <param name="Param7">备用参数7</param>
        /// <param name="Param8">备用参数8</param>
        /// <param name="Param9">备用参数9</param>
        /// <param name="Param10">备用参数10</param>
        public static void WriteEvent(int EventID, int EventState, int EventType, int EventLevel, string EventSource,
            string EventBrief, string detail, string disposal, string DealName,DateTime DealDate,string Param1, string Param2, string Param3, string Param4,
            string Param5, string Param6, string Param7, string Param8, string Param9, string Param10)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 使用对象来完成,参数是EventLog
        /// </summary>
        public static void WriteEvent()
        {

        }
        #endregion

        #region OperationLog
        /// <summary>
        /// 写操作日志到数据库的OprationLog
        /// </summary>
        /// <param name="LoginName">操作人</param>
        /// <param name="ActionID">动作标识符</param>
        /// <param name="Detail">操作详述</param>
        public static void WriteOperation(string LoginName, int ActionID, string Detail)
        {
            WriteOperation(LoginName, ActionID, Detail, "", "", "", "", "", "", "", "", "", "");
        }
        /// <summary>
        /// 写操作日志到数据库的OprationLog
        /// </summary>
        /// <param name="LoginName">操作人</param>
        /// <param name="ActionID">动作标识符</param>
        /// <param name="Detail">操作详述</param>
        /// <param name="Param1">备用参数1</param>
        /// <param name="Param2">备用参数2</param>
        /// <param name="Param3">备用参数3</param>
        /// <param name="Param4">备用参数4</param>
        /// <param name="Param5">备用参数5</param>
        /// <param name="Param6">备用参数6</param>
        /// <param name="Param7">备用参数7</param>
        /// <param name="Param8">备用参数8</param>
        /// <param name="Param9">备用参数9</param>
        /// <param name="Param10">备用参数10</param>
        public static void WriteOperation(string LoginName, int ActionID, string Detail, string param1, string param2, string param3,
            string param4, string param5, string param6, string param7, string param8, string param9, string param10)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 使用对象来完成,参数是OperationLog
        /// </summary>
        public static void WriteOperation()
        {

        }
        #endregion
    }
}
