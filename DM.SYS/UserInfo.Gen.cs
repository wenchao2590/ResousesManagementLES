#region Declaim
//---------------------------------------------------------------------------
// Name:		UserInfo
// Function: 	Expose data in table TS_SYS_USER from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月20日
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
    /// UserInfo对应表[TS_SYS_USER]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class UserInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public UserInfo(		 string aLoginName,
				string aPassword,
				string aEmployeeName,
				string aEmail,
				string aMobile,
				string aOfficePhone,
				int aUserStatus,
				string aComments,
				string aFavoritePic,
				string aDataServiceUrl,
				DateTime aModifyDate,
				string aCreateUser,
				long aId,
				string aWechatUserId,
				int aUserValidType,
				DateTime aCreateDate,
				string aWechatDepId,
				Guid aFid,
				bool aValidFlag,
				string aWechatTagId,
				string aModifyUser				
		) : this()
		{
			 
			LoginName = aLoginName;
		 
			Password = aPassword;
		 
			EmployeeName = aEmployeeName;
		 
			Email = aEmail;
		 
			Mobile = aMobile;
		 
			OfficePhone = aOfficePhone;
		 
			UserStatus = aUserStatus;
		 
			Comments = aComments;
		 
			FavoritePic = aFavoritePic;
		 
			DataServiceUrl = aDataServiceUrl;
		 
			ModifyDate = aModifyDate;
		 
			CreateUser = aCreateUser;
		 
			Id = aId;
		 
			WechatUserId = aWechatUserId;
		 
			UserValidType = aUserValidType;
		 
			CreateDate = aCreateDate;
		 
			WechatDepId = aWechatDepId;
		 
			Fid = aFid;
		 
			ValidFlag = aValidFlag;
		 
			WechatTagId = aWechatTagId;
		 
			ModifyUser = aModifyUser;
		}
		
		public UserInfo():base("TS_SYS_USER")
		{
			List<string> keys = new List<string>();
			             			keys.Add("ID");        _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField LOGIN_NAMEField = new DataSchemaField();
			LOGIN_NAMEField.Name = "LOGIN_NAME";
			LOGIN_NAMEField.Type = typeof(string).ToString();
			LOGIN_NAMEField.Index = 0;
			fields.Add(LOGIN_NAMEField);
			 
			DataSchemaField PASSWORDField = new DataSchemaField();
			PASSWORDField.Name = "PASSWORD";
			PASSWORDField.Type = typeof(string).ToString();
			PASSWORDField.Index = 1;
			fields.Add(PASSWORDField);
			 
			DataSchemaField EMPLOYEE_NAMEField = new DataSchemaField();
			EMPLOYEE_NAMEField.Name = "EMPLOYEE_NAME";
			EMPLOYEE_NAMEField.Type = typeof(string).ToString();
			EMPLOYEE_NAMEField.Index = 2;
			fields.Add(EMPLOYEE_NAMEField);
			 
			DataSchemaField EMAILField = new DataSchemaField();
			EMAILField.Name = "EMAIL";
			EMAILField.Type = typeof(string).ToString();
			EMAILField.Index = 3;
			fields.Add(EMAILField);
			 
			DataSchemaField MOBILEField = new DataSchemaField();
			MOBILEField.Name = "MOBILE";
			MOBILEField.Type = typeof(string).ToString();
			MOBILEField.Index = 4;
			fields.Add(MOBILEField);
			 
			DataSchemaField OFFICE_PHONEField = new DataSchemaField();
			OFFICE_PHONEField.Name = "OFFICE_PHONE";
			OFFICE_PHONEField.Type = typeof(string).ToString();
			OFFICE_PHONEField.Index = 5;
			fields.Add(OFFICE_PHONEField);
			 
			DataSchemaField USER_STATUSField = new DataSchemaField();
			USER_STATUSField.Name = "USER_STATUS";
			USER_STATUSField.Type = typeof(int).ToString();
			USER_STATUSField.Index = 6;
			fields.Add(USER_STATUSField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 7;
			fields.Add(COMMENTSField);
			 
			DataSchemaField FAVORITE_PICField = new DataSchemaField();
			FAVORITE_PICField.Name = "FAVORITE_PIC";
			FAVORITE_PICField.Type = typeof(string).ToString();
			FAVORITE_PICField.Index = 8;
			fields.Add(FAVORITE_PICField);
			 
			DataSchemaField DATA_SERVICE_URLField = new DataSchemaField();
			DATA_SERVICE_URLField.Name = "DATA_SERVICE_URL";
			DATA_SERVICE_URLField.Type = typeof(string).ToString();
			DATA_SERVICE_URLField.Index = 9;
			fields.Add(DATA_SERVICE_URLField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 10;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 11;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 12;
			fields.Add(IDField);
			 
			DataSchemaField WECHAT_USER_IDField = new DataSchemaField();
			WECHAT_USER_IDField.Name = "WECHAT_USER_ID";
			WECHAT_USER_IDField.Type = typeof(string).ToString();
			WECHAT_USER_IDField.Index = 13;
			fields.Add(WECHAT_USER_IDField);
			 
			DataSchemaField USER_VALID_TYPEField = new DataSchemaField();
			USER_VALID_TYPEField.Name = "USER_VALID_TYPE";
			USER_VALID_TYPEField.Type = typeof(int).ToString();
			USER_VALID_TYPEField.Index = 14;
			fields.Add(USER_VALID_TYPEField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 15;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField WECHAT_DEP_IDField = new DataSchemaField();
			WECHAT_DEP_IDField.Name = "WECHAT_DEP_ID";
			WECHAT_DEP_IDField.Type = typeof(string).ToString();
			WECHAT_DEP_IDField.Index = 16;
			fields.Add(WECHAT_DEP_IDField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 17;
			fields.Add(FIDField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 18;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField WECHAT_TAG_IDField = new DataSchemaField();
			WECHAT_TAG_IDField.Name = "WECHAT_TAG_ID";
			WECHAT_TAG_IDField.Type = typeof(string).ToString();
			WECHAT_TAG_IDField.Index = 19;
			fields.Add(WECHAT_TAG_IDField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 20;
			fields.Add(MODIFY_USERField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string LoginName{ get;set; }		
				
		[DataMember]
		public string Password{ get;set; }		
				
		[DataMember]
		public string EmployeeName{ get;set; }		
				
		[DataMember]
		public string Email{ get;set; }		
				
		[DataMember]
		public string Mobile{ get;set; }		
				
		[DataMember]
		public string OfficePhone{ get;set; }		
				
		[DataMember]
		public int? UserStatus{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string FavoritePic{ get;set; }		
				
		[DataMember]
		public string DataServiceUrl{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string WechatUserId{ get;set; }		
				
		[DataMember]
		public int? UserValidType{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string WechatDepId{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string WechatTagId{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			UserInfo info = new UserInfo();

			info.LoginName = this.LoginName;
			info.Password = this.Password;
			info.EmployeeName = this.EmployeeName;
			info.Email = this.Email;
			info.Mobile = this.Mobile;
			info.OfficePhone = this.OfficePhone;
			info.UserStatus = this.UserStatus;
			info.Comments = this.Comments;
			info.FavoritePic = this.FavoritePic;
			info.DataServiceUrl = this.DataServiceUrl;
			info.ModifyDate = this.ModifyDate;
			info.CreateUser = this.CreateUser;
			info.Id = this.Id;
			info.WechatUserId = this.WechatUserId;
			info.UserValidType = this.UserValidType;
			info.CreateDate = this.CreateDate;
			info.WechatDepId = this.WechatDepId;
			info.Fid = this.Fid;
			info.ValidFlag = this.ValidFlag;
			info.WechatTagId = this.WechatTagId;
			info.ModifyUser = this.ModifyUser;
			return info;			
		}
		 
		public UserInfo Clone()
		{
			return ((ICloneable) this).Clone() as UserInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// UserInfoCollection对应表[TS_SYS_USER]
    /// </summary>
	public partial class UserInfoCollection : BusinessObjectCollection<UserInfo>
	{
		public UserInfoCollection():base("TS_SYS_USER"){}	
	}
}
