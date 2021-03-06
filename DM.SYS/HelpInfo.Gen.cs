#region Declaim
//---------------------------------------------------------------------------
// Name:		HelpInfo
// Function: 	Expose data in table TS_SYS_HELP from database as business object to MES system.
// Tool:		T4
// CreateDate:	2016年7月22日
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

namespace DM.SYS 
{   
	/// <summary>
    /// HelpInfo对应表[TS_SYS_HELP]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class HelpInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public HelpInfo(		 long aId,
				Guid aFid,
				Guid aMenuFid,
				string aHelpContextCn,
				string aHelpContextEn,
				bool aValidFlag,
				string aCreateUser,
				DateTime aCreateDate,
				string aModifyUser,
				DateTime aModifyDate				
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			MenuFid = aMenuFid;
		 
			HelpContextCn = aHelpContextCn;
		 
			HelpContextEn = aHelpContextEn;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public HelpInfo():base("TS_SYS_HELP")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");         _Keys = keys.ToArray();
			
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
			 
			DataSchemaField MENU_FIDField = new DataSchemaField();
			MENU_FIDField.Name = "MENU_FID";
			MENU_FIDField.Type = typeof(Guid).ToString();
			MENU_FIDField.Index = 2;
			fields.Add(MENU_FIDField);
			 
			DataSchemaField HELP_CONTEXT_CNField = new DataSchemaField();
			HELP_CONTEXT_CNField.Name = "HELP_CONTEXT_CN";
			HELP_CONTEXT_CNField.Type = typeof(string).ToString();
			HELP_CONTEXT_CNField.Index = 3;
			fields.Add(HELP_CONTEXT_CNField);
			 
			DataSchemaField HELP_CONTEXT_ENField = new DataSchemaField();
			HELP_CONTEXT_ENField.Name = "HELP_CONTEXT_EN";
			HELP_CONTEXT_ENField.Type = typeof(string).ToString();
			HELP_CONTEXT_ENField.Index = 4;
			fields.Add(HELP_CONTEXT_ENField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 5;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 6;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 7;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 8;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 9;
			fields.Add(MODIFY_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public Guid? MenuFid{ get;set; }		
				
		[DataMember]
		public string HelpContextCn{ get;set; }		
				
		[DataMember]
		public string HelpContextEn{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			HelpInfo info = new HelpInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.MenuFid = this.MenuFid;
			info.HelpContextCn = this.HelpContextCn;
			info.HelpContextEn = this.HelpContextEn;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public HelpInfo Clone()
		{
			return ((ICloneable) this).Clone() as HelpInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// HelpInfoCollection对应表[TS_SYS_HELP]
    /// </summary>
	public partial class HelpInfoCollection : BusinessObjectCollection<HelpInfo>
	{
		public HelpInfoCollection():base("TS_SYS_HELP"){}	
	}
}
