#region Declaim
//---------------------------------------------------------------------------
// Name:		PlantInfo
// Function: 	Expose data in table Plant from database as business object to MES system.
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
    /// PlantInfo对应表[TM_BAS_PLANT]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PlantInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PlantInfo( 
					Guid aFid,

					string aPlant,

					string aPlantName,

					string aSapPlantCode,

					string aDescription,

					string aComments,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					long aId

				 
		) : this()
		{
			 
			Fid = aFid;
		 
			Plant = aPlant;
		 
			PlantName = aPlantName;
		 
			SapPlantCode = aSapPlantCode;
		 
			Description = aDescription;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			Id = aId;
		}
		
		public PlantInfo():base("TM_BAS_PLANT")
		{
			List<string> keys = new List<string>();
			            			keys.Add("ID");_Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 0;
			fields.Add(FIDField);
			 
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
			 
			DataSchemaField SAP_PLANT_CODEField = new DataSchemaField();
			SAP_PLANT_CODEField.Name = "SAP_PLANT_CODE";
			SAP_PLANT_CODEField.Type = typeof(string).ToString();
			SAP_PLANT_CODEField.Index = 3;
			fields.Add(SAP_PLANT_CODEField);
			 
			DataSchemaField DESCRIPTIONField = new DataSchemaField();
			DESCRIPTIONField.Name = "DESCRIPTION";
			DESCRIPTIONField.Type = typeof(string).ToString();
			DESCRIPTIONField.Index = 4;
			fields.Add(DESCRIPTIONField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 5;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 6;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 7;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 8;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 9;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 10;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 11;
			fields.Add(IDField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string PlantName{ get;set; }		
				
		[DataMember]
		public string SapPlantCode{ get;set; }		
				
		[DataMember]
		public string Description{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
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
		public long Id{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PlantInfo info = new PlantInfo();

			info.Fid = this.Fid;
			info.Plant = this.Plant;
			info.PlantName = this.PlantName;
			info.SapPlantCode = this.SapPlantCode;
			info.Description = this.Description;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.Id = this.Id;
			return info;			
		}
		 
		public PlantInfo Clone()
		{
			return ((ICloneable) this).Clone() as PlantInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PlantInfoCollection对应表[TM_BAS_PLANT]
    /// </summary>
	public partial class PlantInfoCollection : BusinessObjectCollection<PlantInfo>
	{
		public PlantInfoCollection():base("TM_BAS_PLANT"){}	
	}
}
