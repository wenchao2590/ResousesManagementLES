using System.Security.Principal;

namespace Infrustructure.Security
{
	public class LESIdentity : GenericIdentity
	{
		public LESIdentity(string name) : base(name)
		{
		}
	}

	public class LESPrincipal : GenericPrincipal
	{
		public LESPrincipal(IIdentity identity): base(identity, new string[0])
		{
		}
	}
}
