using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Biblioteca
{
    public class Startup
    {
        private static string MobileOriginCors = "Mobile Origin Cors";
        public Startup(IConfiguration configuration) => Configuration = configuration;        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddSwaggerGen();            
            services.AddControllers();
            services.AddCors(options => {
                options.AddPolicy(name: MobileOriginCors, policy => policy.WithOrigins("*"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var path = "appsettings.json";
            if (env.IsDevelopment())
            {
                path = "appsettings.Development.json";
                app.UseDeveloperExceptionPage();
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(path, optional: true, reloadOnChange: true);

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(MobileOriginCors);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
