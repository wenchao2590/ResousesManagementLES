using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.SRM.InboundDataService
{
    interface IBusiness<T, K>
    {  
        /// <summary> 
        /// 转换为中间对象
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        T ConversionToCentreInfo(K interfaceInfo);
        /// <summary>
        /// 转换为中间集合
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        List<T> ConversionToCentreList(List<K> interfaceList);
        /// <summary>
        /// 中间对象添加到数据库
        /// </summary>
        /// <param name="centreInfo"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        void InsertInfoToCentreTable(K interfaceInfo, Guid logFid,string logSql);
        /// <summary>
        /// 中间集合添加到数据库
        /// </summary>
        /// <param name="centreList"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        int InsertListToCentreTable(List<K> interfaceList, Guid logFid, string logSql);
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        string GetKeyValue(K interfaceInfo);
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        string GetKeyValues(List<K> interfaceList);
    }
}
