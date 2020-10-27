#region Declaim
//---------------------------------------------------------------------------
// Name:		PullOrderBomInfo
// Function: 	Expose data in table PullOrderBom from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年4月17日
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
    /// PullOrderBomInfo对应表[TT_BAS_PULL_ORDER_BOM]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PullOrderBomInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PullOrderBomInfo( 
					long aId,

					Guid aFid,

					Guid aOrderfid,

					string aZordno,

					string aZkwerk,

					string aZbomid,

					string aZcomno,

					string aZcomds,

					string aZvin,

					int aZqty,

					DateTime aZdate,

					string aZloc,

					int aZst,

					string aZmemo,

					string aZmeins,

					string aSupplierNum,

					string aPlatform,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					bool aValidFlag

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			Orderfid = aOrderfid;
		 
			Zordno = aZordno;
		 
			Zkwerk = aZkwerk;
		 
			Zbomid = aZbomid;
		 
			Zcomno = aZcomno;
		 
			Zcomds = aZcomds;
		 
			Zvin = aZvin;
		 
			Zqty = aZqty;
		 
			Zdate = aZdate;
		 
			Zloc = aZloc;
		 
			Zst = aZst;
		 
			Zmemo = aZmemo;
		 
			Zmeins = aZmeins;
		 
			SupplierNum = aSupplierNum;
		 
			Platform = aPlatform;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			ValidFlag = aValidFlag;
		}
		
		public PullOrderBomInfo():base("TT_BAS_PULL_ORDER_BOM")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                     _Keys = keys.ToArray();
			
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
			 
			DataSchemaField ORDERFIDField = new DataSchemaField();
			ORDERFIDField.Name = "ORDERFID";
			ORDERFIDField.Type = typeof(Guid).ToString();
			ORDERFIDField.Index = 2;
			fields.Add(ORDERFIDField);
			 
			DataSchemaField ZORDNOField = new DataSchemaField();
			ZORDNOField.Name = "ZORDNO";
			ZORDNOField.Type = typeof(string).ToString();
			ZORDNOField.Index = 3;
			fields.Add(ZORDNOField);
			 
			DataSchemaField ZKWERKField = new DataSchemaField();
			ZKWERKField.Name = "ZKWERK";
			ZKWERKField.Type = typeof(string).ToString();
			ZKWERKField.Index = 4;
			fields.Add(ZKWERKField);
			 
			DataSchemaField ZBOMIDField = new DataSchemaField();
			ZBOMIDField.Name = "ZBOMID";
			ZBOMIDField.Type = typeof(string).ToString();
			ZBOMIDField.Index = 5;
			fields.Add(ZBOMIDField);
			 
			DataSchemaField ZCOMNOField = new DataSchemaField();
			ZCOMNOField.Name = "ZCOMNO";
			ZCOMNOField.Type = typeof(string).ToString();
			ZCOMNOField.Index = 6;
			fields.Add(ZCOMNOField);
			 
			DataSchemaField ZCOMDSField = new DataSchemaField();
			ZCOMDSField.Name = "ZCOMDS";
			ZCOMDSField.Type = typeof(string).ToString();
			ZCOMDSField.Index = 7;
			fields.Add(ZCOMDSField);
			 
			DataSchemaField ZVINField = new DataSchemaField();
			ZVINField.Name = "ZVIN";
			ZVINField.Type = typeof(string).ToString();
			ZVINField.Index = 8;
			fields.Add(ZVINField);
			 
			DataSchemaField ZQTYField = new DataSchemaField();
			ZQTYField.Name = "ZQTY";
			ZQTYField.Type = typeof(int).ToString();
			ZQTYField.Index = 9;
			fields.Add(ZQTYField);
			 
			DataSchemaField ZDATEField = new DataSchemaField();
			ZDATEField.Name = "ZDATE";
			ZDATEField.Type = typeof(DateTime).ToString();
			ZDATEField.Index = 10;
			fields.Add(ZDATEField);
			 
			DataSchemaField ZLOCField = new DataSchemaField();
			ZLOCField.Name = "ZLOC";
			ZLOCField.Type = typeof(string).ToString();
			ZLOCField.Index = 11;
			fields.Add(ZLOCField);
			 
			DataSchemaField ZSTField = new DataSchemaField();
			ZSTField.Name = "ZST";
			ZSTField.Type = typeof(int).ToString();
			ZSTField.Index = 12;
			fields.Add(ZSTField);
			 
			DataSchemaField ZMEMOField = new DataSchemaField();
			ZMEMOField.Name = "ZMEMO";
			ZMEMOField.Type = typeof(string).ToString();
			ZMEMOField.Index = 13;
			fields.Add(ZMEMOField);
			 
			DataSchemaField ZMEINSField = new DataSchemaField();
			ZMEINSField.Name = "ZMEINS";
			ZMEINSField.Type = typeof(string).ToString();
			ZMEINSField.Index = 14;
			fields.Add(ZMEINSField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 15;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField PLATFORMField = new DataSchemaField();
			PLATFORMField.Name = "PLATFORM";
			PLATFORMField.Type = typeof(string).ToString();
			PLATFORMField.Index = 16;
			fields.Add(PLATFORMField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 17;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 18;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 19;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 20;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 21;
			fields.Add(VALID_FLAGField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public Guid? Orderfid{ get;set; }		
				
		[DataMember]
		public string Zordno{ get;set; }		
				
		[DataMember]
		public string Zkwerk{ get;set; }		
				
		[DataMember]
		public string Zbomid{ get;set; }		
				
		[DataMember]
		public string Zcomno{ get;set; }		
				
		[DataMember]
		public string Zcomds{ get;set; }		
				
		[DataMember]
		public string Zvin{ get;set; }		
				
		[DataMember]
		public int? Zqty{ get;set; }		
				
		[DataMember]
		public DateTime? Zdate{ get;set; }		
				
		[DataMember]
		public string Zloc{ get;set; }		
				
		[DataMember]
		public int? Zst{ get;set; }		
				
		[DataMember]
		public string Zmemo{ get;set; }		
				
		[DataMember]
		public string Zmeins{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string Platform{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PullOrderBomInfo info = new PullOrderBomInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.Orderfid = this.Orderfid;
			info.Zordno = this.Zordno;
			info.Zkwerk = this.Zkwerk;
			info.Zbomid = this.Zbomid;
			info.Zcomno = this.Zcomno;
			info.Zcomds = this.Zcomds;
			info.Zvin = this.Zvin;
			info.Zqty = this.Zqty;
			info.Zdate = this.Zdate;
			info.Zloc = this.Zloc;
			info.Zst = this.Zst;
			info.Zmemo = this.Zmemo;
			info.Zmeins = this.Zmeins;
			info.SupplierNum = this.SupplierNum;
			info.Platform = this.Platform;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.ValidFlag = this.ValidFlag;
			return info;			
		}
		 
		public PullOrderBomInfo Clone()
		{
			return ((ICloneable) this).Clone() as PullOrderBomInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PullOrderBomInfoCollection对应表[TT_BAS_PULL_ORDER_BOM]
    /// </summary>
	public partial class PullOrderBomInfoCollection : BusinessObjectCollection<PullOrderBomInfo>
	{
		public PullOrderBomInfoCollection():base("TT_BAS_PULL_ORDER_BOM"){}	
	}
}
