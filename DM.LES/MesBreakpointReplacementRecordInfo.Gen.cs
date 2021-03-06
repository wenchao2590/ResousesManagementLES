#region Declaim
//---------------------------------------------------------------------------
// Name:		MesBreakpointReplacementRecordInfo
// Function: 	Expose data in table MesBreakpointReplacementRecord from database as business object to MES system.
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
    /// MesBreakpointReplacementRecordInfo对应表[TI_IFM_MES_BREAKPOINT_REPLACEMENT_RECORD]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class MesBreakpointReplacementRecordInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public MesBreakpointReplacementRecordInfo( 
					long aId,

					Guid aFid,

					Guid aLogFid,

					string aOrderno,

					string aOldpartno,

					string aNewpartno,

					string aOldsupplier,

					string aNewsupplier,

					string aOldstation,

					string aNewstation,

					decimal aOldqty,

					decimal aNewqty,

					DateTime aReplacetime,

					int aProcessFlag,

					DateTime aProcessTime,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			LogFid = aLogFid;
		 
			Orderno = aOrderno;
		 
			Oldpartno = aOldpartno;
		 
			Newpartno = aNewpartno;
		 
			Oldsupplier = aOldsupplier;
		 
			Newsupplier = aNewsupplier;
		 
			Oldstation = aOldstation;
		 
			Newstation = aNewstation;
		 
			Oldqty = aOldqty;
		 
			Newqty = aNewqty;
		 
			Replacetime = aReplacetime;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public MesBreakpointReplacementRecordInfo():base("TI_IFM_MES_BREAKPOINT_REPLACEMENT_RECORD")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                   _Keys = keys.ToArray();
			
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
			 
			DataSchemaField ORDERNOField = new DataSchemaField();
			ORDERNOField.Name = "ORDERNO";
			ORDERNOField.Type = typeof(string).ToString();
			ORDERNOField.Index = 3;
			fields.Add(ORDERNOField);
			 
			DataSchemaField OLDPARTNOField = new DataSchemaField();
			OLDPARTNOField.Name = "OLDPARTNO";
			OLDPARTNOField.Type = typeof(string).ToString();
			OLDPARTNOField.Index = 4;
			fields.Add(OLDPARTNOField);
			 
			DataSchemaField NEWPARTNOField = new DataSchemaField();
			NEWPARTNOField.Name = "NEWPARTNO";
			NEWPARTNOField.Type = typeof(string).ToString();
			NEWPARTNOField.Index = 5;
			fields.Add(NEWPARTNOField);
			 
			DataSchemaField OLDSUPPLIERField = new DataSchemaField();
			OLDSUPPLIERField.Name = "OLDSUPPLIER";
			OLDSUPPLIERField.Type = typeof(string).ToString();
			OLDSUPPLIERField.Index = 6;
			fields.Add(OLDSUPPLIERField);
			 
			DataSchemaField NEWSUPPLIERField = new DataSchemaField();
			NEWSUPPLIERField.Name = "NEWSUPPLIER";
			NEWSUPPLIERField.Type = typeof(string).ToString();
			NEWSUPPLIERField.Index = 7;
			fields.Add(NEWSUPPLIERField);
			 
			DataSchemaField OLDSTATIONField = new DataSchemaField();
			OLDSTATIONField.Name = "OLDSTATION";
			OLDSTATIONField.Type = typeof(string).ToString();
			OLDSTATIONField.Index = 8;
			fields.Add(OLDSTATIONField);
			 
			DataSchemaField NEWSTATIONField = new DataSchemaField();
			NEWSTATIONField.Name = "NEWSTATION";
			NEWSTATIONField.Type = typeof(string).ToString();
			NEWSTATIONField.Index = 9;
			fields.Add(NEWSTATIONField);
			 
			DataSchemaField OLDQTYField = new DataSchemaField();
			OLDQTYField.Name = "OLDQTY";
			OLDQTYField.Type = typeof(decimal).ToString();
			OLDQTYField.Index = 10;
			fields.Add(OLDQTYField);
			 
			DataSchemaField NEWQTYField = new DataSchemaField();
			NEWQTYField.Name = "NEWQTY";
			NEWQTYField.Type = typeof(decimal).ToString();
			NEWQTYField.Index = 11;
			fields.Add(NEWQTYField);
			 
			DataSchemaField REPLACETIMEField = new DataSchemaField();
			REPLACETIMEField.Name = "REPLACETIME";
			REPLACETIMEField.Type = typeof(DateTime).ToString();
			REPLACETIMEField.Index = 12;
			fields.Add(REPLACETIMEField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 13;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 14;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 15;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 16;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 17;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 18;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 19;
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
		public Guid? LogFid{ get;set; }		
				
		[DataMember]
		public string Orderno{ get;set; }		
				
		[DataMember]
		public string Oldpartno{ get;set; }		
				
		[DataMember]
		public string Newpartno{ get;set; }		
				
		[DataMember]
		public string Oldsupplier{ get;set; }		
				
		[DataMember]
		public string Newsupplier{ get;set; }		
				
		[DataMember]
		public string Oldstation{ get;set; }		
				
		[DataMember]
		public string Newstation{ get;set; }		
				
		[DataMember]
		public decimal? Oldqty{ get;set; }		
				
		[DataMember]
		public decimal? Newqty{ get;set; }		
				
		[DataMember]
		public DateTime? Replacetime{ get;set; }		
				
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
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			MesBreakpointReplacementRecordInfo info = new MesBreakpointReplacementRecordInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LogFid = this.LogFid;
			info.Orderno = this.Orderno;
			info.Oldpartno = this.Oldpartno;
			info.Newpartno = this.Newpartno;
			info.Oldsupplier = this.Oldsupplier;
			info.Newsupplier = this.Newsupplier;
			info.Oldstation = this.Oldstation;
			info.Newstation = this.Newstation;
			info.Oldqty = this.Oldqty;
			info.Newqty = this.Newqty;
			info.Replacetime = this.Replacetime;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public MesBreakpointReplacementRecordInfo Clone()
		{
			return ((ICloneable) this).Clone() as MesBreakpointReplacementRecordInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// MesBreakpointReplacementRecordInfoCollection对应表[TI_IFM_MES_BREAKPOINT_REPLACEMENT_RECORD]
    /// </summary>
	public partial class MesBreakpointReplacementRecordInfoCollection : BusinessObjectCollection<MesBreakpointReplacementRecordInfo>
	{
		public MesBreakpointReplacementRecordInfoCollection():base("TI_IFM_MES_BREAKPOINT_REPLACEMENT_RECORD"){}	
	}
}
