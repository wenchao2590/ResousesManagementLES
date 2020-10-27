using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public class StringValueDatasourceInfo
    {
        private string stringValue;
        private string itemDisplay;

        public string StringValue
        {
            get
            {
                return stringValue;
            }

            set
            {
                stringValue = value;
            }
        }

        public string ItemDisplay
        {
            get
            {
                return itemDisplay;
            }

            set
            {
                itemDisplay = value;
            }
        }
    }
}
