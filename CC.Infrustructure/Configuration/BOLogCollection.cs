using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrustructure.Configuration
{
    public class BOLogCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BOLog();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BOLog)element).ClassName;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "BOLog";
            }
        }

        public BOLog this[int index]
        {
            get
            {
                return (BOLog)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        private Dictionary<string, BOLog> diclist;
        public BOLog GetBOLog(string type)
        {
            if (diclist == null)
            {
                diclist = new Dictionary<string, BOLog>();
                for (int i = 0; i < this.Count; i++)
                {
                    diclist.Add(this[i].Type, this[i]);
                }
            }
            if (diclist.Keys.Contains(type))
            {
                return diclist[type];
            }
            else
            {
                return null;
            }
        }
    }
}
