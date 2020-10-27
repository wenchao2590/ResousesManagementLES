#region Declaim
//---------------------------------------------------------------------------
// Name:		DcpPointInfo
// Function: 	Expose data in table DcpPoint from database as business object to MES system.
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
    /// DcpPointInfo对应表[TM_BAS_DCP_POINT]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class DcpPointInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public DcpPointInfo( 
					string aPlant,

					string aAssemblyLine,

					string aDcpPoint,

					string aPlantZone,

					string aWorkshop,

					string aDcpName,

					string aVehicleStatus,

					string aDcpSequence,

					string aComments,

					string aUpdateUser,

					string aCreateUser,

					DateTime aUpdateDate,

					DateTime aCreateDate

				 
		) : this()
		{
			 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			DcpPoint = aDcpPoint;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			DcpName = aDcpName;
		 
			VehicleStatus = aVehicleStatus;
		 
			DcpSequence = aDcpSequence;
		 
			Comments = aComments;
		 
			UpdateUser = aUpdateUser;
		 
			CreateUser = aCreateUser;
		 
			UpdateDate = aUpdateDate;
		 
			CreateDate = aCreateDate;
		}
		
		public DcpPointInfo():base("TM_BAS_DCP_POINT")
		{
			List<string> keys = new List<string>();
			 			keys.Add("PLANT"); 			keys.Add("ASSEMBLY_LINE"); 			keys.Add("DCP_POINT");          _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 0;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 1;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField DCP_POINTField = new DataSchemaField();
			DCP_POINTField.Name = "DCP_POINT";
			DCP_POINTField.Type = typeof(string).ToString();
			DCP_POINTField.Index = 2;
			fields.Add(DCP_POINTField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 3;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 4;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField DCP_NAMEField = new DataSchemaField();
			DCP_NAMEField.Name = "DCP_NAME";
			DCP_NAMEField.Type = typeof(string).ToString();
			DCP_NAMEField.Index = 5;
			fields.Add(DCP_NAMEField);
			 
			DataSchemaField VEHICLE_STATUSField = new DataSchemaField();
			VEHICLE_STATUSField.Name = "VEHICLE_STATUS";
			VEHICLE_STATUSField.Type = typeof(string).ToString();
			VEHICLE_STATUSField.Index = 6;
			fields.Add(VEHICLE_STATUSField);
			 
			DataSchemaField DCP_SEQUENCEField = new DataSchemaField();
			DCP_SEQUENCEField.Name = "DCP_SEQUENCE";
			DCP_SEQUENCEField.Type = typeof(string).ToString();
			DCP_SEQUENCEField.Index = 7;
			fields.Add(DCP_SEQUENCEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 8;
			fields.Add(COMMENTSField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 9;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 10;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 11;
			fields.Add(UPDATE_DATEField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 12;
			fields.Add(CREATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string DcpPoint{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string DcpName{ get;set; }		
				
		[DataMember]
		public string VehicleStatus{ get;set; }		
				
		[DataMember]
		public string DcpSequence{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			DcpPointInfo info = new DcpPointInfo();

			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.DcpPoint = this.DcpPoint;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.DcpName = this.DcpName;
			info.VehicleStatus = this.VehicleStatus;
			info.DcpSequence = this.DcpSequence;
			info.Comments = this.Comments;
			info.UpdateUser = this.UpdateUser;
			info.CreateUser = this.CreateUser;
			info.UpdateDate = this.UpdateDate;
			info.CreateDate = this.CreateDate;
			return info;			
		}
		 
		public DcpPointInfo Clone()
		{
			return ((ICloneable) this).Clone() as DcpPointInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// DcpPointInfoCollection对应表[TM_BAS_DCP_POINT]
    /// </summary>
	public partial class DcpPointInfoCollection : BusinessObjectCollection<DcpPointInfo>
	{
		public DcpPointInfoCollection():base("TM_BAS_DCP_POINT"){}	
	}
}
