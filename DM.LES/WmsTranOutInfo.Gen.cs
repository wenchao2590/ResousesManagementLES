#region Declaim
//---------------------------------------------------------------------------
// Name:		WmsTranOutInfo
// Function: 	Expose data in table WmsTranOut from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018-07-20
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
    /// WmsTranOutInfo对应表[TI_IFM_WMS_TRAN_OUT]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class WmsTranOutInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public WmsTranOutInfo( 
					long aId,

					Guid aFid,

					string aSourceOrderCode,

					int aSourceOrderType,

					string aPartNo,

					string aSupplierNum,

					string aSupplierName,

					decimal aDeliveryQty,

					int aProcessFlag,

					DateTime aProcessTime,

					Guid aLogFid,

					bool aValidFlag,

					DateTime aCreateDate,

					string aCreateUser,

					DateTime aModifyDate,

					string aModifyUser,

					string aRunsheetNo,

					string aWmNo,

					string aPlant,

					string aItemNumber

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			SourceOrderCode = aSourceOrderCode;
		 
			SourceOrderType = aSourceOrderType;
		 
			PartNo = aPartNo;
		 
			SupplierNum = aSupplierNum;
		 
			SupplierName = aSupplierName;
		 
			DeliveryQty = aDeliveryQty;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			LogFid = aLogFid;
		 
			ValidFlag = aValidFlag;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		 
			RunsheetNo = aRunsheetNo;
		 
			WmNo = aWmNo;
		 
			Plant = aPlant;
		 
			ItemNumber = aItemNumber;
		}
		
		public WmsTranOutInfo():base("TI_IFM_WMS_TRAN_OUT")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                   _Keys = keys.ToArray();
			
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
			 
			DataSchemaField SOURCE_ORDER_CODEField = new DataSchemaField();
			SOURCE_ORDER_CODEField.Name = "SOURCE_ORDER_CODE";
			SOURCE_ORDER_CODEField.Type = typeof(string).ToString();
			SOURCE_ORDER_CODEField.Index = 2;
			fields.Add(SOURCE_ORDER_CODEField);
			 
			DataSchemaField SOURCE_ORDER_TYPEField = new DataSchemaField();
			SOURCE_ORDER_TYPEField.Name = "SOURCE_ORDER_TYPE";
			SOURCE_ORDER_TYPEField.Type = typeof(int).ToString();
			SOURCE_ORDER_TYPEField.Index = 3;
			fields.Add(SOURCE_ORDER_TYPEField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 4;
			fields.Add(PART_NOField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 5;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField SUPPLIER_NAMEField = new DataSchemaField();
			SUPPLIER_NAMEField.Name = "SUPPLIER_NAME";
			SUPPLIER_NAMEField.Type = typeof(string).ToString();
			SUPPLIER_NAMEField.Index = 6;
			fields.Add(SUPPLIER_NAMEField);
			 
			DataSchemaField DELIVERY_QTYField = new DataSchemaField();
			DELIVERY_QTYField.Name = "DELIVERY_QTY";
			DELIVERY_QTYField.Type = typeof(decimal).ToString();
			DELIVERY_QTYField.Index = 7;
			fields.Add(DELIVERY_QTYField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 8;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 9;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField LOG_FIDField = new DataSchemaField();
			LOG_FIDField.Name = "LOG_FID";
			LOG_FIDField.Type = typeof(Guid).ToString();
			LOG_FIDField.Index = 10;
			fields.Add(LOG_FIDField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 11;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 12;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 13;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 14;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 15;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField RUNSHEET_NOField = new DataSchemaField();
			RUNSHEET_NOField.Name = "RUNSHEET_NO";
			RUNSHEET_NOField.Type = typeof(string).ToString();
			RUNSHEET_NOField.Index = 16;
			fields.Add(RUNSHEET_NOField);
			 
			DataSchemaField WM_NOField = new DataSchemaField();
			WM_NOField.Name = "WM_NO";
			WM_NOField.Type = typeof(string).ToString();
			WM_NOField.Index = 17;
			fields.Add(WM_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 18;
			fields.Add(PLANTField);
			 
			DataSchemaField ITEM_NUMBERField = new DataSchemaField();
			ITEM_NUMBERField.Name = "ITEM_NUMBER";
			ITEM_NUMBERField.Type = typeof(string).ToString();
			ITEM_NUMBERField.Index = 19;
			fields.Add(ITEM_NUMBERField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public string SourceOrderCode{ get;set; }		
				
		[DataMember]
		public int? SourceOrderType{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string SupplierName{ get;set; }		
				
		[DataMember]
		public decimal? DeliveryQty{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public Guid? LogFid{ get;set; }		
				
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
				
		[DataMember]
		public string RunsheetNo{ get;set; }		
				
		[DataMember]
		public string WmNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string ItemNumber{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			WmsTranOutInfo info = new WmsTranOutInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.SourceOrderCode = this.SourceOrderCode;
			info.SourceOrderType = this.SourceOrderType;
			info.PartNo = this.PartNo;
			info.SupplierNum = this.SupplierNum;
			info.SupplierName = this.SupplierName;
			info.DeliveryQty = this.DeliveryQty;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.LogFid = this.LogFid;
			info.ValidFlag = this.ValidFlag;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			info.RunsheetNo = this.RunsheetNo;
			info.WmNo = this.WmNo;
			info.Plant = this.Plant;
			info.ItemNumber = this.ItemNumber;
			return info;			
		}
		 
		public WmsTranOutInfo Clone()
		{
			return ((ICloneable) this).Clone() as WmsTranOutInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// WmsTranOutInfoCollection对应表[TI_IFM_WMS_TRAN_OUT]
    /// </summary>
	public partial class WmsTranOutInfoCollection : BusinessObjectCollection<WmsTranOutInfo>
	{
		public WmsTranOutInfoCollection():base("TI_IFM_WMS_TRAN_OUT"){}	
	}
}
