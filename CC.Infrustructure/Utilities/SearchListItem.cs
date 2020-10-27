#region File Comment
//+-------------------------------------------------------------------+
//+ Name: 	   查询栏的下拉绑定对象
//+ Function:  
//+ Author:    薛海军
//+ Date:      20060702       
//+-------------------------------------------------------------------+
//+ Change History:
//+ Date            Who       		Chages Made        Comments
//+-------------------------------------------------------------------+
//+ 20060702         CodeGenerator        Init Created
//+-------------------------------------------------------------------+
//+-------------------------------------------------------------------+
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Infrustructure.Utilities
{
    [Serializable]
    [DataContract]
	public class SearchListItem
    {
        public SearchListItem(string itemText, string itemValue, string parentRelationField)
        {
            this.itemText = itemText;
            this.itemValue = itemValue;
            this.parentRelationField = parentRelationField;
        }

        public SearchListItem()
        { }

        private string itemText;
        [DataMember]
        public string ItemText
        {
            get
            {
                return itemText;
            }
            set
            {
                itemText = value;
            }
        }

        private string itemValue;
        [DataMember]
        public string ItemValue
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

        private string parentRelationField;
        [DataMember]
        public string ParentRelationField
        {
            get
            {
                return parentRelationField;
            }
            set
            {
                parentRelationField = value;
            }
        }

    }
}
