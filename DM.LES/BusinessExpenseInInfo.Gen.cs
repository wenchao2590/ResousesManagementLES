#region Declaim
//---------------------------------------------------------------------------
// Name:		BusinessExpenseInInfo
// Function: 	Expose data in table BusinessExpenseIn from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月28日
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
    /// BusinessExpenseInInfo对应表[TT_FIM_BUSINESS_EXPENSE_IN]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class BusinessExpenseInInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public BusinessExpenseInInfo( 
					string aSupplierNum,

					string aSupplierName,

					string aOrderNo,

					string aReimburseUser,

					Guid aReimburseOrganFid,

					string aExpenseCode,

					string aExpenseName,

					int aExpenseType,

					decimal aUnitPrice,

					decimal aQuantity,

					decimal aAmount,

					string aCurrency,

					decimal aExchangeRate,

					string aExchangeCurrency,

					decimal aExchangedAmount,

					string aInvoiceNo,

					string aInvoiceTitle,

					bool aCheckFlag,

					DateTime aCheckDate,

					string aCheckUser,

					decimal aActualAmount,

					bool aPaymentFlag,

					DateTime aPaymentDate,

					string aPaymentUser,

					string aFiDocNo,

					bool aNoInvoiceFlag,

					string aApprovalNo,

					bool aApprovalFlag,

					string aApprovalUser,

					DateTime aApproveDate,

					Guid aSourceBusinessFid,

					string aSourceBusinessNo,

					string aCustTrustNo,

					Guid aOrganizationFid,

					string aComments,

					bool aValidFlag,

					long aId,

					string aModifyUser,

					string aCreateUser,

					DateTime aModifyDate,

					Guid aFid,

					DateTime aCreateDate

				 
		) : this()
		{
			 
			SupplierNum = aSupplierNum;
		 
			SupplierName = aSupplierName;
		 
			OrderNo = aOrderNo;
		 
			ReimburseUser = aReimburseUser;
		 
			ReimburseOrganFid = aReimburseOrganFid;
		 
			ExpenseCode = aExpenseCode;
		 
			ExpenseName = aExpenseName;
		 
			ExpenseType = aExpenseType;
		 
			UnitPrice = aUnitPrice;
		 
			Quantity = aQuantity;
		 
			Amount = aAmount;
		 
			Currency = aCurrency;
		 
			ExchangeRate = aExchangeRate;
		 
			ExchangeCurrency = aExchangeCurrency;
		 
			ExchangedAmount = aExchangedAmount;
		 
			InvoiceNo = aInvoiceNo;
		 
			InvoiceTitle = aInvoiceTitle;
		 
			CheckFlag = aCheckFlag;
		 
			CheckDate = aCheckDate;
		 
			CheckUser = aCheckUser;
		 
			ActualAmount = aActualAmount;
		 
			PaymentFlag = aPaymentFlag;
		 
			PaymentDate = aPaymentDate;
		 
			PaymentUser = aPaymentUser;
		 
			FiDocNo = aFiDocNo;
		 
			NoInvoiceFlag = aNoInvoiceFlag;
		 
			ApprovalNo = aApprovalNo;
		 
			ApprovalFlag = aApprovalFlag;
		 
			ApprovalUser = aApprovalUser;
		 
			ApproveDate = aApproveDate;
		 
			SourceBusinessFid = aSourceBusinessFid;
		 
			SourceBusinessNo = aSourceBusinessNo;
		 
			CustTrustNo = aCustTrustNo;
		 
			OrganizationFid = aOrganizationFid;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			Id = aId;
		 
			ModifyUser = aModifyUser;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			Fid = aFid;
		 
			CreateDate = aCreateDate;
		}
		
		public BusinessExpenseInInfo():base("TT_FIM_BUSINESS_EXPENSE_IN")
		{
			List<string> keys = new List<string>();
			                                     			keys.Add("ID");     _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 0;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 1;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField ORDER_NOField = new DataSchemaField();
			ORDER_NOField.Name = "ORDER_NO";
			ORDER_NOField.Type = typeof(string).ToString();
			ORDER_NOField.Index = 2;
			fields.Add(ORDER_NOField);
			 
			DataSchemaField REIMBURSE_USERField = new DataSchemaField();
			REIMBURSE_USERField.Name = "REIMBURSE_USER";
			REIMBURSE_USERField.Type = typeof(string).ToString();
			REIMBURSE_USERField.Index = 3;
			fields.Add(REIMBURSE_USERField);
			 
			DataSchemaField REIMBURSE_ORGAN_FIDField = new DataSchemaField();
			REIMBURSE_ORGAN_FIDField.Name = "REIMBURSE_ORGAN_FID";
			REIMBURSE_ORGAN_FIDField.Type = typeof(Guid).ToString();
			REIMBURSE_ORGAN_FIDField.Index = 4;
			fields.Add(REIMBURSE_ORGAN_FIDField);
			 
			DataSchemaField EXPENSE_CODEField = new DataSchemaField();
			EXPENSE_CODEField.Name = "EXPENSE_CODE";
			EXPENSE_CODEField.Type = typeof(string).ToString();
			EXPENSE_CODEField.Index = 5;
			fields.Add(EXPENSE_CODEField);
			 
			DataSchemaField EXPENSE_NAMEField = new DataSchemaField();
			EXPENSE_NAMEField.Name = "EXPENSE_NAME";
			EXPENSE_NAMEField.Type = typeof(string).ToString();
			EXPENSE_NAMEField.Index = 6;
			fields.Add(EXPENSE_NAMEField);
			 
			DataSchemaField EXPENSE_TYPEField = new DataSchemaField();
			EXPENSE_TYPEField.Name = "EXPENSE_TYPE";
			EXPENSE_TYPEField.Type = typeof(int).ToString();
			EXPENSE_TYPEField.Index = 7;
			fields.Add(EXPENSE_TYPEField);
			 
			DataSchemaField UNIT_PRICEField = new DataSchemaField();
			UNIT_PRICEField.Name = "UNIT_PRICE";
			UNIT_PRICEField.Type = typeof(decimal).ToString();
			UNIT_PRICEField.Index = 8;
			fields.Add(UNIT_PRICEField);
			 
			DataSchemaField QUANTITYField = new DataSchemaField();
			QUANTITYField.Name = "QUANTITY";
			QUANTITYField.Type = typeof(decimal).ToString();
			QUANTITYField.Index = 9;
			fields.Add(QUANTITYField);
			 
			DataSchemaField AMOUNTField = new DataSchemaField();
			AMOUNTField.Name = "AMOUNT";
			AMOUNTField.Type = typeof(decimal).ToString();
			AMOUNTField.Index = 10;
			fields.Add(AMOUNTField);
			 
			DataSchemaField CURRENCYField = new DataSchemaField();
			CURRENCYField.Name = "CURRENCY";
			CURRENCYField.Type = typeof(string).ToString();
			CURRENCYField.Index = 11;
			fields.Add(CURRENCYField);
			 
			DataSchemaField EXCHANGE_RATEField = new DataSchemaField();
			EXCHANGE_RATEField.Name = "EXCHANGE_RATE";
			EXCHANGE_RATEField.Type = typeof(decimal).ToString();
			EXCHANGE_RATEField.Index = 12;
			fields.Add(EXCHANGE_RATEField);
			 
			DataSchemaField EXCHANGE_CURRENCYField = new DataSchemaField();
			EXCHANGE_CURRENCYField.Name = "EXCHANGE_CURRENCY";
			EXCHANGE_CURRENCYField.Type = typeof(string).ToString();
			EXCHANGE_CURRENCYField.Index = 13;
			fields.Add(EXCHANGE_CURRENCYField);
			 
			DataSchemaField EXCHANGED_AMOUNTField = new DataSchemaField();
			EXCHANGED_AMOUNTField.Name = "EXCHANGED_AMOUNT";
			EXCHANGED_AMOUNTField.Type = typeof(decimal).ToString();
			EXCHANGED_AMOUNTField.Index = 14;
			fields.Add(EXCHANGED_AMOUNTField);
			 
			DataSchemaField INVOICE_NOField = new DataSchemaField();
			INVOICE_NOField.Name = "INVOICE_NO";
			INVOICE_NOField.Type = typeof(string).ToString();
			INVOICE_NOField.Index = 15;
			fields.Add(INVOICE_NOField);
			 
			DataSchemaField INVOICE_TITLEField = new DataSchemaField();
			INVOICE_TITLEField.Name = "INVOICE_TITLE";
			INVOICE_TITLEField.Type = typeof(string).ToString();
			INVOICE_TITLEField.Index = 16;
			fields.Add(INVOICE_TITLEField);
			 
			DataSchemaField CHECK_FLAGField = new DataSchemaField();
			CHECK_FLAGField.Name = "CHECK_FLAG";
			CHECK_FLAGField.Type = typeof(bool).ToString();
			CHECK_FLAGField.Index = 17;
			fields.Add(CHECK_FLAGField);
			 
			DataSchemaField CHECK_DATEField = new DataSchemaField();
			CHECK_DATEField.Name = "CHECK_DATE";
			CHECK_DATEField.Type = typeof(DateTime).ToString();
			CHECK_DATEField.Index = 18;
			fields.Add(CHECK_DATEField);
			 
			DataSchemaField CHECK_USERField = new DataSchemaField();
			CHECK_USERField.Name = "CHECK_USER";
			CHECK_USERField.Type = typeof(string).ToString();
			CHECK_USERField.Index = 19;
			fields.Add(CHECK_USERField);
			 
			DataSchemaField ACTUAL_AMOUNTField = new DataSchemaField();
			ACTUAL_AMOUNTField.Name = "ACTUAL_AMOUNT";
			ACTUAL_AMOUNTField.Type = typeof(decimal).ToString();
			ACTUAL_AMOUNTField.Index = 20;
			fields.Add(ACTUAL_AMOUNTField);
			 
			DataSchemaField PAYMENT_FLAGField = new DataSchemaField();
			PAYMENT_FLAGField.Name = "PAYMENT_FLAG";
			PAYMENT_FLAGField.Type = typeof(bool).ToString();
			PAYMENT_FLAGField.Index = 21;
			fields.Add(PAYMENT_FLAGField);
			 
			DataSchemaField PAYMENT_DATEField = new DataSchemaField();
			PAYMENT_DATEField.Name = "PAYMENT_DATE";
			PAYMENT_DATEField.Type = typeof(DateTime).ToString();
			PAYMENT_DATEField.Index = 22;
			fields.Add(PAYMENT_DATEField);
			 
			DataSchemaField PAYMENT_USERField = new DataSchemaField();
			PAYMENT_USERField.Name = "PAYMENT_USER";
			PAYMENT_USERField.Type = typeof(string).ToString();
			PAYMENT_USERField.Index = 23;
			fields.Add(PAYMENT_USERField);
			 
			DataSchemaField FI_DOC_NOField = new DataSchemaField();
			FI_DOC_NOField.Name = "FI_DOC_NO";
			FI_DOC_NOField.Type = typeof(string).ToString();
			FI_DOC_NOField.Index = 24;
			fields.Add(FI_DOC_NOField);
			 
			DataSchemaField NO_INVOICE_FLAGField = new DataSchemaField();
			NO_INVOICE_FLAGField.Name = "NO_INVOICE_FLAG";
			NO_INVOICE_FLAGField.Type = typeof(bool).ToString();
			NO_INVOICE_FLAGField.Index = 25;
			fields.Add(NO_INVOICE_FLAGField);
			 
			DataSchemaField APPROVAL_NOField = new DataSchemaField();
			APPROVAL_NOField.Name = "APPROVAL_NO";
			APPROVAL_NOField.Type = typeof(string).ToString();
			APPROVAL_NOField.Index = 26;
			fields.Add(APPROVAL_NOField);
			 
			DataSchemaField APPROVAL_FLAGField = new DataSchemaField();
			APPROVAL_FLAGField.Name = "APPROVAL_FLAG";
			APPROVAL_FLAGField.Type = typeof(bool).ToString();
			APPROVAL_FLAGField.Index = 27;
			fields.Add(APPROVAL_FLAGField);
			 
			DataSchemaField APPROVAL_USERField = new DataSchemaField();
			APPROVAL_USERField.Name = "APPROVAL_USER";
			APPROVAL_USERField.Type = typeof(string).ToString();
			APPROVAL_USERField.Index = 28;
			fields.Add(APPROVAL_USERField);
			 
			DataSchemaField APPROVE_DATEField = new DataSchemaField();
			APPROVE_DATEField.Name = "APPROVE_DATE";
			APPROVE_DATEField.Type = typeof(DateTime).ToString();
			APPROVE_DATEField.Index = 29;
			fields.Add(APPROVE_DATEField);
			 
			DataSchemaField SOURCE_BUSINESS_FIDField = new DataSchemaField();
			SOURCE_BUSINESS_FIDField.Name = "SOURCE_BUSINESS_FID";
			SOURCE_BUSINESS_FIDField.Type = typeof(Guid).ToString();
			SOURCE_BUSINESS_FIDField.Index = 30;
			fields.Add(SOURCE_BUSINESS_FIDField);
			 
			DataSchemaField SOURCE_BUSINESS_NOField = new DataSchemaField();
			SOURCE_BUSINESS_NOField.Name = "SOURCE_BUSINESS_NO";
			SOURCE_BUSINESS_NOField.Type = typeof(string).ToString();
			SOURCE_BUSINESS_NOField.Index = 31;
			fields.Add(SOURCE_BUSINESS_NOField);
			 
			DataSchemaField CUST_TRUST_NOField = new DataSchemaField();
			CUST_TRUST_NOField.Name = "CUST_TRUST_NO";
			CUST_TRUST_NOField.Type = typeof(string).ToString();
			CUST_TRUST_NOField.Index = 32;
			fields.Add(CUST_TRUST_NOField);
			 
			DataSchemaField ORGANIZATION_FIDField = new DataSchemaField();
			ORGANIZATION_FIDField.Name = "ORGANIZATION_FID";
			ORGANIZATION_FIDField.Type = typeof(Guid).ToString();
			ORGANIZATION_FIDField.Index = 33;
			fields.Add(ORGANIZATION_FIDField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 34;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 35;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 36;
			fields.Add(IDField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 37;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 38;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 39;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 40;
			fields.Add(FIDField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 41;
			fields.Add(CREATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public string OrderNo{ get;set; }		
				
		[DataMember]
		public string ReimburseUser{ get;set; }		
				
		[DataMember]
		public Guid? ReimburseOrganFid{ get;set; }		
				
		[DataMember]
		public string ExpenseCode{ get;set; }		
				
		[DataMember]
		public string ExpenseName{ get;set; }		
				
		[DataMember]
		public int? ExpenseType{ get;set; }		
				
		[DataMember]
		public decimal? UnitPrice{ get;set; }		
				
		[DataMember]
		public decimal? Quantity{ get;set; }		
				
		[DataMember]
		public decimal? Amount{ get;set; }		
				
		[DataMember]
		public string Currency{ get;set; }		
				
		[DataMember]
		public decimal? ExchangeRate{ get;set; }		
				
		[DataMember]
		public string ExchangeCurrency{ get;set; }		
				
		[DataMember]
		public decimal? ExchangedAmount{ get;set; }		
				
		[DataMember]
		public string InvoiceNo{ get;set; }		
				
		[DataMember]
		public string InvoiceTitle{ get;set; }		
				
		[DataMember]
		public bool? CheckFlag{ get;set; }		
				
		[DataMember]
		public DateTime? CheckDate{ get;set; }		
				
		[DataMember]
		public string CheckUser{ get;set; }		
				
		[DataMember]
		public decimal? ActualAmount{ get;set; }		
				
		[DataMember]
		public bool? PaymentFlag{ get;set; }		
				
		[DataMember]
		public DateTime? PaymentDate{ get;set; }		
				
		[DataMember]
		public string PaymentUser{ get;set; }		
				
		[DataMember]
		public string FiDocNo{ get;set; }		
				
		[DataMember]
		public bool? NoInvoiceFlag{ get;set; }		
				
		[DataMember]
		public string ApprovalNo{ get;set; }		
				
		[DataMember]
		public bool? ApprovalFlag{ get;set; }		
				
		[DataMember]
		public string ApprovalUser{ get;set; }		
				
		[DataMember]
		public DateTime? ApproveDate{ get;set; }		
				
		[DataMember]
		public Guid? SourceBusinessFid{ get;set; }		
				
		[DataMember]
		public string SourceBusinessNo{ get;set; }		
				
		[DataMember]
		public string CustTrustNo{ get;set; }		
				
		[DataMember]
		public Guid? OrganizationFid{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public bool ValidFlag{ get;set; }		
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			BusinessExpenseInInfo info = new BusinessExpenseInInfo();

			info.SupplierNum = this.SupplierNum;
			info.SupplierName = this.SupplierName;
			info.OrderNo = this.OrderNo;
			info.ReimburseUser = this.ReimburseUser;
			info.ReimburseOrganFid = this.ReimburseOrganFid;
			info.ExpenseCode = this.ExpenseCode;
			info.ExpenseName = this.ExpenseName;
			info.ExpenseType = this.ExpenseType;
			info.UnitPrice = this.UnitPrice;
			info.Quantity = this.Quantity;
			info.Amount = this.Amount;
			info.Currency = this.Currency;
			info.ExchangeRate = this.ExchangeRate;
			info.ExchangeCurrency = this.ExchangeCurrency;
			info.ExchangedAmount = this.ExchangedAmount;
			info.InvoiceNo = this.InvoiceNo;
			info.InvoiceTitle = this.InvoiceTitle;
			info.CheckFlag = this.CheckFlag;
			info.CheckDate = this.CheckDate;
			info.CheckUser = this.CheckUser;
			info.ActualAmount = this.ActualAmount;
			info.PaymentFlag = this.PaymentFlag;
			info.PaymentDate = this.PaymentDate;
			info.PaymentUser = this.PaymentUser;
			info.FiDocNo = this.FiDocNo;
			info.NoInvoiceFlag = this.NoInvoiceFlag;
			info.ApprovalNo = this.ApprovalNo;
			info.ApprovalFlag = this.ApprovalFlag;
			info.ApprovalUser = this.ApprovalUser;
			info.ApproveDate = this.ApproveDate;
			info.SourceBusinessFid = this.SourceBusinessFid;
			info.SourceBusinessNo = this.SourceBusinessNo;
			info.CustTrustNo = this.CustTrustNo;
			info.OrganizationFid = this.OrganizationFid;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.Id = this.Id;
			info.ModifyUser = this.ModifyUser;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.Fid = this.Fid;
			info.CreateDate = this.CreateDate;
			return info;			
		}
		 
		public BusinessExpenseInInfo Clone()
		{
			return ((ICloneable) this).Clone() as BusinessExpenseInInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// BusinessExpenseInInfoCollection对应表[TT_FIM_BUSINESS_EXPENSE_IN]
    /// </summary>
	public partial class BusinessExpenseInInfoCollection : BusinessObjectCollection<BusinessExpenseInInfo>
	{
		public BusinessExpenseInInfoCollection():base("TT_FIM_BUSINESS_EXPENSE_IN"){}	
	}
}
