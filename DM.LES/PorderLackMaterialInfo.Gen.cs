#region Declaim
//---------------------------------------------------------------------------
// Name:		PorderLackMaterialInfo
// Function: 	Expose data in table PorderLackMaterial from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月10日
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
    /// PorderLackMaterialInfo对应表[TT_ATP_PORDER_LACK_MATERIAL]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PorderLackMaterialInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PorderLackMaterialInfo( 
					long aId,

					Guid aFid,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					Guid aLackOrderFid,

					Guid aProductionOrderFid,

					string aProductionOrderNo,

					string aPlant,

					string aAssemblyLine,

					bool aLackFlag,

					DateTime aOrderDate,

					int aStatus

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			LackOrderFid = aLackOrderFid;
		 
			ProductionOrderFid = aProductionOrderFid;
		 
			ProductionOrderNo = aProductionOrderNo;
		 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			LackFlag = aLackFlag;
		 
			OrderDate = aOrderDate;
		 
			Status = aStatus;
		}
		
		public PorderLackMaterialInfo():base("TT_ATP_PORDER_LACK_MATERIAL")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");              _Keys = keys.ToArray();
			
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
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 2;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 3;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 4;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 5;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 6;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField LACK_ORDER_FIDField = new DataSchemaField();
			LACK_ORDER_FIDField.Name = "LACK_ORDER_FID";
			LACK_ORDER_FIDField.Type = typeof(Guid).ToString();
			LACK_ORDER_FIDField.Index = 7;
			fields.Add(LACK_ORDER_FIDField);
			 
			DataSchemaField PRODUCTION_ORDER_FIDField = new DataSchemaField();
			PRODUCTION_ORDER_FIDField.Name = "PRODUCTION_ORDER_FID";
			PRODUCTION_ORDER_FIDField.Type = typeof(Guid).ToString();
			PRODUCTION_ORDER_FIDField.Index = 8;
			fields.Add(PRODUCTION_ORDER_FIDField);
			 
			DataSchemaField PRODUCTION_ORDER_NOField = new DataSchemaField();
			PRODUCTION_ORDER_NOField.Name = "PRODUCTION_ORDER_NO";
			PRODUCTION_ORDER_NOField.Type = typeof(string).ToString();
			PRODUCTION_ORDER_NOField.Index = 9;
			fields.Add(PRODUCTION_ORDER_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 10;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 11;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField LACK_FLAGField = new DataSchemaField();
			LACK_FLAGField.Name = "LACK_FLAG";
			LACK_FLAGField.Type = typeof(bool).ToString();
			LACK_FLAGField.Index = 12;
			fields.Add(LACK_FLAGField);
			 
			DataSchemaField ORDER_DATEField = new DataSchemaField();
			ORDER_DATEField.Name = "ORDER_DATE";
			ORDER_DATEField.Type = typeof(DateTime).ToString();
			ORDER_DATEField.Index = 13;
			fields.Add(ORDER_DATEField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 14;
			fields.Add(STATUSField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public bool ValidFlag{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public Guid? LackOrderFid{ get;set; }		
				
		[DataMember]
		public Guid? ProductionOrderFid{ get;set; }		
				
		[DataMember]
		public string ProductionOrderNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public bool? LackFlag{ get;set; }		
				
		[DataMember]
		public DateTime? OrderDate{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PorderLackMaterialInfo info = new PorderLackMaterialInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.LackOrderFid = this.LackOrderFid;
			info.ProductionOrderFid = this.ProductionOrderFid;
			info.ProductionOrderNo = this.ProductionOrderNo;
			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.LackFlag = this.LackFlag;
			info.OrderDate = this.OrderDate;
			info.Status = this.Status;
			return info;			
		}
		 
		public PorderLackMaterialInfo Clone()
		{
			return ((ICloneable) this).Clone() as PorderLackMaterialInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PorderLackMaterialInfoCollection对应表[TT_ATP_PORDER_LACK_MATERIAL]
    /// </summary>
	public partial class PorderLackMaterialInfoCollection : BusinessObjectCollection<PorderLackMaterialInfo>
	{
		public PorderLackMaterialInfoCollection():base("TT_ATP_PORDER_LACK_MATERIAL"){}	
	}
}