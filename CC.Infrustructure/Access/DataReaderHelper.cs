using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Infrustructure.Access
{
    public static class DataReaderHelper
    {
        public static Boolean ContainColumn(this IDataReader dr, String columnName)
        {
            int count = dr.FieldCount;

            for (int i = 0; i < count; i++)
            {
                if (dr.GetName(i).Equals(columnName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
