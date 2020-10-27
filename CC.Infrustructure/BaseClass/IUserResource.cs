using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.BaseClass
{
	public enum UserResourceType
    {
        Menu,Action
    }

	public interface IUserResource
    {
        UserResourceType ResourceType { get;set;}
    }
}
