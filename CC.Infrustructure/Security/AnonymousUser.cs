using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.Security
{
	public class AnonymousUser : IUser
	{
		#region IUser Members

		public int UserID
		{
			get
			{
				return -1;
			}
			set
			{

			}
		}

		public string AccountName
		{
			get
			{
				return "Anonymous";
			}
			set
			{

			}
		}

		public string Password
		{
			get
			{
				return "";
			}
			set
			{

			}
		}

		public UserType UserType
		{
			get
			{
				return UserType.JITDD;
			}
			set
			{

			}
		}

		public string EmployeeName
		{
			get
			{
				return "Anonymous";
			}
			set
			{

			}
		}

		public string Email
		{
			get
			{
				return "Anonymous@Foton.com";
			}
			set
			{

			}
		}

		public string Mobile
		{
			get
			{
				return "";
			}
			set
			{

			}
		}

		public string OfficePhone
		{
			get
			{
				return "";
			}
			set
			{

			}
		}

        public UserStatus UserStatus
		{
			get
			{
				return UserStatus.Stop;
			}
			set
			{

			}
		}

		public DateTime CreateDate
		{
			get
			{
				return DateTime.Now;
			}
			set
			{

			}
		}

		public IUser CreateUser
		{
			get
			{
				return null;
			}
			set
			{

			}
		}

        public DateTime? UpdateDate
		{
			get
			{
				return DateTime.Now;
			}
			set
			{

			}
		}

        public IUser UpdateUser
		{
			get
			{
				return null;
			}
			set
			{

			}
		}

		public List<string> Roles
		{
			get
			{
				return null;
			}
			set
			{

			}
		}

		public List<IUserResource> RightList
		{
			get
			{
				return null;
			}
			set
			{

			}
		}

		#endregion
	}
}
