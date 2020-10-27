#region Declaim
//---------------------------------------------------------------------------
// Name:		UserLoginInfo
// Function: 	Expose data in table TL_SYS_USER_LOGIN from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年3月27日
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
    /// UserLoginInfo对应表[TL_SYS_USER_LOGIN]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class UserLoginInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public UserLoginInfo(		 int aId,
				Guid aFid,
				string aLoginName,
				string aIpAddress,
				string aLoginType,
				string aSourceType,
				DateTime aExecuteTime,
				bool aValidFlag,
				string aCreateUser,
				DateTime aCreateDate,
				string aModifyUser,
				DateTime aModifyDate				
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			LoginName = aLoginName;
		 
			IpAddress = aIpAddress;
		 
			LoginType = aLoginType;
		 
			SourceType = aSourceType;
		 
			ExecuteTime = aExecuteTime;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public UserLoginInfo():base("TL_SYS_USER_LOGIN")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");           _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(int).ToString();
			IDField.Index = 0;
			fields.Add(IDField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 1;
			fields.Add(FIDField);
			 
			DataSchemaField LOGIN_NAMEField = new DataSchemaField();
			LOGIN_NAMEField.Name = "LOGIN_NAME";
			LOGIN_NAMEField.Type = typeof(string).ToString();
			LOGIN_NAMEField.Index = 2;
			fields.Add(LOGIN_NAMEField);
			 
			DataSchemaField IP_ADDRESSField = new DataSchemaField();
			IP_ADDRESSField.Name = "IP_ADDRESS";
			IP_ADDRESSField.Type = typeof(string).ToString();
			IP_ADDRESSField.Index = 3;
			fields.Add(IP_ADDRESSField);
			 
			DataSchemaField LOGIN_TYPEField = new DataSchemaField();
			LOGIN_TYPEField.Name = "LOGIN_TYPE";
			LOGIN_TYPEField.Type = typeof(string).ToString();
			LOGIN_TYPEField.Index = 4;
			fields.Add(LOGIN_TYPEField);
			 
			DataSchemaField SOURCE_TYPEField = new DataSchemaField();
			SOURCE_TYPEField.Name = "SOURCE_TYPE";
			SOURCE_TYPEField.Type = typeof(string).ToString();
			SOURCE_TYPEField.Index = 5;
			fields.Add(SOURCE_TYPEField);
			 
			DataSchemaField EXECUTE_TIMEField = new DataSchemaField();
			EXECUTE_TIMEField.Name = "EXECUTE_TIME";
			EXECUTE_TIMEField.Type = typeof(DateTime).ToString();
			EXECUTE_TIMEField.Index = 6;
			fields.Add(EXECUTE_TIMEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 7;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 8;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 9;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 10;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 11;
			fields.Add(MODIFY_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public int Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string LoginName{ get;set; }		
				
		[DataMember]
		public string IpAddress{ get;set; }		
				
		[DataMember]
		public string LoginType{ get;set; }		
				
		[DataMember]
		public string SourceType{ get;set; }		
				
		[DataMember]
		public DateTime? ExecuteTime{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			UserLoginInfo info = new UserLoginInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LoginName = this.LoginName;
			info.IpAddress = this.IpAddress;
			info.LoginType = this.LoginType;
			info.SourceType = this.SourceType;
			info.ExecuteTime = this.ExecuteTime;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public UserLoginInfo Clone()
		{
			return ((ICloneable) this).Clone() as UserLoginInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// UserLoginInfoCollection对应表[TL_SYS_USER_LOGIN]
    /// </summary>
	public partial class UserLoginInfoCollection : BusinessObjectCollection<UserLoginInfo>
	{
		public UserLoginInfoCollection():base("TL_SYS_USER_LOGIN"){}	
	}
}
