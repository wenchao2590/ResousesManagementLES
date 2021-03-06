#region Declaim
//---------------------------------------------------------------------------
// Name:		PackageStocksAdjustInfo
// Function: 	Expose data in table PackageStocksAdjust from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月1日
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
    /// PackageStocksAdjustInfo对应表[TM_RPM_PACKAGE_STOCKS_ADJUST]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PackageStocksAdjustInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PackageStocksAdjustInfo( 
					int aAdjustId,

					int aStockId,

					string aPackageNo,

					string aPlant,

					string aAssemblyLine,

					string aPlantZone,

					string aWorkshop,

					string aWmNo,

					string aZoneNo,

					string aDloc,

					int aAdjustType,

					decimal aAdjustNum,

					string aComments,

					string aCreateUser,

					DateTime aCreateDate,

					string aUpdateUser,

					DateTime aUpdateDate

				 
		) : this()
		{
			 
			AdjustId = aAdjustId;
		 
			StockId = aStockId;
		 
			PackageNo = aPackageNo;
		 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			Dloc = aDloc;
		 
			AdjustType = aAdjustType;
		 
			AdjustNum = aAdjustNum;
		 
			Comments = aComments;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			UpdateUser = aUpdateUser;
		 
			UpdateDate = aUpdateDate;
		}
		
		public PackageStocksAdjustInfo():base("TM_RPM_PACKAGE_STOCKS_ADJUST")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ADJUST_ID");                _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField ADJUST_IDField = new DataSchemaField();
			ADJUST_IDField.Name = "ADJUST_ID";
			ADJUST_IDField.Type = typeof(int).ToString();
			ADJUST_IDField.Index = 0;
			fields.Add(ADJUST_IDField);
			 
			DataSchemaField STOCK_IDField = new DataSchemaField();
			STOCK_IDField.Name = "STOCK_ID";
			STOCK_IDField.Type = typeof(int).ToString();
			STOCK_IDField.Index = 1;
			fields.Add(STOCK_IDField);
			 
			DataSchemaField PACKAGE_NOField = new DataSchemaField();
			PACKAGE_NOField.Name = "PACKAGE_NO";
			PACKAGE_NOField.Type = typeof(string).ToString();
			PACKAGE_NOField.Index = 2;
			fields.Add(PACKAGE_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 3;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 4;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 5;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 6;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 7;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 8;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField DLOCField = new DataSchemaField();
			DLOCField.Name = "DLOC";
			DLOCField.Type = typeof(string).ToString();
			DLOCField.Index = 9;
			fields.Add(DLOCField);
			 
			DataSchemaField ADJUST_TYPEField = new DataSchemaField();
			ADJUST_TYPEField.Name = "ADJUST_TYPE";
			ADJUST_TYPEField.Type = typeof(int).ToString();
			ADJUST_TYPEField.Index = 10;
			fields.Add(ADJUST_TYPEField);
			 
			DataSchemaField ADJUST_NUMField = new DataSchemaField();
			ADJUST_NUMField.Name = "ADJUST_NUM";
			ADJUST_NUMField.Type = typeof(decimal).ToString();
			ADJUST_NUMField.Index = 11;
			fields.Add(ADJUST_NUMField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 12;
			fields.Add(COMMENTSField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 13;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 14;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 15;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 16;
			fields.Add(UPDATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public int AdjustId{ get;set; }		
				
		[DataMember]
		public int? StockId{ get;set; }		
				
		[DataMember]
		public string PackageNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public string Dloc{ get;set; }		
				
		[DataMember]
		public int AdjustType{ get;set; }		
				
		[DataMember]
		public decimal AdjustNum{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PackageStocksAdjustInfo info = new PackageStocksAdjustInfo();

			info.AdjustId = this.AdjustId;
			info.StockId = this.StockId;
			info.PackageNo = this.PackageNo;
			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.Dloc = this.Dloc;
			info.AdjustType = this.AdjustType;
			info.AdjustNum = this.AdjustNum;
			info.Comments = this.Comments;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.UpdateUser = this.UpdateUser;
			info.UpdateDate = this.UpdateDate;
			return info;			
		}
		 
		public PackageStocksAdjustInfo Clone()
		{
			return ((ICloneable) this).Clone() as PackageStocksAdjustInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PackageStocksAdjustInfoCollection对应表[TM_RPM_PACKAGE_STOCKS_ADJUST]
    /// </summary>
	public partial class PackageStocksAdjustInfoCollection : BusinessObjectCollection<PackageStocksAdjustInfo>
	{
		public PackageStocksAdjustInfoCollection():base("TM_RPM_PACKAGE_STOCKS_ADJUST"){}	
	}
}
