using System;

namespace Infrustructure.Logging
{
	public class NullLogger : ILogger
	{
		#region ILogger Members

		public ILogger Error(object sender, string msg)
		{
			return this;
		}

		public ILogger Error(object sender, Exception ex)
		{
			return this;
		}

		public ILogger Error(object sender, string msg, Exception ex)
		{
			return this;
		}

        public ILogger Error(object sender, string msg, string functionName ,Exception ex)
        {
            return this;
        }

		public ILogger Info(object sender, string msg)
		{
			return this;
		}

		public ILogger Trace(object sender, string msg)
		{
			return this;
		}

		public ILogger Log(object sender, string msg)
		{
			return this;
		}

		#endregion
	}
}