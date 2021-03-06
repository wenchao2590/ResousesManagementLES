#region Declaim
//---------------------------------------------------------------------------
// Name:		WmsVmiPackageOutboundInfo
// Function: 	Expose data in table WmsVmiPackageOutbound from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月24日
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
    /// WmsVmiPackageOutboundInfo对应表[TI_IFM_WMS_VMI_PACKAGE_OUTBOUND]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class WmsVmiPackageOutboundInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public WmsVmiPackageOutboundInfo( 
					long aId,

					Guid aFid,

					Guid aLogFid,

					string aExternreceiptkey,

					string aExternlineno,

					string aStorerkey,

					string aSku,

					int aQtyexpected,

					string aVmiwarehousecode,

					string aWerks,

					int aProcessFlag,

					DateTime aProcessTime,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate,

					string aComments

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			LogFid = aLogFid;
		 
			Externreceiptkey = aExternreceiptkey;
		 
			Externlineno = aExternlineno;
		 
			Storerkey = aStorerkey;
		 
			Sku = aSku;
		 
			Qtyexpected = aQtyexpected;
		 
			Vmiwarehousecode = aVmiwarehousecode;
		 
			Werks = aWerks;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			Comments = aComments;
		}
		
		public WmsVmiPackageOutboundInfo():base("TI_IFM_WMS_VMI_PACKAGE_OUTBOUND")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                 _Keys = keys.ToArray();
			
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
			 
			DataSchemaField LOG_FIDField = new DataSchemaField();
			LOG_FIDField.Name = "LOG_FID";
			LOG_FIDField.Type = typeof(Guid).ToString();
			LOG_FIDField.Index = 2;
			fields.Add(LOG_FIDField);
			 
			DataSchemaField EXTERNRECEIPTKEYField = new DataSchemaField();
			EXTERNRECEIPTKEYField.Name = "EXTERNRECEIPTKEY";
			EXTERNRECEIPTKEYField.Type = typeof(string).ToString();
			EXTERNRECEIPTKEYField.Index = 3;
			fields.Add(EXTERNRECEIPTKEYField);
			 
			DataSchemaField EXTERNLINENOField = new DataSchemaField();
			EXTERNLINENOField.Name = "EXTERNLINENO";
			EXTERNLINENOField.Type = typeof(string).ToString();
			EXTERNLINENOField.Index = 4;
			fields.Add(EXTERNLINENOField);
			 
			DataSchemaField STORERKEYField = new DataSchemaField();
			STORERKEYField.Name = "STORERKEY";
			STORERKEYField.Type = typeof(string).ToString();
			STORERKEYField.Index = 5;
			fields.Add(STORERKEYField);
			 
			DataSchemaField SKUField = new DataSchemaField();
			SKUField.Name = "SKU";
			SKUField.Type = typeof(string).ToString();
			SKUField.Index = 6;
			fields.Add(SKUField);
			 
			DataSchemaField QTYEXPECTEDField = new DataSchemaField();
			QTYEXPECTEDField.Name = "QTYEXPECTED";
			QTYEXPECTEDField.Type = typeof(int).ToString();
			QTYEXPECTEDField.Index = 7;
			fields.Add(QTYEXPECTEDField);
			 
			DataSchemaField VMIWAREHOUSECODEField = new DataSchemaField();
			VMIWAREHOUSECODEField.Name = "VMIWAREHOUSECODE";
			VMIWAREHOUSECODEField.Type = typeof(string).ToString();
			VMIWAREHOUSECODEField.Index = 8;
			fields.Add(VMIWAREHOUSECODEField);
			 
			DataSchemaField WERKSField = new DataSchemaField();
			WERKSField.Name = "WERKS";
			WERKSField.Type = typeof(string).ToString();
			WERKSField.Index = 9;
			fields.Add(WERKSField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 10;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 11;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 12;
			fields.Add(VALID_FLAGField);
			 
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
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 15;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 16;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 17;
			fields.Add(COMMENTSField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public Guid? LogFid{ get;set; }		
				
		[DataMember]
		public string Externreceiptkey{ get;set; }		
				
		[DataMember]
		public string Externlineno{ get;set; }		
				
		[DataMember]
		public string Storerkey{ get;set; }		
				
		[DataMember]
		public string Sku{ get;set; }		
				
		[DataMember]
		public int? Qtyexpected{ get;set; }		
				
		[DataMember]
		public string Vmiwarehousecode{ get;set; }		
				
		[DataMember]
		public string Werks{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
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
				
		[DataMember]
		public string Comments{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			WmsVmiPackageOutboundInfo info = new WmsVmiPackageOutboundInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LogFid = this.LogFid;
			info.Externreceiptkey = this.Externreceiptkey;
			info.Externlineno = this.Externlineno;
			info.Storerkey = this.Storerkey;
			info.Sku = this.Sku;
			info.Qtyexpected = this.Qtyexpected;
			info.Vmiwarehousecode = this.Vmiwarehousecode;
			info.Werks = this.Werks;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			info.Comments = this.Comments;
			return info;			
		}
		 
		public WmsVmiPackageOutboundInfo Clone()
		{
			return ((ICloneable) this).Clone() as WmsVmiPackageOutboundInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// WmsVmiPackageOutboundInfoCollection对应表[TI_IFM_WMS_VMI_PACKAGE_OUTBOUND]
    /// </summary>
	public partial class WmsVmiPackageOutboundInfoCollection : BusinessObjectCollection<WmsVmiPackageOutboundInfo>
	{
		public WmsVmiPackageOutboundInfoCollection():base("TI_IFM_WMS_VMI_PACKAGE_OUTBOUND"){}	
	}
}
