#region Declaim
//---------------------------------------------------------------------------
// Name:		UserMobileInfo
// Function: 	Expose data in table TS_SYS_USER_MOBILE from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月21日
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
    /// UserMobileInfo对应表[TS_SYS_USER_MOBILE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class UserMobileInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public UserMobileInfo(		 Guid aUserFid,
				string aUserName,
				string aImei,
				string aImsi,
				string aModel,
				string aVendor,
				string aUuid,
				string aOsLanguage,
				string aOsVersion,
				string aOsName,
				string aOsVendor,
				bool aValidFlag,
				long aId,
				string aModifyUser,
				string aCreateUser,
				DateTime aModifyDate,
				Guid aFid,
				DateTime aCreateDate,
				int aStatus				
		) : this()
		{
			 
			UserFid = aUserFid;
		 
			UserName = aUserName;
		 
			Imei = aImei;
		 
			Imsi = aImsi;
		 
			Model = aModel;
		 
			Vendor = aVendor;
		 
			Uuid = aUuid;
		 
			OsLanguage = aOsLanguage;
		 
			OsVersion = aOsVersion;
		 
			OsName = aOsName;
		 
			OsVendor = aOsVendor;
		 
			ValidFlag = aValidFlag;
		 
			Id = aId;
		 
			ModifyUser = aModifyUser;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			Fid = aFid;
		 
			CreateDate = aCreateDate;
		 
			Status = aStatus;
		}
		
		public UserMobileInfo():base("TS_SYS_USER_MOBILE")
		{
			List<string> keys = new List<string>();
			             			keys.Add("ID");      _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField USER_FIDField = new DataSchemaField();
			USER_FIDField.Name = "USER_FID";
			USER_FIDField.Type = typeof(Guid).ToString();
			USER_FIDField.Index = 0;
			fields.Add(USER_FIDField);
			 
			DataSchemaField USER_NAMEField = new DataSchemaField();
			USER_NAMEField.Name = "USER_NAME";
			USER_NAMEField.Type = typeof(string).ToString();
			USER_NAMEField.Index = 1;
			fields.Add(USER_NAMEField);
			 
			DataSchemaField IMEIField = new DataSchemaField();
			IMEIField.Name = "IMEI";
			IMEIField.Type = typeof(string).ToString();
			IMEIField.Index = 2;
			fields.Add(IMEIField);
			 
			DataSchemaField IMSIField = new DataSchemaField();
			IMSIField.Name = "IMSI";
			IMSIField.Type = typeof(string).ToString();
			IMSIField.Index = 3;
			fields.Add(IMSIField);
			 
			DataSchemaField MODELField = new DataSchemaField();
			MODELField.Name = "MODEL";
			MODELField.Type = typeof(string).ToString();
			MODELField.Index = 4;
			fields.Add(MODELField);
			 
			DataSchemaField VENDORField = new DataSchemaField();
			VENDORField.Name = "VENDOR";
			VENDORField.Type = typeof(string).ToString();
			VENDORField.Index = 5;
			fields.Add(VENDORField);
			 
			DataSchemaField UUIDField = new DataSchemaField();
			UUIDField.Name = "UUID";
			UUIDField.Type = typeof(string).ToString();
			UUIDField.Index = 6;
			fields.Add(UUIDField);
			 
			DataSchemaField OS_LANGUAGEField = new DataSchemaField();
			OS_LANGUAGEField.Name = "OS_LANGUAGE";
			OS_LANGUAGEField.Type = typeof(string).ToString();
			OS_LANGUAGEField.Index = 7;
			fields.Add(OS_LANGUAGEField);
			 
			DataSchemaField OS_VERSIONField = new DataSchemaField();
			OS_VERSIONField.Name = "OS_VERSION";
			OS_VERSIONField.Type = typeof(string).ToString();
			OS_VERSIONField.Index = 8;
			fields.Add(OS_VERSIONField);
			 
			DataSchemaField OS_NAMEField = new DataSchemaField();
			OS_NAMEField.Name = "OS_NAME";
			OS_NAMEField.Type = typeof(string).ToString();
			OS_NAMEField.Index = 9;
			fields.Add(OS_NAMEField);
			 
			DataSchemaField OS_VENDORField = new DataSchemaField();
			OS_VENDORField.Name = "OS_VENDOR";
			OS_VENDORField.Type = typeof(string).ToString();
			OS_VENDORField.Index = 10;
			fields.Add(OS_VENDORField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 11;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 12;
			fields.Add(IDField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 13;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 14;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 15;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 16;
			fields.Add(FIDField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 17;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 18;
			fields.Add(STATUSField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public Guid? UserFid{ get;set; }		
				
		[DataMember]
		public string UserName{ get;set; }		
				
		[DataMember]
		public string Imei{ get;set; }		
				
		[DataMember]
		public string Imsi{ get;set; }		
				
		[DataMember]
		public string Model{ get;set; }		
				
		[DataMember]
		public string Vendor{ get;set; }		
				
		[DataMember]
		public string Uuid{ get;set; }		
				
		[DataMember]
		public string OsLanguage{ get;set; }		
				
		[DataMember]
		public string OsVersion{ get;set; }		
				
		[DataMember]
		public string OsName{ get;set; }		
				
		[DataMember]
		public string OsVendor{ get;set; }		
				
		[DataMember]
		public bool ValidFlag{ get;set; }		
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			UserMobileInfo info = new UserMobileInfo();

			info.UserFid = this.UserFid;
			info.UserName = this.UserName;
			info.Imei = this.Imei;
			info.Imsi = this.Imsi;
			info.Model = this.Model;
			info.Vendor = this.Vendor;
			info.Uuid = this.Uuid;
			info.OsLanguage = this.OsLanguage;
			info.OsVersion = this.OsVersion;
			info.OsName = this.OsName;
			info.OsVendor = this.OsVendor;
			info.ValidFlag = this.ValidFlag;
			info.Id = this.Id;
			info.ModifyUser = this.ModifyUser;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.Fid = this.Fid;
			info.CreateDate = this.CreateDate;
			info.Status = this.Status;
			return info;			
		}
		 
		public UserMobileInfo Clone()
		{
			return ((ICloneable) this).Clone() as UserMobileInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// UserMobileInfoCollection对应表[TS_SYS_USER_MOBILE]
    /// </summary>
	public partial class UserMobileInfoCollection : BusinessObjectCollection<UserMobileInfo>
	{
		public UserMobileInfoCollection():base("TS_SYS_USER_MOBILE"){}	
	}
}
