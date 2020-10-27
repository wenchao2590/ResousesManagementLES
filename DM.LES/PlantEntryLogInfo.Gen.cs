#region Declaim
//---------------------------------------------------------------------------
// Name:		PlantEntryLogInfo
// Function: 	Expose data in table PlantEntryLog from database as business object to MES system.
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
    /// PlantEntryLogInfo对应表[TT_CMM_PLANT_ENTRY_LOG]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PlantEntryLogInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PlantEntryLogInfo( 
					long aId,

					string aVehicleNo,

					string aRunsheetNo,

					int aIsUrgentOrder,

					DateTime aRequireTime,

					int aArriveTime,

					int aExpireTime,

					DateTime aEntryTime,

					DateTime aDockReleaseTime,

					DateTime aDockHoldTime,

					DateTime aExitTime,

					int aWaitingTime,

					int aDockProcessingTime,

					string aDock,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					string aPhoneNo,

					int aStatus,

					int aIsJumpQueue,

					bool aValidFlag

				 
		) : this()
		{
			 
			Id = aId;
		 
			VehicleNo = aVehicleNo;
		 
			RunsheetNo = aRunsheetNo;
		 
			IsUrgentOrder = aIsUrgentOrder;
		 
			RequireTime = aRequireTime;
		 
			ArriveTime = aArriveTime;
		 
			ExpireTime = aExpireTime;
		 
			EntryTime = aEntryTime;
		 
			DockReleaseTime = aDockReleaseTime;
		 
			DockHoldTime = aDockHoldTime;
		 
			ExitTime = aExitTime;
		 
			WaitingTime = aWaitingTime;
		 
			DockProcessingTime = aDockProcessingTime;
		 
			Dock = aDock;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			PhoneNo = aPhoneNo;
		 
			Status = aStatus;
		 
			IsJumpQueue = aIsJumpQueue;
		 
			ValidFlag = aValidFlag;
		}
		
		public PlantEntryLogInfo():base("TT_CMM_PLANT_ENTRY_LOG")
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
			 
			DataSchemaField VEHICLE_NOField = new DataSchemaField();
			VEHICLE_NOField.Name = "VEHICLE_NO";
			VEHICLE_NOField.Type = typeof(string).ToString();
			VEHICLE_NOField.Index = 1;
			fields.Add(VEHICLE_NOField);
			 
			DataSchemaField RUNSHEET_NOField = new DataSchemaField();
			RUNSHEET_NOField.Name = "RUNSHEET_NO";
			RUNSHEET_NOField.Type = typeof(string).ToString();
			RUNSHEET_NOField.Index = 2;
			fields.Add(RUNSHEET_NOField);
			 
			DataSchemaField IS_URGENT_ORDERField = new DataSchemaField();
			IS_URGENT_ORDERField.Name = "IS_URGENT_ORDER";
			IS_URGENT_ORDERField.Type = typeof(int).ToString();
			IS_URGENT_ORDERField.Index = 3;
			fields.Add(IS_URGENT_ORDERField);
			 
			DataSchemaField REQUIRE_TIMEField = new DataSchemaField();
			REQUIRE_TIMEField.Name = "REQUIRE_TIME";
			REQUIRE_TIMEField.Type = typeof(DateTime).ToString();
			REQUIRE_TIMEField.Index = 4;
			fields.Add(REQUIRE_TIMEField);
			 
			DataSchemaField ARRIVE_TIMEField = new DataSchemaField();
			ARRIVE_TIMEField.Name = "ARRIVE_TIME";
			ARRIVE_TIMEField.Type = typeof(int).ToString();
			ARRIVE_TIMEField.Index = 5;
			fields.Add(ARRIVE_TIMEField);
			 
			DataSchemaField EXPIRE_TIMEField = new DataSchemaField();
			EXPIRE_TIMEField.Name = "EXPIRE_TIME";
			EXPIRE_TIMEField.Type = typeof(int).ToString();
			EXPIRE_TIMEField.Index = 6;
			fields.Add(EXPIRE_TIMEField);
			 
			DataSchemaField ENTRY_TIMEField = new DataSchemaField();
			ENTRY_TIMEField.Name = "ENTRY_TIME";
			ENTRY_TIMEField.Type = typeof(DateTime).ToString();
			ENTRY_TIMEField.Index = 7;
			fields.Add(ENTRY_TIMEField);
			 
			DataSchemaField DOCK_RELEASE_TIMEField = new DataSchemaField();
			DOCK_RELEASE_TIMEField.Name = "DOCK_RELEASE_TIME";
			DOCK_RELEASE_TIMEField.Type = typeof(DateTime).ToString();
			DOCK_RELEASE_TIMEField.Index = 8;
			fields.Add(DOCK_RELEASE_TIMEField);
			 
			DataSchemaField DOCK_HOLD_TIMEField = new DataSchemaField();
			DOCK_HOLD_TIMEField.Name = "DOCK_HOLD_TIME";
			DOCK_HOLD_TIMEField.Type = typeof(DateTime).ToString();
			DOCK_HOLD_TIMEField.Index = 9;
			fields.Add(DOCK_HOLD_TIMEField);
			 
			DataSchemaField EXIT_TIMEField = new DataSchemaField();
			EXIT_TIMEField.Name = "EXIT_TIME";
			EXIT_TIMEField.Type = typeof(DateTime).ToString();
			EXIT_TIMEField.Index = 10;
			fields.Add(EXIT_TIMEField);
			 
			DataSchemaField WAITING_TIMEField = new DataSchemaField();
			WAITING_TIMEField.Name = "WAITING_TIME";
			WAITING_TIMEField.Type = typeof(int).ToString();
			WAITING_TIMEField.Index = 11;
			fields.Add(WAITING_TIMEField);
			 
			DataSchemaField DOCK_PROCESSING_TIMEField = new DataSchemaField();
			DOCK_PROCESSING_TIMEField.Name = "DOCK_PROCESSING_TIME";
			DOCK_PROCESSING_TIMEField.Type = typeof(int).ToString();
			DOCK_PROCESSING_TIMEField.Index = 12;
			fields.Add(DOCK_PROCESSING_TIMEField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 13;
			fields.Add(DOCKField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 14;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 15;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 16;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 17;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField PHONE_NOField = new DataSchemaField();
			PHONE_NOField.Name = "PHONE_NO";
			PHONE_NOField.Type = typeof(string).ToString();
			PHONE_NOField.Index = 18;
			fields.Add(PHONE_NOField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 19;
			fields.Add(STATUSField);
			 
			DataSchemaField IS_JUMP_QUEUEField = new DataSchemaField();
			IS_JUMP_QUEUEField.Name = "IS_JUMP_QUEUE";
			IS_JUMP_QUEUEField.Type = typeof(int).ToString();
			IS_JUMP_QUEUEField.Index = 20;
			fields.Add(IS_JUMP_QUEUEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 21;
			fields.Add(VALID_FLAGField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string VehicleNo{ get;set; }		
				
		[DataMember]
		public string RunsheetNo{ get;set; }		
				
		[DataMember]
		public int? IsUrgentOrder{ get;set; }		
				
		[DataMember]
		public DateTime? RequireTime{ get;set; }		
				
		[DataMember]
		public int? ArriveTime{ get;set; }		
				
		[DataMember]
		public int? ExpireTime{ get;set; }		
				
		[DataMember]
		public DateTime? EntryTime{ get;set; }		
				
		[DataMember]
		public DateTime? DockReleaseTime{ get;set; }		
				
		[DataMember]
		public DateTime? DockHoldTime{ get;set; }		
				
		[DataMember]
		public DateTime? ExitTime{ get;set; }		
				
		[DataMember]
		public int? WaitingTime{ get;set; }		
				
		[DataMember]
		public int? DockProcessingTime{ get;set; }		
				
		[DataMember]
		public string Dock{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string PhoneNo{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		[DataMember]
		public int? IsJumpQueue{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PlantEntryLogInfo info = new PlantEntryLogInfo();

			info.Id = this.Id;
			info.VehicleNo = this.VehicleNo;
			info.RunsheetNo = this.RunsheetNo;
			info.IsUrgentOrder = this.IsUrgentOrder;
			info.RequireTime = this.RequireTime;
			info.ArriveTime = this.ArriveTime;
			info.ExpireTime = this.ExpireTime;
			info.EntryTime = this.EntryTime;
			info.DockReleaseTime = this.DockReleaseTime;
			info.DockHoldTime = this.DockHoldTime;
			info.ExitTime = this.ExitTime;
			info.WaitingTime = this.WaitingTime;
			info.DockProcessingTime = this.DockProcessingTime;
			info.Dock = this.Dock;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.PhoneNo = this.PhoneNo;
			info.Status = this.Status;
			info.IsJumpQueue = this.IsJumpQueue;
			info.ValidFlag = this.ValidFlag;
			return info;			
		}
		 
		public PlantEntryLogInfo Clone()
		{
			return ((ICloneable) this).Clone() as PlantEntryLogInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PlantEntryLogInfoCollection对应表[TT_CMM_PLANT_ENTRY_LOG]
    /// </summary>
	public partial class PlantEntryLogInfoCollection : BusinessObjectCollection<PlantEntryLogInfo>
	{
		public PlantEntryLogInfoCollection():base("TT_CMM_PLANT_ENTRY_LOG"){}	
	}
}
