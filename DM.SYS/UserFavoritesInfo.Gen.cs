#region Declaim
//---------------------------------------------------------------------------
// Name:		UserFavoritesInfo
// Function: 	Expose data in table TS_SYS_USER_FAVORITES from database as business object to MES system.
// Tool:		T4
// CreateDate:	2016年8月17日
// <auto-generated>
//     This code was generated by a tool. 
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------
#endregion 

#region Imported Namespace

using Infrustructure.Data;
using Infrustructure.Data.Integration;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace DM.SYS 
{   
	/// <summary>
    /// UserFavoritesInfo对应表[TS_SYS_USER_FAVORITES]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class UserFavoritesInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public UserFavoritesInfo(		 Guid aMenuFid,
				Guid aUserFid,
				Guid aRoleFid,
				bool aValidFlag,
				string aModifyUser,
				string aCreateUser,
				DateTime aModifyDate,
				long aId,
				Guid aFid,
				DateTime aCreateDate				
		) : this()
		{
			 
			MenuFid = aMenuFid;
		 
			UserFid = aUserFid;
		 
			RoleFid = aRoleFid;
		 
			ValidFlag = aValidFlag;
		 
			ModifyUser = aModifyUser;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			Id = aId;
		 
			Fid = aFid;
		 
			CreateDate = aCreateDate;
		}
		
		public UserFavoritesInfo():base("TS_SYS_USER_FAVORITES")
		{
			List<string> keys = new List<string>();
			        			keys.Add("ID");  _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField MENU_FIDField = new DataSchemaField();
			MENU_FIDField.Name = "MENU_FID";
			MENU_FIDField.Type = typeof(Guid).ToString();
			MENU_FIDField.Index = 0;
			fields.Add(MENU_FIDField);
			 
			DataSchemaField USER_FIDField = new DataSchemaField();
			USER_FIDField.Name = "USER_FID";
			USER_FIDField.Type = typeof(Guid).ToString();
			USER_FIDField.Index = 1;
			fields.Add(USER_FIDField);
			 
			DataSchemaField ROLE_FIDField = new DataSchemaField();
			ROLE_FIDField.Name = "ROLE_FID";
			ROLE_FIDField.Type = typeof(Guid).ToString();
			ROLE_FIDField.Index = 2;
			fields.Add(ROLE_FIDField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 3;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 4;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 5;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 6;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 7;
			fields.Add(IDField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 8;
			fields.Add(FIDField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 9;
			fields.Add(CREATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public Guid? MenuFid{ get;set; }		
				
		[DataMember]
		public Guid? UserFid{ get;set; }		
				
		[DataMember]
		public Guid? RoleFid{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			UserFavoritesInfo info = new UserFavoritesInfo();

			info.MenuFid = this.MenuFid;
			info.UserFid = this.UserFid;
			info.RoleFid = this.RoleFid;
			info.ValidFlag = this.ValidFlag;
			info.ModifyUser = this.ModifyUser;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.Id = this.Id;
			info.Fid = this.Fid;
			info.CreateDate = this.CreateDate;
			return info;			
		}
		 
		public UserFavoritesInfo Clone()
		{
			return ((ICloneable) this).Clone() as UserFavoritesInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// UserFavoritesInfoCollection对应表[TS_SYS_USER_FAVORITES]
    /// </summary>
	public partial class UserFavoritesInfoCollection : BusinessObjectCollection<UserFavoritesInfo>
	{
		public UserFavoritesInfoCollection():base("TS_SYS_USER_FAVORITES"){}	
	}
}
