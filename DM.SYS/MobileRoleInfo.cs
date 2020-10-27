using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public class MobileRoleInfo
    {
        private string roleName;
        private string roleType;
        private string comments;
        /// <summary>
        /// 授权类型名称
        /// </summary>
        public string RoleName
        {
            get
            {
                return roleName;
            }

            set
            {
                roleName = value;
            }
        }
        /// <summary>
        /// 授权项目名称
        /// </summary>
        public string RoleType
        {
            get
            {
                return roleType;
            }

            set
            {
                roleType = value;
            }
        }
        /// <summary>
        /// 授权项目的父节点
        /// </summary>
        public string Comments
        {
            get
            {
                return comments;
            }

            set
            {
                comments = value;
            }
        }
    }
}
