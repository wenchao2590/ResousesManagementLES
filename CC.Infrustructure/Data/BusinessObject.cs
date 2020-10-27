using Infrustructure.Data.Integration;
using System;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace Infrustructure.Data
{
    [Serializable]
    [DataContract]
    public class BusinessObject : DataItem
    {
        //[XmlIgnore]
        //[IgnoreDataMember]
        //[JsonIgnore]
        [ScriptIgnore]
        public string _TableName;
        [ScriptIgnore]
        public string[] _Keys;
        public BusinessObject(string tablename) : base(null)
        {
            _TableName = tablename;
        }
    }
}
