using ServiceStack;

namespace HealthCheck
{
    public class HealthCheckService : Service
    {
        public ServiceStatusResponse Get(ServiceStatusRequest req)
        {
            return new ServiceStatusResponse
            {
                Ready = true,
                Message = "Application services up & running",
                RedirectUrl = string.Empty
            };
        }
    }
}
