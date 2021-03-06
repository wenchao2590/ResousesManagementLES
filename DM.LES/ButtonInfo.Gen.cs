#region Declaim
//---------------------------------------------------------------------------
// Name:		ButtonInfo
// Function: 	Expose data in table Button from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年12月22日
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
    /// ButtonInfo对应表[TM_EPS_BUTTON]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class ButtonInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public ButtonInfo( 
					string aPlant,

					string aAssemblyLine,

					string aPlantZone,

					string aWorkshop,

					string aButtonId,

					int aButtonType,

					int aButtonState,

					int aUnlockTime,

					DateTime aLockTime,

					string aComments,

					DateTime aCreateDate,

					string aUpdateUser,

					string aCreateUser,

					DateTime aUpdateDate

				 
		) : this()
		{
			 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			ButtonId = aButtonId;
		 
			ButtonType = aButtonType;
		 
			ButtonState = aButtonState;
		 
			UnlockTime = aUnlockTime;
		 
			LockTime = aLockTime;
		 
			Comments = aComments;
		 
			CreateDate = aCreateDate;
		 
			UpdateUser = aUpdateUser;
		 
			CreateUser = aCreateUser;
		 
			UpdateDate = aUpdateDate;
		}
		
		public ButtonInfo():base("TM_EPS_BUTTON")
		{
			List<string> keys = new List<string>();
			 			keys.Add("PLANT"); 			keys.Add("ASSEMBLY_LINE");   			keys.Add("BUTTON_ID");         _Keys = keys.ToArray();
			
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
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 2;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 3;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField BUTTON_IDField = new DataSchemaField();
			BUTTON_IDField.Name = "BUTTON_ID";
			BUTTON_IDField.Type = typeof(string).ToString();
			BUTTON_IDField.Index = 4;
			fields.Add(BUTTON_IDField);
			 
			DataSchemaField BUTTON_TYPEField = new DataSchemaField();
			BUTTON_TYPEField.Name = "BUTTON_TYPE";
			BUTTON_TYPEField.Type = typeof(int).ToString();
			BUTTON_TYPEField.Index = 5;
			fields.Add(BUTTON_TYPEField);
			 
			DataSchemaField BUTTON_STATEField = new DataSchemaField();
			BUTTON_STATEField.Name = "BUTTON_STATE";
			BUTTON_STATEField.Type = typeof(int).ToString();
			BUTTON_STATEField.Index = 6;
			fields.Add(BUTTON_STATEField);
			 
			DataSchemaField UNLOCK_TIMEField = new DataSchemaField();
			UNLOCK_TIMEField.Name = "UNLOCK_TIME";
			UNLOCK_TIMEField.Type = typeof(int).ToString();
			UNLOCK_TIMEField.Index = 7;
			fields.Add(UNLOCK_TIMEField);
			 
			DataSchemaField LOCK_TIMEField = new DataSchemaField();
			LOCK_TIMEField.Name = "LOCK_TIME";
			LOCK_TIMEField.Type = typeof(DateTime).ToString();
			LOCK_TIMEField.Index = 8;
			fields.Add(LOCK_TIMEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 9;
			fields.Add(COMMENTSField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 10;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 11;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 12;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 13;
			fields.Add(UPDATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string ButtonId{ get;set; }		
				
		[DataMember]
		public int? ButtonType{ get;set; }		
				
		[DataMember]
		public int? ButtonState{ get;set; }		
				
		[DataMember]
		public int? UnlockTime{ get;set; }		
				
		[DataMember]
		public DateTime? LockTime{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			ButtonInfo info = new ButtonInfo();

			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.ButtonId = this.ButtonId;
			info.ButtonType = this.ButtonType;
			info.ButtonState = this.ButtonState;
			info.UnlockTime = this.UnlockTime;
			info.LockTime = this.LockTime;
			info.Comments = this.Comments;
			info.CreateDate = this.CreateDate;
			info.UpdateUser = this.UpdateUser;
			info.CreateUser = this.CreateUser;
			info.UpdateDate = this.UpdateDate;
			return info;			
		}
		 
		public ButtonInfo Clone()
		{
			return ((ICloneable) this).Clone() as ButtonInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// ButtonInfoCollection对应表[TM_EPS_BUTTON]
    /// </summary>
	public partial class ButtonInfoCollection : BusinessObjectCollection<ButtonInfo>
	{
		public ButtonInfoCollection():base("TM_EPS_BUTTON"){}	
	}
}
