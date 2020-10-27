#region Declaim
//---------------------------------------------------------------------------
// Name:		RunsheetInfo
// Function: 	Expose data in table Runsheet from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年5月25日
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
    /// RunsheetInfo对应表[TT_SPS_RUNSHEET]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class RunsheetInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public RunsheetInfo( 
					int aSpsRunsheetSn,

					string aSpsRunsheetNo,

					string aPlant,

					string aAssemblyLine,

					string aWorkshop,

					string aPlantZone,

					DateTime aPublishTime,

					int aRunsheetType,

					string aSupplierNum,

					int aSupplierSn,

					string aDock,

					string aDeliveryLocation,

					string aBoxParts,

					int aPartType,

					DateTime aExpectedArrivalTime,

					DateTime aSuggestDeliveryTime,

					DateTime aActualArrivalTime,

					DateTime aVerifyTime,

					string aRejectReason,

					string aTransSupplierNum,

					string aFeedback,

					int aSheetStatus,

					DateTime aSendTime,

					int aSendStatus,

					string aOperatonUser,

					string aCheckUser,

					int aRetryTimes,

					DateTime aSupplyTime,

					int aSupplyStatus,

					DateTime aFaxTime,

					int aFaxStatus,

					string aOrderNo,

					string aVin,

					DateTime aPtlSendTime,

					int aPtlSendStatus,

					int aPrintTimes,

					int aPrintState,

					string aComments,

					DateTime aUpdateDate,

					string aUpdateUser,

					DateTime aCreateDate,

					string aCreateUser

				 
		) : this()
		{
			 
			SpsRunsheetSn = aSpsRunsheetSn;
		 
			SpsRunsheetNo = aSpsRunsheetNo;
		 
			Plant = aPlant;
		 
			AssemblyLine = aAssemblyLine;
		 
			Workshop = aWorkshop;
		 
			PlantZone = aPlantZone;
		 
			PublishTime = aPublishTime;
		 
			RunsheetType = aRunsheetType;
		 
			SupplierNum = aSupplierNum;
		 
			SupplierSn = aSupplierSn;
		 
			Dock = aDock;
		 
			DeliveryLocation = aDeliveryLocation;
		 
			BoxParts = aBoxParts;
		 
			PartType = aPartType;
		 
			ExpectedArrivalTime = aExpectedArrivalTime;
		 
			SuggestDeliveryTime = aSuggestDeliveryTime;
		 
			ActualArrivalTime = aActualArrivalTime;
		 
			VerifyTime = aVerifyTime;
		 
			RejectReason = aRejectReason;
		 
			TransSupplierNum = aTransSupplierNum;
		 
			Feedback = aFeedback;
		 
			SheetStatus = aSheetStatus;
		 
			SendTime = aSendTime;
		 
			SendStatus = aSendStatus;
		 
			OperatonUser = aOperatonUser;
		 
			CheckUser = aCheckUser;
		 
			RetryTimes = aRetryTimes;
		 
			SupplyTime = aSupplyTime;
		 
			SupplyStatus = aSupplyStatus;
		 
			FaxTime = aFaxTime;
		 
			FaxStatus = aFaxStatus;
		 
			OrderNo = aOrderNo;
		 
			Vin = aVin;
		 
			PtlSendTime = aPtlSendTime;
		 
			PtlSendStatus = aPtlSendStatus;
		 
			PrintTimes = aPrintTimes;
		 
			PrintState = aPrintState;
		 
			Comments = aComments;
		 
			UpdateDate = aUpdateDate;
		 
			UpdateUser = aUpdateUser;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		}
		
		public RunsheetInfo():base("TT_SPS_RUNSHEET")
		{
			List<string> keys = new List<string>();
			 			keys.Add("SPS_RUNSHEET_SN");                                         _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField SPS_RUNSHEET_SNField = new DataSchemaField();
			SPS_RUNSHEET_SNField.Name = "SPS_RUNSHEET_SN";
			SPS_RUNSHEET_SNField.Type = typeof(int).ToString();
			SPS_RUNSHEET_SNField.Index = 0;
			fields.Add(SPS_RUNSHEET_SNField);
			 
			DataSchemaField SPS_RUNSHEET_NOField = new DataSchemaField();
			SPS_RUNSHEET_NOField.Name = "SPS_RUNSHEET_NO";
			SPS_RUNSHEET_NOField.Type = typeof(string).ToString();
			SPS_RUNSHEET_NOField.Index = 1;
			fields.Add(SPS_RUNSHEET_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 2;
			fields.Add(PLANTField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 3;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 4;
			fields.Add(WORKSHOPField);
			 
			DataSchemaField PLANT_ZONEField = new DataSchemaField();
			PLANT_ZONEField.Name = "PLANT_ZONE";
			PLANT_ZONEField.Type = typeof(string).ToString();
			PLANT_ZONEField.Index = 5;
			fields.Add(PLANT_ZONEField);
			 
			DataSchemaField PUBLISH_TIMEField = new DataSchemaField();
			PUBLISH_TIMEField.Name = "PUBLISH_TIME";
			PUBLISH_TIMEField.Type = typeof(DateTime).ToString();
			PUBLISH_TIMEField.Index = 6;
			fields.Add(PUBLISH_TIMEField);
			 
			DataSchemaField RUNSHEET_TYPEField = new DataSchemaField();
			RUNSHEET_TYPEField.Name = "RUNSHEET_TYPE";
			RUNSHEET_TYPEField.Type = typeof(int).ToString();
			RUNSHEET_TYPEField.Index = 7;
			fields.Add(RUNSHEET_TYPEField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 8;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField SUPPLIER_SNField = new DataSchemaField();
			SUPPLIER_SNField.Name = "SUPPLIER_SN";
			SUPPLIER_SNField.Type = typeof(int).ToString();
			SUPPLIER_SNField.Index = 9;
			fields.Add(SUPPLIER_SNField);
			 
			DataSchemaField DOCKField = new DataSchemaField();
			DOCKField.Name = "DOCK";
			DOCKField.Type = typeof(string).ToString();
			DOCKField.Index = 10;
			fields.Add(DOCKField);
			 
			DataSchemaField DELIVERY_LOCATIONField = new DataSchemaField();
			DELIVERY_LOCATIONField.Name = "DELIVERY_LOCATION";
			DELIVERY_LOCATIONField.Type = typeof(string).ToString();
			DELIVERY_LOCATIONField.Index = 11;
			fields.Add(DELIVERY_LOCATIONField);
			 
			DataSchemaField BOX_PARTSField = new DataSchemaField();
			BOX_PARTSField.Name = "BOX_PARTS";
			BOX_PARTSField.Type = typeof(string).ToString();
			BOX_PARTSField.Index = 12;
			fields.Add(BOX_PARTSField);
			 
			DataSchemaField PART_TYPEField = new DataSchemaField();
			PART_TYPEField.Name = "PART_TYPE";
			PART_TYPEField.Type = typeof(int).ToString();
			PART_TYPEField.Index = 13;
			fields.Add(PART_TYPEField);
			 
			DataSchemaField EXPECTED_ARRIVAL_TIMEField = new DataSchemaField();
			EXPECTED_ARRIVAL_TIMEField.Name = "EXPECTED_ARRIVAL_TIME";
			EXPECTED_ARRIVAL_TIMEField.Type = typeof(DateTime).ToString();
			EXPECTED_ARRIVAL_TIMEField.Index = 14;
			fields.Add(EXPECTED_ARRIVAL_TIMEField);
			 
			DataSchemaField SUGGEST_DELIVERY_TIMEField = new DataSchemaField();
			SUGGEST_DELIVERY_TIMEField.Name = "SUGGEST_DELIVERY_TIME";
			SUGGEST_DELIVERY_TIMEField.Type = typeof(DateTime).ToString();
			SUGGEST_DELIVERY_TIMEField.Index = 15;
			fields.Add(SUGGEST_DELIVERY_TIMEField);
			 
			DataSchemaField ACTUAL_ARRIVAL_TIMEField = new DataSchemaField();
			ACTUAL_ARRIVAL_TIMEField.Name = "ACTUAL_ARRIVAL_TIME";
			ACTUAL_ARRIVAL_TIMEField.Type = typeof(DateTime).ToString();
			ACTUAL_ARRIVAL_TIMEField.Index = 16;
			fields.Add(ACTUAL_ARRIVAL_TIMEField);
			 
			DataSchemaField VERIFY_TIMEField = new DataSchemaField();
			VERIFY_TIMEField.Name = "VERIFY_TIME";
			VERIFY_TIMEField.Type = typeof(DateTime).ToString();
			VERIFY_TIMEField.Index = 17;
			fields.Add(VERIFY_TIMEField);
			 
			DataSchemaField REJECT_REASONField = new DataSchemaField();
			REJECT_REASONField.Name = "REJECT_REASON";
			REJECT_REASONField.Type = typeof(string).ToString();
			REJECT_REASONField.Index = 18;
			fields.Add(REJECT_REASONField);
			 
			DataSchemaField TRANS_SUPPLIER_NUMField = new DataSchemaField();
			TRANS_SUPPLIER_NUMField.Name = "TRANS_SUPPLIER_NUM";
			TRANS_SUPPLIER_NUMField.Type = typeof(string).ToString();
			TRANS_SUPPLIER_NUMField.Index = 19;
			fields.Add(TRANS_SUPPLIER_NUMField);
			 
			DataSchemaField FEEDBACKField = new DataSchemaField();
			FEEDBACKField.Name = "FEEDBACK";
			FEEDBACKField.Type = typeof(string).ToString();
			FEEDBACKField.Index = 20;
			fields.Add(FEEDBACKField);
			 
			DataSchemaField SHEET_STATUSField = new DataSchemaField();
			SHEET_STATUSField.Name = "SHEET_STATUS";
			SHEET_STATUSField.Type = typeof(int).ToString();
			SHEET_STATUSField.Index = 21;
			fields.Add(SHEET_STATUSField);
			 
			DataSchemaField SEND_TIMEField = new DataSchemaField();
			SEND_TIMEField.Name = "SEND_TIME";
			SEND_TIMEField.Type = typeof(DateTime).ToString();
			SEND_TIMEField.Index = 22;
			fields.Add(SEND_TIMEField);
			 
			DataSchemaField SEND_STATUSField = new DataSchemaField();
			SEND_STATUSField.Name = "SEND_STATUS";
			SEND_STATUSField.Type = typeof(int).ToString();
			SEND_STATUSField.Index = 23;
			fields.Add(SEND_STATUSField);
			 
			DataSchemaField OPERATON_USERField = new DataSchemaField();
			OPERATON_USERField.Name = "OPERATON_USER";
			OPERATON_USERField.Type = typeof(string).ToString();
			OPERATON_USERField.Index = 24;
			fields.Add(OPERATON_USERField);
			 
			DataSchemaField CHECK_USERField = new DataSchemaField();
			CHECK_USERField.Name = "CHECK_USER";
			CHECK_USERField.Type = typeof(string).ToString();
			CHECK_USERField.Index = 25;
			fields.Add(CHECK_USERField);
			 
			DataSchemaField RETRY_TIMESField = new DataSchemaField();
			RETRY_TIMESField.Name = "RETRY_TIMES";
			RETRY_TIMESField.Type = typeof(int).ToString();
			RETRY_TIMESField.Index = 26;
			fields.Add(RETRY_TIMESField);
			 
			DataSchemaField SUPPLY_TIMEField = new DataSchemaField();
			SUPPLY_TIMEField.Name = "SUPPLY_TIME";
			SUPPLY_TIMEField.Type = typeof(DateTime).ToString();
			SUPPLY_TIMEField.Index = 27;
			fields.Add(SUPPLY_TIMEField);
			 
			DataSchemaField SUPPLY_STATUSField = new DataSchemaField();
			SUPPLY_STATUSField.Name = "SUPPLY_STATUS";
			SUPPLY_STATUSField.Type = typeof(int).ToString();
			SUPPLY_STATUSField.Index = 28;
			fields.Add(SUPPLY_STATUSField);
			 
			DataSchemaField FAX_TIMEField = new DataSchemaField();
			FAX_TIMEField.Name = "FAX_TIME";
			FAX_TIMEField.Type = typeof(DateTime).ToString();
			FAX_TIMEField.Index = 29;
			fields.Add(FAX_TIMEField);
			 
			DataSchemaField FAX_STATUSField = new DataSchemaField();
			FAX_STATUSField.Name = "FAX_STATUS";
			FAX_STATUSField.Type = typeof(int).ToString();
			FAX_STATUSField.Index = 30;
			fields.Add(FAX_STATUSField);
			 
			DataSchemaField ORDER_NOField = new DataSchemaField();
			ORDER_NOField.Name = "ORDER_NO";
			ORDER_NOField.Type = typeof(string).ToString();
			ORDER_NOField.Index = 31;
			fields.Add(ORDER_NOField);
			 
			DataSchemaField VINField = new DataSchemaField();
			VINField.Name = "VIN";
			VINField.Type = typeof(string).ToString();
			VINField.Index = 32;
			fields.Add(VINField);
			 
			DataSchemaField PTL_SEND_TIMEField = new DataSchemaField();
			PTL_SEND_TIMEField.Name = "PTL_SEND_TIME";
			PTL_SEND_TIMEField.Type = typeof(DateTime).ToString();
			PTL_SEND_TIMEField.Index = 33;
			fields.Add(PTL_SEND_TIMEField);
			 
			DataSchemaField PTL_SEND_STATUSField = new DataSchemaField();
			PTL_SEND_STATUSField.Name = "PTL_SEND_STATUS";
			PTL_SEND_STATUSField.Type = typeof(int).ToString();
			PTL_SEND_STATUSField.Index = 34;
			fields.Add(PTL_SEND_STATUSField);
			 
			DataSchemaField PRINT_TIMESField = new DataSchemaField();
			PRINT_TIMESField.Name = "PRINT_TIMES";
			PRINT_TIMESField.Type = typeof(int).ToString();
			PRINT_TIMESField.Index = 35;
			fields.Add(PRINT_TIMESField);
			 
			DataSchemaField PRINT_STATEField = new DataSchemaField();
			PRINT_STATEField.Name = "PRINT_STATE";
			PRINT_STATEField.Type = typeof(int).ToString();
			PRINT_STATEField.Index = 36;
			fields.Add(PRINT_STATEField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 37;
			fields.Add(COMMENTSField);
			 
			DataSchemaField UPDATE_DATEField = new DataSchemaField();
			UPDATE_DATEField.Name = "UPDATE_DATE";
			UPDATE_DATEField.Type = typeof(DateTime).ToString();
			UPDATE_DATEField.Index = 38;
			fields.Add(UPDATE_DATEField);
			 
			DataSchemaField UPDATE_USERField = new DataSchemaField();
			UPDATE_USERField.Name = "UPDATE_USER";
			UPDATE_USERField.Type = typeof(string).ToString();
			UPDATE_USERField.Index = 39;
			fields.Add(UPDATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 40;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 41;
			fields.Add(CREATE_USERField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public int SpsRunsheetSn{ get;set; }		
				
		[DataMember]
		public string SpsRunsheetNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		[DataMember]
		public string PlantZone{ get;set; }		
				
		[DataMember]
		public DateTime PublishTime{ get;set; }		
				
		[DataMember]
		public int RunsheetType{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public int SupplierSn{ get;set; }		
				
		[DataMember]
		public string Dock{ get;set; }		
				
		[DataMember]
		public string DeliveryLocation{ get;set; }		
				
		[DataMember]
		public string BoxParts{ get;set; }		
				
		[DataMember]
		public int? PartType{ get;set; }		
				
		[DataMember]
		public DateTime ExpectedArrivalTime{ get;set; }		
				
		[DataMember]
		public DateTime? SuggestDeliveryTime{ get;set; }		
				
		[DataMember]
		public DateTime? ActualArrivalTime{ get;set; }		
				
		[DataMember]
		public DateTime? VerifyTime{ get;set; }		
				
		[DataMember]
		public string RejectReason{ get;set; }		
				
		[DataMember]
		public string TransSupplierNum{ get;set; }		
				
		[DataMember]
		public string Feedback{ get;set; }		
				
		[DataMember]
		public int SheetStatus{ get;set; }		
				
		[DataMember]
		public DateTime? SendTime{ get;set; }		
				
		[DataMember]
		public int? SendStatus{ get;set; }		
				
		[DataMember]
		public string OperatonUser{ get;set; }		
				
		[DataMember]
		public string CheckUser{ get;set; }		
				
		[DataMember]
		public int? RetryTimes{ get;set; }		
				
		[DataMember]
		public DateTime? SupplyTime{ get;set; }		
				
		[DataMember]
		public int? SupplyStatus{ get;set; }		
				
		[DataMember]
		public DateTime? FaxTime{ get;set; }		
				
		[DataMember]
		public int? FaxStatus{ get;set; }		
				
		[DataMember]
		public string OrderNo{ get;set; }		
				
		[DataMember]
		public string Vin{ get;set; }		
				
		[DataMember]
		public DateTime? PtlSendTime{ get;set; }		
				
		[DataMember]
		public int? PtlSendStatus{ get;set; }		
				
				
		private int _PrintTimes = 0;
		
		[DataMember]	
		public int PrintTimes
		{
			get
			{
				return _PrintTimes;
			}
			set
			{
				_PrintTimes = value;
			}
		}
				
				
				
		private int _PrintState = 0;
		
		[DataMember]	
		public int PrintState
		{
			get
			{
				return _PrintState;
			}
			set
			{
				_PrintState = value;
			}
		}
				
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public DateTime? UpdateDate{ get;set; }		
				
		[DataMember]
		public string UpdateUser{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			RunsheetInfo info = new RunsheetInfo();

			info.SpsRunsheetSn = this.SpsRunsheetSn;
			info.SpsRunsheetNo = this.SpsRunsheetNo;
			info.Plant = this.Plant;
			info.AssemblyLine = this.AssemblyLine;
			info.Workshop = this.Workshop;
			info.PlantZone = this.PlantZone;
			info.PublishTime = this.PublishTime;
			info.RunsheetType = this.RunsheetType;
			info.SupplierNum = this.SupplierNum;
			info.SupplierSn = this.SupplierSn;
			info.Dock = this.Dock;
			info.DeliveryLocation = this.DeliveryLocation;
			info.BoxParts = this.BoxParts;
			info.PartType = this.PartType;
			info.ExpectedArrivalTime = this.ExpectedArrivalTime;
			info.SuggestDeliveryTime = this.SuggestDeliveryTime;
			info.ActualArrivalTime = this.ActualArrivalTime;
			info.VerifyTime = this.VerifyTime;
			info.RejectReason = this.RejectReason;
			info.TransSupplierNum = this.TransSupplierNum;
			info.Feedback = this.Feedback;
			info.SheetStatus = this.SheetStatus;
			info.SendTime = this.SendTime;
			info.SendStatus = this.SendStatus;
			info.OperatonUser = this.OperatonUser;
			info.CheckUser = this.CheckUser;
			info.RetryTimes = this.RetryTimes;
			info.SupplyTime = this.SupplyTime;
			info.SupplyStatus = this.SupplyStatus;
			info.FaxTime = this.FaxTime;
			info.FaxStatus = this.FaxStatus;
			info.OrderNo = this.OrderNo;
			info.Vin = this.Vin;
			info.PtlSendTime = this.PtlSendTime;
			info.PtlSendStatus = this.PtlSendStatus;
			info.PrintTimes = this.PrintTimes;
			info.PrintState = this.PrintState;
			info.Comments = this.Comments;
			info.UpdateDate = this.UpdateDate;
			info.UpdateUser = this.UpdateUser;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			return info;			
		}
		 
		public RunsheetInfo Clone()
		{
			return ((ICloneable) this).Clone() as RunsheetInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// RunsheetInfoCollection对应表[TT_SPS_RUNSHEET]
    /// </summary>
	public partial class RunsheetInfoCollection : BusinessObjectCollection<RunsheetInfo>
	{
		public RunsheetInfoCollection():base("TT_SPS_RUNSHEET"){}	
	}
}