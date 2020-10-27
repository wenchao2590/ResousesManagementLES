using DAL.SYS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public partial class BarcodeBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetBarcodeReceivePrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0) return null;
            string sql = "select b.*,r.[CONTACTS_CODE],r.[CONTACTS2_CODE] " +
                "from [LES].[TT_WMM_BARCODE] b with(nolock) " +
                "left join [LES].[TT_WMM_RECEIVE_DETAIL] d with(nolock) on d.[FID] = b.[CREATE_SOURCE_FID] " +
                "left join [LES].[TT_WMM_RECEIVE] r with(nolock) on r.[FID] = d.[RECEIVE_FID] " +
                "where b.[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }
    }
}
