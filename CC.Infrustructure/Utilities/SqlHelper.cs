using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.Utilities
{
    public class SqlHelper
    {
        /// <summary>
        /// ȥ��sql����Ч�ַ�
        /// </summary>
        /// <param name="sqlCondition"></param>
        /// <returns></returns>
		[Obsolete("Please use 'Infrustructure.Utilities.StringUtil.ReplaceSqlInvalidWords' instead.")]
        public static string ReplaceInvalidWords(string sqlCondition)
        {
            if (string.IsNullOrEmpty(sqlCondition)) return "";

            //���˷Ƿ��ַ� xuehaijun 2011-9-26
            sqlCondition = sqlCondition.Replace("[", "[[]");
            sqlCondition = sqlCondition.Replace("]", "[]]");
            sqlCondition = sqlCondition.Replace("'", "");

            return sqlCondition;
        }
    }
}
