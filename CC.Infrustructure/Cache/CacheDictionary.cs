using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrustructure
{
    public class CacheDictionary : Dictionary<String, CacheItem<Object>>
    {
        public readonly static CacheDictionary Instance = new CacheDictionary();

        /// <summary>
        /// 缓存失效间隔
        /// </summary>
        public static int InvalidInterval
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["cacheInvalidInterval"]); }
        }

        /// <summary>
        /// 根据键值获取换缓存
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="createCacheFunc">创建缓存函数</param>
        /// <returns>缓存数据</returns>
        public Object GetCacheByKey(String key, Func<CacheItem<Object>> createCacheFunc = null)
        {
            //检查字典是否包含Key
            if (this.ContainsKey(key))
            {
                //包含且未过期，直接返回缓存数据
                if (this[key].IsValid)
                {
                    return this[key].Context;
                }
                else
                {
                    //过期，直接删除缓存
                    this.Remove(key);
                }
            }

            //如果提供了创建缓存方法，则执行
            if (createCacheFunc != null)
            {
                var cacheItem = createCacheFunc();
                //数据结果不为null时，将缓存添加到字典中并返回缓存项
                if (cacheItem != null)
                {
                    this.Add(key, createCacheFunc());
                    return this[key].Context;
                }
            }

            return null;
        }
    }
}
