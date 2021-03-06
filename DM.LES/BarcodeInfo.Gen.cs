#region Declaim
//---------------------------------------------------------------------------
// Name:		BarcodeInfo
// Function: 	Expose data in table Barcode from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月13日
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
    /// BarcodeInfo对应表[TT_WMM_BARCODE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class BarcodeInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public BarcodeInfo( 
					long aId,

					Guid aFid,

					string aPartNo,

					string aPartCname,

					string aPartNickname,

					string aBarcodeData,

					int aBarcodeType,

					int aBarcodeStatus,

					int aPrintTimes,

					string aPrintedUser,

					DateTime aPrintDate,

					string aPackageModel,

					decimal aPackage,

					decimal aCurrentQty,

					string aIdentifyPartNo,

					string aMeasuringUnitNo,

					string aSupplierNum,

					string aSupplierName,

					string aSupplierSname,

					string aPlant,

					string aAssemblyLine,

					string aLocation,

					string aDock,

					string aWmNo,

					string aZoneNo,

					string aDloc,

					string aBoxParts,

					string aRunsheetNo,

					string aAsnRunsheetNo,

					int aPickupSeqNo,

					string aRdcDloc,

					string aInnerLocation,

					DateTime aWmsSendTime,

					int aWmsSendStatus,

					DateTime aRequiredDate,

					string aBatthNo,

					string aProductionBatchNo,

					string aPachageType,

					string aLinePosition,

					string aSupermarketAddress,

					decimal aNetWeight,

					string aTimeZone,

					string aRfidNo,

					decimal aPackageLength,

					decimal aPackageWidth,

					decimal aPackageHeight,

					decimal aPerpackageGrossWeight,

					Guid aCreateSourceFid,

					decimal aPackageVolume,

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
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			PartNickname = aPartNickname;
		 
			BarcodeData = aBarcodeData;
		 
			BarcodeType = aBarcodeType;
		 
			BarcodeStatus = aBarcodeStatus;
		 
			PrintTimes = aPrintTimes;
		 
			PrintedUser = aPrintedUser;
		 
			PrintDate = aPrintDate;
		 
			PackageModel = aPackageModel;
		 
			Package = aPackage;
		 
			CurrentQty = aCurrentQty;
		 
			IdentifyPartNo = aIdentifyPartNo;
		 
			MeasuringUnitNo = aMeasuringUnitNo;
		 
			SupplierNum = aSupplierNum;
		 
			SupplierName = aSupplierName;
		 
			SupplierSname = aSupplierSname;
		 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			Location = aLocation;
		 
			Dock = aDock;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			Dloc = aDloc;
		 
			BoxParts = aBoxParts;
		 
			RunsheetNo = aRunsheetNo;
		 
			AsnRunsheetNo = aAsnRunsheetNo;
		 
			PickupSeqNo = aPickupSeqNo;
		 
			RdcDloc = aRdcDloc;
		 
			InnerLocation = aInnerLocation;
		 
			WmsSendTime = aWmsSendTime;
		 
			WmsSendStatus = aWmsSendStatus;
		 
			RequiredDate = aRequiredDate;
		 
			BatthNo = aBatthNo;
		 
			ProductionBatchNo = aProductionBatchNo;
		 
			PachageType = aPachageType;
		 
			LinePosition = aLinePosition;
		 
			SupermarketAddress = aSupermarketAddress;
		 
			NetWeight = aNetWeight;
		 
			TimeZone = aTimeZone;
		 
			RfidNo = aRfidNo;
		 
			PackageLength = aPackageLength;
		 
			PackageWidth = aPackageWidth;
		 
			PackageHeight = aPackageHeight;
		 
			PerpackageGrossWeight = aPerpackageGrossWeight;
		 
			CreateSourceFid = aCreateSourceFid;
		 
			PackageVolume = aPackageVolume;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public BarcodeInfo():base("TT_WMM_BARCODE")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                                                      _Keys = keys.ToArray();
			
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
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 2;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_CNAMEField = new DataSchemaField();
			PART_CNAMEField.Name = "PART_CNAME";
			PART_CNAMEField.Type = typeof(string).ToString();
			PART_CNAMEField.Index = 3;
			fields.Add(PART_CNAMEField);
			 
			DataSchemaField PART_NICKNAMEField = new DataSchemaField();
			PART_NICKNAMEField.Name = "PART_NICKNAME";
			PART_NICKNAMEField.Type = typeof(string).ToString();
			PART_NICKNAMEField.Index = 4;
			fields.Add(PART_NICKNAMEField);
			 
			DataSchemaField BARCODE_DATAField = new DataSchemaField();
			BARCODE_DATAField.Name = "BARCODE_DATA";
			BARCODE_DATAField.Type = typeof(string).ToString();
			BARCODE_DATAField.Index = 5;
			fields.Add(BARCODE_DATAField);
			 
			DataSchemaField BARCODE_TYPEField = new DataSchemaField();
			BARCODE_TYPEField.Name = "BARCODE_TYPE";
			BARCODE_TYPEField.Type = typeof(int).ToString();
			BARCODE_TYPEField.Index = 6;
			fields.Add(BARCODE_TYPEField);
			 
			DataSchemaField BARCODE_STATUSField = new DataSchemaField();
			BARCODE_STATUSField.Name = "BARCODE_STATUS";
			BARCODE_STATUSField.Type = typeof(int).ToString();
			BARCODE_STATUSField.Index = 7;
			fields.Add(BARCODE_STATUSField);
			 
			DataSchemaField PRINT_TIMESField = new DataSchemaField();
			PRINT_TIMESField.Name = "PRINT_TIMES";
			PRINT_TIMESField.Type = typeof(int).ToString();
			PRINT_TIMESField.Index = 8;
			fields.Add(PRINT_TIMESField);
			 
			DataSchemaField PRINTED_USERField = new DataSchemaField();
			PRINTED_USERField.Name = "PRINTED_USER";
			PRINTED_USERField.Type = typeof(string).ToString();
			PRINTED_USERField.Index = 9;
			fields.Add(PRINTED_USERField);
			 
			DataSchemaField PRINT_DATEField = new DataSchemaField();
			PRINT_DATEField.Name = "PRINT_DATE";
			PRINT_DATEField.Type = typeof(DateTime).ToString();
			PRINT_DATEField.Index = 10;
			fields.Add(PRINT_DATEField);
			 
			DataSchemaField PACKAGE_MODELField = new DataSchemaField();
			PACKAGE_MODELField.Name = "PACKAGE_MODEL";
			PACKAGE_MODELField.Type = typeof(string).ToString();
			PACKAGE_MODELField.Index = 11;
			fields.Add(PACKAGE_MODELField);
			 
			DataSchemaField PACKAGEField = new DataSchemaField();
			PACKAGEField.Name = "PACKAGE";
			PACKAGEField.Type = typeof(decimal).ToString();
			PACKAGEField.Index = 12;
			fields.Add(PACKAGEField);
			 
			DataSchemaField CURRENT_QTYField = new DataSchemaField();
			CURRENT_QTYField.Name = "CURRENT_QTY";
			CURRENT_QTYField.Type = typeof(decimal).ToString();
			CURRENT_QTYField.Index = 13;
			fields.Add(CURRENT_QTYField);
			 
			DataSchemaField IDENTIFY_PART_NOField = new DataSchemaField();
			IDENTIFY_PART_NOField.Name = "IDENTIFY_PART_NO";
			IDENTIFY_PART_NOField.Type = typeof(string).ToString();
			IDENTIFY_PART_NOField.Index = 14;
			fields.Add(IDENTIFY_PART_NOField);
			 
			DataSchemaField MEASURING_UNIT_NOField = new DataSchemaField();
			MEASURING_UNIT_NOField.Name = "MEASURING_UNIT_NO";
			MEASURING_UNIT_NOField.Type = typeof(string).ToString();
			MEASURING_UNIT_NOField.Index = 15;
			fields.Add(MEASURING_UNIT_NOField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 16;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 17;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField SUPPLIER_SNAMEField = new DataSchemaField();
			SUPPLIER_SNAMEField.Name = "SUPPLIER_SNAME";
			SUPPLIER_SNAMEField.Type = typeof(string).ToString();
			SUPPLIER_SNAMEField.Index = 18;
			fields.Add(SUPPLIER_SNAMEField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 19;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 20;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField LOCATIONField = new DataSchemaField();
			LOCATIONField.Name = "LOCATION";
			LOCATIONField.Type = typeof(string).ToString();
			LOCATIONField.Index = 21;
			fields.Add(LOCATIONField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 22;
			fields.Add(DOCKField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 23;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 24;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField DLOCField = new DataSchemaField();
			DLOCField.Name = "DLOC";
			DLOCField.Type = typeof(string).ToString();
			DLOCField.Index = 25;
			fields.Add(DLOCField);
			 
			DataSchemaField BOX_PARTSField = new DataSchemaField();
			BOX_PARTSField.Name = "BOX_PARTS";
			BOX_PARTSField.Type = typeof(string).ToString();
			BOX_PARTSField.Index = 26;
			fields.Add(BOX_PARTSField);
			 
			DataSchemaField RUNSHEET_NOField = new DataSchemaField();
			RUNSHEET_NOField.Name = "RUNSHEET_NO";
			RUNSHEET_NOField.Type = typeof(string).ToString();
			RUNSHEET_NOField.Index = 27;
			fields.Add(RUNSHEET_NOField);
			 
			DataSchemaField ASN_RUNSHEET_NOField = new DataSchemaField();
			ASN_RUNSHEET_NOField.Name = "ASN_RUNSHEET_NO";
			ASN_RUNSHEET_NOField.Type = typeof(string).ToString();
			ASN_RUNSHEET_NOField.Index = 28;
			fields.Add(ASN_RUNSHEET_NOField);
			 
			DataSchemaField PICKUP_SEQ_NOField = new DataSchemaField();
			PICKUP_SEQ_NOField.Name = "PICKUP_SEQ_NO";
			PICKUP_SEQ_NOField.Type = typeof(int).ToString();
			PICKUP_SEQ_NOField.Index = 29;
			fields.Add(PICKUP_SEQ_NOField);
			 
			DataSchemaField RDC_DLOCField = new DataSchemaField();
			RDC_DLOCField.Name = "RDC_DLOC";
			RDC_DLOCField.Type = typeof(string).ToString();
			RDC_DLOCField.Index = 30;
			fields.Add(RDC_DLOCField);
			 
			DataSchemaField INNER_LOCATIONField = new DataSchemaField();
			INNER_LOCATIONField.Name = "INNER_LOCATION";
			INNER_LOCATIONField.Type = typeof(string).ToString();
			INNER_LOCATIONField.Index = 31;
			fields.Add(INNER_LOCATIONField);
			 
			DataSchemaField WMS_SEND_TIMEField = new DataSchemaField();
			WMS_SEND_TIMEField.Name = "WMS_SEND_TIME";
			WMS_SEND_TIMEField.Type = typeof(DateTime).ToString();
			WMS_SEND_TIMEField.Index = 32;
			fields.Add(WMS_SEND_TIMEField);
			 
			DataSchemaField WMS_SEND_STATUSField = new DataSchemaField();
			WMS_SEND_STATUSField.Name = "WMS_SEND_STATUS";
			WMS_SEND_STATUSField.Type = typeof(int).ToString();
			WMS_SEND_STATUSField.Index = 33;
			fields.Add(WMS_SEND_STATUSField);
			 
			DataSchemaField REQUIRED_DATEField = new DataSchemaField();
			REQUIRED_DATEField.Name = "REQUIRED_DATE";
			REQUIRED_DATEField.Type = typeof(DateTime).ToString();
			REQUIRED_DATEField.Index = 34;
			fields.Add(REQUIRED_DATEField);
			 
			DataSchemaField BATTH_NOField = new DataSchemaField();
			BATTH_NOField.Name = "BATTH_NO";
			BATTH_NOField.Type = typeof(string).ToString();
			BATTH_NOField.Index = 35;
			fields.Add(BATTH_NOField);
			 
			DataSchemaField PRODUCTION_BATCH_NOField = new DataSchemaField();
			PRODUCTION_BATCH_NOField.Name = "PRODUCTION_BATCH_NO";
			PRODUCTION_BATCH_NOField.Type = typeof(string).ToString();
			PRODUCTION_BATCH_NOField.Index = 36;
			fields.Add(PRODUCTION_BATCH_NOField);
			 
			DataSchemaField PACHAGE_TYPEField = new DataSchemaField();
			PACHAGE_TYPEField.Name = "PACHAGE_TYPE";
			PACHAGE_TYPEField.Type = typeof(string).ToString();
			PACHAGE_TYPEField.Index = 37;
			fields.Add(PACHAGE_TYPEField);
			 
			DataSchemaField LINE_POSITIONField = new DataSchemaField();
			LINE_POSITIONField.Name = "LINE_POSITION";
			LINE_POSITIONField.Type = typeof(string).ToString();
			LINE_POSITIONField.Index = 38;
			fields.Add(LINE_POSITIONField);
			 
			DataSchemaField SUPERMARKET_ADDRESSField = new DataSchemaField();
			SUPERMARKET_ADDRESSField.Name = "SUPERMARKET_ADDRESS";
			SUPERMARKET_ADDRESSField.Type = typeof(string).ToString();
			SUPERMARKET_ADDRESSField.Index = 39;
			fields.Add(SUPERMARKET_ADDRESSField);
			 
			DataSchemaField NET_WEIGHTField = new DataSchemaField();
			NET_WEIGHTField.Name = "NET_WEIGHT";
			NET_WEIGHTField.Type = typeof(decimal).ToString();
			NET_WEIGHTField.Index = 40;
			fields.Add(NET_WEIGHTField);
			 
			DataSchemaField TIME_ZONEField = new DataSchemaField();
			TIME_ZONEField.Name = "TIME_ZONE";
			TIME_ZONEField.Type = typeof(string).ToString();
			TIME_ZONEField.Index = 41;
			fields.Add(TIME_ZONEField);
			 
			DataSchemaField RFID_NOField = new DataSchemaField();
			RFID_NOField.Name = "RFID_NO";
			RFID_NOField.Type = typeof(string).ToString();
			RFID_NOField.Index = 42;
			fields.Add(RFID_NOField);
			 
			DataSchemaField PACKAGE_LENGTHField = new DataSchemaField();
			PACKAGE_LENGTHField.Name = "PACKAGE_LENGTH";
			PACKAGE_LENGTHField.Type = typeof(decimal).ToString();
			PACKAGE_LENGTHField.Index = 43;
			fields.Add(PACKAGE_LENGTHField);
			 
			DataSchemaField PACKAGE_WIDTHField = new DataSchemaField();
			PACKAGE_WIDTHField.Name = "PACKAGE_WIDTH";
			PACKAGE_WIDTHField.Type = typeof(decimal).ToString();
			PACKAGE_WIDTHField.Index = 44;
			fields.Add(PACKAGE_WIDTHField);
			 
			DataSchemaField PACKAGE_HEIGHTField = new DataSchemaField();
			PACKAGE_HEIGHTField.Name = "PACKAGE_HEIGHT";
			PACKAGE_HEIGHTField.Type = typeof(decimal).ToString();
			PACKAGE_HEIGHTField.Index = 45;
			fields.Add(PACKAGE_HEIGHTField);
			 
			DataSchemaField PERPACKAGE_GROSS_WEIGHTField = new DataSchemaField();
			PERPACKAGE_GROSS_WEIGHTField.Name = "PERPACKAGE_GROSS_WEIGHT";
			PERPACKAGE_GROSS_WEIGHTField.Type = typeof(decimal).ToString();
			PERPACKAGE_GROSS_WEIGHTField.Index = 46;
			fields.Add(PERPACKAGE_GROSS_WEIGHTField);
			 
			DataSchemaField CREATE_SOURCE_FIDField = new DataSchemaField();
			CREATE_SOURCE_FIDField.Name = "CREATE_SOURCE_FID";
			CREATE_SOURCE_FIDField.Type = typeof(Guid).ToString();
			CREATE_SOURCE_FIDField.Index = 47;
			fields.Add(CREATE_SOURCE_FIDField);
			 
			DataSchemaField PACKAGE_VOLUMEField = new DataSchemaField();
			PACKAGE_VOLUMEField.Name = "PACKAGE_VOLUME";
			PACKAGE_VOLUMEField.Type = typeof(decimal).ToString();
			PACKAGE_VOLUMEField.Index = 48;
			fields.Add(PACKAGE_VOLUMEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 49;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 50;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 51;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 52;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 53;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 54;
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
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public string PartNickname{ get;set; }		
				
		[DataMember]
		public string BarcodeData{ get;set; }		
				
		[DataMember]
		public int? BarcodeType{ get;set; }		
				
		[DataMember]
		public int? BarcodeStatus{ get;set; }		
				
		[DataMember]
		public int? PrintTimes{ get;set; }		
				
		[DataMember]
		public string PrintedUser{ get;set; }		
				
		[DataMember]
		public DateTime? PrintDate{ get;set; }		
				
		[DataMember]
		public string PackageModel{ get;set; }		
				
		[DataMember]
		public decimal? Package{ get;set; }		
				
		[DataMember]
		public decimal? CurrentQty{ get;set; }		
				
		[DataMember]
		public string IdentifyPartNo{ get;set; }		
				
		[DataMember]
		public string MeasuringUnitNo{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public string SupplierSname{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string Location{ get;set; }		
				
		[DataMember]
		public string Dock{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public string Dloc{ get;set; }		
				
		[DataMember]
		public string BoxParts{ get;set; }		
				
		[DataMember]
		public string RunsheetNo{ get;set; }		
				
		[DataMember]
		public string AsnRunsheetNo{ get;set; }		
				
		[DataMember]
		public int? PickupSeqNo{ get;set; }		
				
		[DataMember]
		public string RdcDloc{ get;set; }		
				
		[DataMember]
		public string InnerLocation{ get;set; }		
				
		[DataMember]
		public DateTime? WmsSendTime{ get;set; }		
				
		[DataMember]
		public int? WmsSendStatus{ get;set; }		
				
		[DataMember]
		public DateTime? RequiredDate{ get;set; }		
				
		[DataMember]
		public string BatthNo{ get;set; }		
				
		[DataMember]
		public string ProductionBatchNo{ get;set; }		
				
		[DataMember]
		public string PachageType{ get;set; }		
				
		[DataMember]
		public string LinePosition{ get;set; }		
				
		[DataMember]
		public string SupermarketAddress{ get;set; }		
				
		[DataMember]
		public decimal? NetWeight{ get;set; }		
				
		[DataMember]
		public string TimeZone{ get;set; }		
				
		[DataMember]
		public string RfidNo{ get;set; }		
				
		[DataMember]
		public decimal? PackageLength{ get;set; }		
				
		[DataMember]
		public decimal? PackageWidth{ get;set; }		
				
		[DataMember]
		public decimal? PackageHeight{ get;set; }		
				
		[DataMember]
		public decimal? PerpackageGrossWeight{ get;set; }		
				
		[DataMember]
		public Guid? CreateSourceFid{ get;set; }		
				
		[DataMember]
		public decimal? PackageVolume{ get;set; }		
				
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
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			BarcodeInfo info = new BarcodeInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.PartNickname = this.PartNickname;
			info.BarcodeData = this.BarcodeData;
			info.BarcodeType = this.BarcodeType;
			info.BarcodeStatus = this.BarcodeStatus;
			info.PrintTimes = this.PrintTimes;
			info.PrintedUser = this.PrintedUser;
			info.PrintDate = this.PrintDate;
			info.PackageModel = this.PackageModel;
			info.Package = this.Package;
			info.CurrentQty = this.CurrentQty;
			info.IdentifyPartNo = this.IdentifyPartNo;
			info.MeasuringUnitNo = this.MeasuringUnitNo;
			info.SupplierNum = this.SupplierNum;
			info.SupplierName = this.SupplierName;
			info.SupplierSname = this.SupplierSname;
			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.Location = this.Location;
			info.Dock = this.Dock;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.Dloc = this.Dloc;
			info.BoxParts = this.BoxParts;
			info.RunsheetNo = this.RunsheetNo;
			info.AsnRunsheetNo = this.AsnRunsheetNo;
			info.PickupSeqNo = this.PickupSeqNo;
			info.RdcDloc = this.RdcDloc;
			info.InnerLocation = this.InnerLocation;
			info.WmsSendTime = this.WmsSendTime;
			info.WmsSendStatus = this.WmsSendStatus;
			info.RequiredDate = this.RequiredDate;
			info.BatthNo = this.BatthNo;
			info.ProductionBatchNo = this.ProductionBatchNo;
			info.PachageType = this.PachageType;
			info.LinePosition = this.LinePosition;
			info.SupermarketAddress = this.SupermarketAddress;
			info.NetWeight = this.NetWeight;
			info.TimeZone = this.TimeZone;
			info.RfidNo = this.RfidNo;
			info.PackageLength = this.PackageLength;
			info.PackageWidth = this.PackageWidth;
			info.PackageHeight = this.PackageHeight;
			info.PerpackageGrossWeight = this.PerpackageGrossWeight;
			info.CreateSourceFid = this.CreateSourceFid;
			info.PackageVolume = this.PackageVolume;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public BarcodeInfo Clone()
		{
			return ((ICloneable) this).Clone() as BarcodeInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// BarcodeInfoCollection对应表[TT_WMM_BARCODE]
    /// </summary>
	public partial class BarcodeInfoCollection : BusinessObjectCollection<BarcodeInfo>
	{
		public BarcodeInfoCollection():base("TT_WMM_BARCODE"){}	
	}
}
