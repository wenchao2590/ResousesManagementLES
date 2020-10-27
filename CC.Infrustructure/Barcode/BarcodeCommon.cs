//-------------------------------------------------------------------------------------
//Name:	     条形码基类
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
    /// 条形码基类
    /// </summary>
    public abstract class BarcodeCommon
    {
        protected string Raw_Data = "";
        protected List<string> _Errors = new List<string>();

        /// <summary>
        /// 原始数据
        /// </summary>
        public string RawData
        {
            get { return this.Raw_Data; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> Errors
        {
            get { return this._Errors; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="ErrorMessage"></param>
        public void Error(string ErrorMessage)
        {
            this._Errors.Add(ErrorMessage);
            throw new System.Exception(ErrorMessage);
        }
        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsNumeric(string input)
        {
            try
            {
                Int32 i32temp = new Int32();
                if (!Int32.TryParse(input, out i32temp))
                {
                    //parse didnt work so check each char because it may just be too long.
                    foreach (char c in input)
                    {
                        if (!char.IsDigit(c))
                            return false;
                    }//foreach
                }//if
                return true;
            }//try
            catch
            {
                return false;
            }//catch
        }//IsNumeric
    }//BarcodeVariables abstract class
}
