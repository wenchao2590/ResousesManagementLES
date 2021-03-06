#region Declaim
//---------------------------------------------------------------------------
// Name:		SeqCurrentValueInfo
// Function: 	Expose data in table TS_SYS_SEQ_CURRENT_VALUE from database as business object to MES system.
// Tool:		T4
// CreateDate:	2016年6月2日
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
    /// SeqCurrentValueInfo对应表[TS_SYS_SEQ_CURRENT_VALUE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SeqCurrentValueInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SeqCurrentValueInfo(		 long aId,
				Guid aFid,
				string aSeqCode,
				Guid aSeqSectionFid,
				string aQueryValue,
				int aCurrentValue,
				bool aValidFlag,
				string aCreateUser,
				DateTime aCreateDate,
				string aModifyUser,
				DateTime aModifyDate				
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			SeqCode = aSeqCode;
		 
			SeqSectionFid = aSeqSectionFid;
		 
			QueryValue = aQueryValue;
		 
			CurrentValue = aCurrentValue;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public SeqCurrentValueInfo():base("TS_SYS_SEQ_CURRENT_VALUE")
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
			 
			DataSchemaField SEQ_CODEField = new DataSchemaField();
			SEQ_CODEField.Name = "SEQ_CODE";
			SEQ_CODEField.Type = typeof(string).ToString();
			SEQ_CODEField.Index = 2;
			fields.Add(SEQ_CODEField);
			 
			DataSchemaField SEQ_SECTION_FIDField = new DataSchemaField();
			SEQ_SECTION_FIDField.Name = "SEQ_SECTION_FID";
			SEQ_SECTION_FIDField.Type = typeof(Guid).ToString();
			SEQ_SECTION_FIDField.Index = 3;
			fields.Add(SEQ_SECTION_FIDField);
			 
			DataSchemaField QUERY_VALUEField = new DataSchemaField();
			QUERY_VALUEField.Name = "QUERY_VALUE";
			QUERY_VALUEField.Type = typeof(string).ToString();
			QUERY_VALUEField.Index = 4;
			fields.Add(QUERY_VALUEField);
			 
			DataSchemaField CURRENT_VALUEField = new DataSchemaField();
			CURRENT_VALUEField.Name = "CURRENT_VALUE";
			CURRENT_VALUEField.Type = typeof(int).ToString();
			CURRENT_VALUEField.Index = 5;
			fields.Add(CURRENT_VALUEField);
			 
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
		public string SeqCode{ get;set; }		
				
		[DataMember]
		public Guid? SeqSectionFid{ get;set; }		
				
		[DataMember]
		public string QueryValue{ get;set; }		
				
		[DataMember]
		public int? CurrentValue{ get;set; }		
				
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
			SeqCurrentValueInfo info = new SeqCurrentValueInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.SeqCode = this.SeqCode;
			info.SeqSectionFid = this.SeqSectionFid;
			info.QueryValue = this.QueryValue;
			info.CurrentValue = this.CurrentValue;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public SeqCurrentValueInfo Clone()
		{
			return ((ICloneable) this).Clone() as SeqCurrentValueInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SeqCurrentValueInfoCollection对应表[TS_SYS_SEQ_CURRENT_VALUE]
    /// </summary>
	public partial class SeqCurrentValueInfoCollection : BusinessObjectCollection<SeqCurrentValueInfo>
	{
		public SeqCurrentValueInfoCollection():base("TS_SYS_SEQ_CURRENT_VALUE"){}	
	}
}
