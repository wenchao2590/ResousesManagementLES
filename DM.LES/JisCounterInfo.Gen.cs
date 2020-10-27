#region Declaim
//---------------------------------------------------------------------------
// Name:		JisCounterInfo
// Function: 	Expose data in table JisCounter from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月11日
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
    /// JisCounterInfo对应表[TT_MPM_JIS_COUNTER]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class JisCounterInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public JisCounterInfo( 
					long aId,

					Guid aFid,

					Guid aPartBoxFid,

					string aPartBoxCode,

					string aPlant,

					string aPlantZone,

					string aWorkshop,

					string aAssemblyLine,

					string aSupplierNum,

					int aAccumulativeType,

					string aWorkshopSection,

					string aLocation,

					decimal aCurrentQty,

					decimal aAccumulativeQty,

					string aPackageModel,

					int aStatus,

					string aComments,

					bool aValidFlag,

					DateTime aCreateDate,

					string aCreateUser,

					DateTime aModifyDate,

					string aModifyUser

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			PartBoxFid = aPartBoxFid;
		 
			PartBoxCode = aPartBoxCode;
		 
			Plant = aPlant;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			AssemblyLine = aAssemblyLine;
		 
			SupplierNum = aSupplierNum;
		 
			AccumulativeType = aAccumulativeType;
		 
			WorkshopSection = aWorkshopSection;
		 
			Location = aLocation;
		 
			CurrentQty = aCurrentQty;
		 
			AccumulativeQty = aAccumulativeQty;
		 
			PackageModel = aPackageModel;
		 
			Status = aStatus;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		}
		
		public JisCounterInfo():base("TT_MPM_JIS_COUNTER")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                     _Keys = keys.ToArray();
			
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
			 
			DataSchemaField PART_BOX_FIDField = new DataSchemaField();
			PART_BOX_FIDField.Name = "PART_BOX_FID";
			PART_BOX_FIDField.Type = typeof(Guid).ToString();
			PART_BOX_FIDField.Index = 2;
			fields.Add(PART_BOX_FIDField);
			 
			DataSchemaField PART_BOX_CODEField = new DataSchemaField();
			PART_BOX_CODEField.Name = "PART_BOX_CODE";
			PART_BOX_CODEField.Type = typeof(string).ToString();
			PART_BOX_CODEField.Index = 3;
			fields.Add(PART_BOX_CODEField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 4;
			fields.Add(PLANTField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 5;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 6;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 7;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 8;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField ACCUMULATIVE_TYPEField = new DataSchemaField();
			ACCUMULATIVE_TYPEField.Name = "ACCUMULATIVE_TYPE";
			ACCUMULATIVE_TYPEField.Type = typeof(int).ToString();
			ACCUMULATIVE_TYPEField.Index = 9;
			fields.Add(ACCUMULATIVE_TYPEField);
			 
			DataSchemaField WORKSHOP_SECTIONField = new DataSchemaField();
			WORKSHOP_SECTIONField.Name = "WORKSHOP_SECTION";
			WORKSHOP_SECTIONField.Type = typeof(string).ToString();
			WORKSHOP_SECTIONField.Index = 10;
			fields.Add(WORKSHOP_SECTIONField);
			 
			DataSchemaField LOCATIONField = new DataSchemaField();
			LOCATIONField.Name = "LOCATION";
			LOCATIONField.Type = typeof(string).ToString();
			LOCATIONField.Index = 11;
			fields.Add(LOCATIONField);
			 
			DataSchemaField CURRENT_QTYField = new DataSchemaField();
			CURRENT_QTYField.Name = "CURRENT_QTY";
			CURRENT_QTYField.Type = typeof(decimal).ToString();
			CURRENT_QTYField.Index = 12;
			fields.Add(CURRENT_QTYField);
			 
			DataSchemaField ACCUMULATIVE_QTYField = new DataSchemaField();
			ACCUMULATIVE_QTYField.Name = "ACCUMULATIVE_QTY";
			ACCUMULATIVE_QTYField.Type = typeof(decimal).ToString();
			ACCUMULATIVE_QTYField.Index = 13;
			fields.Add(ACCUMULATIVE_QTYField);
			 
			DataSchemaField PACKAGE_MODELField = new DataSchemaField();
			PACKAGE_MODELField.Name = "PACKAGE_MODEL";
			PACKAGE_MODELField.Type = typeof(string).ToString();
			PACKAGE_MODELField.Index = 14;
			fields.Add(PACKAGE_MODELField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 15;
			fields.Add(STATUSField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 16;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 17;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 18;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 19;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 20;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 21;
			fields.Add(MODIFY_USERField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public Guid? PartBoxFid{ get;set; }		
				
		[DataMember]
		public string PartBoxCode{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public int? AccumulativeType{ get;set; }		
				
		[DataMember]
		public string WorkshopSection{ get;set; }		
				
		[DataMember]
		public string Location{ get;set; }		
				
		[DataMember]
		public decimal? CurrentQty{ get;set; }		
				
		[DataMember]
		public decimal? AccumulativeQty{ get;set; }		
				
		[DataMember]
		public string PackageModel{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public bool ValidFlag{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			JisCounterInfo info = new JisCounterInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.PartBoxFid = this.PartBoxFid;
			info.PartBoxCode = this.PartBoxCode;
			info.Plant = this.Plant;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.AssemblyLine = this.AssemblyLine;
			info.SupplierNum = this.SupplierNum;
			info.AccumulativeType = this.AccumulativeType;
			info.WorkshopSection = this.WorkshopSection;
			info.Location = this.Location;
			info.CurrentQty = this.CurrentQty;
			info.AccumulativeQty = this.AccumulativeQty;
			info.PackageModel = this.PackageModel;
			info.Status = this.Status;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			return info;			
		}
		 
		public JisCounterInfo Clone()
		{
			return ((ICloneable) this).Clone() as JisCounterInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// JisCounterInfoCollection对应表[TT_MPM_JIS_COUNTER]
    /// </summary>
	public partial class JisCounterInfoCollection : BusinessObjectCollection<JisCounterInfo>
	{
		public JisCounterInfoCollection():base("TT_MPM_JIS_COUNTER"){}	
	}
}