using System;
using System.Data;

namespace Infrustructure.Data
{
	public class BusinessObjectService<T> : MarshalByRefObject, IService
		where T : BusinessObject
	{
        //public static bool ExportData(BusinessObjectCollection<T> bocollection, string directory, out string file,
        //                                       out string message)
        //{
        //    return BusinessObjectProvider<T>.ExportData(bocollection, directory, out file, out message);
        //}

        //public static bool ExportData(DataTable dt, string directory, out string file,
        //                                       out string message)
        //{
        //    return BusinessObjectProvider<T>.ExportData(dt, directory, out file, out message);
        //}

		#region IService Members

		public void Start()
		{
			// TODO:
		}

		public void Stop()
		{
			// TODO:
		}

		#endregion
	}

	public class BusinessObjectServiceFactory<T>
		where T : BusinessObject
	{
		public static K Create<K>() where K : BusinessObjectService<T>
		{
			// TODO: add policy injection
			return Activator.CreateInstance<K>();
		}
	}
}
