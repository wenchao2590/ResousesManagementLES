using System;

namespace Infrustructure.Logging
{
	public interface ILogger
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ILogger Error(object sender, string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        ILogger Error(object sender, Exception ex);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        ILogger Error(object sender, string message,Exception ex);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="functionName"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
		ILogger Error(object sender, string functionName,string message,  Exception ex);
		ILogger Info(object sender, string msg);
		ILogger Trace(object sender, string msg);
		ILogger Log(object sender, string msg);
	}
}