#region Declaim
//---------------------------------------------------------------------------
// Name:		ConfigInfo
// Function: 	Expose data in table TS_SYS_CONFIG from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年4月11日
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
    /// ConfigInfo对应表[TS_SYS_CONFIG]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class ConfigInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public ConfigInfo(		 long aId,
				Guid aFid,
				string aName,
				string aCode,
				string aConfigValue,
				string aComments,
				bool aValidFlag,
				string aCreateUser,
				DateTime aCreateDate,
				string aModifyUser,
				DateTime aModifyDate				
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			Name = aName;
		 
			Code = aCode;
		 
			ConfigValue = aConfigValue;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public ConfigInfo():base("TS_SYS_CONFIG")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");          _Keys = keys.ToArray();
			
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
			 
			DataSchemaField NAMEField = new DataSchemaField();
			NAMEField.Name = "NAME";
			NAMEField.Type = typeof(string).ToString();
			NAMEField.Index = 2;
			fields.Add(NAMEField);
			 
			DataSchemaField CODEField = new DataSchemaField();
			CODEField.Name = "CODE";
			CODEField.Type = typeof(string).ToString();
			CODEField.Index = 3;
			fields.Add(CODEField);
			 
			DataSchemaField CONFIG_VALUEField = new DataSchemaField();
			CONFIG_VALUEField.Name = "CONFIG_VALUE";
			CONFIG_VALUEField.Type = typeof(string).ToString();
			CONFIG_VALUEField.Index = 4;
			fields.Add(CONFIG_VALUEField);
			 
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
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string Name{ get;set; }		
				
		[DataMember]
		public string Code{ get;set; }		
				
		[DataMember]
		public string ConfigValue{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
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
			ConfigInfo info = new ConfigInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.Name = this.Name;
			info.Code = this.Code;
			info.ConfigValue = this.ConfigValue;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public ConfigInfo Clone()
		{
			return ((ICloneable) this).Clone() as ConfigInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// ConfigInfoCollection对应表[TS_SYS_CONFIG]
    /// </summary>
	public partial class ConfigInfoCollection : BusinessObjectCollection<ConfigInfo>
	{
		public ConfigInfoCollection():base("TS_SYS_CONFIG"){}	
	}
}
