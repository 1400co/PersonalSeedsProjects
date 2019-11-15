using Nancy.Hosting.Self;
using System;

namespace ExampleNancy.NancyServer
{
    public class NancyServer : INancyServer
    {
        private readonly NancyHost _host;

        public NancyServer(string uri)
        {
            using ( _host = new NancyHost(new Uri("http://localhost:1070")))
            {

                _host.Start();
                Console.WriteLine("Nancy server Running.");
                Console.ReadKey();
            }
        }

        public NancyHost GetHost()
        {
            return _host;
        }
        
    }
}
