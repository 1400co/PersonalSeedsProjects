using ServiceStack;

namespace HealthCheck
{
    [Route("/status", "GET", Summary = "Checks the service status")]
    public class ServiceStatusRequest : IReturn<ServiceStatusResponse>
    {
    }
}
