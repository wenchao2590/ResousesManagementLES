using Infrustructure.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.SYS.SAMPLE
{
    class SampleThread : ThreadBase
    {
        public SampleThread(Guid serviceFid) : base(serviceFid) { }

        protected override bool Process()
        {
            try
            {
                new Handle().Handler();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
