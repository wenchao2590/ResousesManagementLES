namespace DM.LES
{
    public partial class TwdCounterUpdateInfo : TwdCounterInfo
    {
        private decimal? submitQty;
        /// <summary>
        /// 累加数量
        /// </summary>
        public decimal? SubmitQty { get => submitQty; set => submitQty = value; }
    }
}
