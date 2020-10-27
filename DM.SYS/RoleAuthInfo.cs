using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public partial class RoleAuthInfo
    {
        private string authTypeName;
        private string authSourceName;
        private Guid parentSourceFid;
        private int displayOrder;
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get { return displayOrder; } set { displayOrder = value; } }
        /// <summary>
        /// 授权类型名称
        /// </summary>
        public string AuthTypeName { get { return authTypeName; } set { authTypeName = value; } }
        /// <summary>
        /// 授权项目名称
        /// </summary>
        public string AuthSourceName { get { return authSourceName; } set { authSourceName = value; } }
        /// <summary>
        /// 授权项目的父节点
        /// </summary>
        public Guid ParentSourceFid { get { return parentSourceFid; } set { parentSourceFid = value; } }
    }
}
