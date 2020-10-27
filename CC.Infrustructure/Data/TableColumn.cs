using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Infrustructure.Data
{
    public class TableColumn
    {
        public int Id { get; set; }
        public string DatabaseSchema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnCname { get; set; }
        public int Order { get; set; }
        public int Type { get; set; }
        public int MaxLength { get; set; }
        public int? DesiredLength { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
    }
}
   
