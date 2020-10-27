using System.Collections;

namespace Infrustructure
{
	internal interface IContext
	{
		IDictionary State { get; set; }
	}
}