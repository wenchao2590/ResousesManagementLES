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
        /// д������Ϣ��EventLog����
        /// </summary>
        /// <param name="EventID">�¼��ţ�ϵͳ���壬����ʵ������</param>
        /// <param name="EventState">�¼�״̬,����δ�����Ѵ����ѽ��ܣ���������״̬����ο�ö�ٶ���</param>
        /// <param name="EventType">�¼����ͣ�ϵͳ���壬��eps��Eps Massage,JIT����,JIT��Ϣ����¼�͵ǳ�����ο�ö�ٶ���</param>
        /// <param name="EventLevel">�¼�����,ϵͳ������ ���������󣬾��棬��Ϣ���ɹ����,ʧ����˵ȣ���ο�ö�ٶ���</param>
        /// <param name="EventSource">�¼���Դ</param>
        /// <param name="EventBrief">�¼�����</param>
        /// <param name="detail">�¼�����</param>
        /// <param name="disposal">�¼���������</param>
        public static void WriteEvent(int EventID, int EventState, int EventType, int EventLevel, string EventSource,
            string EventBrief, string detail, string disposal)
        {
            WriteEvent(EventID, EventState, EventType, EventLevel, EventSource, EventBrief, detail, disposal, "", DateTime.MinValue, "", "", "", "", "", "", "", "", "","");
        }


        /// <summary>
        /// д������Ϣ��EventLog����
        /// </summary>
        /// <param name="EventID">�¼��ţ�ϵͳ���壬����ʵ������</param>
        /// <param name="EventState">�¼�״̬,����δ�����Ѵ����ѽ��ܣ���������״̬����ο�ö�ٶ���</param>
        /// <param name="EventType">�¼����ͣ�ϵͳ���壬��eps��Eps Massage,JIT����,JIT��Ϣ����¼�͵ǳ�����ο�ö�ٶ���</param>
        /// <param name="EventLevel">�¼�����,ϵͳ������ ���������󣬾��棬��Ϣ���ɹ����,ʧ����˵ȣ���ο�ö�ٶ���</param>
        /// <param name="EventSource">�¼���Դ</param>
        /// <param name="EventBrief">�¼�����</param>
        /// <param name="detail">�¼�����</param>
        /// <param name="disposal">�¼���������</param>
        /// <param name="DealName">������</param>
        /// <param name="DealDate">����ʱ��</param>
        public static void WriteEvent(int EventID, int EventState, int EventType, int EventLevel, string EventSource,
            string EventBrief, string detail, string disposal,string DealName,DateTime DealDate)
        {
            WriteEvent(EventID, EventState, EventType, EventLevel, EventSource, EventBrief, detail, disposal, DealName, DealDate,"","","","","","","","","","");
        }


        /// <summary>
        /// д������Ϣ��EventLog����
        /// </summary>
        /// <param name="EventID">�¼��ţ�ϵͳ���壬����ʵ������</param>
        /// <param name="EventState">�¼�״̬,����δ�����Ѵ����ѽ��ܣ���������״̬����ο�ö�ٶ���</param>
        /// <param name="EventType">�¼����ͣ�ϵͳ���壬��eps��Eps Massage,JIT����,JIT��Ϣ����¼�͵ǳ�����ο�ö�ٶ���</param>
        /// <param name="EventLevel">�¼�����,ϵͳ������ ���������󣬾��棬��Ϣ���ɹ����,ʧ����˵ȣ���ο�ö�ٶ���</param>
        /// <param name="EventSource">�¼���Դ</param>
        /// <param name="EventBrief">�¼�����</param>
        /// <param name="detail">�¼�����</param>
        /// <param name="disposal">�¼���������</param>
        /// <param name="DealName">������</param>
        /// <param name="DealDate">����ʱ��</param>
        /// <param name="Param1">���ò���1</param>
        /// <param name="Param2">���ò���2</param>
        /// <param name="Param3">���ò���3</param>
        /// <param name="Param4">���ò���4</param>
        /// <param name="Param5">���ò���5</param>
        /// <param name="Param6">���ò���6</param>
        /// <param name="Param7">���ò���7</param>
        /// <param name="Param8">���ò���8</param>
        /// <param name="Param9">���ò���9</param>
        /// <param name="Param10">���ò���10</param>
        public static void WriteEvent(int EventID, int EventState, int EventType, int EventLevel, string EventSource,
            string EventBrief, string detail, string disposal, string DealName,DateTime DealDate,string Param1, string Param2, string Param3, string Param4,
            string Param5, string Param6, string Param7, string Param8, string Param9, string Param10)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// ʹ�ö��������,������EventLog
        /// </summary>
        public static void WriteEvent()
        {

        }
        #endregion

        #region OperationLog
        /// <summary>
        /// д������־�����ݿ��OprationLog
        /// </summary>
        /// <param name="LoginName">������</param>
        /// <param name="ActionID">������ʶ��</param>
        /// <param name="Detail">��������</param>
        public static void WriteOperation(string LoginName, int ActionID, string Detail)
        {
            WriteOperation(LoginName, ActionID, Detail, "", "", "", "", "", "", "", "", "", "");
        }
        /// <summary>
        /// д������־�����ݿ��OprationLog
        /// </summary>
        /// <param name="LoginName">������</param>
        /// <param name="ActionID">������ʶ��</param>
        /// <param name="Detail">��������</param>
        /// <param name="Param1">���ò���1</param>
        /// <param name="Param2">���ò���2</param>
        /// <param name="Param3">���ò���3</param>
        /// <param name="Param4">���ò���4</param>
        /// <param name="Param5">���ò���5</param>
        /// <param name="Param6">���ò���6</param>
        /// <param name="Param7">���ò���7</param>
        /// <param name="Param8">���ò���8</param>
        /// <param name="Param9">���ò���9</param>
        /// <param name="Param10">���ò���10</param>
        public static void WriteOperation(string LoginName, int ActionID, string Detail, string param1, string param2, string param3,
            string param4, string param5, string param6, string param7, string param8, string param9, string param10)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// ʹ�ö��������,������OperationLog
        /// </summary>
        public static void WriteOperation()
        {

        }
        #endregion
    }
}
