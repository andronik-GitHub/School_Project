using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.MVVM.VM
{
    public abstract class BaseVM
    {
        protected HttpClient Client { get; set; }
        protected string HostName { get; }
        protected BaseVM(string hostName)
        {
            Client = new();
            HostName = hostName;

            Client.BaseAddress = new Uri("https://localhost:5001/ca/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
