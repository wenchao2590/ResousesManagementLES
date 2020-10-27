#region Declaim
//---------------------------------------------------------------------------
// Name:		BasInfomationanderrorsInfo
// Function: 	Expose data in table [TS_BAS_InfomationAndErrors] from database as business object to LOB system.
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
    /// BasInfomationanderrorsInfo��Ӧ��[TS_BAS_InfomationAndErrors]
    /// </summary>
	public partial class InfomationanderrorsInfo : BusinessObject, ICloneable
	{		
		#region Constructors
		
		public InfomationanderrorsInfo(
int errorid,
DateTime? timestamp,
string application,
string functionname,
string class1,
string informationorerror,
string exceptionmessage,
string errorcode
		) : this()
		{
		   	ErrorId=errorid;
		   	TimeStamp=timestamp;
		   	Application=application;
		   	FunctionName=functionname;
		   	Class=class1;
		   	InformationOrError=informationorerror;
		   	ExceptionMessage=exceptionmessage;
		   	ErrorCode=errorcode;
		}
		
		public InfomationanderrorsInfo():base("TS_BAS_InfomationAndErrors")
		{
			List<string> keys = new List<string>();
			keys.Add("error_id");
			_Keys = keys.ToArray();
			
			Schema = new DataSchema();
			List<DataSchemaField> fields = new List<DataSchemaField>();
				
			DataSchemaField erroridField = new DataSchemaField();
			erroridField.Name = "error_id";
			erroridField.Type = typeof(int).ToString();
			erroridField.Index = 0;
			fields.Add(erroridField);
			DataSchemaField timestampField = new DataSchemaField();
			timestampField.Name = "time_stamp";
			timestampField.Type = typeof(DateTime?).ToString();
			timestampField.Index = 1;
			fields.Add(timestampField);
			DataSchemaField applicationField = new DataSchemaField();
			applicationField.Name = "application";
			applicationField.Type = typeof(string).ToString();
			applicationField.Index = 2;
			fields.Add(applicationField);
			DataSchemaField functionnameField = new DataSchemaField();
			functionnameField.Name = "function_name";
			functionnameField.Type = typeof(string).ToString();
			functionnameField.Index = 3;
			fields.Add(functionnameField);
			DataSchemaField classField = new DataSchemaField();
			classField.Name = "class";
			classField.Type = typeof(string).ToString();
			classField.Index = 4;
			fields.Add(classField);
			DataSchemaField informationorerrorField = new DataSchemaField();
			informationorerrorField.Name = "information_or_error";
			informationorerrorField.Type = typeof(string).ToString();
			informationorerrorField.Index = 5;
			fields.Add(informationorerrorField);
			DataSchemaField exceptionmessageField = new DataSchemaField();
			exceptionmessageField.Name = "exception_message";
			exceptionmessageField.Type = typeof(string).ToString();
			exceptionmessageField.Index = 6;
			fields.Add(exceptionmessageField);
			DataSchemaField errorcodeField = new DataSchemaField();
			errorcodeField.Name = "error_code";
			errorcodeField.Type = typeof(string).ToString();
			errorcodeField.Index = 7;
			fields.Add(errorcodeField);
			
			Schema.Fields = fields.ToArray();
		}
		
		#endregion
		
		#region Properties
		
		public int ErrorId
		{
			get 
			{
				try
				{
					return (int)this["error_id"].Value;
				}
				catch
				{
					return default(int);
				}
			}
			set {this["error_id"].Value = value;}
		}
		
		public DateTime? TimeStamp
		{
			get 
			{
				try
				{
					return (DateTime?)this["time_stamp"].Value;
				}
				catch
				{
					return default(DateTime?);
				}
			}
			set {this["time_stamp"].Value = value;}
		}
		
		public string Application
		{
			get 
			{
				try
				{
					return (string)this["application"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set {this["application"].Value = value;}
		}
		
		public string FunctionName
		{
			get 
			{
				try
				{
					return (string)this["function_name"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set {this["function_name"].Value = value;}
		}
		
		public string Class
		{
			get 
			{
				try
				{
					return (string)this["class"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set {this["class"].Value = value;}
		}
		
		public string InformationOrError
		{
			get 
			{
				try
				{
					return (string)this["information_or_error"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set {this["information_or_error"].Value = value;}
		}
		
		public string ExceptionMessage
		{
			get 
			{
				try
				{
					return (string)this["exception_message"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set {this["exception_message"].Value = value;}
		}
		
		public string ErrorCode
		{
			get 
			{
				try
				{
					return (string)this["error_code"].Value;
				}
				catch
				{
					return default(string);
				}
			}
			set {this["error_code"].Value = value;}
		}
		
		#endregion		
		
		#region ICloneable Members

		object ICloneable.Clone()
		{
			InfomationanderrorsInfo entity = new InfomationanderrorsInfo();
			entity["error_id"].Value = this["error_id"].Value;
			entity["time_stamp"].Value = this["time_stamp"].Value;
			entity["application"].Value = this["application"].Value;
			entity["function_name"].Value = this["function_name"].Value;
			entity["class"].Value = this["class"].Value;
			entity["information_or_error"].Value = this["information_or_error"].Value;
			entity["exception_message"].Value = this["exception_message"].Value;
			entity["error_code"].Value = this["error_code"].Value;
			return entity;
		}

		public InfomationanderrorsInfo Clone()
		{
			return ((ICloneable)this).Clone() as InfomationanderrorsInfo;
		}

		#endregion

		public override string ToString()
		{
			return base.ToString();
		}
	}
}