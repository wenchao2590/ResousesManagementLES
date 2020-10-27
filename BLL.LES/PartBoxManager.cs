using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// 零件类管理器
    /// </summary>
    public class PartBoxManager
    {
        /// <summary>
        /// 零件类配置匹配
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool ValidRequirement(ref RequirementInfo info)
        {
            return true;
        }
        /// <summary>
        /// 获取零件类
        /// </summary>
        /// <param name="boxCode"></param>
        /// <returns></returns>
        TWDBoxPartInfo GetPartBox(string boxCode)
        {
            return null;
        }
    }
}
