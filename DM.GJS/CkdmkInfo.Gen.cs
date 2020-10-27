#region Declaim
//---------------------------------------------------------------------------
// Name:		CkdmkInfo
// Function: 	Expose data in table Ckdmk from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年10月10日
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

namespace DM.GJS 
{   
	/// <summary>
    /// CkdmkInfo对应表[TM_BAS_CKDMK]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class CkdmkInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public CkdmkInfo( 
					long aId,

					string aCode,

					string aName,

					string aAddr,

					string aTel,

					string aFax,

					string aLxr,

					string aBz,

					bool aValidFlag,

					DateTime aCreateDate,

					string aCreateUser,

					DateTime aModifyDate,

					string aModifyUser

				 
		) : this()
		{
			 
			Id = aId;
		 
			Code = aCode;
		 
			Name = aName;
		 
			Addr = aAddr;
		 
			Tel = aTel;
		 
			Fax = aFax;
		 
			Lxr = aLxr;
		 
			Bz = aBz;
		 
			ValidFlag = aValidFlag;
		 
			CreateDate = aCreateDate;
		 
			CreateUser = aCreateUser;
		 
			ModifyDate = aModifyDate;
		 
			ModifyUser = aModifyUser;
		}
		
		public CkdmkInfo():base("TM_BAS_CKDMK")
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
			 
			DataSchemaField CODEField = new DataSchemaField();
			CODEField.Name = "CODE";
			CODEField.Type = typeof(string).ToString();
			CODEField.Index = 1;
			fields.Add(CODEField);
			 
			DataSchemaField NAMEField = new DataSchemaField();
			NAMEField.Name = "NAME";
			NAMEField.Type = typeof(string).ToString();
			NAMEField.Index = 2;
			fields.Add(NAMEField);
			 
			DataSchemaField ADDRField = new DataSchemaField();
			ADDRField.Name = "ADDR";
			ADDRField.Type = typeof(string).ToString();
			ADDRField.Index = 3;
			fields.Add(ADDRField);
			 
			DataSchemaField TELField = new DataSchemaField();
			TELField.Name = "TEL";
			TELField.Type = typeof(string).ToString();
			TELField.Index = 4;
			fields.Add(TELField);
			 
			DataSchemaField FAXField = new DataSchemaField();
			FAXField.Name = "FAX";
			FAXField.Type = typeof(string).ToString();
			FAXField.Index = 5;
			fields.Add(FAXField);
			 
			DataSchemaField LXRField = new DataSchemaField();
			LXRField.Name = "LXR";
			LXRField.Type = typeof(string).ToString();
			LXRField.Index = 6;
			fields.Add(LXRField);
			 
			DataSchemaField BZField = new DataSchemaField();
			BZField.Name = "BZ";
			BZField.Type = typeof(string).ToString();
			BZField.Index = 7;
			fields.Add(BZField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 8;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 9;
			fields.Add(CREATE_DATEField);
			 
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
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 12;
			fields.Add(MODIFY_USERField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string Code{ get;set; }		
				
		[DataMember]
		public string Name{ get;set; }		
				
		[DataMember]
		public string Addr{ get;set; }		
				
		[DataMember]
		public string Tel{ get;set; }		
				
		[DataMember]
		public string Fax{ get;set; }		
				
		[DataMember]
		public string Lxr{ get;set; }		
				
		[DataMember]
		public string Bz{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
		[DataMember]
		public DateTime? CreateDate{ get;set; }		
				
		[DataMember]
		public string CreateUser{ get;set; }		
				
		[DataMember]
		public DateTime? ModifyDate{ get;set; }		
				
		[DataMember]
		public string ModifyUser{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			CkdmkInfo info = new CkdmkInfo();

			info.Id = this.Id;
			info.Code = this.Code;
			info.Name = this.Name;
			info.Addr = this.Addr;
			info.Tel = this.Tel;
			info.Fax = this.Fax;
			info.Lxr = this.Lxr;
			info.Bz = this.Bz;
			info.ValidFlag = this.ValidFlag;
			info.CreateDate = this.CreateDate;
			info.CreateUser = this.CreateUser;
			info.ModifyDate = this.ModifyDate;
			info.ModifyUser = this.ModifyUser;
			return info;			
		}
		 
		public CkdmkInfo Clone()
		{
			return ((ICloneable) this).Clone() as CkdmkInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// CkdmkInfoCollection对应表[TM_BAS_CKDMK]
    /// </summary>
	public partial class CkdmkInfoCollection : BusinessObjectCollection<CkdmkInfo>
	{
		public CkdmkInfoCollection():base("TM_BAS_CKDMK"){}	
	}
}
