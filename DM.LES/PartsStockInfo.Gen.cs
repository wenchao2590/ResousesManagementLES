#region Declaim
//---------------------------------------------------------------------------
// Name:		PartsStockInfo
// Function: 	Expose data in table PartsStock from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年3月30日
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
    /// PartsStockInfo对应表[TM_BAS_PARTS_STOCK]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PartsStockInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PartsStockInfo( 
					string aPlant,

					string aAssemblyLine,

					string aPlantZone,

					string aWorkshop,

					string aSupplierNum,

					string aPartNo,

					string aPartCname,

					string aPartEname,

					string aPartNickname,

					string aPartUnits,

					string aInhousePackageModel,

					int aInhousePackage,

					string aInboundPackageModel,

					int aInboundPackage,

					string aPackageModel,

					int aPackage,

					string aLogicalPk,

					string aRoute,

					string aWmNo,

					string aZoneNo,

					decimal aOccupyArea,

					string aDloc,

					decimal aMax,

					decimal aMin,

					int aRowNumber,

					int aLineNumber,

					int aHighNumber,

					string aMaterialGroup,

					string aKeeper,

					string aTranser,

					string aInformationer,

					string aEloc,

					int aSafeStock,

					decimal aStocks,

					decimal aFrozenStocks,

					decimal aAvailbleStocks,

					int aIsBatch,

					string aWmsRule,

					decimal aCounter,

					decimal aPartWeight,

					string aPartCls,

					int aIsRepack,

					string aRepackRoute,

					int aIsTriggerPull,

					string aTriggerWmNo,

					string aTriggerZoneNo,

					string aTriggerDloc,

					int aEmgTime,

					string aSupperZoneDloc,

					int aCheckType,

					string aBusinessPk,

					int aRepackageAmount,

					string aPickupRoute,

					string aShelfRoute,

					string aComments,

					int aTrayOutIsall,

					int aIsCreateTask,

					int aTrayPackageModel,

					string aTrayModel,

					int aBatchCommendWay,

					int aBackStockRule,

					string aPartClassifyAreaNo,

					string aLineSiteDloc,

					int aScanBarcodeFlag,

					long aId,

					string aModifyUser,

					bool aValidFlag,

					bool aIsOutput,

					string aCreateUser,

					DateTime aModifyDate,

					decimal aFragmentNum,

					Guid aFid,

					DateTime aCreateDate

				 
		) : this()
		{
			 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			SupplierNum = aSupplierNum;
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			PartEname = aPartEname;
		 
			PartNickname = aPartNickname;
		 
			PartUnits = aPartUnits;
		 
			InhousePackageModel = aInhousePackageModel;
		 
			InhousePackage = aInhousePackage;
		 
			InboundPackageModel = aInboundPackageModel;
		 
			InboundPackage = aInboundPackage;
		 
			PackageModel = aPackageModel;
		 
			Package = aPackage;
		 
			LogicalPk = aLogicalPk;
		 
			Route = aRoute;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			OccupyArea = aOccupyArea;
		 
			Dloc = aDloc;
		 
			Max = aMax;
		 
			Min = aMin;
		 
			RowNumber = aRowNumber;
		 
			LineNumber = aLineNumber;
		 
			HighNumber = aHighNumber;
		 
			MaterialGroup = aMaterialGroup;
		 
			Keeper = aKeeper;
		 
			Transer = aTranser;
		 
			Informationer = aInformationer;
		 
			Eloc = aEloc;
		 
			SafeStock = aSafeStock;
		 
			Stocks = aStocks;
		 
			FrozenStocks = aFrozenStocks;
		 
			AvailbleStocks = aAvailbleStocks;
		 
			IsBatch = aIsBatch;
		 
			WmsRule = aWmsRule;
		 
			Counter = aCounter;
		 
			PartWeight = aPartWeight;
		 
			PartCls = aPartCls;
		 
			IsRepack = aIsRepack;
		 
			RepackRoute = aRepackRoute;
		 
			IsTriggerPull = aIsTriggerPull;
		 
			TriggerWmNo = aTriggerWmNo;
		 
			TriggerZoneNo = aTriggerZoneNo;
		 
			TriggerDloc = aTriggerDloc;
		 
			EmgTime = aEmgTime;
		 
			SupperZoneDloc = aSupperZoneDloc;
		 
			CheckType = aCheckType;
		 
			BusinessPk = aBusinessPk;
		 
			RepackageAmount = aRepackageAmount;
		 
			PickupRoute = aPickupRoute;
		 
			ShelfRoute = aShelfRoute;
		 
			Comments = aComments;
		 
			TrayOutIsall = aTrayOutIsall;
		 
			IsCreateTask = aIsCreateTask;
		 
			TrayPackageModel = aTrayPackageModel;
		 
			TrayModel = aTrayModel;
		 
			BatchCommendWay = aBatchCommendWay;
		 
			BackStockRule = aBackStockRule;
		 
			PartClassifyAreaNo = aPartClassifyAreaNo;
		 
			LineSiteDloc = aLineSiteDloc;
		 
			ScanBarcodeFlag = aScanBarcodeFlag;
		 
			Id = aId;
		 
			ModifyUser = aModifyUser;
		 
			ValidFlag = aValidFlag;
		 
			IsOutput = aIsOutput;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			FragmentNum = aFragmentNum;
		 
			Fid = aFid;
		 
			CreateDate = aCreateDate;
		}
		
		public PartsStockInfo():base("TM_BAS_PARTS_STOCK")
		{
			List<string> keys = new List<string>();
			                                                                 			keys.Add("ID");        _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 0;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 1;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 2;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 3;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 4;
			fields.Add(SUPPLIER_NUMField);
			 
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
			 
			DataSchemaField PART_ENAMEField = new DataSchemaField();
			PART_ENAMEField.Name = "PART_ENAME";
			PART_ENAMEField.Type = typeof(string).ToString();
			PART_ENAMEField.Index = 7;
			fields.Add(PART_ENAMEField);
			 
			DataSchemaField PART_NICKNAMEField = new DataSchemaField();
			PART_NICKNAMEField.Name = "PART_NICKNAME";
			PART_NICKNAMEField.Type = typeof(string).ToString();
			PART_NICKNAMEField.Index = 8;
			fields.Add(PART_NICKNAMEField);
			 
			DataSchemaField PART_UNITSField = new DataSchemaField();
			PART_UNITSField.Name = "PART_UNITS";
			PART_UNITSField.Type = typeof(string).ToString();
			PART_UNITSField.Index = 9;
			fields.Add(PART_UNITSField);
			 
			DataSchemaField INHOUSE_PACKAGE_MODELField = new DataSchemaField();
			INHOUSE_PACKAGE_MODELField.Name = "INHOUSE_PACKAGE_MODEL";
			INHOUSE_PACKAGE_MODELField.Type = typeof(string).ToString();
			INHOUSE_PACKAGE_MODELField.Index = 10;
			fields.Add(INHOUSE_PACKAGE_MODELField);
			 
			DataSchemaField INHOUSE_PACKAGEField = new DataSchemaField();
			INHOUSE_PACKAGEField.Name = "INHOUSE_PACKAGE";
			INHOUSE_PACKAGEField.Type = typeof(int).ToString();
			INHOUSE_PACKAGEField.Index = 11;
			fields.Add(INHOUSE_PACKAGEField);
			 
			DataSchemaField INBOUND_PACKAGE_MODELField = new DataSchemaField();
			INBOUND_PACKAGE_MODELField.Name = "INBOUND_PACKAGE_MODEL";
			INBOUND_PACKAGE_MODELField.Type = typeof(string).ToString();
			INBOUND_PACKAGE_MODELField.Index = 12;
			fields.Add(INBOUND_PACKAGE_MODELField);
			 
			DataSchemaField INBOUND_PACKAGEField = new DataSchemaField();
			INBOUND_PACKAGEField.Name = "INBOUND_PACKAGE";
			INBOUND_PACKAGEField.Type = typeof(int).ToString();
			INBOUND_PACKAGEField.Index = 13;
			fields.Add(INBOUND_PACKAGEField);
			 
			DataSchemaField PACKAGE_MODELField = new DataSchemaField();
			PACKAGE_MODELField.Name = "PACKAGE_MODEL";
			PACKAGE_MODELField.Type = typeof(string).ToString();
			PACKAGE_MODELField.Index = 14;
			fields.Add(PACKAGE_MODELField);
			 
			DataSchemaField PACKAGEField = new DataSchemaField();
			PACKAGEField.Name = "PACKAGE";
			PACKAGEField.Type = typeof(int).ToString();
			PACKAGEField.Index = 15;
			fields.Add(PACKAGEField);
			 
			DataSchemaField LOGICAL_PKField = new DataSchemaField();
			LOGICAL_PKField.Name = "LOGICAL_PK";
			LOGICAL_PKField.Type = typeof(string).ToString();
			LOGICAL_PKField.Index = 16;
			fields.Add(LOGICAL_PKField);
			 
			DataSchemaField ROUTEField = new DataSchemaField();
			ROUTEField.Name = "ROUTE";
			ROUTEField.Type = typeof(string).ToString();
			ROUTEField.Index = 17;
			fields.Add(ROUTEField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 18;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 19;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField OCCUPY_AREAField = new DataSchemaField();
			OCCUPY_AREAField.Name = "OCCUPY_AREA";
			OCCUPY_AREAField.Type = typeof(decimal).ToString();
			OCCUPY_AREAField.Index = 20;
			fields.Add(OCCUPY_AREAField);
			 
			DataSchemaField DLOCField = new DataSchemaField();
			DLOCField.Name = "DLOC";
			DLOCField.Type = typeof(string).ToString();
			DLOCField.Index = 21;
			fields.Add(DLOCField);
			 
			DataSchemaField MAXField = new DataSchemaField();
			MAXField.Name = "MAX";
			MAXField.Type = typeof(decimal).ToString();
			MAXField.Index = 22;
			fields.Add(MAXField);
			 
			DataSchemaField MINField = new DataSchemaField();
			MINField.Name = "MIN";
			MINField.Type = typeof(decimal).ToString();
			MINField.Index = 23;
			fields.Add(MINField);
			 
			DataSchemaField ROW_NUMBERField = new DataSchemaField();
			ROW_NUMBERField.Name = "ROW_NUMBER";
			ROW_NUMBERField.Type = typeof(int).ToString();
			ROW_NUMBERField.Index = 24;
			fields.Add(ROW_NUMBERField);
			 
			DataSchemaField LINE_NUMBERField = new DataSchemaField();
			LINE_NUMBERField.Name = "LINE_NUMBER";
			LINE_NUMBERField.Type = typeof(int).ToString();
			LINE_NUMBERField.Index = 25;
			fields.Add(LINE_NUMBERField);
			 
			DataSchemaField HIGH_NUMBERField = new DataSchemaField();
			HIGH_NUMBERField.Name = "HIGH_NUMBER";
			HIGH_NUMBERField.Type = typeof(int).ToString();
			HIGH_NUMBERField.Index = 26;
			fields.Add(HIGH_NUMBERField);
			 
			DataSchemaField MATERIAL_GROUPField = new DataSchemaField();
			MATERIAL_GROUPField.Name = "MATERIAL_GROUP";
			MATERIAL_GROUPField.Type = typeof(string).ToString();
			MATERIAL_GROUPField.Index = 27;
			fields.Add(MATERIAL_GROUPField);
			 
			DataSchemaField KEEPERField = new DataSchemaField();
			KEEPERField.Name = "KEEPER";
			KEEPERField.Type = typeof(string).ToString();
			KEEPERField.Index = 28;
			fields.Add(KEEPERField);
			 
			DataSchemaField TRANSERField = new DataSchemaField();
			TRANSERField.Name = "TRANSER";
			TRANSERField.Type = typeof(string).ToString();
			TRANSERField.Index = 29;
			fields.Add(TRANSERField);
			 
			DataSchemaField INFORMATIONERField = new DataSchemaField();
			INFORMATIONERField.Name = "INFORMATIONER";
			INFORMATIONERField.Type = typeof(string).ToString();
			INFORMATIONERField.Index = 30;
			fields.Add(INFORMATIONERField);
			 
			DataSchemaField ELOCField = new DataSchemaField();
			ELOCField.Name = "ELOC";
			ELOCField.Type = typeof(string).ToString();
			ELOCField.Index = 31;
			fields.Add(ELOCField);
			 
			DataSchemaField SAFE_STOCKField = new DataSchemaField();
			SAFE_STOCKField.Name = "SAFE_STOCK";
			SAFE_STOCKField.Type = typeof(int).ToString();
			SAFE_STOCKField.Index = 32;
			fields.Add(SAFE_STOCKField);
			 
			DataSchemaField STOCKSField = new DataSchemaField();
			STOCKSField.Name = "STOCKS";
			STOCKSField.Type = typeof(decimal).ToString();
			STOCKSField.Index = 33;
			fields.Add(STOCKSField);
			 
			DataSchemaField FROZEN_STOCKSField = new DataSchemaField();
			FROZEN_STOCKSField.Name = "FROZEN_STOCKS";
			FROZEN_STOCKSField.Type = typeof(decimal).ToString();
			FROZEN_STOCKSField.Index = 34;
			fields.Add(FROZEN_STOCKSField);
			 
			DataSchemaField AVAILBLE_STOCKSField = new DataSchemaField();
			AVAILBLE_STOCKSField.Name = "AVAILBLE_STOCKS";
			AVAILBLE_STOCKSField.Type = typeof(decimal).ToString();
			AVAILBLE_STOCKSField.Index = 35;
			fields.Add(AVAILBLE_STOCKSField);
			 
			DataSchemaField IS_BATCHField = new DataSchemaField();
			IS_BATCHField.Name = "IS_BATCH";
			IS_BATCHField.Type = typeof(int).ToString();
			IS_BATCHField.Index = 36;
			fields.Add(IS_BATCHField);
			 
			DataSchemaField WMS_RULEField = new DataSchemaField();
			WMS_RULEField.Name = "WMS_RULE";
			WMS_RULEField.Type = typeof(string).ToString();
			WMS_RULEField.Index = 37;
			fields.Add(WMS_RULEField);
			 
			DataSchemaField COUNTERField = new DataSchemaField();
			COUNTERField.Name = "COUNTER";
			COUNTERField.Type = typeof(decimal).ToString();
			COUNTERField.Index = 38;
			fields.Add(COUNTERField);
			 
			DataSchemaField PART_WEIGHTField = new DataSchemaField();
			PART_WEIGHTField.Name = "PART_WEIGHT";
			PART_WEIGHTField.Type = typeof(decimal).ToString();
			PART_WEIGHTField.Index = 39;
			fields.Add(PART_WEIGHTField);
			 
			DataSchemaField PART_CLSField = new DataSchemaField();
			PART_CLSField.Name = "PART_CLS";
			PART_CLSField.Type = typeof(string).ToString();
			PART_CLSField.Index = 40;
			fields.Add(PART_CLSField);
			 
			DataSchemaField IS_REPACKField = new DataSchemaField();
			IS_REPACKField.Name = "IS_REPACK";
			IS_REPACKField.Type = typeof(int).ToString();
			IS_REPACKField.Index = 41;
			fields.Add(IS_REPACKField);
			 
			DataSchemaField REPACK_ROUTEField = new DataSchemaField();
			REPACK_ROUTEField.Name = "REPACK_ROUTE";
			REPACK_ROUTEField.Type = typeof(string).ToString();
			REPACK_ROUTEField.Index = 42;
			fields.Add(REPACK_ROUTEField);
			 
			DataSchemaField IS_TRIGGER_PULLField = new DataSchemaField();
			IS_TRIGGER_PULLField.Name = "IS_TRIGGER_PULL";
			IS_TRIGGER_PULLField.Type = typeof(int).ToString();
			IS_TRIGGER_PULLField.Index = 43;
			fields.Add(IS_TRIGGER_PULLField);
			 
			DataSchemaField TRIGGER_WM_NOField = new DataSchemaField();
			TRIGGER_WM_NOField.Name = "TRIGGER_WM_NO";
			TRIGGER_WM_NOField.Type = typeof(string).ToString();
			TRIGGER_WM_NOField.Index = 44;
			fields.Add(TRIGGER_WM_NOField);
			 
			DataSchemaField TRIGGER_ZONE_NOField = new DataSchemaField();
			TRIGGER_ZONE_NOField.Name = "TRIGGER_ZONE_NO";
			TRIGGER_ZONE_NOField.Type = typeof(string).ToString();
			TRIGGER_ZONE_NOField.Index = 45;
			fields.Add(TRIGGER_ZONE_NOField);
			 
			DataSchemaField TRIGGER_DLOCField = new DataSchemaField();
			TRIGGER_DLOCField.Name = "TRIGGER_DLOC";
			TRIGGER_DLOCField.Type = typeof(string).ToString();
			TRIGGER_DLOCField.Index = 46;
			fields.Add(TRIGGER_DLOCField);
			 
			DataSchemaField EMG_TIMEField = new DataSchemaField();
			EMG_TIMEField.Name = "EMG_TIME";
			EMG_TIMEField.Type = typeof(int).ToString();
			EMG_TIMEField.Index = 47;
			fields.Add(EMG_TIMEField);
			 
			DataSchemaField SUPPER_ZONE_DLOCField = new DataSchemaField();
			SUPPER_ZONE_DLOCField.Name = "SUPPER_ZONE_DLOC";
			SUPPER_ZONE_DLOCField.Type = typeof(string).ToString();
			SUPPER_ZONE_DLOCField.Index = 48;
			fields.Add(SUPPER_ZONE_DLOCField);
			 
			DataSchemaField CHECK_TYPEField = new DataSchemaField();
			CHECK_TYPEField.Name = "CHECK_TYPE";
			CHECK_TYPEField.Type = typeof(int).ToString();
			CHECK_TYPEField.Index = 49;
			fields.Add(CHECK_TYPEField);
			 
			DataSchemaField BUSINESS_PKField = new DataSchemaField();
			BUSINESS_PKField.Name = "BUSINESS_PK";
			BUSINESS_PKField.Type = typeof(string).ToString();
			BUSINESS_PKField.Index = 50;
			fields.Add(BUSINESS_PKField);
			 
			DataSchemaField REPACKAGE_AMOUNTField = new DataSchemaField();
			REPACKAGE_AMOUNTField.Name = "REPACKAGE_AMOUNT";
			REPACKAGE_AMOUNTField.Type = typeof(int).ToString();
			REPACKAGE_AMOUNTField.Index = 51;
			fields.Add(REPACKAGE_AMOUNTField);
			 
			DataSchemaField PICKUP_ROUTEField = new DataSchemaField();
			PICKUP_ROUTEField.Name = "PICKUP_ROUTE";
			PICKUP_ROUTEField.Type = typeof(string).ToString();
			PICKUP_ROUTEField.Index = 52;
			fields.Add(PICKUP_ROUTEField);
			 
			DataSchemaField SHELF_ROUTEField = new DataSchemaField();
			SHELF_ROUTEField.Name = "SHELF_ROUTE";
			SHELF_ROUTEField.Type = typeof(string).ToString();
			SHELF_ROUTEField.Index = 53;
			fields.Add(SHELF_ROUTEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 54;
			fields.Add(COMMENTSField);
			 
			DataSchemaField TRAY_OUT_ISALLField = new DataSchemaField();
			TRAY_OUT_ISALLField.Name = "TRAY_OUT_ISALL";
			TRAY_OUT_ISALLField.Type = typeof(int).ToString();
			TRAY_OUT_ISALLField.Index = 55;
			fields.Add(TRAY_OUT_ISALLField);
			 
			DataSchemaField IS_CREATE_TASKField = new DataSchemaField();
			IS_CREATE_TASKField.Name = "IS_CREATE_TASK";
			IS_CREATE_TASKField.Type = typeof(int).ToString();
			IS_CREATE_TASKField.Index = 56;
			fields.Add(IS_CREATE_TASKField);
			 
			DataSchemaField TRAY_PACKAGE_MODELField = new DataSchemaField();
			TRAY_PACKAGE_MODELField.Name = "TRAY_PACKAGE_MODEL";
			TRAY_PACKAGE_MODELField.Type = typeof(int).ToString();
			TRAY_PACKAGE_MODELField.Index = 57;
			fields.Add(TRAY_PACKAGE_MODELField);
			 
			DataSchemaField TRAY_MODELField = new DataSchemaField();
			TRAY_MODELField.Name = "TRAY_MODEL";
			TRAY_MODELField.Type = typeof(string).ToString();
			TRAY_MODELField.Index = 58;
			fields.Add(TRAY_MODELField);
			 
			DataSchemaField BATCH_COMMEND_WAYField = new DataSchemaField();
			BATCH_COMMEND_WAYField.Name = "BATCH_COMMEND_WAY";
			BATCH_COMMEND_WAYField.Type = typeof(int).ToString();
			BATCH_COMMEND_WAYField.Index = 59;
			fields.Add(BATCH_COMMEND_WAYField);
			 
			DataSchemaField BACK_STOCK_RULEField = new DataSchemaField();
			BACK_STOCK_RULEField.Name = "BACK_STOCK_RULE";
			BACK_STOCK_RULEField.Type = typeof(int).ToString();
			BACK_STOCK_RULEField.Index = 60;
			fields.Add(BACK_STOCK_RULEField);
			 
			DataSchemaField PART_CLASSIFY_AREA_NOField = new DataSchemaField();
			PART_CLASSIFY_AREA_NOField.Name = "PART_CLASSIFY_AREA_NO";
			PART_CLASSIFY_AREA_NOField.Type = typeof(string).ToString();
			PART_CLASSIFY_AREA_NOField.Index = 61;
			fields.Add(PART_CLASSIFY_AREA_NOField);
			 
			DataSchemaField LINE_SITE_DLOCField = new DataSchemaField();
			LINE_SITE_DLOCField.Name = "LINE_SITE_DLOC";
			LINE_SITE_DLOCField.Type = typeof(string).ToString();
			LINE_SITE_DLOCField.Index = 62;
			fields.Add(LINE_SITE_DLOCField);
			 
			DataSchemaField SCAN_BARCODE_FLAGField = new DataSchemaField();
			SCAN_BARCODE_FLAGField.Name = "SCAN_BARCODE_FLAG";
			SCAN_BARCODE_FLAGField.Type = typeof(int).ToString();
			SCAN_BARCODE_FLAGField.Index = 63;
			fields.Add(SCAN_BARCODE_FLAGField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 64;
			fields.Add(IDField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 65;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 66;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField IS_OUTPUTField = new DataSchemaField();
			IS_OUTPUTField.Name = "IS_OUTPUT";
			IS_OUTPUTField.Type = typeof(bool).ToString();
			IS_OUTPUTField.Index = 67;
			fields.Add(IS_OUTPUTField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 68;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 69;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField FRAGMENT_NUMField = new DataSchemaField();
			FRAGMENT_NUMField.Name = "FRAGMENT_NUM";
			FRAGMENT_NUMField.Type = typeof(decimal).ToString();
			FRAGMENT_NUMField.Index = 70;
			fields.Add(FRAGMENT_NUMField);
			 
			DataSchemaField FIDField = new DataSchemaField();
			FIDField.Name = "FID";
			FIDField.Type = typeof(Guid).ToString();
			FIDField.Index = 71;
			fields.Add(FIDField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 72;
			fields.Add(CREATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public string PartEname{ get;set; }		
				
		[DataMember]
		public string PartNickname{ get;set; }		
				
		[DataMember]
		public string PartUnits{ get;set; }		
				
		[DataMember]
		public string InhousePackageModel{ get;set; }		
				
		[DataMember]
		public int? InhousePackage{ get;set; }		
				
		[DataMember]
		public string InboundPackageModel{ get;set; }		
				
		[DataMember]
		public int? InboundPackage{ get;set; }		
				
		[DataMember]
		public string PackageModel{ get;set; }		
				
		[DataMember]
		public int? Package{ get;set; }		
				
		[DataMember]
		public string LogicalPk{ get;set; }		
				
		[DataMember]
		public string Route{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public decimal? OccupyArea{ get;set; }		
				
		[DataMember]
		public string Dloc{ get;set; }		
				
		[DataMember]
		public decimal? Max{ get;set; }		
				
		[DataMember]
		public decimal? Min{ get;set; }		
				
		[DataMember]
		public int? RowNumber{ get;set; }		
				
		[DataMember]
		public int? LineNumber{ get;set; }		
				
		[DataMember]
		public int? HighNumber{ get;set; }		
				
		[DataMember]
		public string MaterialGroup{ get;set; }		
				
		[DataMember]
		public string Keeper{ get;set; }		
				
		[DataMember]
		public string Transer{ get;set; }		
				
		[DataMember]
		public string Informationer{ get;set; }		
				
		[DataMember]
		public string Eloc{ get;set; }		
				
		[DataMember]
		public int? SafeStock{ get;set; }		
				
		[DataMember]
		public decimal? Stocks{ get;set; }		
				
		[DataMember]
		public decimal? FrozenStocks{ get;set; }		
				
		[DataMember]
		public decimal? AvailbleStocks{ get;set; }		
				
				
		private int? _IsBatch = 0;
		
		[DataMember]	
		public int? IsBatch
		{
			get
			{
				return _IsBatch;
			}
			set
			{
				_IsBatch = value;
			}
		}
				
				
		[DataMember]
		public string WmsRule{ get;set; }		
				
		[DataMember]
		public decimal? Counter{ get;set; }		
				
		[DataMember]
		public decimal? PartWeight{ get;set; }		
				
		[DataMember]
		public string PartCls{ get;set; }		
				
		[DataMember]
		public int? IsRepack{ get;set; }		
				
		[DataMember]
		public string RepackRoute{ get;set; }		
				
		[DataMember]
		public int? IsTriggerPull{ get;set; }		
				
		[DataMember]
		public string TriggerWmNo{ get;set; }		
				
		[DataMember]
		public string TriggerZoneNo{ get;set; }		
				
		[DataMember]
		public string TriggerDloc{ get;set; }		
				
		[DataMember]
		public int? EmgTime{ get;set; }		
				
		[DataMember]
		public string SupperZoneDloc{ get;set; }		
				
		[DataMember]
		public int? CheckType{ get;set; }		
				
		[DataMember]
		public string BusinessPk{ get;set; }		
				
		[DataMember]
		public int? RepackageAmount{ get;set; }		
				
		[DataMember]
		public string PickupRoute{ get;set; }		
				
		[DataMember]
		public string ShelfRoute{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public int? TrayOutIsall{ get;set; }		
				
		[DataMember]
		public int? IsCreateTask{ get;set; }		
				
		[DataMember]
		public int? TrayPackageModel{ get;set; }		
				
		[DataMember]
		public string TrayModel{ get;set; }		
				
		[DataMember]
		public int? BatchCommendWay{ get;set; }		
				
		[DataMember]
		public int? BackStockRule{ get;set; }		
				
		[DataMember]
		public string PartClassifyAreaNo{ get;set; }		
				
		[DataMember]
		public string LineSiteDloc{ get;set; }		
				
		[DataMember]
		public int? ScanBarcodeFlag{ get;set; }		
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public bool? IsOutput{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public decimal? FragmentNum{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PartsStockInfo info = new PartsStockInfo();

			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.SupplierNum = this.SupplierNum;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.PartEname = this.PartEname;
			info.PartNickname = this.PartNickname;
			info.PartUnits = this.PartUnits;
			info.InhousePackageModel = this.InhousePackageModel;
			info.InhousePackage = this.InhousePackage;
			info.InboundPackageModel = this.InboundPackageModel;
			info.InboundPackage = this.InboundPackage;
			info.PackageModel = this.PackageModel;
			info.Package = this.Package;
			info.LogicalPk = this.LogicalPk;
			info.Route = this.Route;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.OccupyArea = this.OccupyArea;
			info.Dloc = this.Dloc;
			info.Max = this.Max;
			info.Min = this.Min;
			info.RowNumber = this.RowNumber;
			info.LineNumber = this.LineNumber;
			info.HighNumber = this.HighNumber;
			info.MaterialGroup = this.MaterialGroup;
			info.Keeper = this.Keeper;
			info.Transer = this.Transer;
			info.Informationer = this.Informationer;
			info.Eloc = this.Eloc;
			info.SafeStock = this.SafeStock;
			info.Stocks = this.Stocks;
			info.FrozenStocks = this.FrozenStocks;
			info.AvailbleStocks = this.AvailbleStocks;
			info.IsBatch = this.IsBatch;
			info.WmsRule = this.WmsRule;
			info.Counter = this.Counter;
			info.PartWeight = this.PartWeight;
			info.PartCls = this.PartCls;
			info.IsRepack = this.IsRepack;
			info.RepackRoute = this.RepackRoute;
			info.IsTriggerPull = this.IsTriggerPull;
			info.TriggerWmNo = this.TriggerWmNo;
			info.TriggerZoneNo = this.TriggerZoneNo;
			info.TriggerDloc = this.TriggerDloc;
			info.EmgTime = this.EmgTime;
			info.SupperZoneDloc = this.SupperZoneDloc;
			info.CheckType = this.CheckType;
			info.BusinessPk = this.BusinessPk;
			info.RepackageAmount = this.RepackageAmount;
			info.PickupRoute = this.PickupRoute;
			info.ShelfRoute = this.ShelfRoute;
			info.Comments = this.Comments;
			info.TrayOutIsall = this.TrayOutIsall;
			info.IsCreateTask = this.IsCreateTask;
			info.TrayPackageModel = this.TrayPackageModel;
			info.TrayModel = this.TrayModel;
			info.BatchCommendWay = this.BatchCommendWay;
			info.BackStockRule = this.BackStockRule;
			info.PartClassifyAreaNo = this.PartClassifyAreaNo;
			info.LineSiteDloc = this.LineSiteDloc;
			info.ScanBarcodeFlag = this.ScanBarcodeFlag;
			info.Id = this.Id;
			info.ModifyUser = this.ModifyUser;
			info.ValidFlag = this.ValidFlag;
			info.IsOutput = this.IsOutput;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.FragmentNum = this.FragmentNum;
			info.Fid = this.Fid;
			info.CreateDate = this.CreateDate;
			return info;			
		}
		 
		public PartsStockInfo Clone()
		{
			return ((ICloneable) this).Clone() as PartsStockInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PartsStockInfoCollection对应表[TM_BAS_PARTS_STOCK]
    /// </summary>
	public partial class PartsStockInfoCollection : BusinessObjectCollection<PartsStockInfo>
	{
		public PartsStockInfoCollection():base("TM_BAS_PARTS_STOCK"){}	
	}
}
