#region Declaim
//---------------------------------------------------------------------------
// Name:		RepackageHeadInfo
// Function: 	Expose data in table RepackageHead from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年12月21日
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
    /// RepackageHeadInfo对应表[TT_WMM_REPACKAGE_HEAD]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class RepackageHeadInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public RepackageHeadInfo( 
					int aRepackageId,

					string aRepackageNo,

					string aPlant,

					string aWmNo,

					string aZoneNo,

					DateTime aRepackageTime,

					DateTime aRepackageBtime,

					DateTime aRepackageEtime,

					DateTime aRepackagePickupTime,

					string aRepackageRoute,

					string aApplyKeeper,

					string aBookKeeper,

					string aPublishKeeper,

					DateTime aPublishTime,

					int aCountStatus,

					int aRepackageCount,

					int aEmergencyType,

					string aCountReason,

					string aAssemblyLine,

					string aPlantZone,

					string aWorkshop,

					string aComments,

					string aCreateUser,

					DateTime aCreateDate,

					string aUpdateUser,

					DateTime aUpdateDate,

					DateTime aRepackagePickupEtime,

					string aRepackageType

				 
		) : this()
		{
			 
			RepackageId = aRepackageId;
		 
			RepackageNo = aRepackageNo;
		 
			Plant = aPlant;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			RepackageTime = aRepackageTime;
		 
			RepackageBtime = aRepackageBtime;
		 
			RepackageEtime = aRepackageEtime;
		 
			RepackagePickupTime = aRepackagePickupTime;
		 
			RepackageRoute = aRepackageRoute;
		 
			ApplyKeeper = aApplyKeeper;
		 
			BookKeeper = aBookKeeper;
		 
			PublishKeeper = aPublishKeeper;
		 
			PublishTime = aPublishTime;
		 
			CountStatus = aCountStatus;
		 
			RepackageCount = aRepackageCount;
		 
			EmergencyType = aEmergencyType;
		 
			CountReason = aCountReason;
		 
			AssemblyLine = aAssemblyLine;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			Comments = aComments;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			UpdateUser = aUpdateUser;
		 
			UpdateDate = aUpdateDate;
		 
			RepackagePickupEtime = aRepackagePickupEtime;
		 
			RepackageType = aRepackageType;
		}
		
		public RepackageHeadInfo():base("TT_WMM_REPACKAGE_HEAD")
		{
			List<string> keys = new List<string>();
			 			keys.Add("REPACKAGE_ID");                           _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField REPACKAGE_IDField = new DataSchemaField();
			REPACKAGE_IDField.Name = "REPACKAGE_ID";
			REPACKAGE_IDField.Type = typeof(int).ToString();
			REPACKAGE_IDField.Index = 0;
			fields.Add(REPACKAGE_IDField);
			 
			DataSchemaField REPACKAGE_NOField = new DataSchemaField();
			REPACKAGE_NOField.Name = "REPACKAGE_NO";
			REPACKAGE_NOField.Type = typeof(string).ToString();
			REPACKAGE_NOField.Index = 1;
			fields.Add(REPACKAGE_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 2;
			fields.Add(PLANTField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 3;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 4;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField REPACKAGE_TIMEField = new DataSchemaField();
			REPACKAGE_TIMEField.Name = "REPACKAGE_TIME";
			REPACKAGE_TIMEField.Type = typeof(DateTime).ToString();
			REPACKAGE_TIMEField.Index = 5;
			fields.Add(REPACKAGE_TIMEField);
			 
			DataSchemaField REPACKAGE_BTIMEField = new DataSchemaField();
			REPACKAGE_BTIMEField.Name = "REPACKAGE_BTIME";
			REPACKAGE_BTIMEField.Type = typeof(DateTime).ToString();
			REPACKAGE_BTIMEField.Index = 6;
			fields.Add(REPACKAGE_BTIMEField);
			 
			DataSchemaField REPACKAGE_ETIMEField = new DataSchemaField();
			REPACKAGE_ETIMEField.Name = "REPACKAGE_ETIME";
			REPACKAGE_ETIMEField.Type = typeof(DateTime).ToString();
			REPACKAGE_ETIMEField.Index = 7;
			fields.Add(REPACKAGE_ETIMEField);
			 
			DataSchemaField REPACKAGE_PICKUP_TIMEField = new DataSchemaField();
			REPACKAGE_PICKUP_TIMEField.Name = "REPACKAGE_PICKUP_TIME";
			REPACKAGE_PICKUP_TIMEField.Type = typeof(DateTime).ToString();
			REPACKAGE_PICKUP_TIMEField.Index = 8;
			fields.Add(REPACKAGE_PICKUP_TIMEField);
			 
			DataSchemaField REPACKAGE_ROUTEField = new DataSchemaField();
			REPACKAGE_ROUTEField.Name = "REPACKAGE_ROUTE";
			REPACKAGE_ROUTEField.Type = typeof(string).ToString();
			REPACKAGE_ROUTEField.Index = 9;
			fields.Add(REPACKAGE_ROUTEField);
			 
			DataSchemaField APPLY_KEEPERField = new DataSchemaField();
			APPLY_KEEPERField.Name = "APPLY_KEEPER";
			APPLY_KEEPERField.Type = typeof(string).ToString();
			APPLY_KEEPERField.Index = 10;
			fields.Add(APPLY_KEEPERField);
			 
			DataSchemaField BOOK_KEEPERField = new DataSchemaField();
			BOOK_KEEPERField.Name = "BOOK_KEEPER";
			BOOK_KEEPERField.Type = typeof(string).ToString();
			BOOK_KEEPERField.Index = 11;
			fields.Add(BOOK_KEEPERField);
			 
			DataSchemaField PUBLISH_KEEPERField = new DataSchemaField();
			PUBLISH_KEEPERField.Name = "PUBLISH_KEEPER";
			PUBLISH_KEEPERField.Type = typeof(string).ToString();
			PUBLISH_KEEPERField.Index = 12;
			fields.Add(PUBLISH_KEEPERField);
			 
			DataSchemaField PUBLISH_TIMEField = new DataSchemaField();
			PUBLISH_TIMEField.Name = "PUBLISH_TIME";
			PUBLISH_TIMEField.Type = typeof(DateTime).ToString();
			PUBLISH_TIMEField.Index = 13;
			fields.Add(PUBLISH_TIMEField);
			 
			DataSchemaField COUNT_STATUSField = new DataSchemaField();
			COUNT_STATUSField.Name = "COUNT_STATUS";
			COUNT_STATUSField.Type = typeof(int).ToString();
			COUNT_STATUSField.Index = 14;
			fields.Add(COUNT_STATUSField);
			 
			DataSchemaField REPACKAGE_COUNTField = new DataSchemaField();
			REPACKAGE_COUNTField.Name = "REPACKAGE_COUNT";
			REPACKAGE_COUNTField.Type = typeof(int).ToString();
			REPACKAGE_COUNTField.Index = 15;
			fields.Add(REPACKAGE_COUNTField);
			 
			DataSchemaField EMERGENCY_TYPEField = new DataSchemaField();
			EMERGENCY_TYPEField.Name = "EMERGENCY_TYPE";
			EMERGENCY_TYPEField.Type = typeof(int).ToString();
			EMERGENCY_TYPEField.Index = 16;
			fields.Add(EMERGENCY_TYPEField);
			 
			DataSchemaField COUNT_REASONField = new DataSchemaField();
			COUNT_REASONField.Name = "COUNT_REASON";
			COUNT_REASONField.Type = typeof(string).ToString();
			COUNT_REASONField.Index = 17;
			fields.Add(COUNT_REASONField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 18;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 19;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 20;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 21;
			fields.Add(COMMENTSField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 22;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 23;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 24;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 25;
			fields.Add(UPDATE_DATEField);
			 
			DataSchemaField REPACKAGE_PICKUP_ETIMEField = new DataSchemaField();
			REPACKAGE_PICKUP_ETIMEField.Name = "REPACKAGE_PICKUP_ETIME";
			REPACKAGE_PICKUP_ETIMEField.Type = typeof(DateTime).ToString();
			REPACKAGE_PICKUP_ETIMEField.Index = 26;
			fields.Add(REPACKAGE_PICKUP_ETIMEField);
			 
			DataSchemaField REPACKAGE_TYPEField = new DataSchemaField();
			REPACKAGE_TYPEField.Name = "REPACKAGE_TYPE";
			REPACKAGE_TYPEField.Type = typeof(string).ToString();
			REPACKAGE_TYPEField.Index = 27;
			fields.Add(REPACKAGE_TYPEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public int RepackageId{ get;set; }		
				
		[DataMember]
		public string RepackageNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public DateTime? RepackageTime{ get;set; }		
				
		[DataMember]
		public DateTime? RepackageBtime{ get;set; }		
				
		[DataMember]
		public DateTime? RepackageEtime{ get;set; }		
				
		[DataMember]
		public DateTime? RepackagePickupTime{ get;set; }		
				
		[DataMember]
		public string RepackageRoute{ get;set; }		
				
		[DataMember]
		public string ApplyKeeper{ get;set; }		
				
		[DataMember]
		public string BookKeeper{ get;set; }		
				
		[DataMember]
		public string PublishKeeper{ get;set; }		
				
		[DataMember]
		public DateTime? PublishTime{ get;set; }		
				
		[DataMember]
		public int? CountStatus{ get;set; }		
				
		[DataMember]
		public int? RepackageCount{ get;set; }		
				
		[DataMember]
		public int? EmergencyType{ get;set; }		
				
		[DataMember]
		public string CountReason{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		[DataMember]
		public DateTime? RepackagePickupEtime{ get;set; }		
				
				
		private string _RepackageType = "1";
		
		[DataMember]	
		public string RepackageType
		{
			get
			{
				return _RepackageType;
			}
			set
			{
				_RepackageType = value;
			}
		}
				
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			RepackageHeadInfo info = new RepackageHeadInfo();

			info.RepackageId = this.RepackageId;
			info.RepackageNo = this.RepackageNo;
			info.Plant = this.Plant;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.RepackageTime = this.RepackageTime;
			info.RepackageBtime = this.RepackageBtime;
			info.RepackageEtime = this.RepackageEtime;
			info.RepackagePickupTime = this.RepackagePickupTime;
			info.RepackageRoute = this.RepackageRoute;
			info.ApplyKeeper = this.ApplyKeeper;
			info.BookKeeper = this.BookKeeper;
			info.PublishKeeper = this.PublishKeeper;
			info.PublishTime = this.PublishTime;
			info.CountStatus = this.CountStatus;
			info.RepackageCount = this.RepackageCount;
			info.EmergencyType = this.EmergencyType;
			info.CountReason = this.CountReason;
			info.AssemblyLine = this.AssemblyLine;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.Comments = this.Comments;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.UpdateUser = this.UpdateUser;
			info.UpdateDate = this.UpdateDate;
			info.RepackagePickupEtime = this.RepackagePickupEtime;
			info.RepackageType = this.RepackageType;
			return info;			
		}
		 
		public RepackageHeadInfo Clone()
		{
			return ((ICloneable) this).Clone() as RepackageHeadInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// RepackageHeadInfoCollection对应表[TT_WMM_REPACKAGE_HEAD]
    /// </summary>
	public partial class RepackageHeadInfoCollection : BusinessObjectCollection<RepackageHeadInfo>
	{
		public RepackageHeadInfoCollection():base("TT_WMM_REPACKAGE_HEAD"){}	
	}
}
