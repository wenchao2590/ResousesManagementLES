#region Declaim
//---------------------------------------------------------------------------
// Name:		PackageBarcodeInfo
// Function: 	Expose data in table PackageBarcode from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月9日
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
    /// PackageBarcodeInfo对应表[TT_PCM_PACKAGE_BARCODE]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PackageBarcodeInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PackageBarcodeInfo( 
					long aId,

					Guid aFid,

					string aBarcodeNo,

					string aSupplierNum,

					string aSupplierName,

					string aPlant,

					string aWmNo,

					string aZoneNo,

					string aDloc,

					string aDock,

					string aPackageOrderNo,

					int aPackageOrderType,

					Guid aPackageOrderFid,

					int aPrintTimes,

					DateTime aLastPrintDate,

					string aLastPrintUser,

					int aStatus,

					string aComments,

					bool aValidFlag,

					DateTime aCreateDate,

					string aCreateUser,

					DateTime aModifyDate,

					string aModifyUser

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			BarcodeNo = aBarcodeNo;
		 
			SupplierNum = aSupplierNum;
		 
			SupplierName = aSupplierName;
		 
			Plant = aPlant;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			Dloc = aDloc;
		 
			Dock = aDock;
		 
			PackageOrderNo = aPackageOrderNo;
		 
			PackageOrderType = aPackageOrderType;
		 
			PackageOrderFid = aPackageOrderFid;
		 
			PrintTimes = aPrintTimes;
		 
			LastPrintDate = aLastPrintDate;
		 
			LastPrintUser = aLastPrintUser;
		 
			Status = aStatus;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		}
		
		public PackageBarcodeInfo():base("TT_PCM_PACKAGE_BARCODE")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                      _Keys = keys.ToArray();
			
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
			 
			DataSchemaField BARCODE_NOField = new DataSchemaField();
			BARCODE_NOField.Name = "BARCODE_NO";
			BARCODE_NOField.Type = typeof(string).ToString();
			BARCODE_NOField.Index = 2;
			fields.Add(BARCODE_NOField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 3;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 4;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 5;
			fields.Add(PLANTField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 6;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 7;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField DLOCField = new DataSchemaField();
			DLOCField.Name = "DLOC";
			DLOCField.Type = typeof(string).ToString();
			DLOCField.Index = 8;
			fields.Add(DLOCField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 9;
			fields.Add(DOCKField);
			 
			DataSchemaField PACKAGE_ORDER_NOField = new DataSchemaField();
			PACKAGE_ORDER_NOField.Name = "PACKAGE_ORDER_NO";
			PACKAGE_ORDER_NOField.Type = typeof(string).ToString();
			PACKAGE_ORDER_NOField.Index = 10;
			fields.Add(PACKAGE_ORDER_NOField);
			 
			DataSchemaField PACKAGE_ORDER_TYPEField = new DataSchemaField();
			PACKAGE_ORDER_TYPEField.Name = "PACKAGE_ORDER_TYPE";
			PACKAGE_ORDER_TYPEField.Type = typeof(int).ToString();
			PACKAGE_ORDER_TYPEField.Index = 11;
			fields.Add(PACKAGE_ORDER_TYPEField);
			 
			DataSchemaField PACKAGE_ORDER_FIDField = new DataSchemaField();
			PACKAGE_ORDER_FIDField.Name = "PACKAGE_ORDER_FID";
			PACKAGE_ORDER_FIDField.Type = typeof(Guid).ToString();
			PACKAGE_ORDER_FIDField.Index = 12;
			fields.Add(PACKAGE_ORDER_FIDField);
			 
			DataSchemaField PRINT_TIMESField = new DataSchemaField();
			PRINT_TIMESField.Name = "PRINT_TIMES";
			PRINT_TIMESField.Type = typeof(int).ToString();
			PRINT_TIMESField.Index = 13;
			fields.Add(PRINT_TIMESField);
			 
			DataSchemaField LAST_PRINT_DATEField = new DataSchemaField();
			LAST_PRINT_DATEField.Name = "LAST_PRINT_DATE";
			LAST_PRINT_DATEField.Type = typeof(DateTime).ToString();
			LAST_PRINT_DATEField.Index = 14;
			fields.Add(LAST_PRINT_DATEField);
			 
			DataSchemaField LAST_PRINT_USERField = new DataSchemaField();
			LAST_PRINT_USERField.Name = "LAST_PRINT_USER";
			LAST_PRINT_USERField.Type = typeof(string).ToString();
			LAST_PRINT_USERField.Index = 15;
			fields.Add(LAST_PRINT_USERField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 16;
			fields.Add(STATUSField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 17;
			fields.Add(COMMENTSField);
			 
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
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string BarcodeNo{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public string Dloc{ get;set; }		
				
		[DataMember]
		public string Dock{ get;set; }		
				
		[DataMember]
		public string PackageOrderNo{ get;set; }		
				
		[DataMember]
		public int? PackageOrderType{ get;set; }		
				
		[DataMember]
		public Guid? PackageOrderFid{ get;set; }		
				
		[DataMember]
		public int? PrintTimes{ get;set; }		
				
		[DataMember]
		public DateTime? LastPrintDate{ get;set; }		
				
		[DataMember]
		public string LastPrintUser{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
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
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PackageBarcodeInfo info = new PackageBarcodeInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.BarcodeNo = this.BarcodeNo;
			info.SupplierNum = this.SupplierNum;
			info.SupplierName = this.SupplierName;
			info.Plant = this.Plant;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.Dloc = this.Dloc;
			info.Dock = this.Dock;
			info.PackageOrderNo = this.PackageOrderNo;
			info.PackageOrderType = this.PackageOrderType;
			info.PackageOrderFid = this.PackageOrderFid;
			info.PrintTimes = this.PrintTimes;
			info.LastPrintDate = this.LastPrintDate;
			info.LastPrintUser = this.LastPrintUser;
			info.Status = this.Status;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			return info;			
		}
		 
		public PackageBarcodeInfo Clone()
		{
			return ((ICloneable) this).Clone() as PackageBarcodeInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PackageBarcodeInfoCollection对应表[TT_PCM_PACKAGE_BARCODE]
    /// </summary>
	public partial class PackageBarcodeInfoCollection : BusinessObjectCollection<PackageBarcodeInfo>
	{
		public PackageBarcodeInfoCollection():base("TT_PCM_PACKAGE_BARCODE"){}	
	}
}
