using System.ComponentModel;

namespace Infrustructure.Data
{
    public enum WorkOrderAssemblyScanType
    {
        [Description("根据BarcodePartNo表匹配")]
        ByBarcodePartNo = 0,

        [Description("根据Assembly中的Rule字段匹配")]
        ByRule = 1,

        [Description("根据WorkOrderPart表匹配")]
        ByWorkOrderPart = 2,

        [Description("根据WorkOrder表中的PartNo匹配")]
        ByWorkOrderPartNo = 3,

        [Description("根据Assembly中的Rule字段匹配，还需校验唯一性")]
        ByRuleUniqueness = 4,

        [Description("不扫描")]
        NoScan = 5
    }
}