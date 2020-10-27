using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public partial class ReceiveInfo
    {
        private string receiveTypeName;
        private string inspectionModeName;
        private string organizationName;
        private string wmName;
        private string sourceWmName;
        private string zoneName;
        private string sourceZoneName;

        private int inoutFlag;

        public string ReceiveTypeName { get => receiveTypeName; set => receiveTypeName = value; }
        public string InspectionModeName { get => inspectionModeName; set => inspectionModeName = value; }
        public string OrganizationName { get => organizationName; set => organizationName = value; }
        /// <summary>
        /// 入库单则1
        /// 出库单则0
        /// </summary>
        public int InoutFlag { get => inoutFlag; set => inoutFlag = value; }
        public string WmName { get => wmName; set => wmName = value; }
        public string SourceWmName { get => sourceWmName; set => sourceWmName = value; }
        public string ZoneName { get => zoneName; set => zoneName = value; }
        public string SourceZoneName { get => sourceZoneName; set => sourceZoneName = value; }
    }
}
