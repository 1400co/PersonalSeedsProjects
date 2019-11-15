using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;

namespace ExampleNancy.config
{
    //public class CustomResponseProcessorBootstrapper : DefaultNancyBootstrapper
    //{
    //    protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
    //    {
    //        get
    //        {
    //            return NancyInternalConfiguration.WithOverrides(
    //                (c) =>
    //                {
    //                    c.ResponseProcessors.Clear();
    //                    c.ResponseProcessors.Add(typeof(JsonProcessor));
    //                });
    //        }
    //    }

    //    protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
    //    {

    //        //CORS Enable
    //        pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
    //        {
    //            ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
    //                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
    //                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
    //        });
    //    }
    //}
}
