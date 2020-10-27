using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public class EquipmentTypeInfo
    {
        /// <summary>
        /// 分类
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool ValidFlag { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUser { get; set; }
        public long Id { get; set; }
    }
}
