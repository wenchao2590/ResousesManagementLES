#region Declaim
//---------------------------------------------------------------------------
// Name:		BasOperationlogInfo
// Function: 	Expose data in table [TS_BAS_OperationLog] from database as business object to LOB system.
// Date:    	2008��8��27��
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------
#endregion

#region Imported Namespace

using System;
using System.Collections.Generic;
using Infrustructure.Data;
using Infrustructure.Data.Integration;

#endregion

namespace Infrustructure.Logging
{
    /// <summary>
    /// BasOperationlogInfo��Ӧ��[TS_BAS_OperationLog]
    /// </summary>
	public partial class OperationlogInfo : BusinessObject, ICloneable
	{		
		#region Constructors
		
		public OperationlogInfo(
int operationlogsn,
DateTime operationtime,
int actionid,
string otor,
string operationdetail,
string operationparameter1,
string operationparameter2,
string operationparameter3,
string operationparameter4,
string operationparameter5,
string operationparameter6,
string operationparameter7,
string operationparameter8,
string operationparameter9,
string operationparameter10
		) : this()
		{
		   	OperationLogSn=operationlogsn;
		   	OperationTime=operationtime;
		   	ActionId=actionid;
		   	Operator=otor;
		   	OperationDetail=operationdetail;
		   	OperationParameter1=operationparameter1;
		   	OperationParameter2=operationparameter2;
		   	OperationParameter3=operationparameter3;
		   	OperationParameter4=operationparameter4;
		   	OperationParameter5=operationparameter5;
		   	OperationParameter6=operationparameter6;
		   	OperationParameter7=operationparameter7;
		   	OperationParameter8=operationparameter8;
		   	OperationParameter9=operationparameter9;
		   	OperationParameter10=operationparameter10;
		}
		
		public OperationlogInfo():base("TS_BAS_OperationLog")
		{
			List<string> keys = new List<string>();
			keys.Add("operation_log_sn");
			_Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
				
			DataSchemaField operationlogsnField = new DataSchemaField();
			operationlogsnField.Name = "operation_log_sn";
			operationlogsnField.Type = typeof(int).ToString();
			operationlogsnField.Index = 0;
			fields.Add(operationlogsnField);
			DataSchemaField operationtimeField = new DataSchemaField();
			operationtimeField.Name = "operation_time";
			operationtimeField.Type = typeof(DateTime).ToString();
			operationtimeField.Index = 1;
			fields.Add(operationtimeField);
			DataSchemaField actionidField = new DataSchemaField();
			actionidField.Name = "action_id";
			actionidField.Type = typeof(int).ToString();
			actionidField.Index = 2;
			fields.Add(actionidField);
			DataSchemaField operatorField = new DataSchemaField();
			operatorField.Name = "operator";
			operatorField.Type = typeof(string).ToString();
			operatorField.Index = 3;
			fields.Add(operatorField);
			DataSchemaField operationdetailField = new DataSchemaField();
			operationdetailField.Name = "operation_detail";
			operationdetailField.Type = typeof(string).ToString();
			operationdetailField.Index = 4;
			fields.Add(operationdetailField);
			DataSchemaField operationparameter1Field = new DataSchemaField();
			operationparameter1Field.Name = "operation_parameter1";
			operationparameter1Field.Type = typeof(string).ToString();
			operationparameter1Field.Index = 5;
			fields.Add(operationparameter1Field);
			DataSchemaField operationparameter2Field = new DataSchemaField();
			operationparameter2Field.Name = "operation_parameter2";
			operationparameter2Field.Type = typeof(string).ToString();
			operationparameter2Field.Index = 6;
			fields.Add(operationparameter2Field);
			DataSchemaField operationparameter3Field = new DataSchemaField();
			operationparameter3Field.Name = "operation_parameter3";
			operationparameter3Field.Type = typeof(string).ToString();
			operationparameter3Field.Index = 7;
			fields.Add(operationparameter3Field);
			DataSchemaField operationparameter4Field = new DataSchemaField();
			operationparameter4Field.Name = "operation_parameter4";
			operationparameter4Field.Type = typeof(string).ToString();
			operationparameter4Field.Index = 8;
			fields.Add(operationparameter4Field);
			DataSchemaField operationparameter5Field = new DataSchemaField();
			operationparameter5Field.Name = "operation_parameter5";
			operationparameter5Field.Type = typeof(string).ToString();
			operationparameter5Field.Index = 9;
			fields.Add(operationparameter5Field);
			DataSchemaField operationparameter6Field = new DataSchemaField();
			operationparameter6Field.Name = "operation_parameter6";
			operationparameter6Field.Type = typeof(string).ToString();
			operationparameter6Field.Index = 10;
			fields.Add(operationparameter6Field);
			DataSchemaField operationparameter7Field = new DataSchemaField();
			operationparameter7Field.Name = "operation_parameter7";
			operationparameter7Field.Type = typeof(string).ToString();
			operationparameter7Field.Index = 11;
			fields.Add(operationparameter7Field);
			DataSchemaField operationparameter8Field = new DataSchemaField();
			operationparameter8Field.Name = "operation_parameter8";
			operationparameter8Field.Type = typeof(string).ToString();
			operationparameter8Field.Index = 12;
			fields.Add(operationparameter8Field);
			DataSchemaField operationparameter9Field = new DataSchemaField();
			operationparameter9Field.Name = "operation_parameter9";
			operationparameter9Field.Type = typeof(string).ToString();
			operationparameter9Field.Index = 13;
			fields.Add(operationparameter9Field);
			DataSchemaField operationparameter10Field = new DataSchemaField();
			operationparameter10Field.Name = "operation_parameter10";
			operationparameter10Field.Type = typeof(string).ToString();
			operationparameter10Field.Index = 14;
			fields.Add(operationparameter10Field);
			
			Schema.Fields = fields.ToArray();
		}
		
