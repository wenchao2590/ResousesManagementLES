﻿namespace WS.SRM.CalculateSupplyPlanService
{
    using Infrustructure.Logging;
    using Infrustructure.Thread;
    using System;
    class CalculateSupplyPlanThread : ThreadBase
    {
        public CalculateSupplyPlanThread(Guid serviceFid) : base(serviceFid) { }

        protected override bool Process()
        {
            try
            {
                new Handle().Handler();
                Log.WriteLogToFile("Process", AppDomain.CurrentDomain.BaseDirectory + @"\LOG\", DateTime.Now.ToString("yyyyMMdd"));
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }
    }
}
