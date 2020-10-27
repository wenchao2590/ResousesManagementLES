using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// 计数器管理器
    /// </summary>
    public class CounterManager
    {
        /// <summary>
        /// 根据需求更新计数器
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool RequirementCounter(RequirementInfo info)
        {
            return true;
        }
        /// <summary>
        /// 按零件类获取当前计数器
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        List<CounterInfo> GetCounters(BoxPartInfo info)
        {
            return new List<CounterInfo>();
        }
        /// <summary>
        /// 更新计数器
        /// </summary>
        /// <returns></returns>
        bool UpdateCounter(long counterId, decimal orderQty)
        {
            return true;
        }
        /// <summary>
        /// 创建计数器
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool CreateCounter(BoxPartInfo info)
        {
            return true;
        }
    }
}
