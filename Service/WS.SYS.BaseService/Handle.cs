using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WS.SYS.BaseService
{
    public class Handle
    {
        public void Handler()
        {
            ServiceHost _wcfHost = new ServiceHost(typeof(FunctionService), new Uri(AppSettings.GetConfigString("hostUrl")));
            try
            {
                //创建WCF宿主并启动
                _wcfHost.AddServiceEndpoint(typeof(IUser), new WSHttpBinding { Security = { Mode = SecurityMode.None } }, "BaseService");
                _wcfHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                _wcfHost.Open();
            }
            catch (CommunicationException ex)
            {
                //启动失败，终止宿主
                if (_wcfHost != null)
                {
                    _wcfHost.Abort();
                }
            }

        }
    }
}
