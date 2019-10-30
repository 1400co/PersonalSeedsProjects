namespace HealthCheck
{
    public class ServiceStatusResponse
    {
        public bool Ready { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
    }
}