using ServiceStack;
using System;
using System.Configuration;

namespace ConsoleApi
{
    internal class HostService : IHost
    {

        private AppHost _appHost;

        public void Start()
        {
            Console.WriteLine("Dummy ApiService started");

            _appHost = new AppHost();
            Licensing.RegisterLicenseFromFileIfExists("Licence/sslicense.txt".MapHostAbsolutePath());
            _appHost.Init().Start(ConfigurationManager.AppSettings["Main_REST_ServiceURL"]);
        }

        public void Stop()
        {
            Console.WriteLine("Stoping Dummy ApiService  HostService");

            _appHost.Stop();
            _appHost.Dispose();

            Console.WriteLine("Stopped Dummy ApiService HostService");
        }
    }


    public interface IHost
    {
        void Start();
        void Stop();
    }

}
