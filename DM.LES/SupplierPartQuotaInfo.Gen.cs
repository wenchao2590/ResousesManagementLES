#region Declaim
//---------------------------------------------------------------------------
// Name:		SupplierPartQuotaInfo
// Function: 	Expose data in table SupplierPartQuota from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年10月22日
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
    /// SupplierPartQuotaInfo对应表[TT_SPM_SUPPLIER_PART_QUOTA]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SupplierPartQuotaInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SupplierPartQuotaInfo( 
					long aId,

					string aPlant,

					string aPlantName,

					string aPartNo,

					string aPartCname,

					string aWorkshop,

					string aWorkshopName,

					string aSupplierNum,

					string aSupplierName,

					DateTime aStartEffectiveDate,

					DateTime aEndEffectiveDate,

					decimal aQuote,

					string aProject,

					string aAgreementNo,

					string aComments,

					DateTime aModifyDate,

					string aModifyUser,

					DateTime aCreateDate,

					string aCreateUser,

					string aLoekz,

					int aProcessFlag,

					Guid aFid,

					bool aValidFlag

				 
		) : this()
		{
			 
			Id = aId;
		 
			Plant = aPlant;
		 
			PlantName = aPlantName;
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			Workshop = aWorkshop;
		 
			WorkshopName = aWorkshopName;
		 
			SupplierNum = aSupplierNum;
		 
			SupplierName = aSupplierName;
		 
			StartEffectiveDate = aStartEffectiveDate;
		 
			EndEffectiveDate = aEndEffectiveDate;
		 
			Quote = aQuote;
		 
			Project = aProject;
		 
			AgreementNo = aAgreementNo;
		 
			Comments = aComments;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			Loekz = aLoekz;
		 
			ProcessFlag = aProcessFlag;
		 
			Fid = aFid;
		 
			ValidFlag = aValidFlag;
		}
		
		public SupplierPartQuotaInfo():base("TT_SPM_SUPPLIER_PART_QUOTA")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                      _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 0;
			fields.Add(IDField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 1;
			fields.Add(PLANTField);
			 
			DataSchemaField PLANT_NAMEField = new DataSchemaField();
			PLANT_NAMEField.Name = "PLANT_NAME";
			PLANT_NAMEField.Type = typeof(string).ToString();
			PLANT_NAMEField.Index = 2;
			fields.Add(PLANT_NAMEField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 3;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_CNAMEField = new DataSchemaField();
			PART_CNAMEField.Name = "PART_CNAME";
			PART_CNAMEField.Type = typeof(string).ToString();
			PART_CNAMEField.Index = 4;
			fields.Add(PART_CNAMEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 5;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField WORKSHOP_NAMEField = new DataSchemaField();
			WORKSHOP_NAMEField.Name = "WORKSHOP_NAME";
			WORKSHOP_NAMEField.Type = typeof(string).ToString();
			WORKSHOP_NAMEField.Index = 6;
			fields.Add(WORKSHOP_NAMEField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 7;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 8;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField START_EFFECTIVE_DATEField = new DataSchemaField();
			START_EFFECTIVE_DATEField.Name = "START_EFFECTIVE_DATE";
			START_EFFECTIVE_DATEField.Type = typeof(DateTime).ToString();
			START_EFFECTIVE_DATEField.Index = 9;
			fields.Add(START_EFFECTIVE_DATEField);
			 
			DataSchemaField END_EFFECTIVE_DATEField = new DataSchemaField();
			END_EFFECTIVE_DATEField.Name = "END_EFFECTIVE_DATE";
			END_EFFECTIVE_DATEField.Type = typeof(DateTime).ToString();
			END_EFFECTIVE_DATEField.Index = 10;
			fields.Add(END_EFFECTIVE_DATEField);
			 
			DataSchemaField QUOTEField = new DataSchemaField();
			QUOTEField.Name = "QUOTE";
			QUOTEField.Type = typeof(decimal).ToString();
			QUOTEField.Index = 11;
			fields.Add(QUOTEField);
			 
			DataSchemaField PROJECTField = new DataSchemaField();
			PROJECTField.Name = "PROJECT";
			PROJECTField.Type = typeof(string).ToString();
			PROJECTField.Index = 12;
			fields.Add(PROJECTField);
			 
			DataSchemaField AGREEMENT_NOField = new DataSchemaField();
			AGREEMENT_NOField.Name = "AGREEMENT_NO";
			AGREEMENT_NOField.Type = typeof(string).ToString();
			AGREEMENT_NOField.Index = 13;
			fields.Add(AGREEMENT_NOField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 14;
			fields.Add(COMMENTSField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 15;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 16;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 17;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 18;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField LOEKZField = new DataSchemaField();
			LOEKZField.Name = "LOEKZ";
			LOEKZField.Type = typeof(string).ToString();
			LOEKZField.Index = 19;
			fields.Add(LOEKZField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 20;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 21;
			fields.Add(FIDField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 22;
			fields.Add(VALID_FLAGField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string PlantName{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string WorkshopName{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public DateTime? StartEffectiveDate{ get;set; }		
				
		[DataMember]
		public DateTime? EndEffectiveDate{ get;set; }		
				
		[DataMember]
		public decimal? Quote{ get;set; }		
				
		[DataMember]
		public string Project{ get;set; }		
				
		[DataMember]
		public string AgreementNo{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public string Loekz{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			SupplierPartQuotaInfo info = new SupplierPartQuotaInfo();

			info.Id = this.Id;
			info.Plant = this.Plant;
			info.PlantName = this.PlantName;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.Workshop = this.Workshop;
			info.WorkshopName = this.WorkshopName;
			info.SupplierNum = this.SupplierNum;
			info.SupplierName = this.SupplierName;
			info.StartEffectiveDate = this.StartEffectiveDate;
			info.EndEffectiveDate = this.EndEffectiveDate;
			info.Quote = this.Quote;
			info.Project = this.Project;
			info.AgreementNo = this.AgreementNo;
			info.Comments = this.Comments;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.Loekz = this.Loekz;
			info.ProcessFlag = this.ProcessFlag;
			info.Fid = this.Fid;
			info.ValidFlag = this.ValidFlag;
			return info;			
		}
		 
		public SupplierPartQuotaInfo Clone()
		{
			return ((ICloneable) this).Clone() as SupplierPartQuotaInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SupplierPartQuotaInfoCollection对应表[TT_SPM_SUPPLIER_PART_QUOTA]
    /// </summary>
	public partial class SupplierPartQuotaInfoCollection : BusinessObjectCollection<SupplierPartQuotaInfo>
	{
		public SupplierPartQuotaInfoCollection():base("TT_SPM_SUPPLIER_PART_QUOTA"){}	
	}
}
