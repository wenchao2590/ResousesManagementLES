#region Declaim
//---------------------------------------------------------------------------
// Name:		HandlerInfo
// Function: 	Expose data in table TS_SYS_HANDLER from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年3月9日
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
    /// HandlerInfo对应表[TS_SYS_HANDLER]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class HandlerInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public HandlerInfo(		 string aAjaxMethodName,
				string aAssemblyName,
				string aClassName,
				string aServerMethodName,
				string aComments,
				string aCreateUser,
				long aId,
				bool aValidFlag,
				DateTime aModifyDate,
				Guid aFid,
				string aModifyUser,
				DateTime aCreateDate				
		) : this()
		{
			 
			AjaxMethodName = aAjaxMethodName;
		 
			AssemblyName = aAssemblyName;
		 
			ClassName = aClassName;
		 
			ServerMethodName = aServerMethodName;
		 
			Comments = aComments;
		 
			CreateUser = aCreateUser;
		 
			Id = aId;
		 
			ValidFlag = aValidFlag;
		 
			ModifyDate = aModifyDate;
		 
			Fid = aFid;
		 
			ModifyUser = aModifyUser;
		 
			CreateDate = aCreateDate;
		}
		
		public HandlerInfo():base("TS_SYS_HANDLER")
		{
			List<string> keys = new List<string>();
			       			keys.Add("ID");     _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField AJAX_METHOD_NAMEField = new DataSchemaField();
			AJAX_METHOD_NAMEField.Name = "AJAX_METHOD_NAME";
			AJAX_METHOD_NAMEField.Type = typeof(string).ToString();
			AJAX_METHOD_NAMEField.Index = 0;
			fields.Add(AJAX_METHOD_NAMEField);
			 
			DataSchemaField ASSEMBLY_NAMEField = new DataSchemaField();
			ASSEMBLY_NAMEField.Name = "ASSEMBLY_NAME";
			ASSEMBLY_NAMEField.Type = typeof(string).ToString();
			ASSEMBLY_NAMEField.Index = 1;
			fields.Add(ASSEMBLY_NAMEField);
			 
			DataSchemaField CLASS_NAMEField = new DataSchemaField();
			CLASS_NAMEField.Name = "CLASS_NAME";
			CLASS_NAMEField.Type = typeof(string).ToString();
			CLASS_NAMEField.Index = 2;
			fields.Add(CLASS_NAMEField);
			 
			DataSchemaField SERVER_METHOD_NAMEField = new DataSchemaField();
			SERVER_METHOD_NAMEField.Name = "SERVER_METHOD_NAME";
			SERVER_METHOD_NAMEField.Type = typeof(string).ToString();
			SERVER_METHOD_NAMEField.Index = 3;
			fields.Add(SERVER_METHOD_NAMEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 4;
			fields.Add(COMMENTSField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 5;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 6;
			fields.Add(IDField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 7;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 8;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 9;
			fields.Add(FIDField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 10;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 11;
			fields.Add(CREATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string AjaxMethodName{ get;set; }		
				
		[DataMember]
		public string AssemblyName{ get;set; }		
				
		[DataMember]
		public string ClassName{ get;set; }		
				
		[DataMember]
		public string ServerMethodName{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			HandlerInfo info = new HandlerInfo();

			info.AjaxMethodName = this.AjaxMethodName;
			info.AssemblyName = this.AssemblyName;
			info.ClassName = this.ClassName;
			info.ServerMethodName = this.ServerMethodName;
			info.Comments = this.Comments;
			info.CreateUser = this.CreateUser;
			info.Id = this.Id;
			info.ValidFlag = this.ValidFlag;
			info.ModifyDate = this.ModifyDate;
			info.Fid = this.Fid;
			info.ModifyUser = this.ModifyUser;
			info.CreateDate = this.CreateDate;
			return info;			
		}
		 
		public HandlerInfo Clone()
		{
			return ((ICloneable) this).Clone() as HandlerInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// HandlerInfoCollection对应表[TS_SYS_HANDLER]
    /// </summary>
	public partial class HandlerInfoCollection : BusinessObjectCollection<HandlerInfo>
	{
		public HandlerInfoCollection():base("TS_SYS_HANDLER"){}	
	}
}