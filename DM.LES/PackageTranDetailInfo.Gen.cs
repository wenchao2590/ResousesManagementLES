#region Declaim
//---------------------------------------------------------------------------
// Name:		PackageTranDetailInfo
// Function: 	Expose data in table PackageTranDetail from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年5月26日
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
    /// PackageTranDetailInfo对应表[TT_PCM_PACKAGE_TRAN_DETAIL]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PackageTranDetailInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PackageTranDetailInfo( 
					long aId,

					Guid aFid,

					string aTranNo,

					int aTranType,

					string aBarcodeData,

					string aPartNo,

					string aPlant,

					string aAssemblyLine,

					string aSupplierNum,

					string aWmNo,

					string aZoneNo,

					string aDloc,

					string aTargetWm,

					string aTargetZone,

					string aTargetDloc,

					string aPackageNo,

					string aPackageCname,

					string aPackageEname,

					decimal aPackage,

					int aPackageQty,

					int aStatus,

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
		 
			TranNo = aTranNo;
		 
			TranType = aTranType;
		 
			BarcodeData = aBarcodeData;
		 
			PartNo = aPartNo;
		 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			SupplierNum = aSupplierNum;
		 
			WmNo = aWmNo;
		 
			ZoneNo = aZoneNo;
		 
			Dloc = aDloc;
		 
			TargetWm = aTargetWm;
		 
			TargetZone = aTargetZone;
		 
			TargetDloc = aTargetDloc;
		 
			PackageNo = aPackageNo;
		 
			PackageCname = aPackageCname;
		 
			PackageEname = aPackageEname;
		 
			Package = aPackage;
		 
			PackageQty = aPackageQty;
		 
			Status = aStatus;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public PackageTranDetailInfo():base("TT_PCM_PACKAGE_TRAN_DETAIL")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                          _Keys = keys.ToArray();
			
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
			 
			DataSchemaField TRAN_NOField = new DataSchemaField();
			TRAN_NOField.Name = "TRAN_NO";
			TRAN_NOField.Type = typeof(string).ToString();
			TRAN_NOField.Index = 2;
			fields.Add(TRAN_NOField);
			 
			DataSchemaField TRAN_TYPEField = new DataSchemaField();
			TRAN_TYPEField.Name = "TRAN_TYPE";
			TRAN_TYPEField.Type = typeof(int).ToString();
			TRAN_TYPEField.Index = 3;
			fields.Add(TRAN_TYPEField);
			 
			DataSchemaField BARCODE_DATAField = new DataSchemaField();
			BARCODE_DATAField.Name = "BARCODE_DATA";
			BARCODE_DATAField.Type = typeof(string).ToString();
			BARCODE_DATAField.Index = 4;
			fields.Add(BARCODE_DATAField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 5;
			fields.Add(PART_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 6;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 7;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 8;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 9;
			fields.Add(WM_NOField);
			 
			DataSchemaField ZONE_NOField = new DataSchemaField();
			ZONE_NOField.Name = "ZONE_NO";
			ZONE_NOField.Type = typeof(string).ToString();
			ZONE_NOField.Index = 10;
			fields.Add(ZONE_NOField);
			 
			DataSchemaField DLOCField = new DataSchemaField();
			DLOCField.Name = "DLOC";
			DLOCField.Type = typeof(string).ToString();
			DLOCField.Index = 11;
			fields.Add(DLOCField);
			 
			DataSchemaField TARGET_WMField = new DataSchemaField();
			TARGET_WMField.Name = "TARGET_WM";
			TARGET_WMField.Type = typeof(string).ToString();
			TARGET_WMField.Index = 12;
			fields.Add(TARGET_WMField);
			 
			DataSchemaField TARGET_ZONEField = new DataSchemaField();
			TARGET_ZONEField.Name = "TARGET_ZONE";
			TARGET_ZONEField.Type = typeof(string).ToString();
			TARGET_ZONEField.Index = 13;
			fields.Add(TARGET_ZONEField);
			 
			DataSchemaField TARGET_DLOCField = new DataSchemaField();
			TARGET_DLOCField.Name = "TARGET_DLOC";
			TARGET_DLOCField.Type = typeof(string).ToString();
			TARGET_DLOCField.Index = 14;
			fields.Add(TARGET_DLOCField);
			 
			DataSchemaField PACKAGE_NOField = new DataSchemaField();
			PACKAGE_NOField.Name = "PACKAGE_NO";
			PACKAGE_NOField.Type = typeof(string).ToString();
			PACKAGE_NOField.Index = 15;
			fields.Add(PACKAGE_NOField);
			 
			DataSchemaField PACKAGE_CNAMEField = new DataSchemaField();
			PACKAGE_CNAMEField.Name = "PACKAGE_CNAME";
			PACKAGE_CNAMEField.Type = typeof(string).ToString();
			PACKAGE_CNAMEField.Index = 16;
			fields.Add(PACKAGE_CNAMEField);
			 
			DataSchemaField PACKAGE_ENAMEField = new DataSchemaField();
			PACKAGE_ENAMEField.Name = "PACKAGE_ENAME";
			PACKAGE_ENAMEField.Type = typeof(string).ToString();
			PACKAGE_ENAMEField.Index = 17;
			fields.Add(PACKAGE_ENAMEField);
			 
			DataSchemaField PACKAGEField = new DataSchemaField();
			PACKAGEField.Name = "PACKAGE";
			PACKAGEField.Type = typeof(decimal).ToString();
			PACKAGEField.Index = 18;
			fields.Add(PACKAGEField);
			 
			DataSchemaField PACKAGE_QTYField = new DataSchemaField();
			PACKAGE_QTYField.Name = "PACKAGE_QTY";
			PACKAGE_QTYField.Type = typeof(int).ToString();
			PACKAGE_QTYField.Index = 19;
			fields.Add(PACKAGE_QTYField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 20;
			fields.Add(STATUSField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 21;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 22;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 23;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 24;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 25;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 26;
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
		public string TranNo{ get;set; }		
				
		[DataMember]
		public int? TranType{ get;set; }		
				
		[DataMember]
		public string BarcodeData{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string ZoneNo{ get;set; }		
				
		[DataMember]
		public string Dloc{ get;set; }		
				
		[DataMember]
		public string TargetWm{ get;set; }		
				
		[DataMember]
		public string TargetZone{ get;set; }		
				
		[DataMember]
		public string TargetDloc{ get;set; }		
				
		[DataMember]
		public string PackageNo{ get;set; }		
				
		[DataMember]
		public string PackageCname{ get;set; }		
				
		[DataMember]
		public string PackageEname{ get;set; }		
				
		[DataMember]
		public decimal? Package{ get;set; }		
				
		[DataMember]
		public int? PackageQty{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
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
			PackageTranDetailInfo info = new PackageTranDetailInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.TranNo = this.TranNo;
			info.TranType = this.TranType;
			info.BarcodeData = this.BarcodeData;
			info.PartNo = this.PartNo;
			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.SupplierNum = this.SupplierNum;
			info.WmNo = this.WmNo;
			info.ZoneNo = this.ZoneNo;
			info.Dloc = this.Dloc;
			info.TargetWm = this.TargetWm;
			info.TargetZone = this.TargetZone;
			info.TargetDloc = this.TargetDloc;
			info.PackageNo = this.PackageNo;
			info.PackageCname = this.PackageCname;
			info.PackageEname = this.PackageEname;
			info.Package = this.Package;
			info.PackageQty = this.PackageQty;
			info.Status = this.Status;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public PackageTranDetailInfo Clone()
		{
			return ((ICloneable) this).Clone() as PackageTranDetailInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PackageTranDetailInfoCollection对应表[TT_PCM_PACKAGE_TRAN_DETAIL]
    /// </summary>
	public partial class PackageTranDetailInfoCollection : BusinessObjectCollection<PackageTranDetailInfo>
	{
		public PackageTranDetailInfoCollection():base("TT_PCM_PACKAGE_TRAN_DETAIL"){}	
	}
}