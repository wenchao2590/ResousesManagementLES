using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.BaseClass
{
    public enum PlantSystemMode
    {
        /// <summary>
        /// JIS拉动
        /// </summary>
        JIS = 0,

        /// <summary>
        /// PCS拉动
        /// </summary>
        PCS,

        /// <summary>
        /// SPS拉动
        /// </summary>
        SPS,

        /// <summary>
        /// EPS拉动
        /// </summary>
        EPS,

        /// <summary>
        /// TWD拉动
        /// </summary>
        TWD
    }
}
