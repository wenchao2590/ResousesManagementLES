namespace Infrustructure.Event
{
    /// <summary>
    /// 事件报警处理状态
    /// </summary>
    public enum EventState
    {
        /// <summary>
        /// 0 未处理
        /// </summary>
        NotProcess = 0,
        /// <summary>
        /// 1 已处理
        /// </summary>
        Processed = 1,
        /// <summary>
        /// 2 已接收
        /// </summary>
        Accepted = 2,
        /// <summary>
        /// 3 已查阅
        /// </summary>
        Viewed = 3,
    }
}
