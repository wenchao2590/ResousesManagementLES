namespace Infrustructure.Event
{
    /// <summary>
    /// �¼���������: �������, 0-99Ԥ��
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// MES����
        /// </summary>
        MES = 10,    
        /// <summary>
        /// TWD����
        /// </summary>
        TWD = 100,
        /// <summary>
        /// JIS����
        /// </summary>
        JIS = 200,
        /// <summary>
        /// PCS����
        /// </summary>
        PCS = 300,
        /// <summary>
        /// EPS����
        /// </summary>
        EPS = 400,
        /// <summary>
        /// SPS����
        /// </summary>
        SPS = 500,
        /// <summary>
        /// ���㱨��
        /// </summary>
        Settle = 600,    
   
        /// <summary>
        /// ��Ӧ���ն�
        /// </summary>
        Termal = 1000,

        /// <summary>
        /// ҵ������,��PBOM��
        /// </summary>
        Business = 2000,

        /// <summary>
        /// ϵͳ����
        /// </summary>
        System = 3000,

        /// <summary>
        /// ɾ��Ԥ��
        /// </summary>
        Delete = 9000,

        /// <summary>
        /// ��������Ԥ��
        /// </summary>
        CheckSequence = 9001,
        /// <summary>
        /// δ����
        /// </summary>
        Others = 10000,
    }
}
