using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.Security
{
	public enum UserType
	{
		JITDD, Supplier, EPS, RDC
	}

	public enum UserStatus
	{
		Active, Stop
	}

	public enum UserToken
	{
		Login, Logout, Unknown
	}

	public enum UserResourceType
	{
		Menu, Action
	}

	public interface IUserResource
	{
		UserResourceType ResourceType { get; set; }
	}

	public interface IUser
	{
		int UserID { get; set; }
		string AccountName { get; set; }
		string Password { get; set; }
		UserType UserType { get; set; }
		string EmployeeName { get; set; }
		string Email { get; set; }
		string Mobile { get; set; }
		string OfficePhone { get; set; }


		UserStatus UserStatus { get; set; }

		DateTime CreateDate { get; set; }
		IUser CreateUser { get; set; }
		DateTime? UpdateDate { get; set; }
		IUser UpdateUser { get; set; }

		List<string> Roles { get; set; }
		List<IUserResource> RightList { get; set; }


	}
}