		#endregion

		#region Properties

		public int OperationLogSn
		{
			get
			{
				try
				{
					return (int)this["operation_log_sn"].Value;
				}
				catch
				{
					return default(int);
				}
			}
			set { this["operation_log_sn"].Value = value; }
		}

		public DateTime OperationTime
		{
			get
			{
				try
				{
					return (DateTime)this["operation_time"].Value;
				}
				catch
				{
					return new DateTime(1900, 1, 1);
				}
			}
			set { this["operation_time"].Value = value; }
		}

		public int ActionId
		{
			get
			{
				try
				{
					return (int)this["action_id"].Value;
				}
				catch
				{
					return default(int);
				}
			}
			set { this["action_id"].Value = value; }
		}

		public string Operator
		{
			get
			{
				try
				{
					return (string)this["operator"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operator"].Value = value; }
		}

		public string OperationDetail
		{
			get
			{
				try
				{
					return (string)this["operation_detail"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_detail"].Value = value; }
		}

		public string OperationParameter1
		{
			get
			{
				try
				{
					return (string)this["operation_parameter1"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter1"].Value = value; }
		}

		public string OperationParameter2
		{
			get
			{
				try
				{
					return (string)this["operation_parameter2"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter2"].Value = value; }
		}

		public string OperationParameter3
		{
			get
			{
				try
				{
					return (string)this["operation_parameter3"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter3"].Value = value; }
		}

		public string OperationParameter4
		{
			get
			{
				try
				{
					return (string)this["operation_parameter4"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter4"].Value = value; }
		}

		public string OperationParameter5
		{
			get
			{
				try
				{
					return (string)this["operation_parameter5"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter5"].Value = value; }
		}

		public string OperationParameter6
		{
			get
			{
				try
				{
					return (string)this["operation_parameter6"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter6"].Value = value; }
		}

		public string OperationParameter7
		{
			get
			{
				try
				{
					return (string)this["operation_parameter7"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter7"].Value = value; }
		}

		public string OperationParameter8
		{
			get
			{
				try
				{
					return (string)this["operation_parameter8"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter8"].Value = value; }
		}

		public string OperationParameter9
		{
			get
			{
				try
				{
					return (string)this["operation_parameter9"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter9"].Value = value; }
		}

		public string OperationParameter10
		{
			get
			{
				try
				{
					return (string)this["operation_parameter10"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set { this["operation_parameter10"].Value = value; }
		}

		#endregion

		#region ICloneable Members

		object ICloneable.Clone()
		{
			OperationlogInfo entity = new OperationlogInfo();
			entity["operation_log_sn"].Value = this["operation_log_sn"].Value;
			entity["operation_time"].Value = this["operation_time"].Value;
			entity["action_id"].Value = this["action_id"].Value;
			entity["operator"].Value = this["operator"].Value;
			entity["operation_detail"].Value = this["operation_detail"].Value;
			entity["operation_parameter1"].Value = this["operation_parameter1"].Value;
			entity["operation_parameter2"].Value = this["operation_parameter2"].Value;
			entity["operation_parameter3"].Value = this["operation_parameter3"].Value;
			entity["operation_parameter4"].Value = this["operation_parameter4"].Value;
			entity["operation_parameter5"].Value = this["operation_parameter5"].Value;
			entity["operation_parameter6"].Value = this["operation_parameter6"].Value;
			entity["operation_parameter7"].Value = this["operation_parameter7"].Value;
			entity["operation_parameter8"].Value = this["operation_parameter8"].Value;
			entity["operation_parameter9"].Value = this["operation_parameter9"].Value;
			entity["operation_parameter10"].Value = this["operation_parameter10"].Value;
			return entity;
		}

		public OperationlogInfo Clone()
		{
			return ((ICloneable)this).Clone() as OperationlogInfo;
		}

		#endregion

		public override string ToString()
		{
			return base.ToString();
		}
	}
}