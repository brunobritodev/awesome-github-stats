using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Tests.Configuration
{
    public class Warmup
    {
        public ServiceProvider Services { get; set; }
        public Warmup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Warmup>()
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            //Mock IHttpContextAccessor
            var services = new ServiceCollection();


            services.AddScoped<IConfiguration>(s => configuration);
            services.ConfigureGithubServices(configuration);
            Services = services.BuildServiceProvider();
        }
    }
}
