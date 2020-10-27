#region Declaim
//---------------------------------------------------------------------------
// Name:		SrmJisPullOrderDetailInfo
// Function: 	Expose data in table SrmJisPullOrderDetail from database as business object to MES system.
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
    /// SrmJisPullOrderDetailInfo对应表[TI_IFM_SRM_JIS_PULL_ORDER_DETAIL]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SrmJisPullOrderDetailInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SrmJisPullOrderDetailInfo( 
					long aId,

					Guid aFid,

					Guid aOrderFid,

					int aRowNo,

					string aOrderCode,

					int aVehicleSeqNo,

					string aPartNo,

					decimal aPartQty,

					string aVehicleModelNo,

					string aVincode,

					string aCheckMode,

					string aRemark,

					int aProcessFlag,

					DateTime aProcessTime,

					bool aValidFlag,

					DateTime aCreateDate,

					string aCreateUser,

					DateTime aModifyDate,

					string aModifyUser,

					string aComments

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			OrderFid = aOrderFid;
		 
			RowNo = aRowNo;
		 
			OrderCode = aOrderCode;
		 
			VehicleSeqNo = aVehicleSeqNo;
		 
			PartNo = aPartNo;
		 
			PartQty = aPartQty;
		 
			VehicleModelNo = aVehicleModelNo;
		 
			Vincode = aVincode;
		 
			CheckMode = aCheckMode;
		 
			Remark = aRemark;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			ValidFlag = aValidFlag;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		 
			Comments = aComments;
		}
		
		public SrmJisPullOrderDetailInfo():base("TI_IFM_SRM_JIS_PULL_ORDER_DETAIL")
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
			 
			DataSchemaField ORDER_FIDField = new DataSchemaField();
			ORDER_FIDField.Name = "ORDER_FID";
			ORDER_FIDField.Type = typeof(Guid).ToString();
			ORDER_FIDField.Index = 2;
			fields.Add(ORDER_FIDField);
			 
			DataSchemaField ROW_NOField = new DataSchemaField();
			ROW_NOField.Name = "ROW_NO";
			ROW_NOField.Type = typeof(int).ToString();
			ROW_NOField.Index = 3;
			fields.Add(ROW_NOField);
			 
			DataSchemaField ORDER_CODEField = new DataSchemaField();
			ORDER_CODEField.Name = "ORDER_CODE";
			ORDER_CODEField.Type = typeof(string).ToString();
			ORDER_CODEField.Index = 4;
			fields.Add(ORDER_CODEField);
			 
			DataSchemaField VEHICLE_SEQ_NOField = new DataSchemaField();
			VEHICLE_SEQ_NOField.Name = "VEHICLE_SEQ_NO";
			VEHICLE_SEQ_NOField.Type = typeof(int).ToString();
			VEHICLE_SEQ_NOField.Index = 5;
			fields.Add(VEHICLE_SEQ_NOField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 6;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_QTYField = new DataSchemaField();
			PART_QTYField.Name = "PART_QTY";
			PART_QTYField.Type = typeof(decimal).ToString();
			PART_QTYField.Index = 7;
			fields.Add(PART_QTYField);
			 
			DataSchemaField VEHICLE_MODEL_NOField = new DataSchemaField();
			VEHICLE_MODEL_NOField.Name = "VEHICLE_MODEL_NO";
			VEHICLE_MODEL_NOField.Type = typeof(string).ToString();
			VEHICLE_MODEL_NOField.Index = 8;
			fields.Add(VEHICLE_MODEL_NOField);
			 
			DataSchemaField VINCODEField = new DataSchemaField();
			VINCODEField.Name = "VINCODE";
			VINCODEField.Type = typeof(string).ToString();
			VINCODEField.Index = 9;
			fields.Add(VINCODEField);
			 
			DataSchemaField CHECK_MODEField = new DataSchemaField();
			CHECK_MODEField.Name = "CHECK_MODE";
			CHECK_MODEField.Type = typeof(string).ToString();
			CHECK_MODEField.Index = 10;
			fields.Add(CHECK_MODEField);
			 
			DataSchemaField REMARKField = new DataSchemaField();
			REMARKField.Name = "REMARK";
			REMARKField.Type = typeof(string).ToString();
			REMARKField.Index = 11;
			fields.Add(REMARKField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 12;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 13;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 14;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 15;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 16;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 17;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 18;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 19;
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
		public Guid? OrderFid{ get;set; }		
				
		[DataMember]
		public int? RowNo{ get;set; }		
				
		[DataMember]
		public string OrderCode{ get;set; }		
				
		[DataMember]
		public int? VehicleSeqNo{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public decimal? PartQty{ get;set; }		
				
		[DataMember]
		public string VehicleModelNo{ get;set; }		
				
		[DataMember]
		public string Vincode{ get;set; }		
				
		[DataMember]
		public string CheckMode{ get;set; }		
				
		[DataMember]
		public string Remark{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			SrmJisPullOrderDetailInfo info = new SrmJisPullOrderDetailInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.OrderFid = this.OrderFid;
			info.RowNo = this.RowNo;
			info.OrderCode = this.OrderCode;
			info.VehicleSeqNo = this.VehicleSeqNo;
			info.PartNo = this.PartNo;
			info.PartQty = this.PartQty;
			info.VehicleModelNo = this.VehicleModelNo;
			info.Vincode = this.Vincode;
			info.CheckMode = this.CheckMode;
			info.Remark = this.Remark;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.ValidFlag = this.ValidFlag;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			info.Comments = this.Comments;
			return info;			
		}
		 
		public SrmJisPullOrderDetailInfo Clone()
		{
			return ((ICloneable) this).Clone() as SrmJisPullOrderDetailInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SrmJisPullOrderDetailInfoCollection对应表[TI_IFM_SRM_JIS_PULL_ORDER_DETAIL]
    /// </summary>
	public partial class SrmJisPullOrderDetailInfoCollection : BusinessObjectCollection<SrmJisPullOrderDetailInfo>
	{
		public SrmJisPullOrderDetailInfoCollection():base("TI_IFM_SRM_JIS_PULL_ORDER_DETAIL"){}	
	}
}
