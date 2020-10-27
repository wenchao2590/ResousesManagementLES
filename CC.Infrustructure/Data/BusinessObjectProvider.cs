using System;
using System.Data;
using System.Text;

using Infrustructure.Data.Integration;

namespace Infrustructure.Data
{
	public class BusinessObjectProvider<T> : MarshalByRefObject
		where T: BusinessObject
	{
		

		protected static string BuildChangedDataLog(T entity, string action)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<entity action=\"{0}\"><keys>", action);
			foreach(string key in entity._Keys)
			{
				sb.AppendFormat("<field name=\"{0}\" value=\"{1}\" />", key, entity[key].Value);
			}
			sb.Append("</keys><fields>");
			foreach(DataItemField field in entity.Items)
			{
				if(field.HasChanged)
				{
					sb.AppendFormat("<field name=\"{0}\" value=\"{1}\" />", field.Name, field.Value);
				}
			}
			sb.Append("</fields></entity>");
			return sb.ToString();
		}

		//protected static string GetCurrentUserInfo()
		//{
		//    IUser user = MembershipManager.CurrentUser;
		//    return string.Format("{0}({1})", user.AccountName, user.UserID);
		//}
	}

	public class BusinessObjectProviderFactory<T>
		where T: BusinessObject
	{
		public static K Create<K>() where K : BusinessObjectProvider<T>
		{
			// TODO: add policy injection
			return Activator.CreateInstance<K>();
		}
	}
}
