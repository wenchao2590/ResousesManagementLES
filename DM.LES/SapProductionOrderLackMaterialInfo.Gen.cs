#region Declaim
//---------------------------------------------------------------------------
// Name:		SapProductionOrderLackMaterialInfo
// Function: 	Expose data in table SapProductionOrderLackMaterial from database as business object to MES system.
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
    /// SapProductionOrderLackMaterialInfo对应表[TI_IFM_SAP_PRODUCTION_ORDER_LACK_MATERIAL]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SapProductionOrderLackMaterialInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SapProductionOrderLackMaterialInfo( 
					int aId,

					Guid aFid,

					Guid aLogFid,

					string aEnterprise,

					string aSiteNo,

					string aAreaNo,

					string aDmsNo,

					bool aMaterialCheck,

					DateTime aSendTime,

					DateTime aDatuv,

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
		 
			Enterprise = aEnterprise;
		 
			SiteNo = aSiteNo;
		 
			AreaNo = aAreaNo;
		 
			DmsNo = aDmsNo;
		 
			MaterialCheck = aMaterialCheck;
		 
			SendTime = aSendTime;
		 
			Datuv = aDatuv;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		 
			Comments = aComments;
		}
		
		public SapProductionOrderLackMaterialInfo():base("TI_IFM_SAP_PRODUCTION_ORDER_LACK_MATERIAL")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                 _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(int).ToString();
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
			 
			DataSchemaField ENTERPRISEField = new DataSchemaField();
			ENTERPRISEField.Name = "ENTERPRISE";
			ENTERPRISEField.Type = typeof(string).ToString();
			ENTERPRISEField.Index = 3;
			fields.Add(ENTERPRISEField);
			 
			DataSchemaField SITE_NOField = new DataSchemaField();
			SITE_NOField.Name = "SITE_NO";
			SITE_NOField.Type = typeof(string).ToString();
			SITE_NOField.Index = 4;
			fields.Add(SITE_NOField);
			 
			DataSchemaField AREA_NOField = new DataSchemaField();
			AREA_NOField.Name = "AREA_NO";
			AREA_NOField.Type = typeof(string).ToString();
			AREA_NOField.Index = 5;
			fields.Add(AREA_NOField);
			 
			DataSchemaField DMS_NOField = new DataSchemaField();
			DMS_NOField.Name = "DMS_NO";
			DMS_NOField.Type = typeof(string).ToString();
			DMS_NOField.Index = 6;
			fields.Add(DMS_NOField);
			 
			DataSchemaField MATERIAL_CHECKField = new DataSchemaField();
			MATERIAL_CHECKField.Name = "MATERIAL_CHECK";
			MATERIAL_CHECKField.Type = typeof(bool).ToString();
			MATERIAL_CHECKField.Index = 7;
			fields.Add(MATERIAL_CHECKField);
			 
			DataSchemaField SEND_TIMEField = new DataSchemaField();
			SEND_TIMEField.Name = "SEND_TIME";
			SEND_TIMEField.Type = typeof(DateTime).ToString();
			SEND_TIMEField.Index = 8;
			fields.Add(SEND_TIMEField);
			 
			DataSchemaField DATUVField = new DataSchemaField();
			DATUVField.Name = "DATUV";
			DATUVField.Type = typeof(DateTime).ToString();
			DATUVField.Index = 9;
			fields.Add(DATUVField);
			 
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
		public int Id{ get;set; }		
				
		[DataMember]
		public Guid? Fid{ get;set; }		
				
		[DataMember]
		public Guid? LogFid{ get;set; }		
				
		[DataMember]
		public string Enterprise{ get;set; }		
				
		[DataMember]
		public string SiteNo{ get;set; }		
				
		[DataMember]
		public string AreaNo{ get;set; }		
				
		[DataMember]
		public string DmsNo{ get;set; }		
				
		[DataMember]
		public bool? MaterialCheck{ get;set; }		
				
		[DataMember]
		public DateTime? SendTime{ get;set; }		
				
		[DataMember]
		public DateTime? Datuv{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
				
		private bool? _ValidFlag = true;
		
		[DataMember]	
		public bool? ValidFlag
		{
			get
			{
				return _ValidFlag;
			}
			set
			{
				_ValidFlag = value;
			}
		}
				
				
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
			SapProductionOrderLackMaterialInfo info = new SapProductionOrderLackMaterialInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.LogFid = this.LogFid;
			info.Enterprise = this.Enterprise;
			info.SiteNo = this.SiteNo;
			info.AreaNo = this.AreaNo;
			info.DmsNo = this.DmsNo;
			info.MaterialCheck = this.MaterialCheck;
			info.SendTime = this.SendTime;
			info.Datuv = this.Datuv;
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
		 
		public SapProductionOrderLackMaterialInfo Clone()
		{
			return ((ICloneable) this).Clone() as SapProductionOrderLackMaterialInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SapProductionOrderLackMaterialInfoCollection对应表[TI_IFM_SAP_PRODUCTION_ORDER_LACK_MATERIAL]
    /// </summary>
	public partial class SapProductionOrderLackMaterialInfoCollection : BusinessObjectCollection<SapProductionOrderLackMaterialInfo>
	{
		public SapProductionOrderLackMaterialInfoCollection():base("TI_IFM_SAP_PRODUCTION_ORDER_LACK_MATERIAL"){}	
	}
}
