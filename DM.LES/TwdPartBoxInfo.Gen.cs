#region Declaim
//---------------------------------------------------------------------------
// Name:		TwdPartBoxInfo
// Function: 	Expose data in table TwdPartBox from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月20日
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
    /// TwdPartBoxInfo对应表[TM_MPM_TWD_PART_BOX]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class TwdPartBoxInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public TwdPartBoxInfo( 
					long aId,

					Guid aFid,

					string aPartBoxCode,

					string aPartBoxName,

					string aPlant,

					string aPlantZone,

					string aWorkshop,

					string aAssemblyLine,

					string aSupplierNum,

					string aSWmNo,

					string aSZoneNo,

					string aTWmNo,

					string aTZoneNo,

					string aDock,

					string aStatusPointCode,

					int aStatus,

					int aRequirementAccumulateTime,

					int aLoadTime,

					int aTransportTime,

					int aUnloadTime,

					int aDelayTime,

					int aOnlineTime,

					string aRouteCode,

					int aRequirementAccumulateMode,

					int aRoundnessMode,

					int aTwdPullMode,

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
		 
			PartBoxCode = aPartBoxCode;
		 
			PartBoxName = aPartBoxName;
		 
			Plant = aPlant;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			AssemblyLine = aAssemblyLine;
		 
			SupplierNum = aSupplierNum;
		 
			SWmNo = aSWmNo;
		 
			SZoneNo = aSZoneNo;
		 
			TWmNo = aTWmNo;
		 
			TZoneNo = aTZoneNo;
		 
			Dock = aDock;
		 
			StatusPointCode = aStatusPointCode;
		 
			Status = aStatus;
		 
			RequirementAccumulateTime = aRequirementAccumulateTime;
		 
			LoadTime = aLoadTime;
		 
			TransportTime = aTransportTime;
		 
			UnloadTime = aUnloadTime;
		 
			DelayTime = aDelayTime;
		 
			OnlineTime = aOnlineTime;
		 
			RouteCode = aRouteCode;
		 
			RequirementAccumulateMode = aRequirementAccumulateMode;
		 
			RoundnessMode = aRoundnessMode;
		 
			TwdPullMode = aTwdPullMode;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		}
		
		public TwdPartBoxInfo():base("TM_MPM_TWD_PART_BOX")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                               _Keys = keys.ToArray();
			
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
			 
			DataSchemaField PART_BOX_CODEField = new DataSchemaField();
			PART_BOX_CODEField.Name = "PART_BOX_CODE";
			PART_BOX_CODEField.Type = typeof(string).ToString();
			PART_BOX_CODEField.Index = 2;
			fields.Add(PART_BOX_CODEField);
			 
			DataSchemaField PART_BOX_NAMEField = new DataSchemaField();
			PART_BOX_NAMEField.Name = "PART_BOX_NAME";
			PART_BOX_NAMEField.Type = typeof(string).ToString();
			PART_BOX_NAMEField.Index = 3;
			fields.Add(PART_BOX_NAMEField);
			 
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
			 
			DataSchemaField S_WM_NOField = new DataSchemaField();
			S_WM_NOField.Name = "S_WM_NO";
			S_WM_NOField.Type = typeof(string).ToString();
			S_WM_NOField.Index = 9;
			fields.Add(S_WM_NOField);
			 
			DataSchemaField S_ZONE_NOField = new DataSchemaField();
			S_ZONE_NOField.Name = "S_ZONE_NO";
			S_ZONE_NOField.Type = typeof(string).ToString();
			S_ZONE_NOField.Index = 10;
			fields.Add(S_ZONE_NOField);
			 
			DataSchemaField T_WM_NOField = new DataSchemaField();
			T_WM_NOField.Name = "T_WM_NO";
			T_WM_NOField.Type = typeof(string).ToString();
			T_WM_NOField.Index = 11;
			fields.Add(T_WM_NOField);
			 
			DataSchemaField T_ZONE_NOField = new DataSchemaField();
			T_ZONE_NOField.Name = "T_ZONE_NO";
			T_ZONE_NOField.Type = typeof(string).ToString();
			T_ZONE_NOField.Index = 12;
			fields.Add(T_ZONE_NOField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 13;
			fields.Add(DOCKField);
			 
			DataSchemaField STATUS_POINT_CODEField = new DataSchemaField();
			STATUS_POINT_CODEField.Name = "STATUS_POINT_CODE";
			STATUS_POINT_CODEField.Type = typeof(string).ToString();
			STATUS_POINT_CODEField.Index = 14;
			fields.Add(STATUS_POINT_CODEField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 15;
			fields.Add(STATUSField);
			 
			DataSchemaField REQUIREMENT_ACCUMULATE_TIMEField = new DataSchemaField();
			REQUIREMENT_ACCUMULATE_TIMEField.Name = "REQUIREMENT_ACCUMULATE_TIME";
			REQUIREMENT_ACCUMULATE_TIMEField.Type = typeof(int).ToString();
			REQUIREMENT_ACCUMULATE_TIMEField.Index = 16;
			fields.Add(REQUIREMENT_ACCUMULATE_TIMEField);
			 
			DataSchemaField LOAD_TIMEField = new DataSchemaField();
			LOAD_TIMEField.Name = "LOAD_TIME";
			LOAD_TIMEField.Type = typeof(int).ToString();
			LOAD_TIMEField.Index = 17;
			fields.Add(LOAD_TIMEField);
			 
			DataSchemaField TRANSPORT_TIMEField = new DataSchemaField();
			TRANSPORT_TIMEField.Name = "TRANSPORT_TIME";
			TRANSPORT_TIMEField.Type = typeof(int).ToString();
			TRANSPORT_TIMEField.Index = 18;
			fields.Add(TRANSPORT_TIMEField);
			 
			DataSchemaField UNLOAD_TIMEField = new DataSchemaField();
			UNLOAD_TIMEField.Name = "UNLOAD_TIME";
			UNLOAD_TIMEField.Type = typeof(int).ToString();
			UNLOAD_TIMEField.Index = 19;
			fields.Add(UNLOAD_TIMEField);
			 
			DataSchemaField DELAY_TIMEField = new DataSchemaField();
			DELAY_TIMEField.Name = "DELAY_TIME";
			DELAY_TIMEField.Type = typeof(int).ToString();
			DELAY_TIMEField.Index = 20;
			fields.Add(DELAY_TIMEField);
			 
			DataSchemaField ONLINE_TIMEField = new DataSchemaField();
			ONLINE_TIMEField.Name = "ONLINE_TIME";
			ONLINE_TIMEField.Type = typeof(int).ToString();
			ONLINE_TIMEField.Index = 21;
			fields.Add(ONLINE_TIMEField);
			 
			DataSchemaField ROUTE_CODEField = new DataSchemaField();
			ROUTE_CODEField.Name = "ROUTE_CODE";
			ROUTE_CODEField.Type = typeof(string).ToString();
			ROUTE_CODEField.Index = 22;
			fields.Add(ROUTE_CODEField);
			 
			DataSchemaField REQUIREMENT_ACCUMULATE_MODEField = new DataSchemaField();
			REQUIREMENT_ACCUMULATE_MODEField.Name = "REQUIREMENT_ACCUMULATE_MODE";
			REQUIREMENT_ACCUMULATE_MODEField.Type = typeof(int).ToString();
			REQUIREMENT_ACCUMULATE_MODEField.Index = 23;
			fields.Add(REQUIREMENT_ACCUMULATE_MODEField);
			 
			DataSchemaField ROUNDNESS_MODEField = new DataSchemaField();
			ROUNDNESS_MODEField.Name = "ROUNDNESS_MODE";
			ROUNDNESS_MODEField.Type = typeof(int).ToString();
			ROUNDNESS_MODEField.Index = 24;
			fields.Add(ROUNDNESS_MODEField);
			 
			DataSchemaField TWD_PULL_MODEField = new DataSchemaField();
			TWD_PULL_MODEField.Name = "TWD_PULL_MODE";
			TWD_PULL_MODEField.Type = typeof(int).ToString();
			TWD_PULL_MODEField.Index = 25;
			fields.Add(TWD_PULL_MODEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 26;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 27;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 28;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 29;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 30;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 31;
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
		public string PartBoxCode{ get;set; }		
				
		[DataMember]
		public string PartBoxName{ get;set; }		
				
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
		public string SWmNo{ get;set; }		
				
		[DataMember]
		public string SZoneNo{ get;set; }		
				
		[DataMember]
		public string TWmNo{ get;set; }		
				
		[DataMember]
		public string TZoneNo{ get;set; }		
				
		[DataMember]
		public string Dock{ get;set; }		
				
		[DataMember]
		public string StatusPointCode{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		[DataMember]
		public int? RequirementAccumulateTime{ get;set; }		
				
		[DataMember]
		public int? LoadTime{ get;set; }		
				
		[DataMember]
		public int? TransportTime{ get;set; }		
				
		[DataMember]
		public int? UnloadTime{ get;set; }		
				
		[DataMember]
		public int? DelayTime{ get;set; }		
				
		[DataMember]
		public int? OnlineTime{ get;set; }		
				
		[DataMember]
		public string RouteCode{ get;set; }		
				
		[DataMember]
		public int? RequirementAccumulateMode{ get;set; }		
				
		[DataMember]
		public int? RoundnessMode{ get;set; }		
				
		[DataMember]
		public int? TwdPullMode{ get;set; }		
				
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
			TwdPartBoxInfo info = new TwdPartBoxInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.PartBoxCode = this.PartBoxCode;
			info.PartBoxName = this.PartBoxName;
			info.Plant = this.Plant;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.AssemblyLine = this.AssemblyLine;
			info.SupplierNum = this.SupplierNum;
			info.SWmNo = this.SWmNo;
			info.SZoneNo = this.SZoneNo;
			info.TWmNo = this.TWmNo;
			info.TZoneNo = this.TZoneNo;
			info.Dock = this.Dock;
			info.StatusPointCode = this.StatusPointCode;
			info.Status = this.Status;
			info.RequirementAccumulateTime = this.RequirementAccumulateTime;
			info.LoadTime = this.LoadTime;
			info.TransportTime = this.TransportTime;
			info.UnloadTime = this.UnloadTime;
			info.DelayTime = this.DelayTime;
			info.OnlineTime = this.OnlineTime;
			info.RouteCode = this.RouteCode;
			info.RequirementAccumulateMode = this.RequirementAccumulateMode;
			info.RoundnessMode = this.RoundnessMode;
			info.TwdPullMode = this.TwdPullMode;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			return info;			
		}
		 
		public TwdPartBoxInfo Clone()
		{
			return ((ICloneable) this).Clone() as TwdPartBoxInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// TwdPartBoxInfoCollection对应表[TM_MPM_TWD_PART_BOX]
    /// </summary>
	public partial class TwdPartBoxInfoCollection : BusinessObjectCollection<TwdPartBoxInfo>
	{
		public TwdPartBoxInfoCollection():base("TM_MPM_TWD_PART_BOX"){}	
	}
}
