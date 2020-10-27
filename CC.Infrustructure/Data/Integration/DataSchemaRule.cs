using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Collections;
using Infrustructure.Logging;

namespace Infrustructure.Data.Integration
{
    [Serializable]
    public class DataSchemaRule
    {
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// something like this:
		/// 1. Param1="field[plant]" means data from schemafield of named 'plant'
		/// 2. Param2="const[sy]" means data from const value 'sy'
		/// 3. Param3="param[1]" means data from parameters in code which index is '1'
		/// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")]
        [XmlAnyAttribute]
        public XmlAttribute[] Parameters;

        [XmlAttribute("type")]
        public string Type;

        [XmlAttribute("commandtext")]
        public string CommandText;

        [XmlAttribute("name")]
        public string Name;

		[XmlAttribute("message")]
		public string Message;

        [XmlAttribute("index")]
        public int Index;

        [XmlIgnore]
		public IRuleCommand RuleCommand;

        public IRuleCommand BuildCommand(DataItem item, IDictionary values)
		{
			// TODO: currently only support sql. by alex@20080820
            RuleCommand = new SqlRuleCommand(Name, CommandText, "", Message);

            // build parameters
            foreach (XmlAttribute p in Parameters)
            {
                if (string.IsNullOrEmpty(p.Value) ||
                    (!(p.Value.StartsWith("field[") && p.Value.EndsWith("]")) &&
                    !(p.Value.StartsWith("const[") && p.Value.EndsWith("]")) &&
                    !(p.Value.StartsWith("param[") && p.Value.EndsWith("]"))))
                    continue;

                RuleCommandParameter param = new RuleCommandParameter();
                param.Name = p.Name;
                param.Text = p.Value;
                // TODO: refactoring & handling the type of 'param[...]'
                if(p.Value.StartsWith("field["))
                {
                    // value from dataitem
					string fieldname = p.Value.Substring(6, p.Value.Length - 7);
                    if (fieldname.Length == 0)
                    {
                        // should we throw exception here?
                        Logger.Instance.Info(this, "û�ж�����Ч��field");
                        return null;
                        //continue;
                    }
                    if (item[fieldname] == null)
                    {
                        // should we throw exception here?
                        Logger.Instance.Info(this, string.Format("��fieldname'{0}'û�ж�Ӧ��field", fieldname));
                        return null;
                        //continue;
                    }
                    param.Value = item[fieldname].Value;
                    RuleCommand.Parameters.Add(param);
                }
                else if (p.Value.StartsWith("const["))
                {
                    // value from const value
					string constvalue = p.Value.Substring(6, p.Value.Length - 7);
                    if (constvalue.Length == 0)
                    {
                        // should we throw exception here?
                        Logger.Instance.Info(this, "û�ж�����Ч��const");
                        return null;
                        //continue;
                    }
					param.Value = constvalue;
                    RuleCommand.Parameters.Add(param);
                }
                else if(p.Value.StartsWith("param["))
                {
                    // value form param
                    string paramname = p.Value.Substring(6, p.Value.Length - 7);
                    if (values == null)
                    {
                        // should we throw exception here?
                        Logger.Instance.Info(this, "û�л����Ч�Ĳ�������");
                        return null;
                        //continue;
                    }
                    if (string.IsNullOrEmpty(paramname))
                    {
                        // should we throw exception here?
                        Logger.Instance.Info(this, "û�ж�����Ч�Ĳ�������");
                        return null;
                        //continue;
                    }
                    object v = values[paramname];
                    if (v == null)
                    {
                        // should we throw exception here?
                        Logger.Instance.Info(this, string.Format("û�л�ò���[{0}]��Ӧ��ֵ", paramname));
                        return null;
                        //continue;
                    }
                    param.Value = v;
                    RuleCommand.Parameters.Add(param);
                }
            }
            return RuleCommand;
		}

		public bool ExecuteCommand(ValidationResults validationResults, DataItem item, IDictionary paramvalues)
		{
            IRuleCommand cmd = BuildCommand(item, paramvalues);

            if (cmd != null)
                return cmd.Execute(validationResults);
            else
            { 
                validationResults.AddResult(new ValidationResult( "�����Զ�����֤����ʧ��", ""));
                return false; 
            }
		}
    }
}
