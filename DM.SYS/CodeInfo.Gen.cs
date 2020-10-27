#region Declaim
//---------------------------------------------------------------------------
// Name:		CodeInfo
// Function: 	Expose data in table TS_SYS_CODE from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年4月18日
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
    /// CodeInfo对应表[TS_SYS_CODE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class CodeInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public CodeInfo(		 long aId,
				Guid aFid,
				string aCodeName,
				string aCodeNameCn,
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
		 
			CodeName = aCodeName;
		 
			CodeNameCn = aCodeNameCn;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public CodeInfo():base("TS_SYS_CODE")
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
			 
			DataSchemaField CODE_NAMEField = new DataSchemaField();
			CODE_NAMEField.Name = "CODE_NAME";
			CODE_NAMEField.Type = typeof(string).ToString();
			CODE_NAMEField.Index = 2;
			fields.Add(CODE_NAMEField);
			 
			DataSchemaField CODE_NAME_CNField = new DataSchemaField();
			CODE_NAME_CNField.Name = "CODE_NAME_CN";
			CODE_NAME_CNField.Type = typeof(string).ToString();
			CODE_NAME_CNField.Index = 3;
			fields.Add(CODE_NAME_CNField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 4;
			fields.Add(COMMENTSField);
			 
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
		public string CodeName{ get;set; }		
				
		[DataMember]
		public string CodeNameCn{ get;set; }		
				
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
			CodeInfo info = new CodeInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.CodeName = this.CodeName;
			info.CodeNameCn = this.CodeNameCn;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public CodeInfo Clone()
		{
			return ((ICloneable) this).Clone() as CodeInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// CodeInfoCollection对应表[TS_SYS_CODE]
    /// </summary>
	public partial class CodeInfoCollection : BusinessObjectCollection<CodeInfo>
	{
		public CodeInfoCollection():base("TS_SYS_CODE"){}	
	}
}