//-------------------------------------------------------------------------------------
//Name:	     条形码接口类
//Function:	 接口类
//Author:	 Yin xuefeng
//Date:       2013-08-26
//-------------------------------------------------------------------------------------
//Change History:
// Date				    Who			      Changes Made          Purpose        Comments
//-------------------------------------------------------------------------------------
//2013-08-26	     Yinxuefeng	        Initial creation
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.Barcode
{
    /// <summary>
    /// 条形码接口类
    /// </summary>
    public interface IBarcode
    {
        /// <summary>
        /// 值
        /// </summary>
        string Encoded_Value
        {
            get;
        }//Encoded_Value

        /// <summary>
        /// 原始数据
        /// </summary>
        string RawData
        {
            get;
        }//Raw_Data

        /// <summary>
        /// 错误信息
        /// </summary>
        List<string> Errors
        {
            get;
        }//Errors

    }//interface
}
