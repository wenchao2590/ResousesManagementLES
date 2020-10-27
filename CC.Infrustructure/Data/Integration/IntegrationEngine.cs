using Infrustructure.Logging;
using System;
using System.Collections;

namespace Infrustructure.Data.Integration
{
	internal class IntegrationEngine : IDisposable
	{
		private readonly ProviderCollection _providers = new ProviderCollection();

		public ProviderCollection Providers
		{
			get { return _providers; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			Providers.Clear();
		}

		#endregion

		public bool Run(IDictionary state)
		{
			IntegrationContext context = new IntegrationContext(state);
			try
			{
				// TODO: by alex hu, finished the status updating
				//while (context.Status == IntegrationStatus.Running)
				//{
				foreach (IProvider provider in _providers)
					provider.Run(context);
				//}
			}
			catch (System.Exception ex)
			{
				Logger.Instance.Error(this, ex);
				throw;
			}
			//bool result = false;
			//switch(context.Status)
			//{
			//    case IntegrationStatus.Completed:
			//    case IntegrationStatus.Success:
			//        result = true;
			//        break;
			//}
			return true;
		}
	}
}