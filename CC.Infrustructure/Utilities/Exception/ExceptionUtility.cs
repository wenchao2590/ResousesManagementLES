using System;
using System.Collections.Generic;
using System.Text;


namespace Infrustructure.Utilities.Exception
{
	[Obsolete("Never be used.")]
    public class ExceptionUtility
    {
        public static string GetMessage(string code)
        {
            //if (ExceptionRecs.ResourceManager != null)
            //{
            //    return ExceptionRecs.ResourceManager.GetString(code);
            //}
            //return ExceptionRecs.ResourceManager.GetString("E_NotFound");
            return "";
        }
    }
}
