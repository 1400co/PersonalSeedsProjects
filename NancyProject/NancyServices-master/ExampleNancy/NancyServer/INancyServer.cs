using Nancy.Hosting.Self;

namespace ExampleNancy.NancyServer
{
    public interface INancyServer
    {
         NancyHost GetHost();
    }
}
