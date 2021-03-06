#region Declaim
//---------------------------------------------------------------------------
// Name:		SupplierInfo
// Function: 	Expose data in table Supplier from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年3月30日
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

namespace DM.LES 
{   
	/// <summary>
    /// SupplierInfo对应表[TM_BAS_SUPPLIER]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SupplierInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SupplierInfo( 
					Guid aFid,

					string aSupplierNum,

					string aDuns,

					string aSupplierName,

					string aSupplierSname,

					string aSupplierAddress,

					int aSupplierType,

					string aContactName,

					string aContactTel,

					string aContactFax,

					string aContactMobile,

					string aContactEmail,

					string aNightcontactName,

					string aNightcontactTel,

					string aNightcontactFax,

					string aNightcontactMobile,

					string aNightcontactEmail,

					string aDaycontactName,

					string aDaycontactTel,

					string aDaycontactFax,

					string aDaycontactMobile,

					string aDaycontactEmail,

					string aProvince,

					string aCity,

					string aSupplierGroup,

					string aComments,

					bool aAsnFlag,

					bool aBatchFlag,

					DateTime aCreateDate,

					bool aValidFlag,

					long aId,

					string aModifyUser,

					DateTime aModifyDate,

					string aCreateUser,

					bool aAsnVmiFlag

				 
		) : this()
		{
			 
			Fid = aFid;
		 
			SupplierNum = aSupplierNum;
		 
			Duns = aDuns;
		 
			SupplierName = aSupplierName;
		 
			SupplierSname = aSupplierSname;
		 
			SupplierAddress = aSupplierAddress;
		 
			SupplierType = aSupplierType;
		 
			ContactName = aContactName;
		 
			ContactTel = aContactTel;
		 
			ContactFax = aContactFax;
		 
			ContactMobile = aContactMobile;
		 
			ContactEmail = aContactEmail;
		 
			NightcontactName = aNightcontactName;
		 
			NightcontactTel = aNightcontactTel;
		 
			NightcontactFax = aNightcontactFax;
		 
			NightcontactMobile = aNightcontactMobile;
		 
			NightcontactEmail = aNightcontactEmail;
		 
			DaycontactName = aDaycontactName;
		 
			DaycontactTel = aDaycontactTel;
		 
			DaycontactFax = aDaycontactFax;
		 
			DaycontactMobile = aDaycontactMobile;
		 
			DaycontactEmail = aDaycontactEmail;
		 
			Province = aProvince;
		 
			City = aCity;
		 
			SupplierGroup = aSupplierGroup;
		 
			Comments = aComments;
		 
			AsnFlag = aAsnFlag;
		 
			BatchFlag = aBatchFlag;
		 
			CreateDate = aCreateDate;
		 
			ValidFlag = aValidFlag;
		 
			Id = aId;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			CreateUser = aCreateUser;
		 
			AsnVmiFlag = aAsnVmiFlag;
		}
		
		public SupplierInfo():base("TM_BAS_SUPPLIER")
		{
			List<string> keys = new List<string>();
			                               			keys.Add("ID");    _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 0;
			fields.Add(FIDField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 1;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField DUNSField = new DataSchemaField();
			DUNSField.Name = "DUNS";
			DUNSField.Type = typeof(string).ToString();
			DUNSField.Index = 2;
			fields.Add(DUNSField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 3;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField SUPPLIER_SNAMEField = new DataSchemaField();
			SUPPLIER_SNAMEField.Name = "SUPPLIER_SNAME";
			SUPPLIER_SNAMEField.Type = typeof(string).ToString();
			SUPPLIER_SNAMEField.Index = 4;
			fields.Add(SUPPLIER_SNAMEField);
			 
			DataSchemaField SUPPLIER_ADDRESSField = new DataSchemaField();
			SUPPLIER_ADDRESSField.Name = "SUPPLIER_ADDRESS";
			SUPPLIER_ADDRESSField.Type = typeof(string).ToString();
			SUPPLIER_ADDRESSField.Index = 5;
			fields.Add(SUPPLIER_ADDRESSField);
			 
			DataSchemaField SUPPLIER_TYPEField = new DataSchemaField();
			SUPPLIER_TYPEField.Name = "SUPPLIER_TYPE";
			SUPPLIER_TYPEField.Type = typeof(int).ToString();
			SUPPLIER_TYPEField.Index = 6;
			fields.Add(SUPPLIER_TYPEField);
			 
			DataSchemaField CONTACT_NAMEField = new DataSchemaField();
			CONTACT_NAMEField.Name = "CONTACT_NAME";
			CONTACT_NAMEField.Type = typeof(string).ToString();
			CONTACT_NAMEField.Index = 7;
			fields.Add(CONTACT_NAMEField);
			 
			DataSchemaField CONTACT_TELField = new DataSchemaField();
			CONTACT_TELField.Name = "CONTACT_TEL";
			CONTACT_TELField.Type = typeof(string).ToString();
			CONTACT_TELField.Index = 8;
			fields.Add(CONTACT_TELField);
			 
			DataSchemaField CONTACT_FAXField = new DataSchemaField();
			CONTACT_FAXField.Name = "CONTACT_FAX";
			CONTACT_FAXField.Type = typeof(string).ToString();
			CONTACT_FAXField.Index = 9;
			fields.Add(CONTACT_FAXField);
			 
			DataSchemaField CONTACT_MOBILEField = new DataSchemaField();
			CONTACT_MOBILEField.Name = "CONTACT_MOBILE";
			CONTACT_MOBILEField.Type = typeof(string).ToString();
			CONTACT_MOBILEField.Index = 10;
			fields.Add(CONTACT_MOBILEField);
			 
			DataSchemaField CONTACT_EMAILField = new DataSchemaField();
			CONTACT_EMAILField.Name = "CONTACT_EMAIL";
			CONTACT_EMAILField.Type = typeof(string).ToString();
			CONTACT_EMAILField.Index = 11;
			fields.Add(CONTACT_EMAILField);
			 
			DataSchemaField NIGHTCONTACT_NAMEField = new DataSchemaField();
			NIGHTCONTACT_NAMEField.Name = "NIGHTCONTACT_NAME";
			NIGHTCONTACT_NAMEField.Type = typeof(string).ToString();
			NIGHTCONTACT_NAMEField.Index = 12;
			fields.Add(NIGHTCONTACT_NAMEField);
			 
			DataSchemaField NIGHTCONTACT_TELField = new DataSchemaField();
			NIGHTCONTACT_TELField.Name = "NIGHTCONTACT_TEL";
			NIGHTCONTACT_TELField.Type = typeof(string).ToString();
			NIGHTCONTACT_TELField.Index = 13;
			fields.Add(NIGHTCONTACT_TELField);
			 
			DataSchemaField NIGHTCONTACT_FAXField = new DataSchemaField();
			NIGHTCONTACT_FAXField.Name = "NIGHTCONTACT_FAX";
			NIGHTCONTACT_FAXField.Type = typeof(string).ToString();
			NIGHTCONTACT_FAXField.Index = 14;
			fields.Add(NIGHTCONTACT_FAXField);
			 
			DataSchemaField NIGHTCONTACT_MOBILEField = new DataSchemaField();
			NIGHTCONTACT_MOBILEField.Name = "NIGHTCONTACT_MOBILE";
			NIGHTCONTACT_MOBILEField.Type = typeof(string).ToString();
			NIGHTCONTACT_MOBILEField.Index = 15;
			fields.Add(NIGHTCONTACT_MOBILEField);
			 
			DataSchemaField NIGHTCONTACT_EMAILField = new DataSchemaField();
			NIGHTCONTACT_EMAILField.Name = "NIGHTCONTACT_EMAIL";
			NIGHTCONTACT_EMAILField.Type = typeof(string).ToString();
			NIGHTCONTACT_EMAILField.Index = 16;
			fields.Add(NIGHTCONTACT_EMAILField);
			 
			DataSchemaField DAYCONTACT_NAMEField = new DataSchemaField();
			DAYCONTACT_NAMEField.Name = "DAYCONTACT_NAME";
			DAYCONTACT_NAMEField.Type = typeof(string).ToString();
			DAYCONTACT_NAMEField.Index = 17;
			fields.Add(DAYCONTACT_NAMEField);
			 
			DataSchemaField DAYCONTACT_TELField = new DataSchemaField();
			DAYCONTACT_TELField.Name = "DAYCONTACT_TEL";
			DAYCONTACT_TELField.Type = typeof(string).ToString();
			DAYCONTACT_TELField.Index = 18;
			fields.Add(DAYCONTACT_TELField);
			 
			DataSchemaField DAYCONTACT_FAXField = new DataSchemaField();
			DAYCONTACT_FAXField.Name = "DAYCONTACT_FAX";
			DAYCONTACT_FAXField.Type = typeof(string).ToString();
			DAYCONTACT_FAXField.Index = 19;
			fields.Add(DAYCONTACT_FAXField);
			 
			DataSchemaField DAYCONTACT_MOBILEField = new DataSchemaField();
			DAYCONTACT_MOBILEField.Name = "DAYCONTACT_MOBILE";
			DAYCONTACT_MOBILEField.Type = typeof(string).ToString();
			DAYCONTACT_MOBILEField.Index = 20;
			fields.Add(DAYCONTACT_MOBILEField);
			 
			DataSchemaField DAYCONTACT_EMAILField = new DataSchemaField();
			DAYCONTACT_EMAILField.Name = "DAYCONTACT_EMAIL";
			DAYCONTACT_EMAILField.Type = typeof(string).ToString();
			DAYCONTACT_EMAILField.Index = 21;
			fields.Add(DAYCONTACT_EMAILField);
			 
			DataSchemaField PROVINCEField = new DataSchemaField();
			PROVINCEField.Name = "PROVINCE";
			PROVINCEField.Type = typeof(string).ToString();
			PROVINCEField.Index = 22;
			fields.Add(PROVINCEField);
			 
			DataSchemaField CITYField = new DataSchemaField();
			CITYField.Name = "CITY";
			CITYField.Type = typeof(string).ToString();
			CITYField.Index = 23;
			fields.Add(CITYField);
			 
			DataSchemaField SUPPLIER_GROUPField = new DataSchemaField();
			SUPPLIER_GROUPField.Name = "SUPPLIER_GROUP";
			SUPPLIER_GROUPField.Type = typeof(string).ToString();
			SUPPLIER_GROUPField.Index = 24;
			fields.Add(SUPPLIER_GROUPField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 25;
			fields.Add(COMMENTSField);
			 
			DataSchemaField ASN_FLAGField = new DataSchemaField();
			ASN_FLAGField.Name = "ASN_FLAG";
			ASN_FLAGField.Type = typeof(bool).ToString();
			ASN_FLAGField.Index = 26;
			fields.Add(ASN_FLAGField);
			 
			DataSchemaField BATCH_FLAGField = new DataSchemaField();
			BATCH_FLAGField.Name = "BATCH_FLAG";
			BATCH_FLAGField.Type = typeof(bool).ToString();
			BATCH_FLAGField.Index = 27;
			fields.Add(BATCH_FLAGField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 28;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 29;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 30;
			fields.Add(IDField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 31;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 32;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 33;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField ASN_VMI_FLAGField = new DataSchemaField();
			ASN_VMI_FLAGField.Name = "ASN_VMI_FLAG";
			ASN_VMI_FLAGField.Type = typeof(bool).ToString();
			ASN_VMI_FLAGField.Index = 34;
			fields.Add(ASN_VMI_FLAGField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string Duns{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public string SupplierSname{ get;set; }		
				
		[DataMember]
		public string SupplierAddress{ get;set; }		
				
		[DataMember]
		public int? SupplierType{ get;set; }		
				
		[DataMember]
		public string ContactName{ get;set; }		
				
		[DataMember]
		public string ContactTel{ get;set; }		
				
		[DataMember]
		public string ContactFax{ get;set; }		
				
		[DataMember]
		public string ContactMobile{ get;set; }		
				
		[DataMember]
		public string ContactEmail{ get;set; }		
				
		[DataMember]
		public string NightcontactName{ get;set; }		
				
		[DataMember]
		public string NightcontactTel{ get;set; }		
				
		[DataMember]
		public string NightcontactFax{ get;set; }		
				
		[DataMember]
		public string NightcontactMobile{ get;set; }		
				
		[DataMember]
		public string NightcontactEmail{ get;set; }		
				
		[DataMember]
		public string DaycontactName{ get;set; }		
				
		[DataMember]
		public string DaycontactTel{ get;set; }		
				
		[DataMember]
		public string DaycontactFax{ get;set; }		
				
		[DataMember]
		public string DaycontactMobile{ get;set; }		
				
		[DataMember]
		public string DaycontactEmail{ get;set; }		
				
		[DataMember]
		public string Province{ get;set; }		
				
		[DataMember]
		public string City{ get;set; }		
				
		[DataMember]
		public string SupplierGroup{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public bool? AsnFlag{ get;set; }		
				
		[DataMember]
		public bool? BatchFlag{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
				
		private bool _ValidFlag = true;
		
		[DataMember]	
		public bool ValidFlag
		{
			get
			{
				return _ValidFlag;
			}
			set
			{
				_ValidFlag = value;
			}
		}
				
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public bool? AsnVmiFlag{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			SupplierInfo info = new SupplierInfo();

			info.Fid = this.Fid;
			info.SupplierNum = this.SupplierNum;
			info.Duns = this.Duns;
			info.SupplierName = this.SupplierName;
			info.SupplierSname = this.SupplierSname;
			info.SupplierAddress = this.SupplierAddress;
			info.SupplierType = this.SupplierType;
			info.ContactName = this.ContactName;
			info.ContactTel = this.ContactTel;
			info.ContactFax = this.ContactFax;
			info.ContactMobile = this.ContactMobile;
			info.ContactEmail = this.ContactEmail;
			info.NightcontactName = this.NightcontactName;
			info.NightcontactTel = this.NightcontactTel;
			info.NightcontactFax = this.NightcontactFax;
			info.NightcontactMobile = this.NightcontactMobile;
			info.NightcontactEmail = this.NightcontactEmail;
			info.DaycontactName = this.DaycontactName;
			info.DaycontactTel = this.DaycontactTel;
			info.DaycontactFax = this.DaycontactFax;
			info.DaycontactMobile = this.DaycontactMobile;
			info.DaycontactEmail = this.DaycontactEmail;
			info.Province = this.Province;
			info.City = this.City;
			info.SupplierGroup = this.SupplierGroup;
			info.Comments = this.Comments;
			info.AsnFlag = this.AsnFlag;
			info.BatchFlag = this.BatchFlag;
			info.CreateDate = this.CreateDate;
			info.ValidFlag = this.ValidFlag;
			info.Id = this.Id;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.CreateUser = this.CreateUser;
			info.AsnVmiFlag = this.AsnVmiFlag;
			return info;			
		}
		 
		public SupplierInfo Clone()
		{
			return ((ICloneable) this).Clone() as SupplierInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SupplierInfoCollection对应表[TM_BAS_SUPPLIER]
    /// </summary>
	public partial class SupplierInfoCollection : BusinessObjectCollection<SupplierInfo>
	{
		public SupplierInfoCollection():base("TM_BAS_SUPPLIER"){}	
	}
}
