using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public enum EntityType
    {
        [Description("Grid")]
        Grid = 1,
        [Description("Tree")]
        Tree = 2,
        [Description("Detail")]
        Detail = 3,
    }
}
