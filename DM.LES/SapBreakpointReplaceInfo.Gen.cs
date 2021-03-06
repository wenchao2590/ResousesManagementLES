#region Declaim
//---------------------------------------------------------------------------
// Name:		SapBreakpointReplaceInfo
// Function: 	Expose data in table SapBreakpointReplace from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月24日
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
    /// SapBreakpointReplaceInfo对应表[TI_IFM_SAP_BREAKPOINT_REPLACE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SapBreakpointReplaceInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SapBreakpointReplaceInfo( 
					long aId,

					Guid aFid,

					Guid aLogFid,

					string aAufnr,

					string aNmatnr,

					string aOmatnr,

					decimal aMenge,

					string aVlsch,

					DateTime aRdate,

					int aProcessFlag,

					DateTime aProcessTime,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					string aComments

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			LogFid = aLogFid;
		 
			Aufnr = aAufnr;
		 
			Nmatnr = aNmatnr;
		 
			Omatnr = aOmatnr;
		 
			Menge = aMenge;
		 
			Vlsch = aVlsch;
		 
			Rdate = aRdate;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			Comments = aComments;
		}
		
		public SapBreakpointReplaceInfo():base("TI_IFM_SAP_BREAKPOINT_REPLACE")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                _Keys = keys.ToArray();
			
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
			 
			DataSchemaField LOG_FIDField = new DataSchemaField();
			LOG_FIDField.Name = "LOG_FID";
			LOG_FIDField.Type = typeof(Guid).ToString();
			LOG_FIDField.Index = 2;
			fields.Add(LOG_FIDField);
			 
			DataSchemaField AUFNRField = new DataSchemaField();
			AUFNRField.Name = "AUFNR";
			AUFNRField.Type = typeof(string).ToString();
			AUFNRField.Index = 3;
			fields.Add(AUFNRField);
			 
			DataSchemaField NMATNRField = new DataSchemaField();
			NMATNRField.Name = "NMATNR";
			NMATNRField.Type = typeof(string).ToString();
			NMATNRField.Index = 4;
			fields.Add(NMATNRField);
			 
			DataSchemaField OMATNRField = new DataSchemaField();
			OMATNRField.Name = "OMATNR";
			OMATNRField.Type = typeof(string).ToString();
			OMATNRField.Index = 5;
			fields.Add(OMATNRField);
			 
			DataSchemaField MENGEField = new DataSchemaField();
			MENGEField.Name = "MENGE";
			MENGEField.Type = typeof(decimal).ToString();
			MENGEField.Index = 6;
			fields.Add(MENGEField);
			 
			DataSchemaField VLSCHField = new DataSchemaField();
			VLSCHField.Name = "VLSCH";
			VLSCHField.Type = typeof(string).ToString();
			VLSCHField.Index = 7;
			fields.Add(VLSCHField);
			 
			DataSchemaField RDATEField = new DataSchemaField();
			RDATEField.Name = "RDATE";
			RDATEField.Type = typeof(DateTime).ToString();
			RDATEField.Index = 8;
			fields.Add(RDATEField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 9;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 10;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 11;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 12;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 13;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 14;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 15;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 16;
			fields.Add(COMMENTSField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public Guid? LogFid{ get;set; }		
				
		[DataMember]
		public string Aufnr{ get;set; }		
				
		[DataMember]
		public string Nmatnr{ get;set; }		
				
		[DataMember]
		public string Omatnr{ get;set; }		
				
		[DataMember]
		public decimal? Menge{ get;set; }		
				
		[DataMember]
		public string Vlsch{ get;set; }		
				
		[DataMember]
		public DateTime? Rdate{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			SapBreakpointReplaceInfo info = new SapBreakpointReplaceInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LogFid = this.LogFid;
			info.Aufnr = this.Aufnr;
			info.Nmatnr = this.Nmatnr;
			info.Omatnr = this.Omatnr;
			info.Menge = this.Menge;
			info.Vlsch = this.Vlsch;
			info.Rdate = this.Rdate;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.Comments = this.Comments;
			return info;			
		}
		 
		public SapBreakpointReplaceInfo Clone()
		{
			return ((ICloneable) this).Clone() as SapBreakpointReplaceInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SapBreakpointReplaceInfoCollection对应表[TI_IFM_SAP_BREAKPOINT_REPLACE]
    /// </summary>
	public partial class SapBreakpointReplaceInfoCollection : BusinessObjectCollection<SapBreakpointReplaceInfo>
	{
		public SapBreakpointReplaceInfoCollection():base("TI_IFM_SAP_BREAKPOINT_REPLACE"){}	
	}
}
