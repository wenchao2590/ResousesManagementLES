#region Declaim
//---------------------------------------------------------------------------
// Name:		PartsBoxInfo
// Function: 	Expose data in table PartsBox from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年4月2日
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
    /// PartsBoxInfo对应表[V_ALL_PARTS_BOX]
    /// </summary>	
	[Serializable] 
	[DataContract]	
    public partial class PartsBoxInfo : BusinessObject, ICloneable 
	{		  
		#region Constructors
		
		public PartsBoxInfo( 
					int aPullMode,

					string aBoxParts,

					string aBoxPartsName,

					string aSupplierNum,

					string aAssemblyLine,

					string aTWmNo,

					string aTZoneNo,

					string aSWmNo,

					string aSZoneNo,

					string aPlant,

					string aWorkshop

				 
		) : this()
		{
			 
			PullMode = aPullMode;
		 
			BoxParts = aBoxParts;
		 
			BoxPartsName = aBoxPartsName;
		 
			SupplierNum = aSupplierNum;
		 
			AssemblyLine = aAssemblyLine;
		 
			TWmNo = aTWmNo;
		 
			TZoneNo = aTZoneNo;
		 
			SWmNo = aSWmNo;
		 
			SZoneNo = aSZoneNo;
		 
			Plant = aPlant;
		 
			Workshop = aWorkshop;
		}
		
		public PartsBoxInfo():base("V_ALL_PARTS_BOX")
		{
			List<string> keys = new List<string>();
			           _Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
			
			 
			DataSchemaField PULL_MODEField = new DataSchemaField();
			PULL_MODEField.Name = "PULL_MODE";
			PULL_MODEField.Type = typeof(int).ToString();
			PULL_MODEField.Index = 0;
			fields.Add(PULL_MODEField);
			 
			DataSchemaField BOX_PARTSField = new DataSchemaField();
			BOX_PARTSField.Name = "BOX_PARTS";
			BOX_PARTSField.Type = typeof(string).ToString();
			BOX_PARTSField.Index = 1;
			fields.Add(BOX_PARTSField);
			 
			DataSchemaField BOX_PARTS_NAMEField = new DataSchemaField();
			BOX_PARTS_NAMEField.Name = "BOX_PARTS_NAME";
			BOX_PARTS_NAMEField.Type = typeof(string).ToString();
			BOX_PARTS_NAMEField.Index = 2;
			fields.Add(BOX_PARTS_NAMEField);
			 
			DataSchemaField SUPPLIER_NUMField = new DataSchemaField();
			SUPPLIER_NUMField.Name = "SUPPLIER_NUM";
			SUPPLIER_NUMField.Type = typeof(string).ToString();
			SUPPLIER_NUMField.Index = 3;
			fields.Add(SUPPLIER_NUMField);
			 
			DataSchemaField ASSEMBLY_LINEField = new DataSchemaField();
			ASSEMBLY_LINEField.Name = "ASSEMBLY_LINE";
			ASSEMBLY_LINEField.Type = typeof(string).ToString();
			ASSEMBLY_LINEField.Index = 4;
			fields.Add(ASSEMBLY_LINEField);
			 
			DataSchemaField T_WM_NOField = new DataSchemaField();
			T_WM_NOField.Name = "T_WM_NO";
			T_WM_NOField.Type = typeof(string).ToString();
			T_WM_NOField.Index = 5;
			fields.Add(T_WM_NOField);
			 
			DataSchemaField T_ZONE_NOField = new DataSchemaField();
			T_ZONE_NOField.Name = "T_ZONE_NO";
			T_ZONE_NOField.Type = typeof(string).ToString();
			T_ZONE_NOField.Index = 6;
			fields.Add(T_ZONE_NOField);
			 
			DataSchemaField S_WM_NOField = new DataSchemaField();
			S_WM_NOField.Name = "S_WM_NO";
			S_WM_NOField.Type = typeof(string).ToString();
			S_WM_NOField.Index = 7;
			fields.Add(S_WM_NOField);
			 
			DataSchemaField S_ZONE_NOField = new DataSchemaField();
			S_ZONE_NOField.Name = "S_ZONE_NO";
			S_ZONE_NOField.Type = typeof(string).ToString();
			S_ZONE_NOField.Index = 8;
			fields.Add(S_ZONE_NOField);
			 
			DataSchemaField PLANTField = new DataSchemaField();
			PLANTField.Name = "PLANT";
			PLANTField.Type = typeof(string).ToString();
			PLANTField.Index = 9;
			fields.Add(PLANTField);
			 
			DataSchemaField WORKSHOPField = new DataSchemaField();
			WORKSHOPField.Name = "WORKSHOP";
			WORKSHOPField.Type = typeof(string).ToString();
			WORKSHOPField.Index = 10;
			fields.Add(WORKSHOPField);
						
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Attributes

		[DataMember]
		public int PullMode{ get;set; }		
				
		[DataMember]
		public string BoxParts{ get;set; }		
				
		[DataMember]
		public string BoxPartsName{ get;set; }		
				
		[DataMember]
		public string SupplierNum{ get;set; }		
				
		[DataMember]
		public string AssemblyLine{ get;set; }		
				
		[DataMember]
		public string TWmNo{ get;set; }		
				
		[DataMember]
		public string TZoneNo{ get;set; }		
				
		[DataMember]
		public string SWmNo{ get;set; }		
				
		[DataMember]
		public string SZoneNo{ get;set; }		
				
		[DataMember]
		public string Plant{ get;set; }		
				
		[DataMember]
		public string Workshop{ get;set; }		
				
		#endregion

	 
		#region ICloneable Members

		object ICloneable.Clone()
		{
			PartsBoxInfo info = new PartsBoxInfo();

			info.PullMode = this.PullMode;
			info.BoxParts = this.BoxParts;
			info.BoxPartsName = this.BoxPartsName;
			info.SupplierNum = this.SupplierNum;
			info.AssemblyLine = this.AssemblyLine;
			info.TWmNo = this.TWmNo;
			info.TZoneNo = this.TZoneNo;
			info.SWmNo = this.SWmNo;
			info.SZoneNo = this.SZoneNo;
			info.Plant = this.Plant;
			info.Workshop = this.Workshop;
			return info;			
		}
		 
		public PartsBoxInfo Clone()
		{
			return ((ICloneable) this).Clone() as PartsBoxInfo;	
		}
		#endregion

		public override string ToString()
		{
			return base.ToString();
		} 
	}

	/// <summary>
    /// PartsBoxInfoCollection对应表[V_ALL_PARTS_BOX]
    /// </summary>
	public partial class PartsBoxInfoCollection : BusinessObjectCollection<PartsBoxInfo>
	{
		public PartsBoxInfoCollection():base("V_ALL_PARTS_BOX"){}	
	}
}
