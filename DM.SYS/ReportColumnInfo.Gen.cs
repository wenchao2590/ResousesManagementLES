#region Declaim
//---------------------------------------------------------------------------
// Name:		ReportColumnInfo
// Function: 	Expose data in table TS_SYS_REPORT_COLUMN from database as business object to MES system.
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
    /// ReportColumnInfo对应表[TS_SYS_REPORT_COLUMN]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class ReportColumnInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public ReportColumnInfo(		 long aId,
				Guid aFid,
				Guid aPid,
				string aFieldName,
				string aDisplayNameCn,
				string aDisplayNameEn,
				int aFieldType,
				int aDisplayOrder,
				int aDisplayWidth,
				bool aValidFlag,
				string aCreateUser,
				DateTime aCreateDate,
				string aModifyUser,
				DateTime aModifyDate				
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			Pid = aPid;
		 
			FieldName = aFieldName;
		 
			DisplayNameCn = aDisplayNameCn;
		 
			DisplayNameEn = aDisplayNameEn;
		 
			FieldType = aFieldType;
		 
			DisplayOrder = aDisplayOrder;
		 
			DisplayWidth = aDisplayWidth;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public ReportColumnInfo():base("TS_SYS_REPORT_COLUMN")
		{
			List<string> keys = new List<string>();
			 			keys.Add("Id");             _Keys = keys.ToArray();
			
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
			 
			DataSchemaField PIDField = new DataSchemaField();
			PIDField.Name = "PID";
			PIDField.Type = typeof(Guid).ToString();
			PIDField.Index = 2;
			fields.Add(PIDField);
			 
			DataSchemaField FIELD_NAMEField = new DataSchemaField();
			FIELD_NAMEField.Name = "FIELD_NAME";
			FIELD_NAMEField.Type = typeof(string).ToString();
			FIELD_NAMEField.Index = 3;
			fields.Add(FIELD_NAMEField);
			 
			DataSchemaField DISPLAY_NAME_CNField = new DataSchemaField();
			DISPLAY_NAME_CNField.Name = "DISPLAY_NAME_CN";
			DISPLAY_NAME_CNField.Type = typeof(string).ToString();
			DISPLAY_NAME_CNField.Index = 4;
			fields.Add(DISPLAY_NAME_CNField);
			 
			DataSchemaField DISPLAY_NAME_ENField = new DataSchemaField();
			DISPLAY_NAME_ENField.Name = "DISPLAY_NAME_EN";
			DISPLAY_NAME_ENField.Type = typeof(string).ToString();
			DISPLAY_NAME_ENField.Index = 5;
			fields.Add(DISPLAY_NAME_ENField);
			 
			DataSchemaField FIELD_TYPEField = new DataSchemaField();
			FIELD_TYPEField.Name = "FIELD_TYPE";
			FIELD_TYPEField.Type = typeof(int).ToString();
			FIELD_TYPEField.Index = 6;
			fields.Add(FIELD_TYPEField);
			 
			DataSchemaField DISPLAY_ORDERField = new DataSchemaField();
			DISPLAY_ORDERField.Name = "DISPLAY_ORDER";
			DISPLAY_ORDERField.Type = typeof(int).ToString();
			DISPLAY_ORDERField.Index = 7;
			fields.Add(DISPLAY_ORDERField);
			 
			DataSchemaField DISPLAY_WIDTHField = new DataSchemaField();
			DISPLAY_WIDTHField.Name = "DISPLAY_WIDTH";
			DISPLAY_WIDTHField.Type = typeof(int).ToString();
			DISPLAY_WIDTHField.Index = 8;
			fields.Add(DISPLAY_WIDTHField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 9;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 10;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 11;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 12;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 13;
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
		public Guid? Pid{ get;set; }		
				
		[DataMember]
		public string FieldName{ get;set; }		
				
		[DataMember]
		public string DisplayNameCn{ get;set; }		
				
		[DataMember]
		public string DisplayNameEn{ get;set; }		
				
		[DataMember]
		public int? FieldType{ get;set; }		
				
		[DataMember]
		public int? DisplayOrder{ get;set; }		
				
		[DataMember]
		public int? DisplayWidth{ get;set; }		
				
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
			ReportColumnInfo info = new ReportColumnInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.Pid = this.Pid;
			info.FieldName = this.FieldName;
			info.DisplayNameCn = this.DisplayNameCn;
			info.DisplayNameEn = this.DisplayNameEn;
			info.FieldType = this.FieldType;
			info.DisplayOrder = this.DisplayOrder;
			info.DisplayWidth = this.DisplayWidth;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public ReportColumnInfo Clone()
		{
			return ((ICloneable) this).Clone() as ReportColumnInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// ReportColumnInfoCollection对应表[TS_SYS_REPORT_COLUMN]
    /// </summary>
	public partial class ReportColumnInfoCollection : BusinessObjectCollection<ReportColumnInfo>
	{
		public ReportColumnInfoCollection():base("TS_SYS_REPORT_COLUMN"){}	
	}
}
