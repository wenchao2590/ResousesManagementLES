#region Declaim
//---------------------------------------------------------------------------
// Name:		QmisOutboundDetailLogInfo
// Function: 	Expose data in table QmisOutboundDetailLog from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年5月31日
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
    /// QmisOutboundDetailLogInfo对应表[TI_IFM_QMIS_OUTBOUND_DETAIL_LOG]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class QmisOutboundDetailLogInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public QmisOutboundDetailLogInfo( 
					int aId,

					Guid aFid,

					Guid aLogFid,

					string aTransNo,

					string aSourceSystem,

					string aTargetSystem,

					string aMethordCode,

					string aKeyValue,

					DateTime aExecuteStartTime,

					DateTime aExecuteEndTime,

					int aExecuteResult,

					int aExecuteTimes,

					string aMsgContent,

					string aErrorCode,

					string aErrorMsg,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					int aValidFlag

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			LogFid = aLogFid;
		 
			TransNo = aTransNo;
		 
			SourceSystem = aSourceSystem;
		 
			TargetSystem = aTargetSystem;
		 
			MethordCode = aMethordCode;
		 
			KeyValue = aKeyValue;
		 
			ExecuteStartTime = aExecuteStartTime;
		 
			ExecuteEndTime = aExecuteEndTime;
		 
			ExecuteResult = aExecuteResult;
		 
			ExecuteTimes = aExecuteTimes;
		 
			MsgContent = aMsgContent;
		 
			ErrorCode = aErrorCode;
		 
			ErrorMsg = aErrorMsg;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			ValidFlag = aValidFlag;
		}
		
		public QmisOutboundDetailLogInfo():base("TI_IFM_QMIS_OUTBOUND_DETAIL_LOG")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                   _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(int).ToString();
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
			 
			DataSchemaField TRANS_NOField = new DataSchemaField();
			TRANS_NOField.Name = "TRANS_NO";
			TRANS_NOField.Type = typeof(string).ToString();
			TRANS_NOField.Index = 3;
			fields.Add(TRANS_NOField);
			 
			DataSchemaField SOURCE_SYSTEMField = new DataSchemaField();
			SOURCE_SYSTEMField.Name = "SOURCE_SYSTEM";
			SOURCE_SYSTEMField.Type = typeof(string).ToString();
			SOURCE_SYSTEMField.Index = 4;
			fields.Add(SOURCE_SYSTEMField);
			 
			DataSchemaField TARGET_SYSTEMField = new DataSchemaField();
			TARGET_SYSTEMField.Name = "TARGET_SYSTEM";
			TARGET_SYSTEMField.Type = typeof(string).ToString();
			TARGET_SYSTEMField.Index = 5;
			fields.Add(TARGET_SYSTEMField);
			 
			DataSchemaField METHORD_CODEField = new DataSchemaField();
			METHORD_CODEField.Name = "METHOD_CODE";
			METHORD_CODEField.Type = typeof(string).ToString();
			METHORD_CODEField.Index = 6;
			fields.Add(METHORD_CODEField);
			 
			DataSchemaField KEY_VALUEField = new DataSchemaField();
			KEY_VALUEField.Name = "KEY_VALUE";
			KEY_VALUEField.Type = typeof(string).ToString();
			KEY_VALUEField.Index = 7;
			fields.Add(KEY_VALUEField);
			 
			DataSchemaField EXECUTE_START_TIMEField = new DataSchemaField();
			EXECUTE_START_TIMEField.Name = "EXECUTE_START_TIME";
			EXECUTE_START_TIMEField.Type = typeof(DateTime).ToString();
			EXECUTE_START_TIMEField.Index = 8;
			fields.Add(EXECUTE_START_TIMEField);
			 
			DataSchemaField EXECUTE_END_TIMEField = new DataSchemaField();
			EXECUTE_END_TIMEField.Name = "EXECUTE_END_TIME";
			EXECUTE_END_TIMEField.Type = typeof(DateTime).ToString();
			EXECUTE_END_TIMEField.Index = 9;
			fields.Add(EXECUTE_END_TIMEField);
			 
			DataSchemaField EXECUTE_RESULTField = new DataSchemaField();
			EXECUTE_RESULTField.Name = "EXECUTE_RESULT";
			EXECUTE_RESULTField.Type = typeof(int).ToString();
			EXECUTE_RESULTField.Index = 10;
			fields.Add(EXECUTE_RESULTField);
			 
			DataSchemaField EXECUTE_TIMESField = new DataSchemaField();
			EXECUTE_TIMESField.Name = "EXECUTE_TIMES";
			EXECUTE_TIMESField.Type = typeof(int).ToString();
			EXECUTE_TIMESField.Index = 11;
			fields.Add(EXECUTE_TIMESField);
			 
			DataSchemaField MSG_CONTENTField = new DataSchemaField();
			MSG_CONTENTField.Name = "MSG_CONTENT";
			MSG_CONTENTField.Type = typeof(string).ToString();
			MSG_CONTENTField.Index = 12;
			fields.Add(MSG_CONTENTField);
			 
			DataSchemaField ERROR_CODEField = new DataSchemaField();
			ERROR_CODEField.Name = "ERROR_CODE";
			ERROR_CODEField.Type = typeof(string).ToString();
			ERROR_CODEField.Index = 13;
			fields.Add(ERROR_CODEField);
			 
			DataSchemaField ERROR_MSGField = new DataSchemaField();
			ERROR_MSGField.Name = "ERROR_MSG";
			ERROR_MSGField.Type = typeof(string).ToString();
			ERROR_MSGField.Index = 14;
			fields.Add(ERROR_MSGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 15;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 16;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 17;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 18;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(int).ToString();
			VALID_FLAGField.Index = 19;
			fields.Add(VALID_FLAGField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public int Id{ get;set; }		
				
		[DataMember]
		public Guid Fid{ get;set; }		
				
		[DataMember]
		public Guid LogFid{ get;set; }		
				
		[DataMember]
		public string TransNo{ get;set; }		
				
		[DataMember]
		public string SourceSystem{ get;set; }		
				
		[DataMember]
		public string TargetSystem{ get;set; }		
				
		[DataMember]
		public string MethordCode{ get;set; }		
				
		[DataMember]
		public string KeyValue{ get;set; }		
				
		[DataMember]
		public DateTime? ExecuteStartTime{ get;set; }		
				
		[DataMember]
		public DateTime? ExecuteEndTime{ get;set; }		
				
		[DataMember]
		public int? ExecuteResult{ get;set; }		
				
		[DataMember]
		public int? ExecuteTimes{ get;set; }		
				
		[DataMember]
		public string MsgContent{ get;set; }		
				
		[DataMember]
		public string ErrorCode{ get;set; }		
				
		[DataMember]
		public string ErrorMsg{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public int? ValidFlag{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			QmisOutboundDetailLogInfo info = new QmisOutboundDetailLogInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LogFid = this.LogFid;
			info.TransNo = this.TransNo;
			info.SourceSystem = this.SourceSystem;
			info.TargetSystem = this.TargetSystem;
			info.MethordCode = this.MethordCode;
			info.KeyValue = this.KeyValue;
			info.ExecuteStartTime = this.ExecuteStartTime;
			info.ExecuteEndTime = this.ExecuteEndTime;
			info.ExecuteResult = this.ExecuteResult;
			info.ExecuteTimes = this.ExecuteTimes;
			info.MsgContent = this.MsgContent;
			info.ErrorCode = this.ErrorCode;
			info.ErrorMsg = this.ErrorMsg;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.ValidFlag = this.ValidFlag;
			return info;			
		}
		 
		public QmisOutboundDetailLogInfo Clone()
		{
			return ((ICloneable) this).Clone() as QmisOutboundDetailLogInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// QmisOutboundDetailLogInfoCollection对应表[TI_IFM_QMIS_OUTBOUND_DETAIL_LOG]
    /// </summary>
	public partial class QmisOutboundDetailLogInfoCollection : BusinessObjectCollection<QmisOutboundDetailLogInfo>
	{
		public QmisOutboundDetailLogInfoCollection():base("TI_IFM_QMIS_OUTBOUND_DETAIL_LOG"){}	
	}
}
