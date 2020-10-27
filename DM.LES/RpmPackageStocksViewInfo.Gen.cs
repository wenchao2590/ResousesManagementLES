#region Declaim
//---------------------------------------------------------------------------
// Name:		RpmPackageStocksViewInfo
// Function: 	Expose data in table RpmPackageStocksView from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年1月8日
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
    /// RpmPackageStocksViewInfo对应表[V_TM_RPM_PACKAGE_STOCKS_VIEW]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class RpmPackageStocksViewInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public RpmPackageStocksViewInfo( 
					string aTranser,

					string aPlant,

					string aLogisticLation,

					string aAssemblyLine,

					string aPackageTypeName,

					decimal aStock,

					int aTranType,

					decimal aWhFee,

					decimal aAvailableStock,

					int aLogisticesLeadtime,

					DateTime aCreateDate,

					int aHighNumber,

					decimal aPackageStock,

					int aPackageType,

					int aStockId,

					string aDock,

					string aPackageCname,

					string aRoute,

					string aPlantZone,

					string aInformationer,

					decimal aCounter,

					int aStockType,

					string aWmNo,

					string aUpdateUser,

					decimal aPackageFee,

					string aComments,

					decimal aPackageFreezeStock,

					decimal aOccupyArea,

					decimal aMax,

					string aEloc,

					string aPackageEname,

					string aZoneNo,

					string aKeeper,

					string aPackageNo,

					decimal aFreezeStock,

					int aStockState,

					decimal aSage,

					string aWorkshop,

					string aDloc,

					string aCreateUser,

					decimal aPackageAvailableStock,

					decimal aMin,

					decimal aTransFee,

					DateTime aUpdateDate

				 
		) : this()
		{
			 
			Transer = aTranser;
		 
			Plant = aPlant;
		 
			LogisticLation = aLogisticLation;
		 
			AssemblyLine = aAssemblyLine;
		 
			PackageTypeName = aPackageTypeName;
		 
			Stock = aStock;
		 
			TranType = aTranType;
		 
			WhFee = aWhFee;
		 
			AvailableStock = aAvailableStock;
		 
			LogisticesLeadtime = aLogisticesLeadtime;
		 
			CreateDate = aCreateDate;
		 
			HighNumber = aHighNumber;
		 
			PackageStock = aPackageStock;
		 
			PackageType = aPackageType;
		 
			StockId = aStockId;
		 
			Dock = aDock;
		 
			PackageCname = aPackageCname;
		 
			Route = aRoute;
		 
			PlantZone = aPlantZone;
		 
			Informationer = aInformationer;
		 
			Counter = aCounter;
		 
			StockType = aStockType;
		 
			WmNo = aWmNo;
		 
			UpdateUser = aUpdateUser;
		 
			PackageFee = aPackageFee;
		 
			Comments = aComments;
		 
			PackageFreezeStock = aPackageFreezeStock;
		 
			OccupyArea = aOccupyArea;
		 
			Max = aMax;
		 
			Eloc = aEloc;
		 
			PackageEname = aPackageEname;
		 
			ZoneNo = aZoneNo;
		 
			Keeper = aKeeper;
		 
			PackageNo = aPackageNo;
		 
			FreezeStock = aFreezeStock;
		 
			StockState = aStockState;
		 
			Sage = aSage;
		 
			Workshop = aWorkshop;
		 
			Dloc = aDloc;
		 
			CreateUser = aCreateUser;
		 
			PackageAvailableStock = aPackageAvailableStock;
		 
			Min = aMin;
		 
			TransFee = aTransFee;
		 
			UpdateDate = aUpdateDate;
		}
		
		public RpmPackageStocksViewInfo():base("V_TM_RPM_PACKAGE_STOCKS_VIEW")
		{
			List<string> keys = new List<string>();
			                                            _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField TRANSERField = new DataSchemaField();
			TRANSERField.Name = "TRANSER";
			TRANSERField.Type = typeof(string).ToString();
			TRANSERField.Index = 0;
			fields.Add(TRANSERField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 1;
			fields.Add(PLANTField);
			 
			DataSchemaField LOGISTIC_LATIONField = new DataSchemaField();
			LOGISTIC_LATIONField.Name = "LOGISTIC_LATION";
			LOGISTIC_LATIONField.Type = typeof(string).ToString();
			LOGISTIC_LATIONField.Index = 2;
			fields.Add(LOGISTIC_LATIONField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 3;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField PACKAGE_TYPE_NameField = new DataSchemaField();
			PACKAGE_TYPE_NameField.Name = "PACKAGE_TYPE_Name";
			PACKAGE_TYPE_NameField.Type = typeof(string).ToString();
			PACKAGE_TYPE_NameField.Index = 4;
			fields.Add(PACKAGE_TYPE_NameField);
			 
			DataSchemaField STOCKField = new DataSchemaField();
			STOCKField.Name = "STOCK";
			STOCKField.Type = typeof(decimal).ToString();
			STOCKField.Index = 5;
			fields.Add(STOCKField);
			 
			DataSchemaField TRAN_TYPEField = new DataSchemaField();
			TRAN_TYPEField.Name = "TRAN_TYPE";
			TRAN_TYPEField.Type = typeof(int).ToString();
			TRAN_TYPEField.Index = 6;
			fields.Add(TRAN_TYPEField);
			 
			DataSchemaField WH_FEEField = new DataSchemaField();
			WH_FEEField.Name = "WH_FEE";
			WH_FEEField.Type = typeof(decimal).ToString();
			WH_FEEField.Index = 7;
			fields.Add(WH_FEEField);
			 
			DataSchemaField AVAILABLE_STOCKField = new DataSchemaField();
			AVAILABLE_STOCKField.Name = "AVAILABLE_STOCK";
			AVAILABLE_STOCKField.Type = typeof(decimal).ToString();
			AVAILABLE_STOCKField.Index = 8;
			fields.Add(AVAILABLE_STOCKField);
			 
			DataSchemaField LOGISTICES_LEADTIMEField = new DataSchemaField();
			LOGISTICES_LEADTIMEField.Name = "LOGISTICES_LEADTIME";
			LOGISTICES_LEADTIMEField.Type = typeof(int).ToString();
			LOGISTICES_LEADTIMEField.Index = 9;
			fields.Add(LOGISTICES_LEADTIMEField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 10;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField HIGH_NUMBERField = new DataSchemaField();
			HIGH_NUMBERField.Name = "HIGH_NUMBER";
			HIGH_NUMBERField.Type = typeof(int).ToString();
			HIGH_NUMBERField.Index = 11;
			fields.Add(HIGH_NUMBERField);
			 
			DataSchemaField PACKAGE_STOCKField = new DataSchemaField();
			PACKAGE_STOCKField.Name = "PACKAGE_STOCK";
			PACKAGE_STOCKField.Type = typeof(decimal).ToString();
			PACKAGE_STOCKField.Index = 12;
			fields.Add(PACKAGE_STOCKField);
			 
			DataSchemaField PACKAGE_TYPEField = new DataSchemaField();
			PACKAGE_TYPEField.Name = "PACKAGE_TYPE";
			PACKAGE_TYPEField.Type = typeof(int).ToString();
			PACKAGE_TYPEField.Index = 13;
			fields.Add(PACKAGE_TYPEField);
			 
			DataSchemaField STOCK_IDField = new DataSchemaField();
			STOCK_IDField.Name = "STOCK_ID";
			STOCK_IDField.Type = typeof(int).ToString();
			STOCK_IDField.Index = 14;
			fields.Add(STOCK_IDField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 15;
			fields.Add(DOCKField);
			 
			DataSchemaField PACKAGE_CNAMEField = new DataSchemaField();
			PACKAGE_CNAMEField.Name = "PACKAGE_CNAME";
			PACKAGE_CNAMEField.Type = typeof(string).ToString();
			PACKAGE_CNAMEField.Index = 16;
			fields.Add(PACKAGE_CNAMEField);
			 
			DataSchemaField ROUTEField = new DataSchemaField();
			ROUTEField.Name = "ROUTE";
			ROUTEField.Type = typeof(string).ToString();
			ROUTEField.Index = 17;
			fields.Add(ROUTEField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 18;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField INFORMATIONERField = new DataSchemaField();
			INFORMATIONERField.Name = "INFORMATIONER";
			INFORMATIONERField.Type = typeof(string).ToString();
			INFORMATIONERField.Index = 19;
			fields.Add(INFORMATIONERField);
			 
			DataSchemaField COUNTERField = new DataSchemaField();
			COUNTERField.Name = "COUNTER";
			COUNTERField.Type = typeof(decimal).ToString();
			COUNTERField.Index = 20;
			fields.Add(COUNTERField);
			 
			DataSchemaField STOCK_TYPEField = new DataSchemaField();
			STOCK_TYPEField.Name = "STOCK_TYPE";
			STOCK_TYPEField.Type = typeof(int).ToString();
			STOCK_TYPEField.Index = 21;
			fields.Add(STOCK_TYPEField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 22;
			fields.Add(WM_NOField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 23;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField PACKAGE_FEEField = new DataSchemaField();
			PACKAGE_FEEField.Name = "PACKAGE_FEE";
			PACKAGE_FEEField.Type = typeof(decimal).ToString();
			PACKAGE_FEEField.Index = 24;
			fields.Add(PACKAGE_FEEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 25;
			fields.Add(COMMENTSField);
			 
			DataSchemaField PACKAGE_FREEZE_STOCKField = new DataSchemaField();
			PACKAGE_FREEZE_STOCKField.Name = "PACKAGE_FREEZE_STOCK";
			PACKAGE_FREEZE_STOCKField.Type = typeof(decimal).ToString();
			PACKAGE_FREEZE_STOCKField.Index = 26;
			fields.Add(PACKAGE_FREEZE_STOCKField);
			 
			DataSchemaField OCCUPY_AREAField = new DataSchemaField();
			OCCUPY_AREAField.Name = "OCCUPY_AREA";
			OCCUPY_AREAField.Type = typeof(decimal).ToString();
			OCCUPY_AREAField.Index = 27;
			fields.Add(OCCUPY_AREAField);
			 
			DataSchemaField MAXField = new DataSchemaField();
			MAXField.Name = "MAX";
			MAXField.Type = typeof(decimal).ToString();
			MAXField.Index = 28;
			fields.Add(MAXField);
			 
			DataSchemaField ELOCField = new DataSchemaField();
			ELOCField.Name = "ELOC";
			ELOCField.Type = typeof(string).ToString();
			ELOCField.Index = 29;
			fields.Add(ELOCField);
			 
			DataSchemaField PACKAGE_ENAMEField = new DataSchemaField();
			PACKAGE_ENAMEField.Name = "PACKAGE_ENAME";
			PACKAGE_ENAMEField.Type = typeof(string).ToString();
			PACKAGE_ENAMEField.Index = 30;
			fields.Add(PACKAGE_ENAMEField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 31;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField KEEPERField = new DataSchemaField();
			KEEPERField.Name = "KEEPER";
			KEEPERField.Type = typeof(string).ToString();
			KEEPERField.Index = 32;
			fields.Add(KEEPERField);
			 
			DataSchemaField PACKAGE_NOField = new DataSchemaField();
			PACKAGE_NOField.Name = "PACKAGE_NO";
			PACKAGE_NOField.Type = typeof(string).ToString();
			PACKAGE_NOField.Index = 33;
			fields.Add(PACKAGE_NOField);
			 
			DataSchemaField FREEZE_STOCKField = new DataSchemaField();
			FREEZE_STOCKField.Name = "FREEZE_STOCK";
			FREEZE_STOCKField.Type = typeof(decimal).ToString();
			FREEZE_STOCKField.Index = 34;
			fields.Add(FREEZE_STOCKField);
			 
			DataSchemaField STOCK_STATEField = new DataSchemaField();
			STOCK_STATEField.Name = "STOCK_STATE";
			STOCK_STATEField.Type = typeof(int).ToString();
			STOCK_STATEField.Index = 35;
			fields.Add(STOCK_STATEField);
			 
			DataSchemaField SAGEField = new DataSchemaField();
			SAGEField.Name = "SAGE";
			SAGEField.Type = typeof(decimal).ToString();
			SAGEField.Index = 36;
			fields.Add(SAGEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 37;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField DLOCField = new DataSchemaField();
			DLOCField.Name = "DLOC";
			DLOCField.Type = typeof(string).ToString();
			DLOCField.Index = 38;
			fields.Add(DLOCField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 39;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField PACKAGE_AVAILABLE_STOCKField = new DataSchemaField();
			PACKAGE_AVAILABLE_STOCKField.Name = "PACKAGE_AVAILABLE_STOCK";
			PACKAGE_AVAILABLE_STOCKField.Type = typeof(decimal).ToString();
			PACKAGE_AVAILABLE_STOCKField.Index = 40;
			fields.Add(PACKAGE_AVAILABLE_STOCKField);
			 
			DataSchemaField MINField = new DataSchemaField();
			MINField.Name = "MIN";
			MINField.Type = typeof(decimal).ToString();
			MINField.Index = 41;
			fields.Add(MINField);
			 
			DataSchemaField TRANS_FEEField = new DataSchemaField();
			TRANS_FEEField.Name = "TRANS_FEE";
			TRANS_FEEField.Type = typeof(decimal).ToString();
			TRANS_FEEField.Index = 42;
			fields.Add(TRANS_FEEField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 43;
			fields.Add(UPDATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string Transer{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string LogisticLation{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string PackageTypeName{ get;set; }		
				
		[DataMember]
		public decimal Stock{ get;set; }		
				
		[DataMember]
		public int? TranType{ get;set; }		
				
		[DataMember]
		public decimal? WhFee{ get;set; }		
				
		[DataMember]
		public decimal? AvailableStock{ get;set; }		
				
		[DataMember]
		public int? LogisticesLeadtime{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public int? HighNumber{ get;set; }		
				
		[DataMember]
		public decimal? PackageStock{ get;set; }		
				
		[DataMember]
		public int? PackageType{ get;set; }		
				
		[DataMember]
		public int StockId{ get;set; }		
				
		[DataMember]
		public string Dock{ get;set; }		
				
		[DataMember]
		public string PackageCname{ get;set; }		
				
		[DataMember]
		public string Route{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Informationer{ get;set; }		
				
		[DataMember]
		public decimal? Counter{ get;set; }		
				
		[DataMember]
		public int StockType{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public decimal? PackageFee{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public decimal? PackageFreezeStock{ get;set; }		
				
		[DataMember]
		public decimal? OccupyArea{ get;set; }		
				
		[DataMember]
		public decimal? Max{ get;set; }		
				
		[DataMember]
		public string Eloc{ get;set; }		
				
		[DataMember]
		public string PackageEname{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public string Keeper{ get;set; }		
				
		[DataMember]
		public string PackageNo{ get;set; }		
				
		[DataMember]
		public decimal? FreezeStock{ get;set; }		
				
		[DataMember]
		public int StockState{ get;set; }		
				
		[DataMember]
		public decimal? Sage{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string Dloc{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public decimal? PackageAvailableStock{ get;set; }		
				
		[DataMember]
		public decimal? Min{ get;set; }		
				
		[DataMember]
		public decimal? TransFee{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			RpmPackageStocksViewInfo info = new RpmPackageStocksViewInfo();

			info.Transer = this.Transer;
			info.Plant = this.Plant;
			info.LogisticLation = this.LogisticLation;
			info.AssemblyLine = this.AssemblyLine;
			info.PackageTypeName = this.PackageTypeName;
			info.Stock = this.Stock;
			info.TranType = this.TranType;
			info.WhFee = this.WhFee;
			info.AvailableStock = this.AvailableStock;
			info.LogisticesLeadtime = this.LogisticesLeadtime;
			info.CreateDate = this.CreateDate;
			info.HighNumber = this.HighNumber;
			info.PackageStock = this.PackageStock;
			info.PackageType = this.PackageType;
			info.StockId = this.StockId;
			info.Dock = this.Dock;
			info.PackageCname = this.PackageCname;
			info.Route = this.Route;
			info.PlantZone = this.PlantZone;
			info.Informationer = this.Informationer;
			info.Counter = this.Counter;
			info.StockType = this.StockType;
			info.WmNo = this.WmNo;
			info.UpdateUser = this.UpdateUser;
			info.PackageFee = this.PackageFee;
			info.Comments = this.Comments;
			info.PackageFreezeStock = this.PackageFreezeStock;
			info.OccupyArea = this.OccupyArea;
			info.Max = this.Max;
			info.Eloc = this.Eloc;
			info.PackageEname = this.PackageEname;
			info.ZoneNo = this.ZoneNo;
			info.Keeper = this.Keeper;
			info.PackageNo = this.PackageNo;
			info.FreezeStock = this.FreezeStock;
			info.StockState = this.StockState;
			info.Sage = this.Sage;
			info.Workshop = this.Workshop;
			info.Dloc = this.Dloc;
			info.CreateUser = this.CreateUser;
			info.PackageAvailableStock = this.PackageAvailableStock;
			info.Min = this.Min;
			info.TransFee = this.TransFee;
			info.UpdateDate = this.UpdateDate;
			return info;			
		}
		 
		public RpmPackageStocksViewInfo Clone()
		{
			return ((ICloneable) this).Clone() as RpmPackageStocksViewInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// RpmPackageStocksViewInfoCollection对应表[V_TM_RPM_PACKAGE_STOCKS_VIEW]
    /// </summary>
	public partial class RpmPackageStocksViewInfoCollection : BusinessObjectCollection<RpmPackageStocksViewInfo>
	{
		public RpmPackageStocksViewInfoCollection():base("V_TM_RPM_PACKAGE_STOCKS_VIEW"){}	
	}
}
