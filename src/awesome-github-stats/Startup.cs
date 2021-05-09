using AwesomeGithubStats.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AwesomeGithubStats
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _configuration = configuration;
            IsDevelopment = environment.IsDevelopment();
        }

        public bool IsDevelopment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (IsDevelopment)
            {
                services
                   .AddMvcCore(options => options.Filters.Add<LogRequestTimeFilterAttribute>())
                   .AddApiExplorer();

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "awesome github stats", Version = "v1" });
                });
            }
            else
            {
                services
                    .AddMvcCore()
                    .AddApiExplorer();
            }

            services.ConfigureGithubServices(_configuration);
            services.AddHealthChecks();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Awesome Github Stats v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
