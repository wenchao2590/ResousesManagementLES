using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;

namespace Infrustructure.BaseClass
{
	public class MPSIdentity : GenericIdentity
    {
        public MPSIdentity(string name)
            : base(name)
        {

        }
    }
}
