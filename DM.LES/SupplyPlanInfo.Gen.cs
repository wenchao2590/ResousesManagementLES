#region Declaim
//---------------------------------------------------------------------------
// Name:		SupplyPlanInfo
// Function: 	Expose data in table SupplyPlan from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月4日
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
    /// SupplyPlanInfo对应表[TT_ATP_SUPPLY_PLAN]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SupplyPlanInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SupplyPlanInfo( 
					long aId,

					Guid aFid,

					string aPartNo,

					string aPartCname,

					string aSupplierNum,

					string aSupplierName,

					string aPartPurchaser,

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
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			SupplierNum = aSupplierNum;
		 
			SupplierName = aSupplierName;
		 
			PartPurchaser = aPartPurchaser;
		 
			Plant = aPlant;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;		 
		}
		
		public SupplyPlanInfo():base("TT_ATP_SUPPLY_PLAN")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                                                         _Keys = keys.ToArray();
			
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
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 2;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_CNAMEField = new DataSchemaField();
			PART_CNAMEField.Name = "PART_CNAME";
			PART_CNAMEField.Type = typeof(string).ToString();
			PART_CNAMEField.Index = 3;
			fields.Add(PART_CNAMEField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 4;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 5;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField PART_PURCHASERField = new DataSchemaField();
			PART_PURCHASERField.Name = "PART_PURCHASER";
			PART_PURCHASERField.Type = typeof(string).ToString();
			PART_PURCHASERField.Index = 6;
			fields.Add(PART_PURCHASERField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 7;
			fields.Add(PLANTField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 8;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 9;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 10;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 11;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 12;
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
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public string PartPurchaser{ get;set; }		
				
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
			SupplyPlanInfo info = new SupplyPlanInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.SupplierNum = this.SupplierNum;
			info.SupplierName = this.SupplierName;
			info.PartPurchaser = this.PartPurchaser;
			info.Plant = this.Plant;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;		
			return info;			
		}
		 
		public SupplyPlanInfo Clone()
		{
			return ((ICloneable) this).Clone() as SupplyPlanInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SupplyPlanInfoCollection对应表[TT_ATP_SUPPLY_PLAN]
    /// </summary>
	public partial class SupplyPlanInfoCollection : BusinessObjectCollection<SupplyPlanInfo>
	{
		public SupplyPlanInfoCollection():base("TT_ATP_SUPPLY_PLAN"){}	
	}
}
