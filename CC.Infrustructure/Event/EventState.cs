namespace Infrustructure.Event
{
    /// <summary>
    /// �¼���������״̬
    /// </summary>
    public enum EventState
    {
        /// <summary>
        /// 0 δ����
        /// </summary>
        NotProcess = 0,
        /// <summary>
        /// 1 �Ѵ���
        /// </summary>
        Processed = 1,
        /// <summary>
        /// 2 �ѽ���
        /// </summary>
        Accepted = 2,
        /// <summary>
        /// 3 �Ѳ���
        /// </summary>
        Viewed = 3,
    }
}
