using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{
    [XmlRoot("HEAD")]
    public class ESBHeadInfo
    {
     
        /// <summary>
        /// 事务ID，返回时原样带回
        /// </summary>
        [XmlElement("BIZTRANSACTIONID")]
        public string Biztransactionid { get ; set ; }
        /// <summary>
        /// 数据条数
        /// </summary>
        [XmlElement("SUCCESSCOUNT")]
        public string Successcount { get ; set ; }
        /// <summary>
        /// 异常编码
        /// </summary>
        [XmlElement("ERRORCODE")]
        public string Errorcode { get ; set ; }
        /// <summary>
        /// 异常信息
        /// </summary>
        [XmlElement("ERRORINFO")]
        public string Errorinfo { get ; set ; }
        /// <summary>
        /// 备注（可选）
        /// </summary>
        [XmlElement("COMMENTS")]
        public string Comments { get ; set ; }
        /// <summary>
        /// 处理结果，0成功，其他为1
        /// </summary>
        [XmlElement("RESULT")]
        public string Result { get ; set ; }
        /// <summary>
        /// 发送数据条数
        /// </summary>
        [XmlElement("COUNT")]
        public string Count { get ; set ; }
        /// <summary>
        /// 数据接收方的系统简称
        /// </summary>
        [XmlElement("CONSUMER")]
        public string Consumer { get ; set ; }
        /// <summary>
        /// 系统等级
        /// </summary>
        [XmlElement("SRVLEVEL")]
        public string Srvlevel { get ; set ; }
        /// <summary>
        /// 接收方链接账号
        /// </summary>
        [XmlElement("ACCOUNT")]
        public string Account { get ; set ; }
        /// <summary>
        /// 接收方链接密码
        /// </summary>
        [XmlElement("PASSWORD")]
        public string Password { get; set; }
    }
}
