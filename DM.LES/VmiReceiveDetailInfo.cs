using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public partial class VmiReceiveDetailInfo
    {
        private DateTime tranTime;
        private string costCenter;
        private string bookKeeper;
        private int receiveType;
        private Guid organizationFid;
        private string contractNo;
        private string partUnits;
        private string partGroup;
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime TranTime { get => tranTime; set => tranTime = value; }
        /// <summary>
        /// 核算项目
        /// </summary>
        public string CostCenter { get => costCenter; set => costCenter = value; }
        public string BookKeeper { get => bookKeeper; set => bookKeeper = value; }
        public int ReceiveType { get => receiveType; set => receiveType = value; }
        public Guid OrganizationFid { get => organizationFid; set => organizationFid = value; }
        public string ContractNo { get => contractNo; set => contractNo = value; }
        public string PartUnits { get => partUnits; set => partUnits = value; }
        public string PartGroup { get => partGroup; set => partGroup = value; }
    }
}
