namespace Infrustructure.Event
{
    /// <summary>
    /// 事件报警类型: 按大类分, 0-99预留
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// MES报警
        /// </summary>
        MES = 10,    
        /// <summary>
        /// TWD报警
        /// </summary>
        TWD = 100,
        /// <summary>
        /// JIS报警
        /// </summary>
        JIS = 200,
        /// <summary>
        /// PCS报警
        /// </summary>
        PCS = 300,
        /// <summary>
        /// EPS报警
        /// </summary>
        EPS = 400,
        /// <summary>
        /// SPS报警
        /// </summary>
        SPS = 500,
        /// <summary>
        /// 结算报警
        /// </summary>
        Settle = 600,    
   
        /// <summary>
        /// 供应商终端
        /// </summary>
        Termal = 1000,

        /// <summary>
        /// 业务数据,如PBOM等
        /// </summary>
        Business = 2000,

        /// <summary>
        /// 系统操作
        /// </summary>
        System = 3000,

        /// <summary>
        /// 删除预警
        /// </summary>
        Delete = 9000,

        /// <summary>
        /// 排序不连续预警
        /// </summary>
        CheckSequence = 9001,
        /// <summary>
        /// 未归类
        /// </summary>
        Others = 10000,
    }
}
