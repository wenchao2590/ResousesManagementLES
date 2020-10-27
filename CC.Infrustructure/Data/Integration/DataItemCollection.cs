using System.Collections.Generic;
using System;

namespace Infrustructure.Data.Integration
{
    [Serializable]
	public class DataItemCollection : List<DataItem>
	{
		public new void Add(DataItem item)
		{
			item.Container = this;
			//item.DoValidate();
			base.Add(item);
		}
	}
}