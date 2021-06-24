using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyDomain.Controllers
{
    public class ConnectToProxyController: Controller
    {
        private static HttpClient Client = CreateProxyClient();

        public async Task<ActionResult> Index()
        {
            try
            {
                
                var proxy = System.Web.Configuration.WebConfigurationManager.GetSection("system.net/defaultProxy") as System.Net.Configuration.DefaultProxySection;
                var resp = await Client.GetAsync(proxy.Proxy.ProxyAddress);
                //var resp = await Client.GetAsync("http://a7248b4143ad.ngrok.io");
                var result = await resp.Content.ReadAsStringAsync();
                return Content(result);
            }
            catch(Exception e)
            {
                string err = e.Message;
                return Content(err);
            }
            
        }


        private static HttpClient CreateProxyClient()
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = false,
                Proxy = new WebProxy("http://0fa369f52b82.ngrok.io", false),
                UseProxy = true
            };

            var client = new HttpClient(handler);

            return client;
        }


        /*
            <system.net>
    <defaultProxy enabled="true" useDefaultCredentials="true">
      <proxy  usesystemdefault="False" bypassonlocal="False" proxyaddress="http://0fa369f52b82.ngrok.io"/>
    </defaultProxy>
  </system.net> 
         
         
        */
    }
}