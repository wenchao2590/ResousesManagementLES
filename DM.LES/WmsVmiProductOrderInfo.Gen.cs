#region Declaim
//---------------------------------------------------------------------------
// Name:		WmsVmiProductOrderInfo
// Function: 	Expose data in table WmsVmiProductOrder from database as business object to MES system.
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
    /// WmsVmiProductOrderInfo对应表[TI_IFM_WMS_VMI_PRODUCT_ORDER]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class WmsVmiProductOrderInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public WmsVmiProductOrderInfo( 
					long aId,

					Guid aFid,

					Guid aLogFid,

					string aOrderNo,

					string aPartNo,

					DateTime aOrderDate,

					string aAssemblyLine,

					int aQty,

					DateTime aOnlineTime,

					DateTime aDownLineTime,

					string aModelYear,

					bool aLockFlag,

					int aSeq,

					string aWerks,

					int aProcessFlag,

					DateTime aProcessTime,

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
		 
			LogFid = aLogFid;
		 
			OrderNo = aOrderNo;
		 
			PartNo = aPartNo;
		 
			OrderDate = aOrderDate;
		 
			AssemblyLine = aAssemblyLine;
		 
			Qty = aQty;
		 
			OnlineTime = aOnlineTime;
		 
			DownLineTime = aDownLineTime;
		 
			ModelYear = aModelYear;
		 
			LockFlag = aLockFlag;
		 
			Seq = aSeq;
		 
			Werks = aWerks;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public WmsVmiProductOrderInfo():base("TI_IFM_WMS_VMI_PRODUCT_ORDER")
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
			 
			DataSchemaField LOG_FIDField = new DataSchemaField();
			LOG_FIDField.Name = "LOG_FID";
			LOG_FIDField.Type = typeof(Guid).ToString();
			LOG_FIDField.Index = 2;
			fields.Add(LOG_FIDField);
			 
			DataSchemaField ORDER_NOField = new DataSchemaField();
			ORDER_NOField.Name = "ORDER_NO";
			ORDER_NOField.Type = typeof(string).ToString();
			ORDER_NOField.Index = 3;
			fields.Add(ORDER_NOField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 4;
			fields.Add(PART_NOField);
			 
			DataSchemaField ORDER_DATEField = new DataSchemaField();
			ORDER_DATEField.Name = "ORDER_DATE";
			ORDER_DATEField.Type = typeof(DateTime).ToString();
			ORDER_DATEField.Index = 5;
			fields.Add(ORDER_DATEField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 6;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField QTYField = new DataSchemaField();
			QTYField.Name = "QTY";
			QTYField.Type = typeof(int).ToString();
			QTYField.Index = 7;
			fields.Add(QTYField);
			 
			DataSchemaField ONLINE_TIMEField = new DataSchemaField();
			ONLINE_TIMEField.Name = "ONLINE_TIME";
			ONLINE_TIMEField.Type = typeof(DateTime).ToString();
			ONLINE_TIMEField.Index = 8;
			fields.Add(ONLINE_TIMEField);
			 
			DataSchemaField DOWN_LINE_TIMEField = new DataSchemaField();
			DOWN_LINE_TIMEField.Name = "DOWN_LINE_TIME";
			DOWN_LINE_TIMEField.Type = typeof(DateTime).ToString();
			DOWN_LINE_TIMEField.Index = 9;
			fields.Add(DOWN_LINE_TIMEField);
			 
			DataSchemaField MODEL_YEARField = new DataSchemaField();
			MODEL_YEARField.Name = "MODEL_YEAR";
			MODEL_YEARField.Type = typeof(string).ToString();
			MODEL_YEARField.Index = 10;
			fields.Add(MODEL_YEARField);
			 
			DataSchemaField LOCK_FLAGField = new DataSchemaField();
			LOCK_FLAGField.Name = "LOCK_FLAG";
			LOCK_FLAGField.Type = typeof(bool).ToString();
			LOCK_FLAGField.Index = 11;
			fields.Add(LOCK_FLAGField);
			 
			DataSchemaField SEQField = new DataSchemaField();
			SEQField.Name = "SEQ";
			SEQField.Type = typeof(int).ToString();
			SEQField.Index = 12;
			fields.Add(SEQField);
			 
			DataSchemaField WERKSField = new DataSchemaField();
			WERKSField.Name = "WERKS";
			WERKSField.Type = typeof(string).ToString();
			WERKSField.Index = 13;
			fields.Add(WERKSField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 14;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 15;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 16;
			fields.Add(COMMENTSField);
			 
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
		public Guid? LogFid{ get;set; }		
				
		[DataMember]
		public string OrderNo{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public DateTime? OrderDate{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public int? Qty{ get;set; }		
				
		[DataMember]
		public DateTime? OnlineTime{ get;set; }		
				
		[DataMember]
		public DateTime? DownLineTime{ get;set; }		
				
		[DataMember]
		public string ModelYear{ get;set; }		
				
		[DataMember]
		public bool? LockFlag{ get;set; }		
				
		[DataMember]
		public int? Seq{ get;set; }		
				
		[DataMember]
		public string Werks{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public bool ValidFlag{ get;set; }		
				
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
			WmsVmiProductOrderInfo info = new WmsVmiProductOrderInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LogFid = this.LogFid;
			info.OrderNo = this.OrderNo;
			info.PartNo = this.PartNo;
			info.OrderDate = this.OrderDate;
			info.AssemblyLine = this.AssemblyLine;
			info.Qty = this.Qty;
			info.OnlineTime = this.OnlineTime;
			info.DownLineTime = this.DownLineTime;
			info.ModelYear = this.ModelYear;
			info.LockFlag = this.LockFlag;
			info.Seq = this.Seq;
			info.Werks = this.Werks;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public WmsVmiProductOrderInfo Clone()
		{
			return ((ICloneable) this).Clone() as WmsVmiProductOrderInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// WmsVmiProductOrderInfoCollection对应表[TI_IFM_WMS_VMI_PRODUCT_ORDER]
    /// </summary>
	public partial class WmsVmiProductOrderInfoCollection : BusinessObjectCollection<WmsVmiProductOrderInfo>
	{
		public WmsVmiProductOrderInfoCollection():base("TI_IFM_WMS_VMI_PRODUCT_ORDER"){}	
	}
}
