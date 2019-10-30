using StructureMap;
using System;
using Topshelf;

namespace ConsoleApi
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = new Container(cfg =>
                {
                    cfg.For<IHost>().Singleton().Use<HostService>();
                });

                var runConfiguration = HostFactory.New(config =>
                {
                    config.RunAsLocalSystem();
                    config.SetDisplayName("DocumentValidationService");
                    config.SetServiceName("DocumentValidationService");
                    config.SetDescription("DocumentValidationService");
                    config.StartManually();
                    config.EnableServiceRecovery(rc => rc.RestartService(1));
                    config.Service<IHost>(service =>
                    {
                        service.ConstructUsing(name => container.GetInstance<IHost>());
                        service.WhenStarted(s => s.Start());
                        service.WhenStopped(s => s.Stop());
                    });
                });

                runConfiguration.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
