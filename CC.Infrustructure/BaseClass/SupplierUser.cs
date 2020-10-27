
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.BaseClass
{
    public partial class SupplierUser
    {
        #region 供应商登录的基本信息
        /// <summary>
        /// 登录用户ID
        /// </summary>
        public int UserID { get; set; } 

        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get; set; } 

        /// <summary>
        /// 用户名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public int UserStatus { get; set; }

        /// <summary>
        /// 失败登录次数
        /// </summary>
        public int FailLogin { get; set; }

        /// <summary>
        /// 密码过期时间
        /// </summary>
        public DateTime? PasswordExpireTime { get; set; }

        /// <summary>
        /// 登录用户口令
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        public string SupplierNum { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 供应商类型
        /// </summary>
        public int SupplierType { get; set; }


        /// <summary>
        /// 供应商地址
        /// </summary>
        public string SupplierAddress { get; set; }

        
        #endregion

    }
}
