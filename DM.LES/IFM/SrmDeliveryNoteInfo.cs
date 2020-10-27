using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public partial class SrmDeliveryNoteInfo
    {
        private List<SrmDeliveryNoteDetailInfo> detailInfos;
        /// <summary>
        /// 
        /// </summary>


        public List<SrmDeliveryNoteDetailInfo> DetailInfos;

        public List<SrmDeliveryNoteDetailInfo> DetailInfos1 { get => detailInfos; set => detailInfos = value; }

        ///public List<SrmDeliveryNoteDetailInfo> DetailInfos { get => detailInfos; set => detailInfos = value; }
    }
}
