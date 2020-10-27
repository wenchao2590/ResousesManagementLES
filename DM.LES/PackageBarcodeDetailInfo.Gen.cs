#region Declaim
//---------------------------------------------------------------------------
// Name:		PackageBarcodeDetailInfo
// Function: 	Expose data in table PackageBarcodeDetail from database as business object to MES system.
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
    /// PackageBarcodeDetailInfo对应表[TT_PCM_PACKAGE_BARCODE_DETAIL]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PackageBarcodeDetailInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PackageBarcodeDetailInfo( 
					long aId,

					Guid aFid,

					Guid aPackageBarcodeFid,

					string aPackageModel,

					int aPackageQty,

					string aPackageCname,

					int aPackageType,

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
		 
			PackageBarcodeFid = aPackageBarcodeFid;
		 
			PackageModel = aPackageModel;
		 
			PackageQty = aPackageQty;
		 
			PackageCname = aPackageCname;
		 
			PackageType = aPackageType;
		 
			Comments = aComments;
		 
			ValidFlag = aValidFlag;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		}
		
		public PackageBarcodeDetailInfo():base("TT_PCM_PACKAGE_BARCODE_DETAIL")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");            _Keys = keys.ToArray();
			
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
			 
			DataSchemaField PACKAGE_BARCODE_FIDField = new DataSchemaField();
			PACKAGE_BARCODE_FIDField.Name = "PACKAGE_BARCODE_FID";
			PACKAGE_BARCODE_FIDField.Type = typeof(Guid).ToString();
			PACKAGE_BARCODE_FIDField.Index = 2;
			fields.Add(PACKAGE_BARCODE_FIDField);
			 
			DataSchemaField PACKAGE_MODELField = new DataSchemaField();
			PACKAGE_MODELField.Name = "PACKAGE_MODEL";
			PACKAGE_MODELField.Type = typeof(string).ToString();
			PACKAGE_MODELField.Index = 3;
			fields.Add(PACKAGE_MODELField);
			 
			DataSchemaField PACKAGE_QTYField = new DataSchemaField();
			PACKAGE_QTYField.Name = "PACKAGE_QTY";
			PACKAGE_QTYField.Type = typeof(int).ToString();
			PACKAGE_QTYField.Index = 4;
			fields.Add(PACKAGE_QTYField);
			 
			DataSchemaField PACKAGE_CNAMEField = new DataSchemaField();
			PACKAGE_CNAMEField.Name = "PACKAGE_CNAME";
			PACKAGE_CNAMEField.Type = typeof(string).ToString();
			PACKAGE_CNAMEField.Index = 5;
			fields.Add(PACKAGE_CNAMEField);
			 
			DataSchemaField PACKAGE_TYPEField = new DataSchemaField();
			PACKAGE_TYPEField.Name = "PACKAGE_TYPE";
			PACKAGE_TYPEField.Type = typeof(int).ToString();
			PACKAGE_TYPEField.Index = 6;
			fields.Add(PACKAGE_TYPEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 7;
			fields.Add(COMMENTSField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 8;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 9;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 10;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 11;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 12;
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
		public Guid? PackageBarcodeFid{ get;set; }		
				
		[DataMember]
		public string PackageModel{ get;set; }		
				
		[DataMember]
		public int? PackageQty{ get;set; }		
				
		[DataMember]
		public string PackageCname{ get;set; }		
				
		[DataMember]
		public int? PackageType{ get;set; }		
				
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
			PackageBarcodeDetailInfo info = new PackageBarcodeDetailInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.PackageBarcodeFid = this.PackageBarcodeFid;
			info.PackageModel = this.PackageModel;
			info.PackageQty = this.PackageQty;
			info.PackageCname = this.PackageCname;
			info.PackageType = this.PackageType;
			info.Comments = this.Comments;
			info.ValidFlag = this.ValidFlag;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			return info;			
		}
		 
		public PackageBarcodeDetailInfo Clone()
		{
			return ((ICloneable) this).Clone() as PackageBarcodeDetailInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PackageBarcodeDetailInfoCollection对应表[TT_PCM_PACKAGE_BARCODE_DETAIL]
    /// </summary>
	public partial class PackageBarcodeDetailInfoCollection : BusinessObjectCollection<PackageBarcodeDetailInfo>
	{
		public PackageBarcodeDetailInfoCollection():base("TT_PCM_PACKAGE_BARCODE_DETAIL"){}	
	}
}
