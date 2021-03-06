#region Declaim
//---------------------------------------------------------------------------
// Name:		BarcodeStatusInfo
// Function: 	Expose data in table BarcodeStatus from database as business object to MES system.
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
    /// BarcodeStatusInfo对应表[TT_WMM_BARCODE_STATUS]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class BarcodeStatusInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public BarcodeStatusInfo( 
					long aId,

					Guid aFid,

					Guid aBarcodeFid,

					string aPartNo,

					string aPartCname,

					string aBarcodeData,

					int aBarcodeStatus,

					string aPackageModel,

					decimal aPackage,

					decimal aCurrentQty,

					string aMeasuringUnitNo,

					string aSupplierNum,

					string aPlant,

					string aAssemblyLine,

					string aLocation,

					string aDock,

					string aWmNo,

					string aZoneNo,

					string aDloc,

					string aBatthNo,

					string aRfidNo,

					string aAsnRunsheetNo,

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
		 
			BarcodeFid = aBarcodeFid;
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			BarcodeData = aBarcodeData;
		 
			BarcodeStatus = aBarcodeStatus;
		 
			PackageModel = aPackageModel;
		 
			Package = aPackage;
		 
			CurrentQty = aCurrentQty;
		 
			MeasuringUnitNo = aMeasuringUnitNo;
		 
			SupplierNum = aSupplierNum;
		 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			Location = aLocation;
		 
			Dock = aDock;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			Dloc = aDloc;
		 
			BatthNo = aBatthNo;
		 
			RfidNo = aRfidNo;
		 
			AsnRunsheetNo = aAsnRunsheetNo;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public BarcodeStatusInfo():base("TT_WMM_BARCODE_STATUS")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                           _Keys = keys.ToArray();
			
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
			 
			DataSchemaField BARCODE_FIDField = new DataSchemaField();
			BARCODE_FIDField.Name = "BARCODE_FID";
			BARCODE_FIDField.Type = typeof(Guid).ToString();
			BARCODE_FIDField.Index = 2;
			fields.Add(BARCODE_FIDField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 3;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_CNAMEField = new DataSchemaField();
			PART_CNAMEField.Name = "PART_CNAME";
			PART_CNAMEField.Type = typeof(string).ToString();
			PART_CNAMEField.Index = 4;
			fields.Add(PART_CNAMEField);
			 
			DataSchemaField BARCODE_DATAField = new DataSchemaField();
			BARCODE_DATAField.Name = "BARCODE_DATA";
			BARCODE_DATAField.Type = typeof(string).ToString();
			BARCODE_DATAField.Index = 5;
			fields.Add(BARCODE_DATAField);
			 
			DataSchemaField BARCODE_STATUSField = new DataSchemaField();
			BARCODE_STATUSField.Name = "BARCODE_STATUS";
			BARCODE_STATUSField.Type = typeof(int).ToString();
			BARCODE_STATUSField.Index = 6;
			fields.Add(BARCODE_STATUSField);
			 
			DataSchemaField PACKAGE_MODELField = new DataSchemaField();
			PACKAGE_MODELField.Name = "PACKAGE_MODEL";
			PACKAGE_MODELField.Type = typeof(string).ToString();
			PACKAGE_MODELField.Index = 7;
			fields.Add(PACKAGE_MODELField);
			 
			DataSchemaField PACKAGEField = new DataSchemaField();
			PACKAGEField.Name = "PACKAGE";
			PACKAGEField.Type = typeof(decimal).ToString();
			PACKAGEField.Index = 8;
			fields.Add(PACKAGEField);
			 
			DataSchemaField CURRENT_QTYField = new DataSchemaField();
			CURRENT_QTYField.Name = "CURRENT_QTY";
			CURRENT_QTYField.Type = typeof(decimal).ToString();
			CURRENT_QTYField.Index = 9;
			fields.Add(CURRENT_QTYField);
			 
			DataSchemaField MEASURING_UNIT_NOField = new DataSchemaField();
			MEASURING_UNIT_NOField.Name = "MEASURING_UNIT_NO";
			MEASURING_UNIT_NOField.Type = typeof(string).ToString();
			MEASURING_UNIT_NOField.Index = 10;
			fields.Add(MEASURING_UNIT_NOField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 11;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 12;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 13;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField LOCATIONField = new DataSchemaField();
			LOCATIONField.Name = "LOCATION";
			LOCATIONField.Type = typeof(string).ToString();
			LOCATIONField.Index = 14;
			fields.Add(LOCATIONField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 15;
			fields.Add(DOCKField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 16;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 17;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField DLOCField = new DataSchemaField();
			DLOCField.Name = "DLOC";
			DLOCField.Type = typeof(string).ToString();
			DLOCField.Index = 18;
			fields.Add(DLOCField);
			 
			DataSchemaField BATTH_NOField = new DataSchemaField();
			BATTH_NOField.Name = "BATTH_NO";
			BATTH_NOField.Type = typeof(string).ToString();
			BATTH_NOField.Index = 19;
			fields.Add(BATTH_NOField);
			 
			DataSchemaField RFID_NOField = new DataSchemaField();
			RFID_NOField.Name = "RFID_NO";
			RFID_NOField.Type = typeof(string).ToString();
			RFID_NOField.Index = 20;
			fields.Add(RFID_NOField);
			 
			DataSchemaField ASN_RUNSHEET_NOField = new DataSchemaField();
			ASN_RUNSHEET_NOField.Name = "ASN_RUNSHEET_NO";
			ASN_RUNSHEET_NOField.Type = typeof(string).ToString();
			ASN_RUNSHEET_NOField.Index = 21;
			fields.Add(ASN_RUNSHEET_NOField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 22;
			fields.Add(COMMENTSField);
			 
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
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public Guid? BarcodeFid{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public string BarcodeData{ get;set; }		
				
		[DataMember]
		public int? BarcodeStatus{ get;set; }		
				
		[DataMember]
		public string PackageModel{ get;set; }		
				
		[DataMember]
		public decimal? Package{ get;set; }		
				
		[DataMember]
		public decimal? CurrentQty{ get;set; }		
				
		[DataMember]
		public string MeasuringUnitNo{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
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
		public string BatthNo{ get;set; }		
				
		[DataMember]
		public string RfidNo{ get;set; }		
				
		[DataMember]
		public string AsnRunsheetNo{ get;set; }		
				
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
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			BarcodeStatusInfo info = new BarcodeStatusInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.BarcodeFid = this.BarcodeFid;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.BarcodeData = this.BarcodeData;
			info.BarcodeStatus = this.BarcodeStatus;
			info.PackageModel = this.PackageModel;
			info.Package = this.Package;
			info.CurrentQty = this.CurrentQty;
			info.MeasuringUnitNo = this.MeasuringUnitNo;
			info.SupplierNum = this.SupplierNum;
			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.Location = this.Location;
			info.Dock = this.Dock;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.Dloc = this.Dloc;
			info.BatthNo = this.BatthNo;
			info.RfidNo = this.RfidNo;
			info.AsnRunsheetNo = this.AsnRunsheetNo;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public BarcodeStatusInfo Clone()
		{
			return ((ICloneable) this).Clone() as BarcodeStatusInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// BarcodeStatusInfoCollection对应表[TT_WMM_BARCODE_STATUS]
    /// </summary>
	public partial class BarcodeStatusInfoCollection : BusinessObjectCollection<BarcodeStatusInfo>
	{
		public BarcodeStatusInfoCollection():base("TT_WMM_BARCODE_STATUS"){}	
	}
}
