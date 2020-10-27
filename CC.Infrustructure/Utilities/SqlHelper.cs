using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.Utilities
{
    public class SqlHelper
    {
        /// <summary>
        /// 去除sql中无效字符
        /// </summary>
        /// <param name="sqlCondition"></param>
        /// <returns></returns>
		[Obsolete("Please use 'Infrustructure.Utilities.StringUtil.ReplaceSqlInvalidWords' instead.")]
        public static string ReplaceInvalidWords(string sqlCondition)
        {
            if (string.IsNullOrEmpty(sqlCondition)) return "";

            //过滤非法字符 xuehaijun 2011-9-26
            sqlCondition = sqlCondition.Replace("[", "[[]");
            sqlCondition = sqlCondition.Replace("]", "[]]");
            sqlCondition = sqlCondition.Replace("'", "");

            return sqlCondition;
        }
    }
}
