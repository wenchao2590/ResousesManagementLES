#region Declaim
//---------------------------------------------------------------------------
// Name:		MesVehiclePointScanInfo
// Function: 	Expose data in table MesVehiclePointScan from database as business object to MES system.
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
    /// MesVehiclePointScanInfo对应表[TI_IFM_MES_VEHICLE_POINT_SCAN]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class MesVehiclePointScanInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public MesVehiclePointScanInfo( 
					long aId,

					Guid aFid,

					string aEnterprise,

					string aSiteNo,

					string aAreaNo,

					string aUnitNo,

					int aDmsSeq,

					string aDmsNo,

					string aVin,

					string aPreviousDmsNo,

					DateTime aSendTime,

					string aFalg,

					int aProcessFlag,

					DateTime aProcessTime,

					Guid aLogFid,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate

				 
		) : this()
		{
			 
			Id = aId;
		 
			Fid = aFid;
		 
			Enterprise = aEnterprise;
		 
			SiteNo = aSiteNo;
		 
			AreaNo = aAreaNo;
		 
			UnitNo = aUnitNo;
		 
			DmsSeq = aDmsSeq;
		 
			DmsNo = aDmsNo;
		 
			Vin = aVin;
		 
			PreviousDmsNo = aPreviousDmsNo;
		 
			SendTime = aSendTime;
		 
			Falg = aFalg;
		 
			ProcessFlag = aProcessFlag;
		 
			ProcessTime = aProcessTime;
		 
			LogFid = aLogFid;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public MesVehiclePointScanInfo():base("TI_IFM_MES_VEHICLE_POINT_SCAN")
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
			 
			DataSchemaField ENTERPRISEField = new DataSchemaField();
			ENTERPRISEField.Name = "ENTERPRISE";
			ENTERPRISEField.Type = typeof(string).ToString();
			ENTERPRISEField.Index = 2;
			fields.Add(ENTERPRISEField);
			 
			DataSchemaField SITE_NOField = new DataSchemaField();
			SITE_NOField.Name = "SITE_NO";
			SITE_NOField.Type = typeof(string).ToString();
			SITE_NOField.Index = 3;
			fields.Add(SITE_NOField);
			 
			DataSchemaField AREA_NOField = new DataSchemaField();
			AREA_NOField.Name = "AREA_NO";
			AREA_NOField.Type = typeof(string).ToString();
			AREA_NOField.Index = 4;
			fields.Add(AREA_NOField);
			 
			DataSchemaField UNIT_NOField = new DataSchemaField();
			UNIT_NOField.Name = "UNIT_NO";
			UNIT_NOField.Type = typeof(string).ToString();
			UNIT_NOField.Index = 5;
			fields.Add(UNIT_NOField);
			 
			DataSchemaField DMS_SEQField = new DataSchemaField();
			DMS_SEQField.Name = "DMS_SEQ";
			DMS_SEQField.Type = typeof(int).ToString();
			DMS_SEQField.Index = 6;
			fields.Add(DMS_SEQField);
			 
			DataSchemaField DMS_NOField = new DataSchemaField();
			DMS_NOField.Name = "DMS_NO";
			DMS_NOField.Type = typeof(string).ToString();
			DMS_NOField.Index = 7;
			fields.Add(DMS_NOField);
			 
			DataSchemaField VINField = new DataSchemaField();
			VINField.Name = "VIN";
			VINField.Type = typeof(string).ToString();
			VINField.Index = 8;
			fields.Add(VINField);
			 
			DataSchemaField PREVIOUS_DMS_NOField = new DataSchemaField();
			PREVIOUS_DMS_NOField.Name = "PREVIOUS_DMS_NO";
			PREVIOUS_DMS_NOField.Type = typeof(string).ToString();
			PREVIOUS_DMS_NOField.Index = 9;
			fields.Add(PREVIOUS_DMS_NOField);
			 
			DataSchemaField SEND_TIMEField = new DataSchemaField();
			SEND_TIMEField.Name = "SEND_TIME";
			SEND_TIMEField.Type = typeof(DateTime).ToString();
			SEND_TIMEField.Index = 10;
			fields.Add(SEND_TIMEField);
			 
			DataSchemaField FALGField = new DataSchemaField();
			FALGField.Name = "FALG";
			FALGField.Type = typeof(string).ToString();
			FALGField.Index = 11;
			fields.Add(FALGField);
			 
			DataSchemaField PROCESS_FLAGField = new DataSchemaField();
			PROCESS_FLAGField.Name = "PROCESS_FLAG";
			PROCESS_FLAGField.Type = typeof(int).ToString();
			PROCESS_FLAGField.Index = 12;
			fields.Add(PROCESS_FLAGField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 13;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField LOG_FIDField = new DataSchemaField();
			LOG_FIDField.Name = "LOG_FID";
			LOG_FIDField.Type = typeof(Guid).ToString();
			LOG_FIDField.Index = 14;
			fields.Add(LOG_FIDField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 15;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 16;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 17;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 18;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 19;
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
		public string Enterprise{ get;set; }		
				
		[DataMember]
		public string SiteNo{ get;set; }		
				
		[DataMember]
		public string AreaNo{ get;set; }		
				
		[DataMember]
		public string UnitNo{ get;set; }		
				
		[DataMember]
		public int? DmsSeq{ get;set; }		
				
		[DataMember]
		public string DmsNo{ get;set; }		
				
		[DataMember]
		public string Vin{ get;set; }		
				
		[DataMember]
		public string PreviousDmsNo{ get;set; }		
				
		[DataMember]
		public DateTime? SendTime{ get;set; }		
				
		[DataMember]
		public string Falg{ get;set; }		
				
		[DataMember]
		public int? ProcessFlag{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public Guid? LogFid{ get;set; }		
				
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
			MesVehiclePointScanInfo info = new MesVehiclePointScanInfo();

			info.Id = this.Id;
			info.Fid = this.Fid;
			info.Enterprise = this.Enterprise;
			info.SiteNo = this.SiteNo;
			info.AreaNo = this.AreaNo;
			info.UnitNo = this.UnitNo;
			info.DmsSeq = this.DmsSeq;
			info.DmsNo = this.DmsNo;
			info.Vin = this.Vin;
			info.PreviousDmsNo = this.PreviousDmsNo;
			info.SendTime = this.SendTime;
			info.Falg = this.Falg;
			info.ProcessFlag = this.ProcessFlag;
			info.ProcessTime = this.ProcessTime;
			info.LogFid = this.LogFid;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public MesVehiclePointScanInfo Clone()
		{
			return ((ICloneable) this).Clone() as MesVehiclePointScanInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// MesVehiclePointScanInfoCollection对应表[TI_IFM_MES_VEHICLE_POINT_SCAN]
    /// </summary>
	public partial class MesVehiclePointScanInfoCollection : BusinessObjectCollection<MesVehiclePointScanInfo>
	{
		public MesVehiclePointScanInfoCollection():base("TI_IFM_MES_VEHICLE_POINT_SCAN"){}	
	}
}
