#region Declaim
//---------------------------------------------------------------------------
// Name:		SimulatePassConfigInfo
// Function: 	Expose data in table SimulatePassConfig from database as business object to MES system.
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
    /// SimulatePassConfigInfo对应表[TM_BAS_SIMULATE_PASS_CONFIG]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class SimulatePassConfigInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public SimulatePassConfigInfo( 
					long aId,

					string aAssemblyLineFrom,

					string aDcpPointFrom,

					string aAssemblyLineTo,

					string aDcpPointTo,

					int aDelayType,

					int aDelayCount,

					string aPlant,

					bool aValidFlag,

					string aModifyUser,

					string aCreateUser,

					DateTime aModifyDate,

					DateTime aCreateDate

				 
		) : this()
		{
			 
			Id = aId;
		 
			AssemblyLineFrom = aAssemblyLineFrom;
		 
			DcpPointFrom = aDcpPointFrom;
		 
			AssemblyLineTo = aAssemblyLineTo;
		 
			DcpPointTo = aDcpPointTo;
		 
			DelayType = aDelayType;
		 
			DelayCount = aDelayCount;
		 
			Plant = aPlant;
		 
			ValidFlag = aValidFlag;
		 
			ModifyUser = aModifyUser;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			CreateDate = aCreateDate;
		}
		
		public SimulatePassConfigInfo():base("TM_BAS_SIMULATE_PASS_CONFIG")
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
			 
			DataSchemaField ASSEMBLY_LINE_FROMField = new DataSchemaField();
			ASSEMBLY_LINE_FROMField.Name = "ASSEMBLY_LINE_FROM";
			ASSEMBLY_LINE_FROMField.Type = typeof(string).ToString();
			ASSEMBLY_LINE_FROMField.Index = 1;
			fields.Add(ASSEMBLY_LINE_FROMField);
			 
			DataSchemaField DCP_POINT_FROMField = new DataSchemaField();
			DCP_POINT_FROMField.Name = "DCP_POINT_FROM";
			DCP_POINT_FROMField.Type = typeof(string).ToString();
			DCP_POINT_FROMField.Index = 2;
			fields.Add(DCP_POINT_FROMField);
			 
			DataSchemaField ASSEMBLY_LINE_TOField = new DataSchemaField();
			ASSEMBLY_LINE_TOField.Name = "ASSEMBLY_LINE_TO";
			ASSEMBLY_LINE_TOField.Type = typeof(string).ToString();
			ASSEMBLY_LINE_TOField.Index = 3;
			fields.Add(ASSEMBLY_LINE_TOField);
			 
			DataSchemaField DCP_POINT_TOField = new DataSchemaField();
			DCP_POINT_TOField.Name = "DCP_POINT_TO";
			DCP_POINT_TOField.Type = typeof(string).ToString();
			DCP_POINT_TOField.Index = 4;
			fields.Add(DCP_POINT_TOField);
			 
			DataSchemaField DELAY_TYPEField = new DataSchemaField();
			DELAY_TYPEField.Name = "DELAY_TYPE";
			DELAY_TYPEField.Type = typeof(int).ToString();
			DELAY_TYPEField.Index = 5;
			fields.Add(DELAY_TYPEField);
			 
			DataSchemaField DELAY_COUNTField = new DataSchemaField();
			DELAY_COUNTField.Name = "DELAY_COUNT";
			DELAY_COUNTField.Type = typeof(int).ToString();
			DELAY_COUNTField.Index = 6;
			fields.Add(DELAY_COUNTField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 7;
			fields.Add(PLANTField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 8;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 9;
			fields.Add(MODIFY_USERField);
			 
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
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 12;
			fields.Add(CREATE_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string AssemblyLineFrom{ get;set; }		
				
		[DataMember]
		public string DcpPointFrom{ get;set; }		
				
		[DataMember]
		public string AssemblyLineTo{ get;set; }		
				
		[DataMember]
		public string DcpPointTo{ get;set; }		
				
		[DataMember]
		public int? DelayType{ get;set; }		
				
		[DataMember]
		public int? DelayCount{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			SimulatePassConfigInfo info = new SimulatePassConfigInfo();

			info.Id = this.Id;
			info.AssemblyLineFrom = this.AssemblyLineFrom;
			info.DcpPointFrom = this.DcpPointFrom;
			info.AssemblyLineTo = this.AssemblyLineTo;
			info.DcpPointTo = this.DcpPointTo;
			info.DelayType = this.DelayType;
			info.DelayCount = this.DelayCount;
			info.Plant = this.Plant;
			info.ValidFlag = this.ValidFlag;
			info.ModifyUser = this.ModifyUser;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.CreateDate = this.CreateDate;
			return info;			
		}
		 
		public SimulatePassConfigInfo Clone()
		{
			return ((ICloneable) this).Clone() as SimulatePassConfigInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// SimulatePassConfigInfoCollection对应表[TM_BAS_SIMULATE_PASS_CONFIG]
    /// </summary>
	public partial class SimulatePassConfigInfoCollection : BusinessObjectCollection<SimulatePassConfigInfo>
	{
		public SimulatePassConfigInfoCollection():base("TM_BAS_SIMULATE_PASS_CONFIG"){}	
	}
}
