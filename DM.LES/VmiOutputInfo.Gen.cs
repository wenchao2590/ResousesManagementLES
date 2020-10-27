#region Declaim
//---------------------------------------------------------------------------
// Name:		VmiOutputInfo
// Function: 	Expose data in table VmiOutput from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月12日
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
    /// VmiOutputInfo对应表[TT_WMM_VMI_OUTPUT]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class VmiOutputInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public VmiOutputInfo( 
					long aId,

					Guid aFid,

					string aOutputNo,

					string aPlant,

					string aSupplierNum,

					string aWmNo,

					string aZoneNo,

					string aTWmNo,

					string aTZoneNo,

					string aTDock,

					string aPartBoxCode,

					DateTime aSendTime,

					int aOutputType,

					DateTime aTranTime,

					string aOutputReason,

					string aBookKeeper,

					int aConfirmFlag,

					string aPlanNo,

					string aAsnNo,

					string aRunsheetNo,

					string aAssemblyLine,

					string aPlantZone,

					string aWorkshop,

					string aTransSupplierNum,

					int aPartType,

					int aSupplierType,

					string aRunsheetCode,

					int aErpFlag,

					string aLogicalPk,

					string aBusinessPk,

					string aRoute,

					DateTime aRequestTime,

					string aCustCode,

					string aCustName,

					string aCostCenter,

					Guid aOrganizationFid,

					string aConfirmUser,

					DateTime aConfirmDate,

					string aLiableUser,

					DateTime aLiableDate,

					string aFinanceUser,

					DateTime aFinanceDate,

					decimal aSumPartQty,

					decimal aSumOfPrice,

					int aStatus,

					string aConveyance,

					string aCarrierTel,

					decimal aSumWeight,

					decimal aSumVolume,

					DateTime aPlanShippingTime,

					DateTime aPlanDeliveryTime,

					int aPrintCount,

					DateTime aPrintTime,

					string aComments,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					string aLastPrintUser,

					int aSumPackageQty,

					int aPullMode

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			OutputNo = aOutputNo;
		 
			Plant = aPlant;
		 
			SupplierNum = aSupplierNum;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			TWmNo = aTWmNo;
		 
			TZoneNo = aTZoneNo;
		 
			TDock = aTDock;
		 
			PartBoxCode = aPartBoxCode;
		 
			SendTime = aSendTime;
		 
			OutputType = aOutputType;
		 
			TranTime = aTranTime;
		 
			OutputReason = aOutputReason;
		 
			BookKeeper = aBookKeeper;
		 
			ConfirmFlag = aConfirmFlag;
		 
			PlanNo = aPlanNo;
		 
			AsnNo = aAsnNo;
		 
			RunsheetNo = aRunsheetNo;
		 
			AssemblyLine = aAssemblyLine;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			TransSupplierNum = aTransSupplierNum;
		 
			PartType = aPartType;
		 
			SupplierType = aSupplierType;
		 
			RunsheetCode = aRunsheetCode;
		 
			ErpFlag = aErpFlag;
		 
			LogicalPk = aLogicalPk;
		 
			BusinessPk = aBusinessPk;
		 
			Route = aRoute;
		 
			RequestTime = aRequestTime;
		 
			CustCode = aCustCode;
		 
			CustName = aCustName;
		 
			CostCenter = aCostCenter;
		 
			OrganizationFid = aOrganizationFid;
		 
			ConfirmUser = aConfirmUser;
		 
			ConfirmDate = aConfirmDate;
		 
			LiableUser = aLiableUser;
		 
			LiableDate = aLiableDate;
		 
			FinanceUser = aFinanceUser;
		 
			FinanceDate = aFinanceDate;
		 
			SumPartQty = aSumPartQty;
		 
			SumOfPrice = aSumOfPrice;
		 
			Status = aStatus;
		 
			Conveyance = aConveyance;
		 
			CarrierTel = aCarrierTel;
		 
			SumWeight = aSumWeight;
		 
			SumVolume = aSumVolume;
		 
			PlanShippingTime = aPlanShippingTime;
		 
			PlanDeliveryTime = aPlanDeliveryTime;
		 
			PrintCount = aPrintCount;
		 
			PrintTime = aPrintTime;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			LastPrintUser = aLastPrintUser;
		 
			SumPackageQty = aSumPackageQty;
		 
			PullMode = aPullMode;
		}
		
		public VmiOutputInfo():base("TT_WMM_VMI_OUTPUT")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                                                             _Keys = keys.ToArray();
			
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
			 
			DataSchemaField OUTPUT_NOField = new DataSchemaField();
			OUTPUT_NOField.Name = "OUTPUT_NO";
			OUTPUT_NOField.Type = typeof(string).ToString();
			OUTPUT_NOField.Index = 2;
			fields.Add(OUTPUT_NOField);
			 
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
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 5;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 6;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField T_WM_NOField = new DataSchemaField();
			T_WM_NOField.Name = "T_WM_NO";
			T_WM_NOField.Type = typeof(string).ToString();
			T_WM_NOField.Index = 7;
			fields.Add(T_WM_NOField);
			 
			DataSchemaField T_ZONE_NOField = new DataSchemaField();
			T_ZONE_NOField.Name = "T_ZONE_NO";
			T_ZONE_NOField.Type = typeof(string).ToString();
			T_ZONE_NOField.Index = 8;
			fields.Add(T_ZONE_NOField);
			 
			DataSchemaField T_DOCKField = new DataSchemaField();
			T_DOCKField.Name = "T_DOCK";
			T_DOCKField.Type = typeof(string).ToString();
			T_DOCKField.Index = 9;
			fields.Add(T_DOCKField);
			 
			DataSchemaField PART_BOX_CODEField = new DataSchemaField();
			PART_BOX_CODEField.Name = "PART_BOX_CODE";
			PART_BOX_CODEField.Type = typeof(string).ToString();
			PART_BOX_CODEField.Index = 10;
			fields.Add(PART_BOX_CODEField);
			 
			DataSchemaField SEND_TIMEField = new DataSchemaField();
			SEND_TIMEField.Name = "SEND_TIME";
			SEND_TIMEField.Type = typeof(DateTime).ToString();
			SEND_TIMEField.Index = 11;
			fields.Add(SEND_TIMEField);
			 
			DataSchemaField OUTPUT_TYPEField = new DataSchemaField();
			OUTPUT_TYPEField.Name = "OUTPUT_TYPE";
			OUTPUT_TYPEField.Type = typeof(int).ToString();
			OUTPUT_TYPEField.Index = 12;
			fields.Add(OUTPUT_TYPEField);
			 
			DataSchemaField TRAN_TIMEField = new DataSchemaField();
			TRAN_TIMEField.Name = "TRAN_TIME";
			TRAN_TIMEField.Type = typeof(DateTime).ToString();
			TRAN_TIMEField.Index = 13;
			fields.Add(TRAN_TIMEField);
			 
			DataSchemaField OUTPUT_REASONField = new DataSchemaField();
			OUTPUT_REASONField.Name = "OUTPUT_REASON";
			OUTPUT_REASONField.Type = typeof(string).ToString();
			OUTPUT_REASONField.Index = 14;
			fields.Add(OUTPUT_REASONField);
			 
			DataSchemaField BOOK_KEEPERField = new DataSchemaField();
			BOOK_KEEPERField.Name = "BOOK_KEEPER";
			BOOK_KEEPERField.Type = typeof(string).ToString();
			BOOK_KEEPERField.Index = 15;
			fields.Add(BOOK_KEEPERField);
			 
			DataSchemaField CONFIRM_FLAGField = new DataSchemaField();
			CONFIRM_FLAGField.Name = "CONFIRM_FLAG";
			CONFIRM_FLAGField.Type = typeof(int).ToString();
			CONFIRM_FLAGField.Index = 16;
			fields.Add(CONFIRM_FLAGField);
			 
			DataSchemaField PLAN_NOField = new DataSchemaField();
			PLAN_NOField.Name = "PLAN_NO";
			PLAN_NOField.Type = typeof(string).ToString();
			PLAN_NOField.Index = 17;
			fields.Add(PLAN_NOField);
			 
			DataSchemaField ASN_NOField = new DataSchemaField();
			ASN_NOField.Name = "ASN_NO";
			ASN_NOField.Type = typeof(string).ToString();
			ASN_NOField.Index = 18;
			fields.Add(ASN_NOField);
			 
			DataSchemaField RUNSHEET_NOField = new DataSchemaField();
			RUNSHEET_NOField.Name = "RUNSHEET_NO";
			RUNSHEET_NOField.Type = typeof(string).ToString();
			RUNSHEET_NOField.Index = 19;
			fields.Add(RUNSHEET_NOField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 20;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 21;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 22;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField TRANS_SUPPLIER_NUMField = new DataSchemaField();
			TRANS_SUPPLIER_NUMField.Name = "TRANS_SUPPLIER_NUM";
			TRANS_SUPPLIER_NUMField.Type = typeof(string).ToString();
			TRANS_SUPPLIER_NUMField.Index = 23;
			fields.Add(TRANS_SUPPLIER_NUMField);
			 
			DataSchemaField PART_TYPEField = new DataSchemaField();
			PART_TYPEField.Name = "PART_TYPE";
			PART_TYPEField.Type = typeof(int).ToString();
			PART_TYPEField.Index = 24;
			fields.Add(PART_TYPEField);
			 
			DataSchemaField SUPPLIER_TYPEField = new DataSchemaField();
			SUPPLIER_TYPEField.Name = "SUPPLIER_TYPE";
			SUPPLIER_TYPEField.Type = typeof(int).ToString();
			SUPPLIER_TYPEField.Index = 25;
			fields.Add(SUPPLIER_TYPEField);
			 
			DataSchemaField RUNSHEET_CODEField = new DataSchemaField();
			RUNSHEET_CODEField.Name = "RUNSHEET_CODE";
			RUNSHEET_CODEField.Type = typeof(string).ToString();
			RUNSHEET_CODEField.Index = 26;
			fields.Add(RUNSHEET_CODEField);
			 
			DataSchemaField ERP_FLAGField = new DataSchemaField();
			ERP_FLAGField.Name = "ERP_FLAG";
			ERP_FLAGField.Type = typeof(int).ToString();
			ERP_FLAGField.Index = 27;
			fields.Add(ERP_FLAGField);
			 
			DataSchemaField LOGICAL_PKField = new DataSchemaField();
			LOGICAL_PKField.Name = "LOGICAL_PK";
			LOGICAL_PKField.Type = typeof(string).ToString();
			LOGICAL_PKField.Index = 28;
			fields.Add(LOGICAL_PKField);
			 
			DataSchemaField BUSINESS_PKField = new DataSchemaField();
			BUSINESS_PKField.Name = "BUSINESS_PK";
			BUSINESS_PKField.Type = typeof(string).ToString();
			BUSINESS_PKField.Index = 29;
			fields.Add(BUSINESS_PKField);
			 
			DataSchemaField ROUTEField = new DataSchemaField();
			ROUTEField.Name = "ROUTE";
			ROUTEField.Type = typeof(string).ToString();
			ROUTEField.Index = 30;
			fields.Add(ROUTEField);
			 
			DataSchemaField REQUEST_TIMEField = new DataSchemaField();
			REQUEST_TIMEField.Name = "REQUEST_TIME";
			REQUEST_TIMEField.Type = typeof(DateTime).ToString();
			REQUEST_TIMEField.Index = 31;
			fields.Add(REQUEST_TIMEField);
			 
			DataSchemaField CUST_CODEField = new DataSchemaField();
			CUST_CODEField.Name = "CUST_CODE";
			CUST_CODEField.Type = typeof(string).ToString();
			CUST_CODEField.Index = 32;
			fields.Add(CUST_CODEField);
			 
			DataSchemaField CUST_NAMEField = new DataSchemaField();
			CUST_NAMEField.Name = "CUST_NAME";
			CUST_NAMEField.Type = typeof(string).ToString();
			CUST_NAMEField.Index = 33;
			fields.Add(CUST_NAMEField);
			 
			DataSchemaField COST_CENTERField = new DataSchemaField();
			COST_CENTERField.Name = "COST_CENTER";
			COST_CENTERField.Type = typeof(string).ToString();
			COST_CENTERField.Index = 34;
			fields.Add(COST_CENTERField);
			 
			DataSchemaField ORGANIZATION_FIDField = new DataSchemaField();
			ORGANIZATION_FIDField.Name = "ORGANIZATION_FID";
			ORGANIZATION_FIDField.Type = typeof(Guid).ToString();
			ORGANIZATION_FIDField.Index = 35;
			fields.Add(ORGANIZATION_FIDField);
			 
			DataSchemaField CONFIRM_USERField = new DataSchemaField();
			CONFIRM_USERField.Name = "CONFIRM_USER";
			CONFIRM_USERField.Type = typeof(string).ToString();
			CONFIRM_USERField.Index = 36;
			fields.Add(CONFIRM_USERField);
			 
			DataSchemaField CONFIRM_DATEField = new DataSchemaField();
			CONFIRM_DATEField.Name = "CONFIRM_DATE";
			CONFIRM_DATEField.Type = typeof(DateTime).ToString();
			CONFIRM_DATEField.Index = 37;
			fields.Add(CONFIRM_DATEField);
			 
			DataSchemaField LIABLE_USERField = new DataSchemaField();
			LIABLE_USERField.Name = "LIABLE_USER";
			LIABLE_USERField.Type = typeof(string).ToString();
			LIABLE_USERField.Index = 38;
			fields.Add(LIABLE_USERField);
			 
			DataSchemaField LIABLE_DATEField = new DataSchemaField();
			LIABLE_DATEField.Name = "LIABLE_DATE";
			LIABLE_DATEField.Type = typeof(DateTime).ToString();
			LIABLE_DATEField.Index = 39;
			fields.Add(LIABLE_DATEField);
			 
			DataSchemaField FINANCE_USERField = new DataSchemaField();
			FINANCE_USERField.Name = "FINANCE_USER";
			FINANCE_USERField.Type = typeof(string).ToString();
			FINANCE_USERField.Index = 40;
			fields.Add(FINANCE_USERField);
			 
			DataSchemaField FINANCE_DATEField = new DataSchemaField();
			FINANCE_DATEField.Name = "FINANCE_DATE";
			FINANCE_DATEField.Type = typeof(DateTime).ToString();
			FINANCE_DATEField.Index = 41;
			fields.Add(FINANCE_DATEField);
			 
			DataSchemaField SUM_PART_QTYField = new DataSchemaField();
			SUM_PART_QTYField.Name = "SUM_PART_QTY";
			SUM_PART_QTYField.Type = typeof(decimal).ToString();
			SUM_PART_QTYField.Index = 42;
			fields.Add(SUM_PART_QTYField);
			 
			DataSchemaField SUM_OF_PRICEField = new DataSchemaField();
			SUM_OF_PRICEField.Name = "SUM_OF_PRICE";
			SUM_OF_PRICEField.Type = typeof(decimal).ToString();
			SUM_OF_PRICEField.Index = 43;
			fields.Add(SUM_OF_PRICEField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 44;
			fields.Add(STATUSField);
			 
			DataSchemaField CONVEYANCEField = new DataSchemaField();
			CONVEYANCEField.Name = "CONVEYANCE";
			CONVEYANCEField.Type = typeof(string).ToString();
			CONVEYANCEField.Index = 45;
			fields.Add(CONVEYANCEField);
			 
			DataSchemaField CARRIER_TELField = new DataSchemaField();
			CARRIER_TELField.Name = "CARRIER_TEL";
			CARRIER_TELField.Type = typeof(string).ToString();
			CARRIER_TELField.Index = 46;
			fields.Add(CARRIER_TELField);
			 
			DataSchemaField SUM_WEIGHTField = new DataSchemaField();
			SUM_WEIGHTField.Name = "SUM_WEIGHT";
			SUM_WEIGHTField.Type = typeof(decimal).ToString();
			SUM_WEIGHTField.Index = 47;
			fields.Add(SUM_WEIGHTField);
			 
			DataSchemaField SUM_VOLUMEField = new DataSchemaField();
			SUM_VOLUMEField.Name = "SUM_VOLUME";
			SUM_VOLUMEField.Type = typeof(decimal).ToString();
			SUM_VOLUMEField.Index = 48;
			fields.Add(SUM_VOLUMEField);
			 
			DataSchemaField PLAN_SHIPPING_TIMEField = new DataSchemaField();
			PLAN_SHIPPING_TIMEField.Name = "PLAN_SHIPPING_TIME";
			PLAN_SHIPPING_TIMEField.Type = typeof(DateTime).ToString();
			PLAN_SHIPPING_TIMEField.Index = 49;
			fields.Add(PLAN_SHIPPING_TIMEField);
			 
			DataSchemaField PLAN_DELIVERY_TIMEField = new DataSchemaField();
			PLAN_DELIVERY_TIMEField.Name = "PLAN_DELIVERY_TIME";
			PLAN_DELIVERY_TIMEField.Type = typeof(DateTime).ToString();
			PLAN_DELIVERY_TIMEField.Index = 50;
			fields.Add(PLAN_DELIVERY_TIMEField);
			 
			DataSchemaField PRINT_COUNTField = new DataSchemaField();
			PRINT_COUNTField.Name = "PRINT_COUNT";
			PRINT_COUNTField.Type = typeof(int).ToString();
			PRINT_COUNTField.Index = 51;
			fields.Add(PRINT_COUNTField);
			 
			DataSchemaField PRINT_TIMEField = new DataSchemaField();
			PRINT_TIMEField.Name = "PRINT_TIME";
			PRINT_TIMEField.Type = typeof(DateTime).ToString();
			PRINT_TIMEField.Index = 52;
			fields.Add(PRINT_TIMEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 53;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 54;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 55;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 56;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 57;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 58;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField LAST_PRINT_USERField = new DataSchemaField();
			LAST_PRINT_USERField.Name = "LAST_PRINT_USER";
			LAST_PRINT_USERField.Type = typeof(string).ToString();
			LAST_PRINT_USERField.Index = 59;
			fields.Add(LAST_PRINT_USERField);
			 
			DataSchemaField SUM_PACKAGE_QTYField = new DataSchemaField();
			SUM_PACKAGE_QTYField.Name = "SUM_PACKAGE_QTY";
			SUM_PACKAGE_QTYField.Type = typeof(int).ToString();
			SUM_PACKAGE_QTYField.Index = 60;
			fields.Add(SUM_PACKAGE_QTYField);
			 
			DataSchemaField PULL_MODEField = new DataSchemaField();
			PULL_MODEField.Name = "PULL_MODE";
			PULL_MODEField.Type = typeof(int).ToString();
			PULL_MODEField.Index = 61;
			fields.Add(PULL_MODEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string OutputNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public string TWmNo{ get;set; }		
				
		[DataMember]
		public string TZoneNo{ get;set; }		
				
		[DataMember]
		public string TDock{ get;set; }		
				
		[DataMember]
		public string PartBoxCode{ get;set; }		
				
		[DataMember]
		public DateTime? SendTime{ get;set; }		
				
		[DataMember]
		public int? OutputType{ get;set; }		
				
		[DataMember]
		public DateTime? TranTime{ get;set; }		
				
		[DataMember]
		public string OutputReason{ get;set; }		
				
		[DataMember]
		public string BookKeeper{ get;set; }		
				
		[DataMember]
		public int? ConfirmFlag{ get;set; }		
				
		[DataMember]
		public string PlanNo{ get;set; }		
				
		[DataMember]
		public string AsnNo{ get;set; }		
				
		[DataMember]
		public string RunsheetNo{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string TransSupplierNum{ get;set; }		
				
		[DataMember]
		public int? PartType{ get;set; }		
				
		[DataMember]
		public int? SupplierType{ get;set; }		
				
		[DataMember]
		public string RunsheetCode{ get;set; }		
				
		[DataMember]
		public int? ErpFlag{ get;set; }		
				
		[DataMember]
		public string LogicalPk{ get;set; }		
				
		[DataMember]
		public string BusinessPk{ get;set; }		
				
		[DataMember]
		public string Route{ get;set; }		
				
		[DataMember]
		public DateTime? RequestTime{ get;set; }		
				
		[DataMember]
		public string CustCode{ get;set; }		
				
		[DataMember]
		public string CustName{ get;set; }		
				
		[DataMember]
		public string CostCenter{ get;set; }		
				
		[DataMember]
		public Guid? OrganizationFid{ get;set; }		
				
		[DataMember]
		public string ConfirmUser{ get;set; }		
				
		[DataMember]
		public DateTime? ConfirmDate{ get;set; }		
				
		[DataMember]
		public string LiableUser{ get;set; }		
				
		[DataMember]
		public DateTime? LiableDate{ get;set; }		
				
		[DataMember]
		public string FinanceUser{ get;set; }		
				
		[DataMember]
		public DateTime? FinanceDate{ get;set; }		
				
		[DataMember]
		public decimal? SumPartQty{ get;set; }		
				
		[DataMember]
		public decimal? SumOfPrice{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		[DataMember]
		public string Conveyance{ get;set; }		
				
		[DataMember]
		public string CarrierTel{ get;set; }		
				
		[DataMember]
		public decimal? SumWeight{ get;set; }		
				
		[DataMember]
		public decimal? SumVolume{ get;set; }		
				
		[DataMember]
		public DateTime? PlanShippingTime{ get;set; }		
				
		[DataMember]
		public DateTime? PlanDeliveryTime{ get;set; }		
				
		[DataMember]
		public int? PrintCount{ get;set; }		
				
		[DataMember]
		public DateTime? PrintTime{ get;set; }		
				
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
				
		[DataMember]
		public string LastPrintUser{ get;set; }		
				
		[DataMember]
		public int? SumPackageQty{ get;set; }		
				
		[DataMember]
		public int? PullMode{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			VmiOutputInfo info = new VmiOutputInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.OutputNo = this.OutputNo;
			info.Plant = this.Plant;
			info.SupplierNum = this.SupplierNum;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.TWmNo = this.TWmNo;
			info.TZoneNo = this.TZoneNo;
			info.TDock = this.TDock;
			info.PartBoxCode = this.PartBoxCode;
			info.SendTime = this.SendTime;
			info.OutputType = this.OutputType;
			info.TranTime = this.TranTime;
			info.OutputReason = this.OutputReason;
			info.BookKeeper = this.BookKeeper;
			info.ConfirmFlag = this.ConfirmFlag;
			info.PlanNo = this.PlanNo;
			info.AsnNo = this.AsnNo;
			info.RunsheetNo = this.RunsheetNo;
			info.AssemblyLine = this.AssemblyLine;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.TransSupplierNum = this.TransSupplierNum;
			info.PartType = this.PartType;
			info.SupplierType = this.SupplierType;
			info.RunsheetCode = this.RunsheetCode;
			info.ErpFlag = this.ErpFlag;
			info.LogicalPk = this.LogicalPk;
			info.BusinessPk = this.BusinessPk;
			info.Route = this.Route;
			info.RequestTime = this.RequestTime;
			info.CustCode = this.CustCode;
			info.CustName = this.CustName;
			info.CostCenter = this.CostCenter;
			info.OrganizationFid = this.OrganizationFid;
			info.ConfirmUser = this.ConfirmUser;
			info.ConfirmDate = this.ConfirmDate;
			info.LiableUser = this.LiableUser;
			info.LiableDate = this.LiableDate;
			info.FinanceUser = this.FinanceUser;
			info.FinanceDate = this.FinanceDate;
			info.SumPartQty = this.SumPartQty;
			info.SumOfPrice = this.SumOfPrice;
			info.Status = this.Status;
			info.Conveyance = this.Conveyance;
			info.CarrierTel = this.CarrierTel;
			info.SumWeight = this.SumWeight;
			info.SumVolume = this.SumVolume;
			info.PlanShippingTime = this.PlanShippingTime;
			info.PlanDeliveryTime = this.PlanDeliveryTime;
			info.PrintCount = this.PrintCount;
			info.PrintTime = this.PrintTime;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.LastPrintUser = this.LastPrintUser;
			info.SumPackageQty = this.SumPackageQty;
			info.PullMode = this.PullMode;
			return info;			
		}
		 
		public VmiOutputInfo Clone()
		{
			return ((ICloneable) this).Clone() as VmiOutputInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// VmiOutputInfoCollection对应表[TT_WMM_VMI_OUTPUT]
    /// </summary>
	public partial class VmiOutputInfoCollection : BusinessObjectCollection<VmiOutputInfo>
	{
		public VmiOutputInfoCollection():base("TT_WMM_VMI_OUTPUT"){}	
	}
}