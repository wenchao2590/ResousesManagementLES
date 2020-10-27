#region Declaim
//---------------------------------------------------------------------------
// Name:		WmsVmiBarcodeInfo
// Function: 	Expose data in table WmsVmiBarcode from database as business object to MES system.
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
    /// WmsVmiBarcodeInfo对应表[TI_IFM_WMS_VMI_BARCODE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class WmsVmiBarcodeInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public WmsVmiBarcodeInfo( 
					long aId,

					Guid aFid,

					Guid aLogFid,

					string aPackageBarcode,

					string aSourceOrderCode,

					string aPartNo,

					string aPartCname,

					decimal aSnp,

					decimal aPartQty,

					string aLinePosition,

					string aSupermarketRepository,

					string aTargetSlcode,

					string aPackageCode,

					string aSupplierCode,

					string aSupplierName,

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
		 
			LogFid = aLogFid;
		 
			PackageBarcode = aPackageBarcode;
		 
			SourceOrderCode = aSourceOrderCode;
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			Snp = aSnp;
		 
			PartQty = aPartQty;
		 
			LinePosition = aLinePosition;
		 
			SupermarketRepository = aSupermarketRepository;
		 
			TargetSlcode = aTargetSlcode;
		 
			PackageCode = aPackageCode;
		 
			SupplierCode = aSupplierCode;
		 
			SupplierName = aSupplierName;
		 
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
		
		public WmsVmiBarcodeInfo():base("TI_IFM_WMS_VMI_BARCODE")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                       _Keys = keys.ToArray();
			
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
			 
			DataSchemaField PACKAGE_BARCODEField = new DataSchemaField();
			PACKAGE_BARCODEField.Name = "PACKAGE_BARCODE";
			PACKAGE_BARCODEField.Type = typeof(string).ToString();
			PACKAGE_BARCODEField.Index = 3;
			fields.Add(PACKAGE_BARCODEField);
			 
			DataSchemaField SOURCE_ORDER_CODEField = new DataSchemaField();
			SOURCE_ORDER_CODEField.Name = "SOURCE_ORDER_CODE";
			SOURCE_ORDER_CODEField.Type = typeof(string).ToString();
			SOURCE_ORDER_CODEField.Index = 4;
			fields.Add(SOURCE_ORDER_CODEField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 5;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_CNAMEField = new DataSchemaField();
			PART_CNAMEField.Name = "PART_CNAME";
			PART_CNAMEField.Type = typeof(string).ToString();
			PART_CNAMEField.Index = 6;
			fields.Add(PART_CNAMEField);
			 
			DataSchemaField SNPField = new DataSchemaField();
			SNPField.Name = "SNP";
			SNPField.Type = typeof(decimal).ToString();
			SNPField.Index = 7;
			fields.Add(SNPField);
			 
			DataSchemaField PART_QTYField = new DataSchemaField();
			PART_QTYField.Name = "PART_QTY";
			PART_QTYField.Type = typeof(decimal).ToString();
			PART_QTYField.Index = 8;
			fields.Add(PART_QTYField);
			 
			DataSchemaField LINE_POSITIONField = new DataSchemaField();
			LINE_POSITIONField.Name = "LINE_POSITION";
			LINE_POSITIONField.Type = typeof(string).ToString();
			LINE_POSITIONField.Index = 9;
			fields.Add(LINE_POSITIONField);
			 
			DataSchemaField SUPERMARKET_REPOSITORYField = new DataSchemaField();
			SUPERMARKET_REPOSITORYField.Name = "SUPERMARKET_REPOSITORY";
			SUPERMARKET_REPOSITORYField.Type = typeof(string).ToString();
			SUPERMARKET_REPOSITORYField.Index = 10;
			fields.Add(SUPERMARKET_REPOSITORYField);
			 
			DataSchemaField TARGET_SLCODEField = new DataSchemaField();
			TARGET_SLCODEField.Name = "TARGET_SLCODE";
			TARGET_SLCODEField.Type = typeof(string).ToString();
			TARGET_SLCODEField.Index = 11;
			fields.Add(TARGET_SLCODEField);
			 
			DataSchemaField PACKAGE_CODEField = new DataSchemaField();
			PACKAGE_CODEField.Name = "PACKAGE_CODE";
			PACKAGE_CODEField.Type = typeof(string).ToString();
			PACKAGE_CODEField.Index = 12;
			fields.Add(PACKAGE_CODEField);
			 
			DataSchemaField SUPPLIER_CODEField = new DataSchemaField();
			SUPPLIER_CODEField.Name = "SUPPLIER_CODE";
			SUPPLIER_CODEField.Type = typeof(string).ToString();
			SUPPLIER_CODEField.Index = 13;
			fields.Add(SUPPLIER_CODEField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 14;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField REMARKField = new DataSchemaField();
			REMARKField.Name = "REMARK";
			REMARKField.Type = typeof(string).ToString();
			REMARKField.Index = 15;
			fields.Add(REMARKField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 16;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 17;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 18;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 19;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 20;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 21;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 22;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 23;
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
		public string PackageBarcode{ get;set; }		
				
		[DataMember]
		public string SourceOrderCode{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public decimal? Snp{ get;set; }		
				
		[DataMember]
		public decimal? PartQty{ get;set; }		
				
		[DataMember]
		public string LinePosition{ get;set; }		
				
		[DataMember]
		public string SupermarketRepository{ get;set; }		
				
		[DataMember]
		public string TargetSlcode{ get;set; }		
				
		[DataMember]
		public string PackageCode{ get;set; }		
				
		[DataMember]
		public string SupplierCode{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public string Remark{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public bool ValidFlag{ get;set; }		
				
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
			WmsVmiBarcodeInfo info = new WmsVmiBarcodeInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LogFid = this.LogFid;
			info.PackageBarcode = this.PackageBarcode;
			info.SourceOrderCode = this.SourceOrderCode;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.Snp = this.Snp;
			info.PartQty = this.PartQty;
			info.LinePosition = this.LinePosition;
			info.SupermarketRepository = this.SupermarketRepository;
			info.TargetSlcode = this.TargetSlcode;
			info.PackageCode = this.PackageCode;
			info.SupplierCode = this.SupplierCode;
			info.SupplierName = this.SupplierName;
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
		 
		public WmsVmiBarcodeInfo Clone()
		{
			return ((ICloneable) this).Clone() as WmsVmiBarcodeInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// WmsVmiBarcodeInfoCollection对应表[TI_IFM_WMS_VMI_BARCODE]
    /// </summary>
	public partial class WmsVmiBarcodeInfoCollection : BusinessObjectCollection<WmsVmiBarcodeInfo>
	{
		public WmsVmiBarcodeInfoCollection():base("TI_IFM_WMS_VMI_BARCODE"){}	
	}
}