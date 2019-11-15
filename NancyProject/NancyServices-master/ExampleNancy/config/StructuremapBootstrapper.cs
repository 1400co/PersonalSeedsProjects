using System;
using System.Reflection;
using ExampleNancy.Raven;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Configuration;
using Nancy.Responses.Negotiation;
using StructureMap;
using StructureMap.Graph;

namespace ExampleNancy.config
{
    public class StructuremapBootstrapper : StructureMapNancyBootstrapper
    {
        protected override INancyEnvironmentConfigurator GetEnvironmentConfigurator()
        {
            return this.ApplicationContainer.GetInstance<INancyEnvironmentConfigurator>();
        }

        public override INancyEnvironment GetEnvironment()
        {
            return this.ApplicationContainer.GetInstance<INancyEnvironment>();
        }

        protected override void RegisterNancyEnvironment(IContainer container, INancyEnvironment environment)
        {
            container.Configure(registry => registry.For<INancyEnvironment>().Use(environment));

        }

        protected override void ConfigureApplicationContainer(IContainer container)
        {
            container.Configure(x =>
            {
                x.Scan(scanner =>
                {
                    scanner.Assembly("ExampleNancy");
                    scanner.WithDefaultConventions();
                });

                x.For<IDocStore>().Singleton().Use(new DocStore("http://127.0.0.1:8091", "SubscribersPart0"));
            });
            base.ConfigureApplicationContainer(container);
        }

        protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context)
        {
            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            });
        }

        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(
                    (c) =>
                    {
                        c.ResponseProcessors.Clear();
                        c.ResponseProcessors.Add(typeof(JsonProcessor));
                    });
            }
        }

    }
}
