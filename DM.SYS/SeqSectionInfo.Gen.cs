#region Declaim
//---------------------------------------------------------------------------
// Name:		SeqSectionInfo
// Function: 	Expose data in table TS_SYS_SEQ_SECTION from database as business object to MES system.
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
    /// SeqSectionInfo对应表[TS_SYS_SEQ_SECTION]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SeqSectionInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SeqSectionInfo(		 long aId,
				Guid aFid,
				Guid aDefineFid,
				string aSeqCode,
				int aSectionSeq,
				bool aIsFixedLength,
				int aLength,
				int aFillType,
				string aFillChar,
				int aDataGenerateType,
				int aStepLength,
				string aDefaultValue,
				int aMinValue,
				int aMaxValue,
				bool aIsCycle,
				bool aIsAutoup,
				bool aIsSeedValue,
				bool aValidFlag,
				string aCreateUser,
				DateTime aCreateDate,
				string aModifyUser,
				DateTime aModifyDate				
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			DefineFid = aDefineFid;
		 
			SeqCode = aSeqCode;
		 
			SectionSeq = aSectionSeq;
		 
			IsFixedLength = aIsFixedLength;
		 
			Length = aLength;
		 
			FillType = aFillType;
		 
			FillChar = aFillChar;
		 
			DataGenerateType = aDataGenerateType;
		 
			StepLength = aStepLength;
		 
			DefaultValue = aDefaultValue;
		 
			MinValue = aMinValue;
		 
			MaxValue = aMaxValue;
		 
			IsCycle = aIsCycle;
		 
			IsAutoup = aIsAutoup;
		 
			IsSeedValue = aIsSeedValue;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public SeqSectionInfo():base("TS_SYS_SEQ_SECTION")
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
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 1;
			fields.Add(FIDField);
			 
			DataSchemaField DEFINE_FIDField = new DataSchemaField();
			DEFINE_FIDField.Name = "DEFINE_FID";
			DEFINE_FIDField.Type = typeof(Guid).ToString();
			DEFINE_FIDField.Index = 2;
			fields.Add(DEFINE_FIDField);
			 
			DataSchemaField SEQ_CODEField = new DataSchemaField();
			SEQ_CODEField.Name = "SEQ_CODE";
			SEQ_CODEField.Type = typeof(string).ToString();
			SEQ_CODEField.Index = 3;
			fields.Add(SEQ_CODEField);
			 
			DataSchemaField SECTION_SEQField = new DataSchemaField();
			SECTION_SEQField.Name = "SECTION_SEQ";
			SECTION_SEQField.Type = typeof(int).ToString();
			SECTION_SEQField.Index = 4;
			fields.Add(SECTION_SEQField);
			 
			DataSchemaField IS_FIXED_LENGTHField = new DataSchemaField();
			IS_FIXED_LENGTHField.Name = "IS_FIXED_LENGTH";
			IS_FIXED_LENGTHField.Type = typeof(bool).ToString();
			IS_FIXED_LENGTHField.Index = 5;
			fields.Add(IS_FIXED_LENGTHField);
			 
			DataSchemaField LENGTHField = new DataSchemaField();
			LENGTHField.Name = "LENGTH";
			LENGTHField.Type = typeof(int).ToString();
			LENGTHField.Index = 6;
			fields.Add(LENGTHField);
			 
			DataSchemaField FILL_TYPEField = new DataSchemaField();
			FILL_TYPEField.Name = "FILL_TYPE";
			FILL_TYPEField.Type = typeof(int).ToString();
			FILL_TYPEField.Index = 7;
			fields.Add(FILL_TYPEField);
			 
			DataSchemaField FILL_CHARField = new DataSchemaField();
			FILL_CHARField.Name = "FILL_CHAR";
			FILL_CHARField.Type = typeof(string).ToString();
			FILL_CHARField.Index = 8;
			fields.Add(FILL_CHARField);
			 
			DataSchemaField DATA_GENERATE_TYPEField = new DataSchemaField();
			DATA_GENERATE_TYPEField.Name = "DATA_GENERATE_TYPE";
			DATA_GENERATE_TYPEField.Type = typeof(int).ToString();
			DATA_GENERATE_TYPEField.Index = 9;
			fields.Add(DATA_GENERATE_TYPEField);
			 
			DataSchemaField STEP_LENGTHField = new DataSchemaField();
			STEP_LENGTHField.Name = "STEP_LENGTH";
			STEP_LENGTHField.Type = typeof(int).ToString();
			STEP_LENGTHField.Index = 10;
			fields.Add(STEP_LENGTHField);
			 
			DataSchemaField DEFAULT_VALUEField = new DataSchemaField();
			DEFAULT_VALUEField.Name = "DEFAULT_VALUE";
			DEFAULT_VALUEField.Type = typeof(string).ToString();
			DEFAULT_VALUEField.Index = 11;
			fields.Add(DEFAULT_VALUEField);
			 
			DataSchemaField MIN_VALUEField = new DataSchemaField();
			MIN_VALUEField.Name = "MIN_VALUE";
			MIN_VALUEField.Type = typeof(int).ToString();
			MIN_VALUEField.Index = 12;
			fields.Add(MIN_VALUEField);
			 
			DataSchemaField MAX_VALUEField = new DataSchemaField();
			MAX_VALUEField.Name = "MAX_VALUE";
			MAX_VALUEField.Type = typeof(int).ToString();
			MAX_VALUEField.Index = 13;
			fields.Add(MAX_VALUEField);
			 
			DataSchemaField IS_CYCLEField = new DataSchemaField();
			IS_CYCLEField.Name = "IS_CYCLE";
			IS_CYCLEField.Type = typeof(bool).ToString();
			IS_CYCLEField.Index = 14;
			fields.Add(IS_CYCLEField);
			 
			DataSchemaField IS_AUTOUPField = new DataSchemaField();
			IS_AUTOUPField.Name = "IS_AUTOUP";
			IS_AUTOUPField.Type = typeof(bool).ToString();
			IS_AUTOUPField.Index = 15;
			fields.Add(IS_AUTOUPField);
			 
			DataSchemaField IS_SEED_VALUEField = new DataSchemaField();
			IS_SEED_VALUEField.Name = "IS_SEED_VALUE";
			IS_SEED_VALUEField.Type = typeof(bool).ToString();
			IS_SEED_VALUEField.Index = 16;
			fields.Add(IS_SEED_VALUEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 17;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 18;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 19;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 20;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 21;
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
		public Guid? DefineFid{ get;set; }		
				
		[DataMember]
		public string SeqCode{ get;set; }		
				
		[DataMember]
		public int? SectionSeq{ get;set; }		
				
		[DataMember]
		public bool? IsFixedLength{ get;set; }		
				
		[DataMember]
		public int? Length{ get;set; }		
				
		[DataMember]
		public int? FillType{ get;set; }		
				
		[DataMember]
		public string FillChar{ get;set; }		
				
		[DataMember]
		public int? DataGenerateType{ get;set; }		
				
		[DataMember]
		public int? StepLength{ get;set; }		
				
		[DataMember]
		public string DefaultValue{ get;set; }		
				
		[DataMember]
		public int? MinValue{ get;set; }		
				
		[DataMember]
		public int? MaxValue{ get;set; }		
				
		[DataMember]
		public bool? IsCycle{ get;set; }		
				
		[DataMember]
		public bool? IsAutoup{ get;set; }		
				
		[DataMember]
		public bool? IsSeedValue{ get;set; }		
				
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
			SeqSectionInfo info = new SeqSectionInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.DefineFid = this.DefineFid;
			info.SeqCode = this.SeqCode;
			info.SectionSeq = this.SectionSeq;
			info.IsFixedLength = this.IsFixedLength;
			info.Length = this.Length;
			info.FillType = this.FillType;
			info.FillChar = this.FillChar;
			info.DataGenerateType = this.DataGenerateType;
			info.StepLength = this.StepLength;
			info.DefaultValue = this.DefaultValue;
			info.MinValue = this.MinValue;
			info.MaxValue = this.MaxValue;
			info.IsCycle = this.IsCycle;
			info.IsAutoup = this.IsAutoup;
			info.IsSeedValue = this.IsSeedValue;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public SeqSectionInfo Clone()
		{
			return ((ICloneable) this).Clone() as SeqSectionInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SeqSectionInfoCollection对应表[TS_SYS_SEQ_SECTION]
    /// </summary>
	public partial class SeqSectionInfoCollection : BusinessObjectCollection<SeqSectionInfo>
	{
		public SeqSectionInfoCollection():base("TS_SYS_SEQ_SECTION"){}	
	}
}
