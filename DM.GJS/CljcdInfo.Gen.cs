#region Declaim
//---------------------------------------------------------------------------
// Name:		CljcdInfo
// Function: 	Expose data in table Cljcd from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年10月11日
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
    /// CljcdInfo对应表[TT_WMS_CLJCD]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class CljcdInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public CljcdInfo( 
					long aId,

					string aJcdh,

					int aJclb,

					DateTime aJcsj,

					string aGysdm,

					string aGysmc,

					decimal aZsl,

					decimal aZmz,

					decimal aZtj,

					decimal aZje,

					int aZt,

					string aBm,

					bool aZdwcbj,

					DateTime aZdwcsj,

					string aZdwcr,

					bool aZgshbj,

					DateTime aZgshsj,

					string aZg,

					bool aCwshbj,

					DateTime aCwshsj,

					string aCw,

					int aHsxm,

					string aCkCode,

					string aCkName,

					string aBz,

					bool aValidFlag,

					string aCreateUser,

					DateTime aCreateDate,

					string aModifyUser,

					DateTime aModifyDate

				 
		) : this()
		{
			 
			Id = aId;
		 
			Jcdh = aJcdh;
		 
			Jclb = aJclb;
		 
			Jcsj = aJcsj;
		 
			Gysdm = aGysdm;
		 
			Gysmc = aGysmc;
		 
			Zsl = aZsl;
		 
			Zmz = aZmz;
		 
			Ztj = aZtj;
		 
			Zje = aZje;
		 
			Zt = aZt;
		 
			Bm = aBm;
		 
			Zdwcbj = aZdwcbj;
		 
			Zdwcsj = aZdwcsj;
		 
			Zdwcr = aZdwcr;
		 
			Zgshbj = aZgshbj;
		 
			Zgshsj = aZgshsj;
		 
			Zg = aZg;
		 
			Cwshbj = aCwshbj;
		 
			Cwshsj = aCwshsj;
		 
			Cw = aCw;
		 
			Hsxm = aHsxm;
		 
			CkCode = aCkCode;
		 
			CkName = aCkName;
		 
			Bz = aBz;
		 
			ValidFlag = aValidFlag;
		 
			CreateUser = aCreateUser;
		 
			CreateDate = aCreateDate;
		 
			ModifyUser = aModifyUser;
		 
			ModifyDate = aModifyDate;
		}
		
		public CljcdInfo():base("TT_WMS_CLJCD")
		{
			List<string> keys = new List<string>();
			 			keys.Add("ID");                             _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField IDField = new DataSchemaField();
			IDField.Name = "ID";
			IDField.Type = typeof(long).ToString();
			IDField.Index = 0;
			fields.Add(IDField);
			 
			DataSchemaField JCDHField = new DataSchemaField();
			JCDHField.Name = "JCDH";
			JCDHField.Type = typeof(string).ToString();
			JCDHField.Index = 1;
			fields.Add(JCDHField);
			 
			DataSchemaField JCLBField = new DataSchemaField();
			JCLBField.Name = "JCLB";
			JCLBField.Type = typeof(int).ToString();
			JCLBField.Index = 2;
			fields.Add(JCLBField);
			 
			DataSchemaField JCSJField = new DataSchemaField();
			JCSJField.Name = "JCSJ";
			JCSJField.Type = typeof(DateTime).ToString();
			JCSJField.Index = 3;
			fields.Add(JCSJField);
			 
			DataSchemaField GYSDMField = new DataSchemaField();
			GYSDMField.Name = "GYSDM";
			GYSDMField.Type = typeof(string).ToString();
			GYSDMField.Index = 4;
			fields.Add(GYSDMField);
			 
			DataSchemaField GYSMCField = new DataSchemaField();
			GYSMCField.Name = "GYSMC";
			GYSMCField.Type = typeof(string).ToString();
			GYSMCField.Index = 5;
			fields.Add(GYSMCField);
			 
			DataSchemaField ZSLField = new DataSchemaField();
			ZSLField.Name = "ZSL";
			ZSLField.Type = typeof(decimal).ToString();
			ZSLField.Index = 6;
			fields.Add(ZSLField);
			 
			DataSchemaField ZMZField = new DataSchemaField();
			ZMZField.Name = "ZMZ";
			ZMZField.Type = typeof(decimal).ToString();
			ZMZField.Index = 7;
			fields.Add(ZMZField);
			 
			DataSchemaField ZTJField = new DataSchemaField();
			ZTJField.Name = "ZTJ";
			ZTJField.Type = typeof(decimal).ToString();
			ZTJField.Index = 8;
			fields.Add(ZTJField);
			 
			DataSchemaField ZJEField = new DataSchemaField();
			ZJEField.Name = "ZJE";
			ZJEField.Type = typeof(decimal).ToString();
			ZJEField.Index = 9;
			fields.Add(ZJEField);
			 
			DataSchemaField ZTField = new DataSchemaField();
			ZTField.Name = "ZT";
			ZTField.Type = typeof(int).ToString();
			ZTField.Index = 10;
			fields.Add(ZTField);
			 
			DataSchemaField BMField = new DataSchemaField();
			BMField.Name = "BM";
			BMField.Type = typeof(string).ToString();
			BMField.Index = 11;
			fields.Add(BMField);
			 
			DataSchemaField ZDWCBJField = new DataSchemaField();
			ZDWCBJField.Name = "ZDWCBJ";
			ZDWCBJField.Type = typeof(bool).ToString();
			ZDWCBJField.Index = 12;
			fields.Add(ZDWCBJField);
			 
			DataSchemaField ZDWCSJField = new DataSchemaField();
			ZDWCSJField.Name = "ZDWCSJ";
			ZDWCSJField.Type = typeof(DateTime).ToString();
			ZDWCSJField.Index = 13;
			fields.Add(ZDWCSJField);
			 
			DataSchemaField ZDWCRField = new DataSchemaField();
			ZDWCRField.Name = "ZDWCR";
			ZDWCRField.Type = typeof(string).ToString();
			ZDWCRField.Index = 14;
			fields.Add(ZDWCRField);
			 
			DataSchemaField ZGSHBJField = new DataSchemaField();
			ZGSHBJField.Name = "ZGSHBJ";
			ZGSHBJField.Type = typeof(bool).ToString();
			ZGSHBJField.Index = 15;
			fields.Add(ZGSHBJField);
			 
			DataSchemaField ZGSHSJField = new DataSchemaField();
			ZGSHSJField.Name = "ZGSHSJ";
			ZGSHSJField.Type = typeof(DateTime).ToString();
			ZGSHSJField.Index = 16;
			fields.Add(ZGSHSJField);
			 
			DataSchemaField ZGField = new DataSchemaField();
			ZGField.Name = "ZG";
			ZGField.Type = typeof(string).ToString();
			ZGField.Index = 17;
			fields.Add(ZGField);
			 
			DataSchemaField CWSHBJField = new DataSchemaField();
			CWSHBJField.Name = "CWSHBJ";
			CWSHBJField.Type = typeof(bool).ToString();
			CWSHBJField.Index = 18;
			fields.Add(CWSHBJField);
			 
			DataSchemaField CWSHSJField = new DataSchemaField();
			CWSHSJField.Name = "CWSHSJ";
			CWSHSJField.Type = typeof(DateTime).ToString();
			CWSHSJField.Index = 19;
			fields.Add(CWSHSJField);
			 
			DataSchemaField CWField = new DataSchemaField();
			CWField.Name = "CW";
			CWField.Type = typeof(string).ToString();
			CWField.Index = 20;
			fields.Add(CWField);
			 
			DataSchemaField HSXMField = new DataSchemaField();
			HSXMField.Name = "HSXM";
			HSXMField.Type = typeof(int).ToString();
			HSXMField.Index = 21;
			fields.Add(HSXMField);
			 
			DataSchemaField CK_CODEField = new DataSchemaField();
			CK_CODEField.Name = "CK_CODE";
			CK_CODEField.Type = typeof(string).ToString();
			CK_CODEField.Index = 22;
			fields.Add(CK_CODEField);
			 
			DataSchemaField CK_NAMEField = new DataSchemaField();
			CK_NAMEField.Name = "CK_NAME";
			CK_NAMEField.Type = typeof(string).ToString();
			CK_NAMEField.Index = 23;
			fields.Add(CK_NAMEField);
			 
			DataSchemaField BZField = new DataSchemaField();
			BZField.Name = "BZ";
			BZField.Type = typeof(string).ToString();
			BZField.Index = 24;
			fields.Add(BZField);
			 
			DataSchemaField VALID_FLAGField = new DataSchemaField();
			VALID_FLAGField.Name = "VALID_FLAG";
			VALID_FLAGField.Type = typeof(bool).ToString();
			VALID_FLAGField.Index = 25;
			fields.Add(VALID_FLAGField);
			 
			DataSchemaField CREATE_USERField = new DataSchemaField();
			CREATE_USERField.Name = "CREATE_USER";
			CREATE_USERField.Type = typeof(string).ToString();
			CREATE_USERField.Index = 26;
			fields.Add(CREATE_USERField);
			 
			DataSchemaField CREATE_DATEField = new DataSchemaField();
			CREATE_DATEField.Name = "CREATE_DATE";
			CREATE_DATEField.Type = typeof(DateTime).ToString();
			CREATE_DATEField.Index = 27;
			fields.Add(CREATE_DATEField);
			 
			DataSchemaField MODIFY_USERField = new DataSchemaField();
			MODIFY_USERField.Name = "MODIFY_USER";
			MODIFY_USERField.Type = typeof(string).ToString();
			MODIFY_USERField.Index = 28;
			fields.Add(MODIFY_USERField);
			 
			DataSchemaField MODIFY_DATEField = new DataSchemaField();
			MODIFY_DATEField.Name = "MODIFY_DATE";
			MODIFY_DATEField.Type = typeof(DateTime).ToString();
			MODIFY_DATEField.Index = 29;
			fields.Add(MODIFY_DATEField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public long Id{ get;set; }		
				
		[DataMember]
		public string Jcdh{ get;set; }		
				
		[DataMember]
		public int? Jclb{ get;set; }		
				
		[DataMember]
		public DateTime? Jcsj{ get;set; }		
				
		[DataMember]
		public string Gysdm{ get;set; }		
				
		[DataMember]
		public string Gysmc{ get;set; }		
				
		[DataMember]
		public decimal? Zsl{ get;set; }		
				
		[DataMember]
		public decimal? Zmz{ get;set; }		
				
		[DataMember]
		public decimal? Ztj{ get;set; }		
				
		[DataMember]
		public decimal? Zje{ get;set; }		
				
		[DataMember]
		public int? Zt{ get;set; }		
				
		[DataMember]
		public string Bm{ get;set; }		
				
		[DataMember]
		public bool? Zdwcbj{ get;set; }		
				
		[DataMember]
		public DateTime? Zdwcsj{ get;set; }		
				
		[DataMember]
		public string Zdwcr{ get;set; }		
				
		[DataMember]
		public bool? Zgshbj{ get;set; }		
				
		[DataMember]
		public DateTime? Zgshsj{ get;set; }		
				
		[DataMember]
		public string Zg{ get;set; }		
				
		[DataMember]
		public bool? Cwshbj{ get;set; }		
				
		[DataMember]
		public DateTime? Cwshsj{ get;set; }		
				
		[DataMember]
		public string Cw{ get;set; }		
				
		[DataMember]
		public int? Hsxm{ get;set; }		
				
		[DataMember]
		public string CkCode{ get;set; }		
				
		[DataMember]
		public string CkName{ get;set; }		
				
		[DataMember]
		public string Bz{ get;set; }		
				
		[DataMember]
		public bool? ValidFlag{ get;set; }		
				
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
			CljcdInfo info = new CljcdInfo();

			info.Id = this.Id;
			info.Jcdh = this.Jcdh;
			info.Jclb = this.Jclb;
			info.Jcsj = this.Jcsj;
			info.Gysdm = this.Gysdm;
			info.Gysmc = this.Gysmc;
			info.Zsl = this.Zsl;
			info.Zmz = this.Zmz;
			info.Ztj = this.Ztj;
			info.Zje = this.Zje;
			info.Zt = this.Zt;
			info.Bm = this.Bm;
			info.Zdwcbj = this.Zdwcbj;
			info.Zdwcsj = this.Zdwcsj;
			info.Zdwcr = this.Zdwcr;
			info.Zgshbj = this.Zgshbj;
			info.Zgshsj = this.Zgshsj;
			info.Zg = this.Zg;
			info.Cwshbj = this.Cwshbj;
			info.Cwshsj = this.Cwshsj;
			info.Cw = this.Cw;
			info.Hsxm = this.Hsxm;
			info.CkCode = this.CkCode;
			info.CkName = this.CkName;
			info.Bz = this.Bz;
			info.ValidFlag = this.ValidFlag;
			info.CreateUser = this.CreateUser;
			info.CreateDate = this.CreateDate;
			info.ModifyUser = this.ModifyUser;
			info.ModifyDate = this.ModifyDate;
			return info;			
		}
		 
		public CljcdInfo Clone()
		{
			return ((ICloneable) this).Clone() as CljcdInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// CljcdInfoCollection对应表[TT_WMS_CLJCD]
    /// </summary>
	public partial class CljcdInfoCollection : BusinessObjectCollection<CljcdInfo>
	{
		public CljcdInfoCollection():base("TT_WMS_CLJCD"){}	
	}
}
