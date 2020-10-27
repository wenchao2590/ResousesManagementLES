#region Declaim
//---------------------------------------------------------------------------
// Name:		WarehouseInfo
// Function: 	Expose data in table Warehouse from database as business object to MES system.
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
    /// WarehouseInfo对应表[TM_BAS_WAREHOUSE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class WarehouseInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public WarehouseInfo( 
					long aId,

					string aWarehouse,

					string aWarehouseName,

					int aWarehouseType,

					bool aVmiEnable,

					string aPlant,

					string aPlantZone,

					string aWorkshop,

					string aAssemblyLine,

					string aComments,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					bool aValidFlag,

					Guid aFid

				 
		) : this()
		{
			 
			Id = aId;
		 
			Warehouse = aWarehouse;
		 
			WarehouseName = aWarehouseName;
		 
			WarehouseType = aWarehouseType;
		 
			VmiEnable = aVmiEnable;
		 
			Plant = aPlant;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			AssemblyLine = aAssemblyLine;
		 
			Comments = aComments;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			ValidFlag = aValidFlag;
		 
			Fid = aFid;
		}
		
		public WarehouseInfo():base("TM_BAS_WAREHOUSE")
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
			 
			DataSchemaField WAREHOUSEField = new DataSchemaField();
			WAREHOUSEField.Name = "WAREHOUSE";
			WAREHOUSEField.Type = typeof(string).ToString();
			WAREHOUSEField.Index = 1;
			fields.Add(WAREHOUSEField);
			 
			DataSchemaField WAREHOUSE_NAMEField = new DataSchemaField();
			WAREHOUSE_NAMEField.Name = "WAREHOUSE_NAME";
			WAREHOUSE_NAMEField.Type = typeof(string).ToString();
			WAREHOUSE_NAMEField.Index = 2;
			fields.Add(WAREHOUSE_NAMEField);
			 
			DataSchemaField WAREHOUSE_TYPEField = new DataSchemaField();
			WAREHOUSE_TYPEField.Name = "WAREHOUSE_TYPE";
			WAREHOUSE_TYPEField.Type = typeof(int).ToString();
			WAREHOUSE_TYPEField.Index = 3;
			fields.Add(WAREHOUSE_TYPEField);
			 
			DataSchemaField VMI_ENABLEField = new DataSchemaField();
			VMI_ENABLEField.Name = "VMI_ENABLE";
			VMI_ENABLEField.Type = typeof(bool).ToString();
			VMI_ENABLEField.Index = 4;
			fields.Add(VMI_ENABLEField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 5;
			fields.Add(PLANTField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 6;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 7;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 8;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 9;
			fields.Add(COMMENTSField);
			 
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
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 14;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 15;
			fields.Add(FIDField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string Warehouse{ get;set; }		
				
		[DataMember]
		public string WarehouseName{ get;set; }		
				
		[DataMember]
		public int? WarehouseType{ get;set; }		
				
		[DataMember]
		public bool? VmiEnable{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
				
		private bool? _ValidFlag = true;
		
		[DataMember]	
		public bool? ValidFlag
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
		public Guid? Fid{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			WarehouseInfo info = new WarehouseInfo();

			info.Id = this.Id;
			info.Warehouse = this.Warehouse;
			info.WarehouseName = this.WarehouseName;
			info.WarehouseType = this.WarehouseType;
			info.VmiEnable = this.VmiEnable;
			info.Plant = this.Plant;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.AssemblyLine = this.AssemblyLine;
			info.Comments = this.Comments;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.ValidFlag = this.ValidFlag;
			info.Fid = this.Fid;
			return info;			
		}
		 
		public WarehouseInfo Clone()
		{
			return ((ICloneable) this).Clone() as WarehouseInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// WarehouseInfoCollection对应表[TM_BAS_WAREHOUSE]
    /// </summary>
	public partial class WarehouseInfoCollection : BusinessObjectCollection<WarehouseInfo>
	{
		public WarehouseInfoCollection():base("TM_BAS_WAREHOUSE"){}	
	}
}
