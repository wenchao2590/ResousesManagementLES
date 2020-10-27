using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public class CodeItemDatasourceInfo
    {
        private int itemValue;
        private string itemDisplay;
        private string itemDisplayEn;

        public int ItemValue
        {
            get
            {
                return itemValue;
            }

            set
            {
                itemValue = value;
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

        public string ItemDisplayEn
        {
            get
            {
                return itemDisplayEn;
            }

            set
            {
                itemDisplayEn = value;
            }
        }
        
    }
}
