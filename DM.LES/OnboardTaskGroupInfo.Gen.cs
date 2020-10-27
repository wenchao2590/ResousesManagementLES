#region Declaim
//---------------------------------------------------------------------------
// Name:		OnboardTaskGroupInfo
// Function: 	Expose data in table OnboardTaskGroup from database as business object to MES system.
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
    /// OnboardTaskGroupInfo对应表[TM_BAS_ONBOARD_TASK_GROUP]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class OnboardTaskGroupInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public OnboardTaskGroupInfo( 
					string aGroupCode,

					string aGroupName,

					int aTaskType,

					string aPlant,

					string aWmNo,

					string aZoneNo,

					string aComments,

					int aStatus,

					int aIsGiveup,

					string aRoute,

					int aDelayTime,

					DateTime aCreateDate,

					long aId,

					bool aValidFlag,

					string aModifyUser,

					string aCreateUser,

					DateTime aModifyDate

				 
		) : this()
		{
			 
			GroupCode = aGroupCode;
		 
			GroupName = aGroupName;
		 
			TaskType = aTaskType;
		 
			Plant = aPlant;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			Comments = aComments;
		 
			Status = aStatus;
		 
			IsGiveup = aIsGiveup;
		 
			Route = aRoute;
		 
			DelayTime = aDelayTime;
		 
			CreateDate = aCreateDate;
		 
			Id = aId;
		 
			ValidFlag = aValidFlag;
		 
			ModifyUser = aModifyUser;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public OnboardTaskGroupInfo():base("TM_BAS_ONBOARD_TASK_GROUP")
		{
			List<string> keys = new List<string>();
			             			keys.Add("ID");    _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField GROUP_CODEField = new DataSchemaField();
			GROUP_CODEField.Name = "GROUP_CODE";
			GROUP_CODEField.Type = typeof(string).ToString();
			GROUP_CODEField.Index = 0;
			fields.Add(GROUP_CODEField);
			 
			DataSchemaField GROUP_NAMEField = new DataSchemaField();
			GROUP_NAMEField.Name = "GROUP_NAME";
			GROUP_NAMEField.Type = typeof(string).ToString();
			GROUP_NAMEField.Index = 1;
			fields.Add(GROUP_NAMEField);
			 
			DataSchemaField TASK_TYPEField = new DataSchemaField();
			TASK_TYPEField.Name = "TASK_TYPE";
			TASK_TYPEField.Type = typeof(int).ToString();
			TASK_TYPEField.Index = 2;
			fields.Add(TASK_TYPEField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 3;
			fields.Add(PLANTField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 4;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 5;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 6;
			fields.Add(COMMENTSField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 7;
			fields.Add(STATUSField);
			 
			DataSchemaField IS_GIVEUPField = new DataSchemaField();
			IS_GIVEUPField.Name = "IS_GIVEUP";
			IS_GIVEUPField.Type = typeof(int).ToString();
			IS_GIVEUPField.Index = 8;
			fields.Add(IS_GIVEUPField);
			 
			DataSchemaField ROUTEField = new DataSchemaField();
			ROUTEField.Name = "ROUTE";
			ROUTEField.Type = typeof(string).ToString();
			ROUTEField.Index = 9;
			fields.Add(ROUTEField);
			 
			DataSchemaField DELAY_TIMEField = new DataSchemaField();
			DELAY_TIMEField.Name = "DELAY_TIME";
			DELAY_TIMEField.Type = typeof(int).ToString();
			DELAY_TIMEField.Index = 10;
			fields.Add(DELAY_TIMEField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 11;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 12;
			fields.Add(IDField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 13;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 14;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 15;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 16;
			fields.Add(MODIFY_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string GroupCode{ get;set; }		
				
		[DataMember]
		public string GroupName{ get;set; }		
				
		[DataMember]
		public int? TaskType{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		[DataMember]
		public int? IsGiveup{ get;set; }		
				
		[DataMember]
		public string Route{ get;set; }		
				
		[DataMember]
		public int? DelayTime{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			OnboardTaskGroupInfo info = new OnboardTaskGroupInfo();

			info.GroupCode = this.GroupCode;
			info.GroupName = this.GroupName;
			info.TaskType = this.TaskType;
			info.Plant = this.Plant;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.Comments = this.Comments;
			info.Status = this.Status;
			info.IsGiveup = this.IsGiveup;
			info.Route = this.Route;
			info.DelayTime = this.DelayTime;
			info.CreateDate = this.CreateDate;
			info.Id = this.Id;
			info.ValidFlag = this.ValidFlag;
			info.ModifyUser = this.ModifyUser;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public OnboardTaskGroupInfo Clone()
		{
			return ((ICloneable) this).Clone() as OnboardTaskGroupInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// OnboardTaskGroupInfoCollection对应表[TM_BAS_ONBOARD_TASK_GROUP]
    /// </summary>
	public partial class OnboardTaskGroupInfoCollection : BusinessObjectCollection<OnboardTaskGroupInfo>
	{
		public OnboardTaskGroupInfoCollection():base("TM_BAS_ONBOARD_TASK_GROUP"){}	
	}
}