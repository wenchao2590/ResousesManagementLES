#region Declaim
//---------------------------------------------------------------------------
// Name:		QmisCheckModeInfo
// Function: 	Expose data in table QmisCheckMode from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月24日
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
    /// QmisCheckModeInfo对应表[TI_IFM_QMIS_CHECK_MODE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class QmisCheckModeInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public QmisCheckModeInfo( 
					long aId,

					Guid aFid,

					Guid aLogFid,

					string aPartNo,

					string aPartName,

					string aSupplierNo,

					string aSupplierName,

					string aCheckMode,

					int aProcessFlag,

					DateTime aProcessTime,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					string aComments

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			LogFid = aLogFid;
		 
			PartNo = aPartNo;
		 
			PartName = aPartName;
		 
			SupplierNo = aSupplierNo;
		 
			SupplierName = aSupplierName;
		 
			CheckMode = aCheckMode;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			Comments = aComments;
		}
		
		public QmisCheckModeInfo():base("TI_IFM_QMIS_CHECK_MODE")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");               _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 0;
			fields.Add(IDField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 1;
			fields.Add(FIDField);
			 
			DataSchemaField LOG_FIDField = new DataSchemaField();
			LOG_FIDField.Name = "LOG_FID";
			LOG_FIDField.Type = typeof(Guid).ToString();
			LOG_FIDField.Index = 2;
			fields.Add(LOG_FIDField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 3;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_NAMEField = new DataSchemaField();
			PART_NAMEField.Name = "PART_NAME";
			PART_NAMEField.Type = typeof(string).ToString();
			PART_NAMEField.Index = 4;
			fields.Add(PART_NAMEField);
			 
			DataSchemaField SUPPLIER_NOField = new DataSchemaField();
			SUPPLIER_NOField.Name = "SUPPLIER_NO";
			SUPPLIER_NOField.Type = typeof(string).ToString();
			SUPPLIER_NOField.Index = 5;
			fields.Add(SUPPLIER_NOField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 6;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField CHECK_MODEField = new DataSchemaField();
			CHECK_MODEField.Name = "CHECK_MODE";
			CHECK_MODEField.Type = typeof(string).ToString();
			CHECK_MODEField.Index = 7;
			fields.Add(CHECK_MODEField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 8;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 9;
			fields.Add(PROCESS_TIMEField);
			 
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
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 15;
			fields.Add(COMMENTSField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public Guid? LogFid{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartName{ get;set; }		
				
		[DataMember]
		public string SupplierNo{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public string CheckMode{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			QmisCheckModeInfo info = new QmisCheckModeInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LogFid = this.LogFid;
			info.PartNo = this.PartNo;
			info.PartName = this.PartName;
			info.SupplierNo = this.SupplierNo;
			info.SupplierName = this.SupplierName;
			info.CheckMode = this.CheckMode;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.Comments = this.Comments;
			return info;			
		}
		 
		public QmisCheckModeInfo Clone()
		{
			return ((ICloneable) this).Clone() as QmisCheckModeInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// QmisCheckModeInfoCollection对应表[TI_IFM_QMIS_CHECK_MODE]
    /// </summary>
	public partial class QmisCheckModeInfoCollection : BusinessObjectCollection<QmisCheckModeInfo>
	{
		public QmisCheckModeInfoCollection():base("TI_IFM_QMIS_CHECK_MODE"){}	
	}
}
