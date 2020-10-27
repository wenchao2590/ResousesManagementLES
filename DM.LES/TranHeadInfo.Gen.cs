#region Declaim
//---------------------------------------------------------------------------
// Name:		TranHeadInfo
// Function: 	Expose data in table TranHead from database as business object to MES system.
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
    /// TranHeadInfo对应表[TT_WMM_TRAN_HEAD]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class TranHeadInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public TranHeadInfo( 
					int aTranId,

					string aTranNo,

					string aPlant,

					string aSWmNo,

					string aSZoneNo,

					string aOWmNo,

					string aOZoneNo,

					int aTranType,

					DateTime aTranTime,

					string aBatchNo,

					string aFinancialCode,

					string aCostCode,

					string aInternalCode,

					string aWbsCode,

					int aTranStatus,

					DateTime aPublishTime,

					string aTranKeeper,

					string aPublishKeeper,

					string aComments,

					string aCreateUser,

					DateTime aCreateDate,

					string aUpdateUser,

					DateTime aUpdateDate,

					int aErpFlag

				 
		) : this()
		{
			 
			TranId = aTranId;
		 
			TranNo = aTranNo;
		 
			Plant = aPlant;
		 
			SWmNo = aSWmNo;
		 
			SZoneNo = aSZoneNo;
		 
			OWmNo = aOWmNo;
		 
			OZoneNo = aOZoneNo;
		 
			TranType = aTranType;
		 
			TranTime = aTranTime;
		 
			BatchNo = aBatchNo;
		 
			FinancialCode = aFinancialCode;
		 
			CostCode = aCostCode;
		 
			InternalCode = aInternalCode;
		 
			WbsCode = aWbsCode;
		 
			TranStatus = aTranStatus;
		 
			PublishTime = aPublishTime;
		 
			TranKeeper = aTranKeeper;
		 
			PublishKeeper = aPublishKeeper;
		 
			Comments = aComments;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			UpdateUser = aUpdateUser;
		 
			UpdateDate = aUpdateDate;
		 
			ErpFlag = aErpFlag;
		}
		
		public TranHeadInfo():base("TT_WMM_TRAN_HEAD")
		{
			List<string> keys = new List<string>();
			 			keys.Add("TRAN_ID");                       _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField TRAN_IDField = new DataSchemaField();
			TRAN_IDField.Name = "TRAN_ID";
			TRAN_IDField.Type = typeof(int).ToString();
			TRAN_IDField.Index = 0;
			fields.Add(TRAN_IDField);
			 
			DataSchemaField TRAN_NOField = new DataSchemaField();
			TRAN_NOField.Name = "TRAN_NO";
			TRAN_NOField.Type = typeof(string).ToString();
			TRAN_NOField.Index = 1;
			fields.Add(TRAN_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 2;
			fields.Add(PLANTField);
			 
			DataSchemaField S_WM_NOField = new DataSchemaField();
			S_WM_NOField.Name = "S_WM_NO";
			S_WM_NOField.Type = typeof(string).ToString();
			S_WM_NOField.Index = 3;
			fields.Add(S_WM_NOField);
			 
			DataSchemaField S_ZONE_NOField = new DataSchemaField();
			S_ZONE_NOField.Name = "S_ZONE_NO";
			S_ZONE_NOField.Type = typeof(string).ToString();
			S_ZONE_NOField.Index = 4;
			fields.Add(S_ZONE_NOField);
			 
			DataSchemaField O_WM_NOField = new DataSchemaField();
			O_WM_NOField.Name = "O_WM_NO";
			O_WM_NOField.Type = typeof(string).ToString();
			O_WM_NOField.Index = 5;
			fields.Add(O_WM_NOField);
			 
			DataSchemaField O_ZONE_NOField = new DataSchemaField();
			O_ZONE_NOField.Name = "O_ZONE_NO";
			O_ZONE_NOField.Type = typeof(string).ToString();
			O_ZONE_NOField.Index = 6;
			fields.Add(O_ZONE_NOField);
			 
			DataSchemaField TRAN_TYPEField = new DataSchemaField();
			TRAN_TYPEField.Name = "TRAN_TYPE";
			TRAN_TYPEField.Type = typeof(int).ToString();
			TRAN_TYPEField.Index = 7;
			fields.Add(TRAN_TYPEField);
			 
			DataSchemaField TRAN_TIMEField = new DataSchemaField();
			TRAN_TIMEField.Name = "TRAN_TIME";
			TRAN_TIMEField.Type = typeof(DateTime).ToString();
			TRAN_TIMEField.Index = 8;
			fields.Add(TRAN_TIMEField);
			 
			DataSchemaField BATCH_NOField = new DataSchemaField();
			BATCH_NOField.Name = "BATCH_NO";
			BATCH_NOField.Type = typeof(string).ToString();
			BATCH_NOField.Index = 9;
			fields.Add(BATCH_NOField);
			 
			DataSchemaField FINANCIAL_CODEField = new DataSchemaField();
			FINANCIAL_CODEField.Name = "FINANCIAL_CODE";
			FINANCIAL_CODEField.Type = typeof(string).ToString();
			FINANCIAL_CODEField.Index = 10;
			fields.Add(FINANCIAL_CODEField);
			 
			DataSchemaField COST_CODEField = new DataSchemaField();
			COST_CODEField.Name = "COST_CODE";
			COST_CODEField.Type = typeof(string).ToString();
			COST_CODEField.Index = 11;
			fields.Add(COST_CODEField);
			 
			DataSchemaField INTERNAL_CODEField = new DataSchemaField();
			INTERNAL_CODEField.Name = "INTERNAL_CODE";
			INTERNAL_CODEField.Type = typeof(string).ToString();
			INTERNAL_CODEField.Index = 12;
			fields.Add(INTERNAL_CODEField);
			 
			DataSchemaField WBS_CODEField = new DataSchemaField();
			WBS_CODEField.Name = "WBS_CODE";
			WBS_CODEField.Type = typeof(string).ToString();
			WBS_CODEField.Index = 13;
			fields.Add(WBS_CODEField);
			 
			DataSchemaField TRAN_STATUSField = new DataSchemaField();
			TRAN_STATUSField.Name = "TRAN_STATUS";
			TRAN_STATUSField.Type = typeof(int).ToString();
			TRAN_STATUSField.Index = 14;
			fields.Add(TRAN_STATUSField);
			 
			DataSchemaField PUBLISH_TIMEField = new DataSchemaField();
			PUBLISH_TIMEField.Name = "PUBLISH_TIME";
			PUBLISH_TIMEField.Type = typeof(DateTime).ToString();
			PUBLISH_TIMEField.Index = 15;
			fields.Add(PUBLISH_TIMEField);
			 
			DataSchemaField TRAN_KEEPERField = new DataSchemaField();
			TRAN_KEEPERField.Name = "TRAN_KEEPER";
			TRAN_KEEPERField.Type = typeof(string).ToString();
			TRAN_KEEPERField.Index = 16;
			fields.Add(TRAN_KEEPERField);
			 
			DataSchemaField PUBLISH_KEEPERField = new DataSchemaField();
			PUBLISH_KEEPERField.Name = "PUBLISH_KEEPER";
			PUBLISH_KEEPERField.Type = typeof(string).ToString();
			PUBLISH_KEEPERField.Index = 17;
			fields.Add(PUBLISH_KEEPERField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 18;
			fields.Add(COMMENTSField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 19;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 20;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 21;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 22;
			fields.Add(UPDATE_DATEField);
			 
			DataSchemaField ERP_FLAGField = new DataSchemaField();
			ERP_FLAGField.Name = "ERP_FLAG";
			ERP_FLAGField.Type = typeof(int).ToString();
			ERP_FLAGField.Index = 23;
			fields.Add(ERP_FLAGField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public int TranId{ get;set; }		
				
		[DataMember]
		public string TranNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string SWmNo{ get;set; }		
				
		[DataMember]
		public string SZoneNo{ get;set; }		
				
		[DataMember]
		public string OWmNo{ get;set; }		
				
		[DataMember]
		public string OZoneNo{ get;set; }		
				
		[DataMember]
		public int? TranType{ get;set; }		
				
		[DataMember]
		public DateTime? TranTime{ get;set; }		
				
		[DataMember]
		public string BatchNo{ get;set; }		
				
		[DataMember]
		public string FinancialCode{ get;set; }		
				
		[DataMember]
		public string CostCode{ get;set; }		
				
		[DataMember]
		public string InternalCode{ get;set; }		
				
		[DataMember]
		public string WbsCode{ get;set; }		
				
		[DataMember]
		public int? TranStatus{ get;set; }		
				
		[DataMember]
		public DateTime? PublishTime{ get;set; }		
				
		[DataMember]
		public string TranKeeper{ get;set; }		
				
		[DataMember]
		public string PublishKeeper{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		[DataMember]
		public int? ErpFlag{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			TranHeadInfo info = new TranHeadInfo();

			info.TranId = this.TranId;
			info.TranNo = this.TranNo;
			info.Plant = this.Plant;
			info.SWmNo = this.SWmNo;
			info.SZoneNo = this.SZoneNo;
			info.OWmNo = this.OWmNo;
			info.OZoneNo = this.OZoneNo;
			info.TranType = this.TranType;
			info.TranTime = this.TranTime;
			info.BatchNo = this.BatchNo;
			info.FinancialCode = this.FinancialCode;
			info.CostCode = this.CostCode;
			info.InternalCode = this.InternalCode;
			info.WbsCode = this.WbsCode;
			info.TranStatus = this.TranStatus;
			info.PublishTime = this.PublishTime;
			info.TranKeeper = this.TranKeeper;
			info.PublishKeeper = this.PublishKeeper;
			info.Comments = this.Comments;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.UpdateUser = this.UpdateUser;
			info.UpdateDate = this.UpdateDate;
			info.ErpFlag = this.ErpFlag;
			return info;			
		}
		 
		public TranHeadInfo Clone()
		{
			return ((ICloneable) this).Clone() as TranHeadInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// TranHeadInfoCollection对应表[TT_WMM_TRAN_HEAD]
    /// </summary>
	public partial class TranHeadInfoCollection : BusinessObjectCollection<TranHeadInfo>
	{
		public TranHeadInfoCollection():base("TT_WMM_TRAN_HEAD"){}	
	}
}