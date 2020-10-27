using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public class GuidValueDatasourceInfo
    {
        private Guid guidValue;
        private string stringDisplay;
        /// <summary>
        /// Value
        /// </summary>
        public Guid GuidValue
        {
            get
            {
                return guidValue;
            }

            set
            {
                guidValue = value;
            }
        }
        /// <summary>
        /// Display
        /// </summary>
        public string StringDisplay
        {
            get
            {
                return stringDisplay;
            }

            set
            {
                stringDisplay = value;
            }
        }
    }
}
