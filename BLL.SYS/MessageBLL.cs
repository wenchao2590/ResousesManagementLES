using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// MessageBLL
    /// </summary>
    public class MessageBLL
    {
        #region Common
        /// <summary>
        /// MessageDAL
        /// </summary>
        MessageDAL dal = new MessageDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<MessageInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(MessageInfo info)
        {
            int cnt = dal.GetCounts("[MESSAGE_CN] = N'" + info.MessageCn + "' and [MESSAGE_TYPE] = " + info.MessageType.GetValueOrDefault() + "");
            if (cnt > 0)
                throw new Exception("MC:1x00000019");///同一类型下提示消息不能重复

            if (!string.IsNullOrEmpty(info.MessageEn))
            {
                cnt = dal.GetCounts("[MESSAGE_EN] = N'" + info.MessageEn + "' and [MESSAGE_TYPE] = " + info.MessageType.GetValueOrDefault() + "");
                if (cnt > 0)
                    throw new Exception("MC:1x00000019");///同一类型下提示消息不能重复
            }
            ///消息代码
            string messageType = "0";
            switch (info.MessageType.GetValueOrDefault())
            {
                case (int)MessageTypeConstants.SysMsg: messageType = "1"; break;
                case (int)MessageTypeConstants.ProgramMsg: messageType = "7"; break;
                case (int)MessageTypeConstants.ClientMsg: messageType = "3"; break;
                default: messageType = "0"; break;
            }
            ///
            string messageCode = new SeqDefineDAL().GetCurrentCode("MESSAGE_CODE", messageType);
            cnt = dal.GetCounts("[MESSAGE_CODE] = N'" + messageCode + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000000");///消息代码重复，请确认后重新保存

            info.MessageCode = messageCode;
            return dal.Add(info);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string messageCn = CommonBLL.GetFieldValue(fields, "MESSAGE_CN");
            string messageType = CommonBLL.GetFieldValue(fields, "MESSAGE_TYPE");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [MESSAGE_CN] = N'" + messageCn + "' and [MESSAGE_TYPE] = " + (string.IsNullOrEmpty(messageType) ? "0" : messageType) + "");
            if (cnt > 0)
                throw new Exception("MC:1x00000019");///同一类型下提示消息不能重复
            string messageEn = CommonBLL.GetFieldValue(fields, "MESSAGE_EN");
            if (!string.IsNullOrEmpty(messageEn))
            {
                cnt = dal.GetCounts("[ID] <> " + id + " and [MESSAGE_EN] = N'" + messageEn + "' and [MESSAGE_TYPE] = " + (string.IsNullOrEmpty(messageType) ? "0" : messageType) + "");
                if (cnt > 0)
                    throw new Exception("MC:1x00000019");///同一类型下提示消息不能重复
            }
            ///
            string messageCode = CommonBLL.GetFieldValue(fields, "MESSAGE_CODE");
            if (!string.IsNullOrEmpty(messageCode))
            {
                cnt = dal.GetCounts("[ID] <> " + id + " and [MESSAGE_CODE] = N'" + messageCode + "'");
                if (cnt > 0)
                    throw new Exception("MC:1x00000000");///消息代码重复，请确认后重新保存
            }
            else
                fields = CommonBLL.ClearField(fields, "MESSAGE_CODE");
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        /// <summary>
        /// 获取提示信息
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public string GetMessage(string lang, string messageCode)
        {
            if (lang.ToLower() == "en-us")
                return dal.GetMessageEn(messageCode);
            return dal.GetMessageCn(messageCode);
        }

        public string GetListMessage()
        {
            string langureData = "{";
            List<MessageInfo> list = dal.GetList("select * from TS_SYS_MESSAGE where VALID_FLAG='true'");
            for (int i = 0; i < list.Count; i++)
            {
                string code = list[i].MessageCode;
                string cn = list[i].MessageCn;
                string en = list[i].MessageEn;
                langureData += "code" + code + ":";
                langureData += "{cn:" + "'" + cn + "',en:'" + en + "'},";
            }
            langureData = langureData.Substring(0, langureData.Length - 1);
            return langureData = langureData + "}";
        }
        /// <summary>
        /// 获取PDA提示信息实体集合
        /// </summary>
        /// <returns>返回 List MessageInfo 实体集合</returns>
        public List<MessageInfo> GetList(string whereText, string orderText)
        {
            return dal.GetList(whereText, orderText);
        }
        /// <summary>
        /// 获取PDA提示信息实体集合json字符串
        /// </summary>
        /// <returns>返回 MessageInfo list json字符串 </returns>
        public string GetListJsonMessageForPDA()
        {
            string langureData = "[";
            List<MessageInfo> list = dal.GetList("select * from TS_SYS_MESSAGE where VALID_FLAG='true' AND MESSAGE_CODE LIKE '%3x%'");

            for (int i = 0; i < list.Count; i++)
            {
                string code = list[i].MessageCode;
                string cn = list[i].MessageCn;
                string en = list[i].MessageEn;
                langureData += "{code:'" + code + "',cn:" + "'" + cn + "',en:'" + en + "'},";
            }
            langureData = langureData.Substring(0, langureData.Length - 1);
            return langureData = langureData + "]";
        }
    }
}
