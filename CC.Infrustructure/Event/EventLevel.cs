namespace Infrustructure.Event
{
    /// <summary>
    /// 事件级别
    /// </summary>
    public enum EventLevel
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 1,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 信息
        /// </summary>
        Information = 3,
        /// <summary>
        /// 审核成功
        /// </summary>
        Success = 4,
        /// <summary>
        /// 审核失败
        /// </summary>
        Failure = 5,
    }

}
