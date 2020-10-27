#region Declaim
//---------------------------------------------------------------------------
// Name:		PlanPullOrderDetailInfo
// Function: 	Expose data in table PlanPullOrderDetail from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月6日
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
    /// PlanPullOrderDetailInfo对应表[TT_MPM_PLAN_PULL_ORDER_DETAIL]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PlanPullOrderDetailInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PlanPullOrderDetailInfo( 
					long aId,

					Guid aFid,

					Guid aOrderFid,

					int aOrderStatus,

					string aOrderCode,

					string aSupplierNum,

					string aPartNo,

					string aPartCname,

					string aPartEname,

					string aMeasuringUnitNo,

					decimal aInboundPackageQty,

					string aInboundPackageModel,

					int aRequiredPackageQty,

					decimal aRequiredPartQty,

					decimal aAsnDraftQty,

					decimal aAsnConfirmQty,

					int aActualPackageQty,

					decimal aActualPartQty,

					string aComments,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					int aInspectionMode

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			OrderFid = aOrderFid;
		 
			OrderStatus = aOrderStatus;
		 
			OrderCode = aOrderCode;
		 
			SupplierNum = aSupplierNum;
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			PartEname = aPartEname;
		 
			MeasuringUnitNo = aMeasuringUnitNo;
		 
			InboundPackageQty = aInboundPackageQty;
		 
			InboundPackageModel = aInboundPackageModel;
		 
			RequiredPackageQty = aRequiredPackageQty;
		 
			RequiredPartQty = aRequiredPartQty;
		 
			AsnDraftQty = aAsnDraftQty;
		 
			AsnConfirmQty = aAsnConfirmQty;
		 
			ActualPackageQty = aActualPackageQty;
		 
			ActualPartQty = aActualPartQty;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			InspectionMode = aInspectionMode;
		}
		
		public PlanPullOrderDetailInfo():base("TT_MPM_PLAN_PULL_ORDER_DETAIL")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                        _Keys = keys.ToArray();
			
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
			 
			DataSchemaField ORDER_STATUSField = new DataSchemaField();
			ORDER_STATUSField.Name = "ORDER_STATUS";
			ORDER_STATUSField.Type = typeof(int).ToString();
			ORDER_STATUSField.Index = 3;
			fields.Add(ORDER_STATUSField);
			 
			DataSchemaField ORDER_CODEField = new DataSchemaField();
			ORDER_CODEField.Name = "ORDER_CODE";
			ORDER_CODEField.Type = typeof(string).ToString();
			ORDER_CODEField.Index = 4;
			fields.Add(ORDER_CODEField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 5;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 6;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_CNAMEField = new DataSchemaField();
			PART_CNAMEField.Name = "PART_CNAME";
			PART_CNAMEField.Type = typeof(string).ToString();
			PART_CNAMEField.Index = 7;
			fields.Add(PART_CNAMEField);
			 
			DataSchemaField PART_ENAMEField = new DataSchemaField();
			PART_ENAMEField.Name = "PART_ENAME";
			PART_ENAMEField.Type = typeof(string).ToString();
			PART_ENAMEField.Index = 8;
			fields.Add(PART_ENAMEField);
			 
			DataSchemaField MEASURING_UNIT_NOField = new DataSchemaField();
			MEASURING_UNIT_NOField.Name = "MEASURING_UNIT_NO";
			MEASURING_UNIT_NOField.Type = typeof(string).ToString();
			MEASURING_UNIT_NOField.Index = 9;
			fields.Add(MEASURING_UNIT_NOField);
			 
			DataSchemaField INBOUND_PACKAGE_QTYField = new DataSchemaField();
			INBOUND_PACKAGE_QTYField.Name = "INBOUND_PACKAGE_QTY";
			INBOUND_PACKAGE_QTYField.Type = typeof(decimal).ToString();
			INBOUND_PACKAGE_QTYField.Index = 10;
			fields.Add(INBOUND_PACKAGE_QTYField);
			 
			DataSchemaField INBOUND_PACKAGE_MODELField = new DataSchemaField();
			INBOUND_PACKAGE_MODELField.Name = "INBOUND_PACKAGE_MODEL";
			INBOUND_PACKAGE_MODELField.Type = typeof(string).ToString();
			INBOUND_PACKAGE_MODELField.Index = 11;
			fields.Add(INBOUND_PACKAGE_MODELField);
			 
			DataSchemaField REQUIRED_PACKAGE_QTYField = new DataSchemaField();
			REQUIRED_PACKAGE_QTYField.Name = "REQUIRED_PACKAGE_QTY";
			REQUIRED_PACKAGE_QTYField.Type = typeof(int).ToString();
			REQUIRED_PACKAGE_QTYField.Index = 12;
			fields.Add(REQUIRED_PACKAGE_QTYField);
			 
			DataSchemaField REQUIRED_PART_QTYField = new DataSchemaField();
			REQUIRED_PART_QTYField.Name = "REQUIRED_PART_QTY";
			REQUIRED_PART_QTYField.Type = typeof(decimal).ToString();
			REQUIRED_PART_QTYField.Index = 13;
			fields.Add(REQUIRED_PART_QTYField);
			 
			DataSchemaField ASN_DRAFT_QTYField = new DataSchemaField();
			ASN_DRAFT_QTYField.Name = "ASN_DRAFT_QTY";
			ASN_DRAFT_QTYField.Type = typeof(decimal).ToString();
			ASN_DRAFT_QTYField.Index = 14;
			fields.Add(ASN_DRAFT_QTYField);
			 
			DataSchemaField ASN_CONFIRM_QTYField = new DataSchemaField();
			ASN_CONFIRM_QTYField.Name = "ASN_CONFIRM_QTY";
			ASN_CONFIRM_QTYField.Type = typeof(decimal).ToString();
			ASN_CONFIRM_QTYField.Index = 15;
			fields.Add(ASN_CONFIRM_QTYField);
			 
			DataSchemaField ACTUAL_PACKAGE_QTYField = new DataSchemaField();
			ACTUAL_PACKAGE_QTYField.Name = "ACTUAL_PACKAGE_QTY";
			ACTUAL_PACKAGE_QTYField.Type = typeof(int).ToString();
			ACTUAL_PACKAGE_QTYField.Index = 16;
			fields.Add(ACTUAL_PACKAGE_QTYField);
			 
			DataSchemaField ACTUAL_PART_QTYField = new DataSchemaField();
			ACTUAL_PART_QTYField.Name = "ACTUAL_PART_QTY";
			ACTUAL_PART_QTYField.Type = typeof(decimal).ToString();
			ACTUAL_PART_QTYField.Index = 17;
			fields.Add(ACTUAL_PART_QTYField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 18;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 19;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 20;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 21;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 22;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 23;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField INSPECTION_MODEField = new DataSchemaField();
			INSPECTION_MODEField.Name = "INSPECTION_MODE";
			INSPECTION_MODEField.Type = typeof(int).ToString();
			INSPECTION_MODEField.Index = 24;
			fields.Add(INSPECTION_MODEField);
						
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
		public int? OrderStatus{ get;set; }		
				
		[DataMember]
		public string OrderCode{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public string PartEname{ get;set; }		
				
		[DataMember]
		public string MeasuringUnitNo{ get;set; }		
				
		[DataMember]
		public decimal? InboundPackageQty{ get;set; }		
				
		[DataMember]
		public string InboundPackageModel{ get;set; }		
				
		[DataMember]
		public int? RequiredPackageQty{ get;set; }		
				
		[DataMember]
		public decimal? RequiredPartQty{ get;set; }		
				
		[DataMember]
		public decimal? AsnDraftQty{ get;set; }		
				
		[DataMember]
		public decimal? AsnConfirmQty{ get;set; }		
				
		[DataMember]
		public int? ActualPackageQty{ get;set; }		
				
		[DataMember]
		public decimal? ActualPartQty{ get;set; }		
				
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
				
		[DataMember]
		public int? InspectionMode{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PlanPullOrderDetailInfo info = new PlanPullOrderDetailInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.OrderFid = this.OrderFid;
			info.OrderStatus = this.OrderStatus;
			info.OrderCode = this.OrderCode;
			info.SupplierNum = this.SupplierNum;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.PartEname = this.PartEname;
			info.MeasuringUnitNo = this.MeasuringUnitNo;
			info.InboundPackageQty = this.InboundPackageQty;
			info.InboundPackageModel = this.InboundPackageModel;
			info.RequiredPackageQty = this.RequiredPackageQty;
			info.RequiredPartQty = this.RequiredPartQty;
			info.AsnDraftQty = this.AsnDraftQty;
			info.AsnConfirmQty = this.AsnConfirmQty;
			info.ActualPackageQty = this.ActualPackageQty;
			info.ActualPartQty = this.ActualPartQty;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.InspectionMode = this.InspectionMode;
			return info;			
		}
		 
		public PlanPullOrderDetailInfo Clone()
		{
			return ((ICloneable) this).Clone() as PlanPullOrderDetailInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PlanPullOrderDetailInfoCollection对应表[TT_MPM_PLAN_PULL_ORDER_DETAIL]
    /// </summary>
	public partial class PlanPullOrderDetailInfoCollection : BusinessObjectCollection<PlanPullOrderDetailInfo>
	{
		public PlanPullOrderDetailInfoCollection():base("TT_MPM_PLAN_PULL_ORDER_DETAIL"){}	
	}
}