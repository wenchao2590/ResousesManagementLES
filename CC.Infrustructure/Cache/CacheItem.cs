using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure
{
    /// <summary>
    /// 缓存项目
    /// </summary>
    public class CacheItem<T>
    {
        /// <summary>
        /// 缓存内容
        /// </summary>
        public T Context { get; set; }

        /// <summary>
        /// 缓存过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public Boolean IsValid
        {
            get { return ExpireTime >= DateTime.Now; }
        }
    }
}