#region Declaim
//---------------------------------------------------------------------------
// Name:		MaterialRequestsInfo
// Function: 	Expose data in table MaterialRequests from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年12月21日
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
    /// MaterialRequestsInfo对应表[TI_PCS_MATERIAL_REQUESTS]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class MaterialRequestsInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public MaterialRequestsInfo( 
					int aInterfaceId,

					string aPlantZone,

					string aWorkshop,

					string aAssemblyLine,

					string aPlant,

					string aLocation,

					DateTime aRequestTime,

					int aInterfaceStatus,

					DateTime aProcessTime,

					string aPartNo,

					string aPartCname,

					string aPartEname,

					string aSupplierNum,

					string aDock,

					string aBoxParts,

					int aInterfaceType,

					int aPackCount,

					int aRequriedPack,

					string aInhousePackageModel,

					int aInhousePackage,

					string aMeasuringUnitNo,

					DateTime aExpectedArrivalTime,

					string aRdcDloc,

					int aPickupSeqNo,

					int aSequenceNo,

					int aIsOrganizeSheet,

					int aSendStatus,

					DateTime aSendTime,

					bool aIsCancel,

					int aWmsSendStatus,

					DateTime aWmsSendTime,

					string aComments,

					DateTime aUpdateDate,

					string aUpdateUser,

					DateTime aCreateDate,

					string aCreateUser

				 
		) : this()
		{
			 
			InterfaceId = aInterfaceId;
		 
			PlantZone = aPlantZone;
		 
			Workshop = aWorkshop;
		 
			AssemblyLine = aAssemblyLine;
		 
			Plant = aPlant;
		 
			Location = aLocation;
		 
			RequestTime = aRequestTime;
		 
			InterfaceStatus = aInterfaceStatus;
		 
			ProcessTime = aProcessTime;
		 
			PartNo = aPartNo;
		 
			PartCname = aPartCname;
		 
			PartEname = aPartEname;
		 
			SupplierNum = aSupplierNum;
		 
			Dock = aDock;
		 
			BoxParts = aBoxParts;
		 
			InterfaceType = aInterfaceType;
		 
			PackCount = aPackCount;
		 
			RequriedPack = aRequriedPack;
		 
			InhousePackageModel = aInhousePackageModel;
		 
			InhousePackage = aInhousePackage;
		 
			MeasuringUnitNo = aMeasuringUnitNo;
		 
			ExpectedArrivalTime = aExpectedArrivalTime;
		 
			RdcDloc = aRdcDloc;
		 
			PickupSeqNo = aPickupSeqNo;
		 
			SequenceNo = aSequenceNo;
		 
			IsOrganizeSheet = aIsOrganizeSheet;
		 
			SendStatus = aSendStatus;
		 
			SendTime = aSendTime;
		 
			IsCancel = aIsCancel;
		 
			WmsSendStatus = aWmsSendStatus;
		 
			WmsSendTime = aWmsSendTime;
		 
			Comments = aComments;
		 
			UpdateDate = aUpdateDate;
		 
			UpdateUser = aUpdateUser;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		}
		
		public MaterialRequestsInfo():base("TI_PCS_MATERIAL_REQUESTS")
		{
			List<string> keys = new List<string>();
			 			keys.Add("INTERFACE_ID");                                   _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField INTERFACE_IDField = new DataSchemaField();
			INTERFACE_IDField.Name = "INTERFACE_ID";
			INTERFACE_IDField.Type = typeof(int).ToString();
			INTERFACE_IDField.Index = 0;
			fields.Add(INTERFACE_IDField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 1;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 2;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 3;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 4;
			fields.Add(PLANTField);
			 
			DataSchemaField LOCATIONField = new DataSchemaField();
			LOCATIONField.Name = "LOCATION";
			LOCATIONField.Type = typeof(string).ToString();
			LOCATIONField.Index = 5;
			fields.Add(LOCATIONField);
			 
			DataSchemaField REQUEST_TIMEField = new DataSchemaField();
			REQUEST_TIMEField.Name = "REQUEST_TIME";
			REQUEST_TIMEField.Type = typeof(DateTime).ToString();
			REQUEST_TIMEField.Index = 6;
			fields.Add(REQUEST_TIMEField);
			 
			DataSchemaField INTERFACE_STATUSField = new DataSchemaField();
			INTERFACE_STATUSField.Name = "INTERFACE_STATUS";
			INTERFACE_STATUSField.Type = typeof(int).ToString();
			INTERFACE_STATUSField.Index = 7;
			fields.Add(INTERFACE_STATUSField);
			 
			DataSchemaField PROCESS_TIMEField = new DataSchemaField();
			PROCESS_TIMEField.Name = "PROCESS_TIME";
			PROCESS_TIMEField.Type = typeof(DateTime).ToString();
			PROCESS_TIMEField.Index = 8;
			fields.Add(PROCESS_TIMEField);
			 
			DataSchemaField PART_NOField = new DataSchemaField();
			PART_NOField.Name = "PART_NO";
			PART_NOField.Type = typeof(string).ToString();
			PART_NOField.Index = 9;
			fields.Add(PART_NOField);
			 
			DataSchemaField PART_CNAMEField = new DataSchemaField();
			PART_CNAMEField.Name = "PART_CNAME";
			PART_CNAMEField.Type = typeof(string).ToString();
			PART_CNAMEField.Index = 10;
			fields.Add(PART_CNAMEField);
			 
			DataSchemaField PART_ENAMEField = new DataSchemaField();
			PART_ENAMEField.Name = "PART_ENAME";
			PART_ENAMEField.Type = typeof(string).ToString();
			PART_ENAMEField.Index = 11;
			fields.Add(PART_ENAMEField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 12;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 13;
			fields.Add(DOCKField);
			 
			DataSchemaField BOX_PARTSField = new DataSchemaField();
			BOX_PARTSField.Name = "BOX_PARTS";
			BOX_PARTSField.Type = typeof(string).ToString();
			BOX_PARTSField.Index = 14;
			fields.Add(BOX_PARTSField);
			 
			DataSchemaField INTERFACE_TYPEField = new DataSchemaField();
			INTERFACE_TYPEField.Name = "INTERFACE_TYPE";
			INTERFACE_TYPEField.Type = typeof(int).ToString();
			INTERFACE_TYPEField.Index = 15;
			fields.Add(INTERFACE_TYPEField);
			 
			DataSchemaField PACK_COUNTField = new DataSchemaField();
			PACK_COUNTField.Name = "PACK_COUNT";
			PACK_COUNTField.Type = typeof(int).ToString();
			PACK_COUNTField.Index = 16;
			fields.Add(PACK_COUNTField);
			 
			DataSchemaField REQURIED_PACKField = new DataSchemaField();
			REQURIED_PACKField.Name = "REQURIED_PACK";
			REQURIED_PACKField.Type = typeof(int).ToString();
			REQURIED_PACKField.Index = 17;
			fields.Add(REQURIED_PACKField);
			 
			DataSchemaField INHOUSE_PACKAGE_MODELField = new DataSchemaField();
			INHOUSE_PACKAGE_MODELField.Name = "INHOUSE_PACKAGE_MODEL";
			INHOUSE_PACKAGE_MODELField.Type = typeof(string).ToString();
			INHOUSE_PACKAGE_MODELField.Index = 18;
			fields.Add(INHOUSE_PACKAGE_MODELField);
			 
			DataSchemaField INHOUSE_PACKAGEField = new DataSchemaField();
			INHOUSE_PACKAGEField.Name = "INHOUSE_PACKAGE";
			INHOUSE_PACKAGEField.Type = typeof(int).ToString();
			INHOUSE_PACKAGEField.Index = 19;
			fields.Add(INHOUSE_PACKAGEField);
			 
			DataSchemaField MEASURING_UNIT_NOField = new DataSchemaField();
			MEASURING_UNIT_NOField.Name = "MEASURING_UNIT_NO";
			MEASURING_UNIT_NOField.Type = typeof(string).ToString();
			MEASURING_UNIT_NOField.Index = 20;
			fields.Add(MEASURING_UNIT_NOField);
			 
			DataSchemaField EXPECTED_ARRIVAL_TIMEField = new DataSchemaField();
			EXPECTED_ARRIVAL_TIMEField.Name = "EXPECTED_ARRIVAL_TIME";
			EXPECTED_ARRIVAL_TIMEField.Type = typeof(DateTime).ToString();
			EXPECTED_ARRIVAL_TIMEField.Index = 21;
			fields.Add(EXPECTED_ARRIVAL_TIMEField);
			 
			DataSchemaField RDC_DLOCField = new DataSchemaField();
			RDC_DLOCField.Name = "RDC_DLOC";
			RDC_DLOCField.Type = typeof(string).ToString();
			RDC_DLOCField.Index = 22;
			fields.Add(RDC_DLOCField);
			 
			DataSchemaField PICKUP_SEQ_NOField = new DataSchemaField();
			PICKUP_SEQ_NOField.Name = "PICKUP_SEQ_NO";
			PICKUP_SEQ_NOField.Type = typeof(int).ToString();
			PICKUP_SEQ_NOField.Index = 23;
			fields.Add(PICKUP_SEQ_NOField);
			 
			DataSchemaField SEQUENCE_NOField = new DataSchemaField();
			SEQUENCE_NOField.Name = "SEQUENCE_NO";
			SEQUENCE_NOField.Type = typeof(int).ToString();
			SEQUENCE_NOField.Index = 24;
			fields.Add(SEQUENCE_NOField);
			 
			DataSchemaField IS_ORGANIZE_SHEETField = new DataSchemaField();
			IS_ORGANIZE_SHEETField.Name = "IS_ORGANIZE_SHEET";
			IS_ORGANIZE_SHEETField.Type = typeof(int).ToString();
			IS_ORGANIZE_SHEETField.Index = 25;
			fields.Add(IS_ORGANIZE_SHEETField);
			 
			DataSchemaField SEND_STATUSField = new DataSchemaField();
			SEND_STATUSField.Name = "SEND_STATUS";
			SEND_STATUSField.Type = typeof(int).ToString();
			SEND_STATUSField.Index = 26;
			fields.Add(SEND_STATUSField);
			 
			DataSchemaField SEND_TIMEField = new DataSchemaField();
			SEND_TIMEField.Name = "SEND_TIME";
			SEND_TIMEField.Type = typeof(DateTime).ToString();
			SEND_TIMEField.Index = 27;
			fields.Add(SEND_TIMEField);
			 
			DataSchemaField IS_CANCELField = new DataSchemaField();
			IS_CANCELField.Name = "IS_CANCEL";
			IS_CANCELField.Type = typeof(bool).ToString();
			IS_CANCELField.Index = 28;
			fields.Add(IS_CANCELField);
			 
			DataSchemaField WMS_SEND_STATUSField = new DataSchemaField();
			WMS_SEND_STATUSField.Name = "WMS_SEND_STATUS";
			WMS_SEND_STATUSField.Type = typeof(int).ToString();
			WMS_SEND_STATUSField.Index = 29;
			fields.Add(WMS_SEND_STATUSField);
			 
			DataSchemaField WMS_SEND_TIMEField = new DataSchemaField();
			WMS_SEND_TIMEField.Name = "WMS_SEND_TIME";
			WMS_SEND_TIMEField.Type = typeof(DateTime).ToString();
			WMS_SEND_TIMEField.Index = 30;
			fields.Add(WMS_SEND_TIMEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 31;
			fields.Add(COMMENTSField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 32;
			fields.Add(UPDATE_DATEField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 33;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 34;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 35;
			fields.Add(CREATE_USERField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public int InterfaceId{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string Location{ get;set; }		
				
		[DataMember]
		public DateTime? RequestTime{ get;set; }		
				
		[DataMember]
		public int InterfaceStatus{ get;set; }		
				
		[DataMember]
		public DateTime? ProcessTime{ get;set; }		
				
		[DataMember]
		public string PartNo{ get;set; }		
				
		[DataMember]
		public string PartCname{ get;set; }		
				
		[DataMember]
		public string PartEname{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string Dock{ get;set; }		
				
		[DataMember]
		public string BoxParts{ get;set; }		
				
		[DataMember]
		public int InterfaceType{ get;set; }		
				
		[DataMember]
		public int PackCount{ get;set; }		
				
		[DataMember]
		public int? RequriedPack{ get;set; }		
				
		[DataMember]
		public string InhousePackageModel{ get;set; }		
				
		[DataMember]
		public int InhousePackage{ get;set; }		
				
		[DataMember]
		public string MeasuringUnitNo{ get;set; }		
				
		[DataMember]
		public DateTime? ExpectedArrivalTime{ get;set; }		
				
		[DataMember]
		public string RdcDloc{ get;set; }		
				
		[DataMember]
		public int? PickupSeqNo{ get;set; }		
				
		[DataMember]
		public int? SequenceNo{ get;set; }		
				
		[DataMember]
		public int? IsOrganizeSheet{ get;set; }		
				
		[DataMember]
		public int? SendStatus{ get;set; }		
				
		[DataMember]
		public DateTime? SendTime{ get;set; }		
				
		[DataMember]
		public bool? IsCancel{ get;set; }		
				
		[DataMember]
		public int? WmsSendStatus{ get;set; }		
				
		[DataMember]
		public DateTime? WmsSendTime{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			MaterialRequestsInfo info = new MaterialRequestsInfo();

			info.InterfaceId = this.InterfaceId;
			info.PlantZone = this.PlantZone;
			info.Workshop = this.Workshop;
			info.AssemblyLine = this.AssemblyLine;
			info.Plant = this.Plant;
			info.Location = this.Location;
			info.RequestTime = this.RequestTime;
			info.InterfaceStatus = this.InterfaceStatus;
			info.ProcessTime = this.ProcessTime;
			info.PartNo = this.PartNo;
			info.PartCname = this.PartCname;
			info.PartEname = this.PartEname;
			info.SupplierNum = this.SupplierNum;
			info.Dock = this.Dock;
			info.BoxParts = this.BoxParts;
			info.InterfaceType = this.InterfaceType;
			info.PackCount = this.PackCount;
			info.RequriedPack = this.RequriedPack;
			info.InhousePackageModel = this.InhousePackageModel;
			info.InhousePackage = this.InhousePackage;
			info.MeasuringUnitNo = this.MeasuringUnitNo;
			info.ExpectedArrivalTime = this.ExpectedArrivalTime;
			info.RdcDloc = this.RdcDloc;
			info.PickupSeqNo = this.PickupSeqNo;
			info.SequenceNo = this.SequenceNo;
			info.IsOrganizeSheet = this.IsOrganizeSheet;
			info.SendStatus = this.SendStatus;
			info.SendTime = this.SendTime;
			info.IsCancel = this.IsCancel;
			info.WmsSendStatus = this.WmsSendStatus;
			info.WmsSendTime = this.WmsSendTime;
			info.Comments = this.Comments;
			info.UpdateDate = this.UpdateDate;
			info.UpdateUser = this.UpdateUser;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			return info;			
		}
		 
		public MaterialRequestsInfo Clone()
		{
			return ((ICloneable) this).Clone() as MaterialRequestsInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// MaterialRequestsInfoCollection对应表[TI_PCS_MATERIAL_REQUESTS]
    /// </summary>
	public partial class MaterialRequestsInfoCollection : BusinessObjectCollection<MaterialRequestsInfo>
	{
		public MaterialRequestsInfoCollection():base("TI_PCS_MATERIAL_REQUESTS"){}	
	}
}