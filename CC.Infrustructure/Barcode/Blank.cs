//-------------------------------------------------------------------------------------
//Name:	     空白码辅助类
//Function:	 辅助类
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
    /// 空白码辅助类
    /// </summary>
    public class Blank : BarcodeCommon, IBarcode
    {

        #region IBarcode Members
        /// <summary>
        /// 编码值
        /// </summary>
        public string Encoded_Value
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
