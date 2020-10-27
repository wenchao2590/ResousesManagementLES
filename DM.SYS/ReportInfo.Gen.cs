#region Declaim
//---------------------------------------------------------------------------
// Name:		ReportInfo
// Function: 	Expose data in table TS_SYS_REPORT from database as business object to MES system.
// Tool:		T4
// CreateDate:	2016年5月13日
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
    /// ReportInfo对应表[TS_SYS_REPORT]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class ReportInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public ReportInfo(		 long aId,
				Guid aFid,
				string aName,
				string aNameEn,
				bool aIsAuth,
				int aReportType,
				string aSortField,
				string aKeyField,
				string aSqlString,
				string aPlant,
				bool aValidFlag,
				string aCreateUser,
				DateTime aCreateDate,
				string aModifyUser,
				DateTime aModifyDate				
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			Name = aName;
		 
			NameEn = aNameEn;
		 
			IsAuth = aIsAuth;
		 
			ReportType = aReportType;
		 
			SortField = aSortField;
		 
			KeyField = aKeyField;
		 
			SqlString = aSqlString;
		 
			Plant = aPlant;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public ReportInfo():base("TS_SYS_REPORT")
		{
			List<string> keys = new List<string>();
			 			keys.Add("Id");              _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField IdField = new DataSchemaField();
			IdField.Name = "Id";
			IdField.Type = typeof(long).ToString();
			IdField.Index = 0;
			fields.Add(IdField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 1;
			fields.Add(FIDField);
			 
			DataSchemaField NAMEField = new DataSchemaField();
			NAMEField.Name = "NAME";
			NAMEField.Type = typeof(string).ToString();
			NAMEField.Index = 2;
			fields.Add(NAMEField);
			 
			DataSchemaField NAME_ENField = new DataSchemaField();
			NAME_ENField.Name = "NAME_EN";
			NAME_ENField.Type = typeof(string).ToString();
			NAME_ENField.Index = 3;
			fields.Add(NAME_ENField);
			 
			DataSchemaField IS_AUTHField = new DataSchemaField();
			IS_AUTHField.Name = "IS_AUTH";
			IS_AUTHField.Type = typeof(bool).ToString();
			IS_AUTHField.Index = 4;
			fields.Add(IS_AUTHField);
			 
			DataSchemaField REPORT_TYPEField = new DataSchemaField();
			REPORT_TYPEField.Name = "REPORT_TYPE";
			REPORT_TYPEField.Type = typeof(int).ToString();
			REPORT_TYPEField.Index = 5;
			fields.Add(REPORT_TYPEField);
			 
			DataSchemaField SORT_FIELDField = new DataSchemaField();
			SORT_FIELDField.Name = "SORT_FIELD";
			SORT_FIELDField.Type = typeof(string).ToString();
			SORT_FIELDField.Index = 6;
			fields.Add(SORT_FIELDField);
			 
			DataSchemaField KEY_FIELDField = new DataSchemaField();
			KEY_FIELDField.Name = "KEY_FIELD";
			KEY_FIELDField.Type = typeof(string).ToString();
			KEY_FIELDField.Index = 7;
			fields.Add(KEY_FIELDField);
			 
			DataSchemaField SQL_STRINGField = new DataSchemaField();
			SQL_STRINGField.Name = "SQL_STRING";
			SQL_STRINGField.Type = typeof(string).ToString();
			SQL_STRINGField.Index = 8;
			fields.Add(SQL_STRINGField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 9;
			fields.Add(PLANTField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 10;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 11;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 12;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 13;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 14;
			fields.Add(MODIFY_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string Name{ get;set; }		
				
		[DataMember]
		public string NameEn{ get;set; }		
				
		[DataMember]
		public bool? IsAuth{ get;set; }		
				
		[DataMember]
		public int? ReportType{ get;set; }		
				
		[DataMember]
		public string SortField{ get;set; }		
				
		[DataMember]
		public string KeyField{ get;set; }		
				
		[DataMember]
		public string SqlString{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
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
			ReportInfo info = new ReportInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.Name = this.Name;
			info.NameEn = this.NameEn;
			info.IsAuth = this.IsAuth;
			info.ReportType = this.ReportType;
			info.SortField = this.SortField;
			info.KeyField = this.KeyField;
			info.SqlString = this.SqlString;
			info.Plant = this.Plant;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public ReportInfo Clone()
		{
			return ((ICloneable) this).Clone() as ReportInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// ReportInfoCollection对应表[TS_SYS_REPORT]
    /// </summary>
	public partial class ReportInfoCollection : BusinessObjectCollection<ReportInfo>
	{
		public ReportInfoCollection():base("TS_SYS_REPORT"){}	
	}
}
