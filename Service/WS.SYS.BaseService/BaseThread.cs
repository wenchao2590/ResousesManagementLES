using Infrustructure.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.SYS.BaseService
{
    class BaseThread : ThreadBase
    {
        public BaseThread(Guid serviceFid) : base(serviceFid) { }

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
