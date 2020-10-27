using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.Utilities.Exception
{
    public class MESBusinessException : System.Exception
    {
        public MESBusinessException(string message) : base(message) { }
    }
}
