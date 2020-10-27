using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public partial class RangeAuthInfo
    {
        private long id;
        private string conditionFieldValue;
        private string conditionFieldDisplay;
        private bool authedFlag;
        private Guid roleFid;
        private Guid conditionFid;
        private string comments;

        /// <summary>
        /// 权限范围匹配值
        /// </summary>
        public string ConditionFieldValue { get => conditionFieldValue; set => conditionFieldValue = value; }
        /// <summary>
        /// 权限范围显示内容
        /// </summary>
        public string ConditionFieldDisplay { get => conditionFieldDisplay; set => conditionFieldDisplay = value; }
        /// <summary>
        /// 是否授权
        /// </summary>
        public bool AuthedFlag { get => authedFlag; set => authedFlag = value; }
        public Guid RoleFid { get => roleFid; set => roleFid = value; }
        public Guid ConditionFid { get => conditionFid; set => conditionFid = value; }
        public long Id { get => id; set => id = value; }
        public string Comments { get => comments; set => comments = value; }
    }
}
