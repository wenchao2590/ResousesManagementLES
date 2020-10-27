using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Infrustructure.BaseClass
{
	public class BaseUserControl : UserControl
    {
        public override Page Page
        {
            get
            {
                return base.Page as BasePage;
            }
            set
            {
                base.Page = value;
            }
        }
    }
}
