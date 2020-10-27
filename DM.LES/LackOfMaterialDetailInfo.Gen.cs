#region Declaim
//---------------------------------------------------------------------------
// Name:		LackOfMaterialDetailInfo
// Function: 	Expose data in table LackOfMaterialDetail from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月5日
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
    /// LackOfMaterialDetailInfo对应表[TT_ATP_LACK_OF_MATERIAL_DETAIL]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class LackOfMaterialDetailInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public LackOfMaterialDetailInfo( 
					long aId,

					Guid aFid,

					Guid aLackOrderFid,

					string aPartNo,

					string aSupplierNum,

					string aPlant,

					string aKeeper,

					string aPartPurchaser,

					decimal aLackQty,

					bool aFeedbackFlag,

					decimal aFeedbackLackQty,

					DateTime aFeedbackTime,

					bool aValidFlag,

					string aComments,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			LackOrderFid = aLackOrderFid;
		 
			PartNo = aPartNo;
		 
			SupplierNum = aSupplierNum;
		 
			Plant = aPlant;
		 
			Keeper = aKeeper;
		 
			PartPurchaser = aPartPurchaser;
		 
			LackQty = aLackQty;
		 
			FeedbackFlag = aFeedbackFlag;
		 
			FeedbackLackQty = aFeedbackLackQty;
		 
			FeedbackTime = aFeedbackTime;
		 
			ValidFlag = aValidFlag;
		 
			Comments = aComments;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public LackOfMaterialDetailInfo():base("TT_ATP_LACK_OF_MATERIAL_DETAIL")
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
			 
			DataSchemaField LACK_ORDER_FIDField = new DataSchemaField();
			LACK_ORDER_FIDField.Name = "LACK_ORDER_FID";
			LACK_ORDER_FIDField.Type = typeof(Guid).ToString();
			LACK_ORDER_FIDField.Index = 2;
			fields.Add(LACK_ORDER_FIDField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 3;
			fields.Add(PART_NOField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 4;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 5;
			fields.Add(PLANTField);
			 
			DataSchemaField KEEPERField = new DataSchemaField();
			KEEPERField.Name = "KEEPER";
			KEEPERField.Type = typeof(string).ToString();
			KEEPERField.Index = 6;
			fields.Add(KEEPERField);
			 
			DataSchemaField PART_PURCHASERField = new DataSchemaField();
			PART_PURCHASERField.Name = "PART_PURCHASER";
			PART_PURCHASERField.Type = typeof(string).ToString();
			PART_PURCHASERField.Index = 7;
			fields.Add(PART_PURCHASERField);
			 
			DataSchemaField LACK_QTYField = new DataSchemaField();
			LACK_QTYField.Name = "LACK_QTY";
			LACK_QTYField.Type = typeof(decimal).ToString();
			LACK_QTYField.Index = 8;
			fields.Add(LACK_QTYField);
			 
			DataSchemaField FEEDBACK_FLAGField = new DataSchemaField();
			FEEDBACK_FLAGField.Name = "FEEDBACK_FLAG";
			FEEDBACK_FLAGField.Type = typeof(bool).ToString();
			FEEDBACK_FLAGField.Index = 9;
			fields.Add(FEEDBACK_FLAGField);
			 
			DataSchemaField FEEDBACK_LACK_QTYField = new DataSchemaField();
			FEEDBACK_LACK_QTYField.Name = "FEEDBACK_LACK_QTY";
			FEEDBACK_LACK_QTYField.Type = typeof(decimal).ToString();
			FEEDBACK_LACK_QTYField.Index = 10;
			fields.Add(FEEDBACK_LACK_QTYField);
			 
			DataSchemaField FEEDBACK_TIMEField = new DataSchemaField();
			FEEDBACK_TIMEField.Name = "FEEDBACK_TIME";
			FEEDBACK_TIMEField.Type = typeof(DateTime).ToString();
			FEEDBACK_TIMEField.Index = 11;
			fields.Add(FEEDBACK_TIMEField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 12;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 13;
			fields.Add(COMMENTSField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 14;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 15;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 16;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 17;
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
		public Guid? LackOrderFid{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string Keeper{ get;set; }		
				
		[DataMember]
		public string PartPurchaser{ get;set; }		
				
		[DataMember]
		public decimal? LackQty{ get;set; }		
				
		[DataMember]
		public bool? FeedbackFlag{ get;set; }		
				
		[DataMember]
		public decimal? FeedbackLackQty{ get;set; }		
				
		[DataMember]
		public DateTime? FeedbackTime{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			LackOfMaterialDetailInfo info = new LackOfMaterialDetailInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LackOrderFid = this.LackOrderFid;
			info.PartNo = this.PartNo;
			info.SupplierNum = this.SupplierNum;
			info.Plant = this.Plant;
			info.Keeper = this.Keeper;
			info.PartPurchaser = this.PartPurchaser;
			info.LackQty = this.LackQty;
			info.FeedbackFlag = this.FeedbackFlag;
			info.FeedbackLackQty = this.FeedbackLackQty;
			info.FeedbackTime = this.FeedbackTime;
			info.ValidFlag = this.ValidFlag;
			info.Comments = this.Comments;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public LackOfMaterialDetailInfo Clone()
		{
			return ((ICloneable) this).Clone() as LackOfMaterialDetailInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// LackOfMaterialDetailInfoCollection对应表[TT_ATP_LACK_OF_MATERIAL_DETAIL]
    /// </summary>
	public partial class LackOfMaterialDetailInfoCollection : BusinessObjectCollection<LackOfMaterialDetailInfo>
	{
		public LackOfMaterialDetailInfoCollection():base("TT_ATP_LACK_OF_MATERIAL_DETAIL"){}	
	}
}