using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public class CommonField
    {
        private string fieldName;
        private string propName;
        private string dataType;
        private string fieldValue;

        public string FieldName
        {
            get
            {
                return fieldName;
            }

            set
            {
                fieldName = value;
            }
        }

        public string PropName
        {
            get
            {
                return propName;
            }

            set
            {
                propName = value;
            }
        }

        public string DataType
        {
            get
            {
                return dataType;
            }

            set
            {
                dataType = value;
            }
        }

        public string FieldValue
        {
            get
            {
                return fieldValue;
            }

            set
            {
                fieldValue = value;
            }
        }
    }
}
