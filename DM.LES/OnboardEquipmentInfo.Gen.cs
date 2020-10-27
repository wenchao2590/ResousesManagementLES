#region Declaim
//---------------------------------------------------------------------------
// Name:		OnboardEquipmentInfo
// Function: 	Expose data in table OnboardEquipment from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年3月30日
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
    /// OnboardEquipmentInfo对应表[TM_BAS_ONBOARD_EQUIPMENT]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class OnboardEquipmentInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public OnboardEquipmentInfo( 
					string aEquipCpu,

					string aEquipHdd,

					string aComments,

					string aOsVersion,

					string aLesVersion,

					string aIpAddress,

					int aStatus,

					string aRfidIp,

					int aRfidPort,

					bool aValidFlag,

					string aModifyUser,

					long aId,

					string aCreateUser,

					DateTime aModifyDate,

					DateTime aCreateDate

				 
		) : this()
		{
			 
			EquipCpu = aEquipCpu;
		 
			EquipHdd = aEquipHdd;
		 
			Comments = aComments;
		 
			OsVersion = aOsVersion;
		 
			LesVersion = aLesVersion;
		 
			IpAddress = aIpAddress;
		 
			Status = aStatus;
		 
			RfidIp = aRfidIp;
		 
			RfidPort = aRfidPort;
		 
			ValidFlag = aValidFlag;
		 
			ModifyUser = aModifyUser;
		 
			Id = aId;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			CreateDate = aCreateDate;
		}
		
		public OnboardEquipmentInfo():base("TM_BAS_ONBOARD_EQUIPMENT")
		{
			List<string> keys = new List<string>();
			            			keys.Add("ID");   _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField EQUIP_CPUField = new DataSchemaField();
			EQUIP_CPUField.Name = "EQUIP_CPU";
			EQUIP_CPUField.Type = typeof(string).ToString();
			EQUIP_CPUField.Index = 0;
			fields.Add(EQUIP_CPUField);
			 
			DataSchemaField EQUIP_HDDField = new DataSchemaField();
			EQUIP_HDDField.Name = "EQUIP_HDD";
			EQUIP_HDDField.Type = typeof(string).ToString();
			EQUIP_HDDField.Index = 1;
			fields.Add(EQUIP_HDDField);
			 
			DataSchemaField COMMENTSField = new DataSchemaField();
			COMMENTSField.Name = "COMMENTS";
			COMMENTSField.Type = typeof(string).ToString();
			COMMENTSField.Index = 2;
			fields.Add(COMMENTSField);
			 
			DataSchemaField OS_VERSIONField = new DataSchemaField();
			OS_VERSIONField.Name = "OS_VERSION";
			OS_VERSIONField.Type = typeof(string).ToString();
			OS_VERSIONField.Index = 3;
			fields.Add(OS_VERSIONField);
			 
			DataSchemaField LES_VERSIONField = new DataSchemaField();
			LES_VERSIONField.Name = "LES_VERSION";
			LES_VERSIONField.Type = typeof(string).ToString();
			LES_VERSIONField.Index = 4;
			fields.Add(LES_VERSIONField);
			 
			DataSchemaField IP_ADDRESSField = new DataSchemaField();
			IP_ADDRESSField.Name = "IP_ADDRESS";
			IP_ADDRESSField.Type = typeof(string).ToString();
			IP_ADDRESSField.Index = 5;
			fields.Add(IP_ADDRESSField);
			 
			DataSchemaField STATUSField = new DataSchemaField();
			STATUSField.Name = "STATUS";
			STATUSField.Type = typeof(int).ToString();
			STATUSField.Index = 6;
			fields.Add(STATUSField);
			 
			DataSchemaField RFID_IPField = new DataSchemaField();
			RFID_IPField.Name = "RFID_IP";
			RFID_IPField.Type = typeof(string).ToString();
			RFID_IPField.Index = 7;
			fields.Add(RFID_IPField);
			 
			DataSchemaField RFID_PORTField = new DataSchemaField();
			RFID_PORTField.Name = "RFID_PORT";
			RFID_PORTField.Type = typeof(int).ToString();
			RFID_PORTField.Index = 8;
			fields.Add(RFID_PORTField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 9;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 10;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 11;
			fields.Add(IDField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 12;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 13;
			fields.Add(MODIFY_DATEField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 14;
			fields.Add(CREATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public string EquipCpu{ get;set; }		
				
		[DataMember]
		public string EquipHdd{ get;set; }		
				
		[DataMember]
		public string Comments{ get;set; }		
				
		[DataMember]
		public string OsVersion{ get;set; }		
				
		[DataMember]
		public string LesVersion{ get;set; }		
				
		[DataMember]
		public string IpAddress{ get;set; }		
				
		[DataMember]
		public int? Status{ get;set; }		
				
		[DataMember]
		public string RfidIp{ get;set; }		
				
		[DataMember]
		public int? RfidPort{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public DateTime CreateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			OnboardEquipmentInfo info = new OnboardEquipmentInfo();

			info.EquipCpu = this.EquipCpu;
			info.EquipHdd = this.EquipHdd;
			info.Comments = this.Comments;
			info.OsVersion = this.OsVersion;
			info.LesVersion = this.LesVersion;
			info.IpAddress = this.IpAddress;
			info.Status = this.Status;
			info.RfidIp = this.RfidIp;
			info.RfidPort = this.RfidPort;
			info.ValidFlag = this.ValidFlag;
			info.ModifyUser = this.ModifyUser;
			info.Id = this.Id;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.CreateDate = this.CreateDate;
			return info;			
		}
		 
		public OnboardEquipmentInfo Clone()
		{
			return ((ICloneable) this).Clone() as OnboardEquipmentInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// OnboardEquipmentInfoCollection对应表[TM_BAS_ONBOARD_EQUIPMENT]
    /// </summary>
	public partial class OnboardEquipmentInfoCollection : BusinessObjectCollection<OnboardEquipmentInfo>
	{
		public OnboardEquipmentInfoCollection():base("TM_BAS_ONBOARD_EQUIPMENT"){}	
	}
}