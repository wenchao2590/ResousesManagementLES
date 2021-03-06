#region Declaim
//---------------------------------------------------------------------------
// Name:		PackageOutboundDetailInfo
// Function: 	Expose data in table PackageOutboundDetail from database as business object to MES system.
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
    /// PackageOutboundDetailInfo对应表[TT_PCM_PACKAGE_OUTBOUND_DETAIL]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PackageOutboundDetailInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PackageOutboundDetailInfo( 
					long aId,

					Guid aFid,

					Guid aOrderFid,

					string aOrderNo,

					string aSWmNo,

					string aSZoneNo,

					string aSDloc,

					string aTWmNo,

					string aTZoneNo,

					string aTDloc,

					string aSupplierNum,

					string aPackageModel,

					string aPackageCname,

					int aPackageType,

					int aPackageQty,

					string aPackageBarcode,

					Guid aPackageBarcodeFid,

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
		 
			OrderFid = aOrderFid;
		 
			OrderNo = aOrderNo;
		 
			SWmNo = aSWmNo;
		 
			SZoneNo = aSZoneNo;
		 
			SDloc = aSDloc;
		 
			TWmNo = aTWmNo;
		 
			TZoneNo = aTZoneNo;
		 
			TDloc = aTDloc;
		 
			SupplierNum = aSupplierNum;
		 
			PackageModel = aPackageModel;
		 
			PackageCname = aPackageCname;
		 
			PackageType = aPackageType;
		 
			PackageQty = aPackageQty;
		 
			PackageBarcode = aPackageBarcode;
		 
			PackageBarcodeFid = aPackageBarcodeFid;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		}
		
		public PackageOutboundDetailInfo():base("TT_PCM_PACKAGE_OUTBOUND_DETAIL")
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
			 
			DataSchemaField ORDER_FIDField = new DataSchemaField();
			ORDER_FIDField.Name = "ORDER_FID";
			ORDER_FIDField.Type = typeof(Guid).ToString();
			ORDER_FIDField.Index = 2;
			fields.Add(ORDER_FIDField);
			 
			DataSchemaField ORDER_NOField = new DataSchemaField();
			ORDER_NOField.Name = "ORDER_NO";
			ORDER_NOField.Type = typeof(string).ToString();
			ORDER_NOField.Index = 3;
			fields.Add(ORDER_NOField);
			 
			DataSchemaField S_WM_NOField = new DataSchemaField();
			S_WM_NOField.Name = "S_WM_NO";
			S_WM_NOField.Type = typeof(string).ToString();
			S_WM_NOField.Index = 4;
			fields.Add(S_WM_NOField);
			 
			DataSchemaField S_ZONE_NOField = new DataSchemaField();
			S_ZONE_NOField.Name = "S_ZONE_NO";
			S_ZONE_NOField.Type = typeof(string).ToString();
			S_ZONE_NOField.Index = 5;
			fields.Add(S_ZONE_NOField);
			 
			DataSchemaField S_DLOCField = new DataSchemaField();
			S_DLOCField.Name = "S_DLOC";
			S_DLOCField.Type = typeof(string).ToString();
			S_DLOCField.Index = 6;
			fields.Add(S_DLOCField);
			 
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
			 
			DataSchemaField T_DLOCField = new DataSchemaField();
			T_DLOCField.Name = "T_DLOC";
			T_DLOCField.Type = typeof(string).ToString();
			T_DLOCField.Index = 9;
			fields.Add(T_DLOCField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 10;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField PACKAGE_MODELField = new DataSchemaField();
			PACKAGE_MODELField.Name = "PACKAGE_MODEL";
			PACKAGE_MODELField.Type = typeof(string).ToString();
			PACKAGE_MODELField.Index = 11;
			fields.Add(PACKAGE_MODELField);
			 
			DataSchemaField PACKAGE_CNAMEField = new DataSchemaField();
			PACKAGE_CNAMEField.Name = "PACKAGE_CNAME";
			PACKAGE_CNAMEField.Type = typeof(string).ToString();
			PACKAGE_CNAMEField.Index = 12;
			fields.Add(PACKAGE_CNAMEField);
			 
			DataSchemaField PACKAGE_TYPEField = new DataSchemaField();
			PACKAGE_TYPEField.Name = "PACKAGE_TYPE";
			PACKAGE_TYPEField.Type = typeof(int).ToString();
			PACKAGE_TYPEField.Index = 13;
			fields.Add(PACKAGE_TYPEField);
			 
			DataSchemaField PACKAGE_QTYField = new DataSchemaField();
			PACKAGE_QTYField.Name = "PACKAGE_QTY";
			PACKAGE_QTYField.Type = typeof(int).ToString();
			PACKAGE_QTYField.Index = 14;
			fields.Add(PACKAGE_QTYField);
			 
			DataSchemaField PACKAGE_BARCODEField = new DataSchemaField();
			PACKAGE_BARCODEField.Name = "PACKAGE_BARCODE";
			PACKAGE_BARCODEField.Type = typeof(string).ToString();
			PACKAGE_BARCODEField.Index = 15;
			fields.Add(PACKAGE_BARCODEField);
			 
			DataSchemaField PACKAGE_BARCODE_FIDField = new DataSchemaField();
			PACKAGE_BARCODE_FIDField.Name = "PACKAGE_BARCODE_FID";
			PACKAGE_BARCODE_FIDField.Type = typeof(Guid).ToString();
			PACKAGE_BARCODE_FIDField.Index = 16;
			fields.Add(PACKAGE_BARCODE_FIDField);
			 
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
		public Guid? OrderFid{ get;set; }		
				
		[DataMember]
		public string OrderNo{ get;set; }		
				
		[DataMember]
		public string SWmNo{ get;set; }		
				
		[DataMember]
		public string SZoneNo{ get;set; }		
				
		[DataMember]
		public string SDloc{ get;set; }		
				
		[DataMember]
		public string TWmNo{ get;set; }		
				
		[DataMember]
		public string TZoneNo{ get;set; }		
				
		[DataMember]
		public string TDloc{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string PackageModel{ get;set; }		
				
		[DataMember]
		public string PackageCname{ get;set; }		
				
		[DataMember]
		public int? PackageType{ get;set; }		
				
		[DataMember]
		public int? PackageQty{ get;set; }		
				
		[DataMember]
		public string PackageBarcode{ get;set; }		
				
		[DataMember]
		public Guid? PackageBarcodeFid{ get;set; }		
				
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
			PackageOutboundDetailInfo info = new PackageOutboundDetailInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.OrderFid = this.OrderFid;
			info.OrderNo = this.OrderNo;
			info.SWmNo = this.SWmNo;
			info.SZoneNo = this.SZoneNo;
			info.SDloc = this.SDloc;
			info.TWmNo = this.TWmNo;
			info.TZoneNo = this.TZoneNo;
			info.TDloc = this.TDloc;
			info.SupplierNum = this.SupplierNum;
			info.PackageModel = this.PackageModel;
			info.PackageCname = this.PackageCname;
			info.PackageType = this.PackageType;
			info.PackageQty = this.PackageQty;
			info.PackageBarcode = this.PackageBarcode;
			info.PackageBarcodeFid = this.PackageBarcodeFid;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			return info;			
		}
		 
		public PackageOutboundDetailInfo Clone()
		{
			return ((ICloneable) this).Clone() as PackageOutboundDetailInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PackageOutboundDetailInfoCollection对应表[TT_PCM_PACKAGE_OUTBOUND_DETAIL]
    /// </summary>
	public partial class PackageOutboundDetailInfoCollection : BusinessObjectCollection<PackageOutboundDetailInfo>
	{
		public PackageOutboundDetailInfoCollection():base("TT_PCM_PACKAGE_OUTBOUND_DETAIL"){}	
	}
}
