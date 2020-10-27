#region Declaim
//---------------------------------------------------------------------------
// Name:		InhouseBreakpointPartInfo
// Function: 	Expose data in table InhouseBreakpointPart from database as business object to MES system.
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
    /// InhouseBreakpointPartInfo对应表[TM_BAS_INHOUSE_BREAKPOINT_PART]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class InhouseBreakpointPartInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public InhouseBreakpointPartInfo( 
					string aInhouseBreakpointNo,

					string aPlant,

					string aAssemblyLine,

					string aPlantZone,

					string aWorkshop,

					string aPartNo,

					string aPartCname,

					string aKnr,

					string aRunningNo,

					int aInhouseIdentity,

					string aVin,

					string aInPlantSystemMode,

					string aInhouseSystemMode,

					string aModel,

					string aModelNo,

					int aStatus,

					int aBreakpointStatus,

					int aBreakpointType,

					int aRemainCount,

					int aEnforeSave,

					int aActualRemainCount,

					int aDifferentCount,

					int aModifyRemainCount,

					DateTime aBreakTime,

					string aInhouseidString,

					string aEwo,

					string aNewPartNo,

					string aNewPartCname,

					string aNewPartNickName,

					string aComments,

					string aUpdateUser,

					DateTime aCreateDate,

					string aCreateUser,

					DateTime aUpdateDate

				 
		) : this()
		{
			 
			InhouseBreakpointNo = aInhouseBreakpointNo;
		 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			Knr = aKnr;
		 
			RunningNo = aRunningNo;
		 
			InhouseIdentity = aInhouseIdentity;
		 
			Vin = aVin;
		 
			InPlantSystemMode = aInPlantSystemMode;
		 
			InhouseSystemMode = aInhouseSystemMode;
		 
			Model = aModel;
		 
			ModelNo = aModelNo;
		 
			Status = aStatus;
		 
			BreakpointStatus = aBreakpointStatus;
		 
			BreakpointType = aBreakpointType;
		 
			RemainCount = aRemainCount;
		 
			EnforeSave = aEnforeSave;
		 
			ActualRemainCount = aActualRemainCount;
		 
			DifferentCount = aDifferentCount;
		 
			ModifyRemainCount = aModifyRemainCount;
		 
			BreakTime = aBreakTime;
		 
			InhouseidString = aInhouseidString;
		 
			Ewo = aEwo;
		 
			NewPartNo = aNewPartNo;
		 
			NewPartCname = aNewPartCname;
		 
			NewPartNickName = aNewPartNickName;
		 
			Comments = aComments;
		 
			UpdateUser = aUpdateUser;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			UpdateDate = aUpdateDate;
		}
		
		public InhouseBreakpointPartInfo():base("TM_BAS_INHOUSE_BREAKPOINT_PART")
		{
			List<string> keys = new List<string>();
			 			keys.Add("INHOUSE_BREAKPOINT_NO");                                 _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField INHOUSE_BREAKPOINT_NOField = new DataSchemaField();
			INHOUSE_BREAKPOINT_NOField.Name = "INHOUSE_BREAKPOINT_NO";
			INHOUSE_BREAKPOINT_NOField.Type = typeof(string).ToString();
			INHOUSE_BREAKPOINT_NOField.Index = 0;
			fields.Add(INHOUSE_BREAKPOINT_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 1;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 2;
			fields.Add(ASSEMBLY_LINEField);
			 
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
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 5;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_CNAMEField = new DataSchemaField();
			PART_CNAMEField.Name = "PART_CNAME";
			PART_CNAMEField.Type = typeof(string).ToString();
			PART_CNAMEField.Index = 6;
			fields.Add(PART_CNAMEField);
			 
			DataSchemaField KNRField = new DataSchemaField();
			KNRField.Name = "KNR";
			KNRField.Type = typeof(string).ToString();
			KNRField.Index = 7;
			fields.Add(KNRField);
			 
			DataSchemaField RUNNING_NOField = new DataSchemaField();
			RUNNING_NOField.Name = "RUNNING_NO";
			RUNNING_NOField.Type = typeof(string).ToString();
			RUNNING_NOField.Index = 8;
			fields.Add(RUNNING_NOField);
			 
			DataSchemaField INHOUSE_IDENTITYField = new DataSchemaField();
			INHOUSE_IDENTITYField.Name = "INHOUSE_IDENTITY";
			INHOUSE_IDENTITYField.Type = typeof(int).ToString();
			INHOUSE_IDENTITYField.Index = 9;
			fields.Add(INHOUSE_IDENTITYField);
			 
			DataSchemaField VINField = new DataSchemaField();
			VINField.Name = "VIN";
			VINField.Type = typeof(string).ToString();
			VINField.Index = 10;
			fields.Add(VINField);
			 
			DataSchemaField IN_PLANT_SYSTEM_MODEField = new DataSchemaField();
			IN_PLANT_SYSTEM_MODEField.Name = "IN_PLANT_SYSTEM_MODE";
			IN_PLANT_SYSTEM_MODEField.Type = typeof(string).ToString();
			IN_PLANT_SYSTEM_MODEField.Index = 11;
			fields.Add(IN_PLANT_SYSTEM_MODEField);
			 
			DataSchemaField INHOUSE_SYSTEM_MODEField = new DataSchemaField();
			INHOUSE_SYSTEM_MODEField.Name = "INHOUSE_SYSTEM_MODE";
			INHOUSE_SYSTEM_MODEField.Type = typeof(string).ToString();
			INHOUSE_SYSTEM_MODEField.Index = 12;
			fields.Add(INHOUSE_SYSTEM_MODEField);
			 
			DataSchemaField MODELField = new DataSchemaField();
			MODELField.Name = "MODEL";
			MODELField.Type = typeof(string).ToString();
			MODELField.Index = 13;
			fields.Add(MODELField);
			 
			DataSchemaField MODEL_NOField = new DataSchemaField();
			MODEL_NOField.Name = "MODEL_NO";
			MODEL_NOField.Type = typeof(string).ToString();
			MODEL_NOField.Index = 14;
			fields.Add(MODEL_NOField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 15;
			fields.Add(STATUSField);
			 
			DataSchemaField BREAKPOINT_STATUSField = new DataSchemaField();
			BREAKPOINT_STATUSField.Name = "BREAKPOINT_STATUS";
			BREAKPOINT_STATUSField.Type = typeof(int).ToString();
			BREAKPOINT_STATUSField.Index = 16;
			fields.Add(BREAKPOINT_STATUSField);
			 
			DataSchemaField BREAKPOINT_TYPEField = new DataSchemaField();
			BREAKPOINT_TYPEField.Name = "BREAKPOINT_TYPE";
			BREAKPOINT_TYPEField.Type = typeof(int).ToString();
			BREAKPOINT_TYPEField.Index = 17;
			fields.Add(BREAKPOINT_TYPEField);
			 
			DataSchemaField REMAIN_COUNTField = new DataSchemaField();
			REMAIN_COUNTField.Name = "REMAIN_COUNT";
			REMAIN_COUNTField.Type = typeof(int).ToString();
			REMAIN_COUNTField.Index = 18;
			fields.Add(REMAIN_COUNTField);
			 
			DataSchemaField ENFORE_SAVEField = new DataSchemaField();
			ENFORE_SAVEField.Name = "ENFORE_SAVE";
			ENFORE_SAVEField.Type = typeof(int).ToString();
			ENFORE_SAVEField.Index = 19;
			fields.Add(ENFORE_SAVEField);
			 
			DataSchemaField ACTUAL_REMAIN_COUNTField = new DataSchemaField();
			ACTUAL_REMAIN_COUNTField.Name = "ACTUAL_REMAIN_COUNT";
			ACTUAL_REMAIN_COUNTField.Type = typeof(int).ToString();
			ACTUAL_REMAIN_COUNTField.Index = 20;
			fields.Add(ACTUAL_REMAIN_COUNTField);
			 
			DataSchemaField DIFFERENT_COUNTField = new DataSchemaField();
			DIFFERENT_COUNTField.Name = "DIFFERENT_COUNT";
			DIFFERENT_COUNTField.Type = typeof(int).ToString();
			DIFFERENT_COUNTField.Index = 21;
			fields.Add(DIFFERENT_COUNTField);
			 
			DataSchemaField MODIFY_REMAIN_COUNTField = new DataSchemaField();
			MODIFY_REMAIN_COUNTField.Name = "MODIFY_REMAIN_COUNT";
			MODIFY_REMAIN_COUNTField.Type = typeof(int).ToString();
			MODIFY_REMAIN_COUNTField.Index = 22;
			fields.Add(MODIFY_REMAIN_COUNTField);
			 
			DataSchemaField BREAK_TIMEField = new DataSchemaField();
			BREAK_TIMEField.Name = "BREAK_TIME";
			BREAK_TIMEField.Type = typeof(DateTime).ToString();
			BREAK_TIMEField.Index = 23;
			fields.Add(BREAK_TIMEField);
			 
			DataSchemaField INHOUSEID_STRINGField = new DataSchemaField();
			INHOUSEID_STRINGField.Name = "INHOUSEID_STRING";
			INHOUSEID_STRINGField.Type = typeof(string).ToString();
			INHOUSEID_STRINGField.Index = 24;
			fields.Add(INHOUSEID_STRINGField);
			 
			DataSchemaField EWOField = new DataSchemaField();
			EWOField.Name = "EWO";
			EWOField.Type = typeof(string).ToString();
			EWOField.Index = 25;
			fields.Add(EWOField);
			 
			DataSchemaField NEW_PART_NOField = new DataSchemaField();
			NEW_PART_NOField.Name = "NEW_PART_NO";
			NEW_PART_NOField.Type = typeof(string).ToString();
			NEW_PART_NOField.Index = 26;
			fields.Add(NEW_PART_NOField);
			 
			DataSchemaField NEW_PART_CNAMEField = new DataSchemaField();
			NEW_PART_CNAMEField.Name = "NEW_PART_CNAME";
			NEW_PART_CNAMEField.Type = typeof(string).ToString();
			NEW_PART_CNAMEField.Index = 27;
			fields.Add(NEW_PART_CNAMEField);
			 
			DataSchemaField NEW_PART_NICK_NAMEField = new DataSchemaField();
			NEW_PART_NICK_NAMEField.Name = "NEW_PART_NICK_NAME";
			NEW_PART_NICK_NAMEField.Type = typeof(string).ToString();
			NEW_PART_NICK_NAMEField.Index = 28;
			fields.Add(NEW_PART_NICK_NAMEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 29;
			fields.Add(COMMENTSField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 30;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 31;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 32;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 33;
			fields.Add(UPDATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string InhouseBreakpointNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public string Knr{ get;set; }		
				
		[DataMember]
		public string RunningNo{ get;set; }		
				
		[DataMember]
		public int? InhouseIdentity{ get;set; }		
				
		[DataMember]
		public string Vin{ get;set; }		
				
		[DataMember]
		public string InPlantSystemMode{ get;set; }		
				
		[DataMember]
		public string InhouseSystemMode{ get;set; }		
				
		[DataMember]
		public string Model{ get;set; }		
				
		[DataMember]
		public string ModelNo{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		[DataMember]
		public int? BreakpointStatus{ get;set; }		
				
		[DataMember]
		public int? BreakpointType{ get;set; }		
				
		[DataMember]
		public int? RemainCount{ get;set; }		
				
		[DataMember]
		public int? EnforeSave{ get;set; }		
				
		[DataMember]
		public int? ActualRemainCount{ get;set; }		
				
		[DataMember]
		public int? DifferentCount{ get;set; }		
				
		[DataMember]
		public int? ModifyRemainCount{ get;set; }		
				
		[DataMember]
		public DateTime? BreakTime{ get;set; }		
				
		[DataMember]
		public string InhouseidString{ get;set; }		
				
		[DataMember]
		public string Ewo{ get;set; }		
				
		[DataMember]
		public string NewPartNo{ get;set; }		
				
		[DataMember]
		public string NewPartCname{ get;set; }		
				
		[DataMember]
		public string NewPartNickName{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			InhouseBreakpointPartInfo info = new InhouseBreakpointPartInfo();

			info.InhouseBreakpointNo = this.InhouseBreakpointNo;
			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.Knr = this.Knr;
			info.RunningNo = this.RunningNo;
			info.InhouseIdentity = this.InhouseIdentity;
			info.Vin = this.Vin;
			info.InPlantSystemMode = this.InPlantSystemMode;
			info.InhouseSystemMode = this.InhouseSystemMode;
			info.Model = this.Model;
			info.ModelNo = this.ModelNo;
			info.Status = this.Status;
			info.BreakpointStatus = this.BreakpointStatus;
			info.BreakpointType = this.BreakpointType;
			info.RemainCount = this.RemainCount;
			info.EnforeSave = this.EnforeSave;
			info.ActualRemainCount = this.ActualRemainCount;
			info.DifferentCount = this.DifferentCount;
			info.ModifyRemainCount = this.ModifyRemainCount;
			info.BreakTime = this.BreakTime;
			info.InhouseidString = this.InhouseidString;
			info.Ewo = this.Ewo;
			info.NewPartNo = this.NewPartNo;
			info.NewPartCname = this.NewPartCname;
			info.NewPartNickName = this.NewPartNickName;
			info.Comments = this.Comments;
			info.UpdateUser = this.UpdateUser;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.UpdateDate = this.UpdateDate;
			return info;			
		}
		 
		public InhouseBreakpointPartInfo Clone()
		{
			return ((ICloneable) this).Clone() as InhouseBreakpointPartInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// InhouseBreakpointPartInfoCollection对应表[TM_BAS_INHOUSE_BREAKPOINT_PART]
    /// </summary>
	public partial class InhouseBreakpointPartInfoCollection : BusinessObjectCollection<InhouseBreakpointPartInfo>
	{
		public InhouseBreakpointPartInfoCollection():base("TM_BAS_INHOUSE_BREAKPOINT_PART"){}	
	}
}