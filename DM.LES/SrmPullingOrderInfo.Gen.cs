#region Declaim
//---------------------------------------------------------------------------
// Name:		SrmPullingOrderInfo
// Function: 	Expose data in table SrmPullingOrder from database as business object to MES system.
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
    /// SrmPullingOrderInfo对应表[TI_IFM_SRM_PULLING_ORDER]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SrmPullingOrderInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SrmPullingOrderInfo( 
					long aId,

					Guid aFid,

					string aOrderNo,

					string aPlant,

					string aSupplierNum,

					string aSourceZoneNo,

					string aKeeper,

					string aTargetZoneNo,

					string aDock,

					string aPartBoxCode,

					int aOrderType,

					DateTime aPublishTime,

					string aPartBoxName,

					string aSupplierName,

					DateTime aPlanShippingTime,

					DateTime aPlanDeliveryTime,

					string aRemark,

					bool aAsnFlag,

					bool aEmergencyFlag,

					bool aInspectFlag,

					int aProcessFlag,

					DateTime aProcessTime,

					Guid aLogFid,

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
		 
			OrderNo = aOrderNo;
		 
			Plant = aPlant;
		 
			SupplierNum = aSupplierNum;
		 
			SourceZoneNo = aSourceZoneNo;
		 
			Keeper = aKeeper;
		 
			TargetZoneNo = aTargetZoneNo;
		 
			Dock = aDock;
		 
			PartBoxCode = aPartBoxCode;
		 
			OrderType = aOrderType;
		 
			PublishTime = aPublishTime;
		 
			PartBoxName = aPartBoxName;
		 
			SupplierName = aSupplierName;
		 
			PlanShippingTime = aPlanShippingTime;
		 
			PlanDeliveryTime = aPlanDeliveryTime;
		 
			Remark = aRemark;
		 
			AsnFlag = aAsnFlag;
		 
			EmergencyFlag = aEmergencyFlag;
		 
			InspectFlag = aInspectFlag;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			LogFid = aLogFid;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			Comments = aComments;
		}
		
		public SrmPullingOrderInfo():base("TI_IFM_SRM_PULLING_ORDER")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                            _Keys = keys.ToArray();
			
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
			 
			DataSchemaField ORDER_NOField = new DataSchemaField();
			ORDER_NOField.Name = "ORDER_NO";
			ORDER_NOField.Type = typeof(string).ToString();
			ORDER_NOField.Index = 2;
			fields.Add(ORDER_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 3;
			fields.Add(PLANTField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 4;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField SOURCE_ZONE_NOField = new DataSchemaField();
			SOURCE_ZONE_NOField.Name = "SOURCE_ZONE_NO";
			SOURCE_ZONE_NOField.Type = typeof(string).ToString();
			SOURCE_ZONE_NOField.Index = 5;
			fields.Add(SOURCE_ZONE_NOField);
			 
			DataSchemaField KEEPERField = new DataSchemaField();
			KEEPERField.Name = "KEEPER";
			KEEPERField.Type = typeof(string).ToString();
			KEEPERField.Index = 6;
			fields.Add(KEEPERField);
			 
			DataSchemaField TARGET_ZONE_NOField = new DataSchemaField();
			TARGET_ZONE_NOField.Name = "TARGET_ZONE_NO";
			TARGET_ZONE_NOField.Type = typeof(string).ToString();
			TARGET_ZONE_NOField.Index = 7;
			fields.Add(TARGET_ZONE_NOField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 8;
			fields.Add(DOCKField);
			 
			DataSchemaField PART_BOX_CODEField = new DataSchemaField();
			PART_BOX_CODEField.Name = "PART_BOX_CODE";
			PART_BOX_CODEField.Type = typeof(string).ToString();
			PART_BOX_CODEField.Index = 9;
			fields.Add(PART_BOX_CODEField);
			 
			DataSchemaField ORDER_TYPEField = new DataSchemaField();
			ORDER_TYPEField.Name = "ORDER_TYPE";
			ORDER_TYPEField.Type = typeof(int).ToString();
			ORDER_TYPEField.Index = 10;
			fields.Add(ORDER_TYPEField);
			 
			DataSchemaField PUBLISH_TIMEField = new DataSchemaField();
			PUBLISH_TIMEField.Name = "PUBLISH_TIME";
			PUBLISH_TIMEField.Type = typeof(DateTime).ToString();
			PUBLISH_TIMEField.Index = 11;
			fields.Add(PUBLISH_TIMEField);
			 
			DataSchemaField PART_BOX_NAMEField = new DataSchemaField();
			PART_BOX_NAMEField.Name = "PART_BOX_NAME";
			PART_BOX_NAMEField.Type = typeof(string).ToString();
			PART_BOX_NAMEField.Index = 12;
			fields.Add(PART_BOX_NAMEField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 13;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField PLAN_SHIPPING_TIMEField = new DataSchemaField();
			PLAN_SHIPPING_TIMEField.Name = "PLAN_SHIPPING_TIME";
			PLAN_SHIPPING_TIMEField.Type = typeof(DateTime).ToString();
			PLAN_SHIPPING_TIMEField.Index = 14;
			fields.Add(PLAN_SHIPPING_TIMEField);
			 
			DataSchemaField PLAN_DELIVERY_TIMEField = new DataSchemaField();
			PLAN_DELIVERY_TIMEField.Name = "PLAN_DELIVERY_TIME";
			PLAN_DELIVERY_TIMEField.Type = typeof(DateTime).ToString();
			PLAN_DELIVERY_TIMEField.Index = 15;
			fields.Add(PLAN_DELIVERY_TIMEField);
			 
			DataSchemaField REMARKField = new DataSchemaField();
			REMARKField.Name = "REMARK";
			REMARKField.Type = typeof(string).ToString();
			REMARKField.Index = 16;
			fields.Add(REMARKField);
			 
			DataSchemaField ASN_FLAGField = new DataSchemaField();
			ASN_FLAGField.Name = "ASN_FLAG";
			ASN_FLAGField.Type = typeof(bool).ToString();
			ASN_FLAGField.Index = 17;
			fields.Add(ASN_FLAGField);
			 
			DataSchemaField EMERGENCY_FLAGField = new DataSchemaField();
			EMERGENCY_FLAGField.Name = "EMERGENCY_FLAG";
			EMERGENCY_FLAGField.Type = typeof(bool).ToString();
			EMERGENCY_FLAGField.Index = 18;
			fields.Add(EMERGENCY_FLAGField);
			 
			DataSchemaField INSPECT_FLAGField = new DataSchemaField();
			INSPECT_FLAGField.Name = "INSPECT_FLAG";
			INSPECT_FLAGField.Type = typeof(bool).ToString();
			INSPECT_FLAGField.Index = 19;
			fields.Add(INSPECT_FLAGField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 20;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 21;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField LOG_FIDField = new DataSchemaField();
			LOG_FIDField.Name = "LOG_FID";
			LOG_FIDField.Type = typeof(Guid).ToString();
			LOG_FIDField.Index = 22;
			fields.Add(LOG_FIDField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 23;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 24;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 25;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 26;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 27;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 28;
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
		public string OrderNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string SourceZoneNo{ get;set; }		
				
		[DataMember]
		public string Keeper{ get;set; }		
				
		[DataMember]
		public string TargetZoneNo{ get;set; }		
				
		[DataMember]
		public string Dock{ get;set; }		
				
		[DataMember]
		public string PartBoxCode{ get;set; }		
				
		[DataMember]
		public int? OrderType{ get;set; }		
				
		[DataMember]
		public DateTime? PublishTime{ get;set; }		
				
		[DataMember]
		public string PartBoxName{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public DateTime? PlanShippingTime{ get;set; }		
				
		[DataMember]
		public DateTime? PlanDeliveryTime{ get;set; }		
				
		[DataMember]
		public string Remark{ get;set; }		
				
		[DataMember]
		public bool? AsnFlag{ get;set; }		
				
		[DataMember]
		public bool? EmergencyFlag{ get;set; }		
				
		[DataMember]
		public bool? InspectFlag{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public Guid? LogFid{ get;set; }		
				
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
		public string Comments{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			SrmPullingOrderInfo info = new SrmPullingOrderInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.OrderNo = this.OrderNo;
			info.Plant = this.Plant;
			info.SupplierNum = this.SupplierNum;
			info.SourceZoneNo = this.SourceZoneNo;
			info.Keeper = this.Keeper;
			info.TargetZoneNo = this.TargetZoneNo;
			info.Dock = this.Dock;
			info.PartBoxCode = this.PartBoxCode;
			info.OrderType = this.OrderType;
			info.PublishTime = this.PublishTime;
			info.PartBoxName = this.PartBoxName;
			info.SupplierName = this.SupplierName;
			info.PlanShippingTime = this.PlanShippingTime;
			info.PlanDeliveryTime = this.PlanDeliveryTime;
			info.Remark = this.Remark;
			info.AsnFlag = this.AsnFlag;
			info.EmergencyFlag = this.EmergencyFlag;
			info.InspectFlag = this.InspectFlag;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.LogFid = this.LogFid;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.Comments = this.Comments;
			return info;			
		}
		 
		public SrmPullingOrderInfo Clone()
		{
			return ((ICloneable) this).Clone() as SrmPullingOrderInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SrmPullingOrderInfoCollection对应表[TI_IFM_SRM_PULLING_ORDER]
    /// </summary>
	public partial class SrmPullingOrderInfoCollection : BusinessObjectCollection<SrmPullingOrderInfo>
	{
		public SrmPullingOrderInfoCollection():base("TI_IFM_SRM_PULLING_ORDER"){}	
	}
}
