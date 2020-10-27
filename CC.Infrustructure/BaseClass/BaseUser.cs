using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;

namespace Infrustructure.BaseClass
{
	public class MPSPrincipal : GenericPrincipal
    {
        public MPSPrincipal(IIdentity identity)
            : base(identity, new string[0])
        {

        }
    }
}
