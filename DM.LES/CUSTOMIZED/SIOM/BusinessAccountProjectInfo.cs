using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public partial class BusinessAccountProjectInfo
    {
        public string ItemNameEn { get; set; }
        public string ItemName { get; set; }
        public string Comments { get; set; }
        public bool ValidFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public long Id { get; set; }
    }
}
