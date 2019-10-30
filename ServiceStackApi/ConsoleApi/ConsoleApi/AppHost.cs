using ConsoleApi.Services;
using HealthCheck;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Text;
using System;

namespace ConsoleApi
{
    public class AppHost : AppSelfHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("DummyService", typeof(DummyService).Assembly, typeof(HealthCheckService).Assembly) { }


        public override void Configure(Funq.Container container)
        {
            this.SetConfig(new HostConfig
            {
                DefaultContentType = MimeTypes.Json
            });

            this.Plugins.Add(new SwaggerFeature { UseBootstrapTheme = true });
            this.Plugins.Add(new CorsFeature(allowedOrigins: "*",
                allowedMethods: "GET, POST, PUT, DELETE, OPTIONS",
                allowedHeaders: "Content-Type, LFAuth",
                allowCredentials: false));

            this.GlobalRequestFilters.Add(
                (req, res, dto) =>
                {
                    Console.WriteLine($"[Request] {req.AbsoluteUri}", dto.Dump());
                });

            this.GlobalResponseFilters.Add(
                (req, res, dto) =>
                {
                    string dump = null;
                    if (dto != null)
                    {
                        dump = dto.GetType() == typeof(HttpResult) ? null : dto.Dump();
                    }
                    Console.WriteLine($"[Response] {req.AbsoluteUri}", dump);
                });

            this.ServiceExceptionHandlers.Add(
                (request, dto, ex) =>
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Error Request {0}", request);
                    Console.WriteLine("Error DTO {0}", dto);
                    return DtoUtils.CreateErrorResponse(request, ex);
                });

            // Handle Unhandled Exceptions occurring outside of Services
            // e.g. Exceptions during Request binding or in filters:
            this.UncaughtExceptionHandlers.Add(
                (req, res, operationName, ex) =>
                {
                    Console.WriteLine(ex);
                    res.Write("Error: {0}: {1}".Fmt(ex.GetType().Name, ex.Message));
                    res.EndRequest(true);
                });

           
        }
        
    }
}