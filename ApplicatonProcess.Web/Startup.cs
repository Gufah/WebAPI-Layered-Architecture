using ApplicatonProcess.Data.Models;
using ApplicatonProcess.Data.Repositories;
using ApplicatonProcess.Domain.Repositories;
using ApplicatonProcess.Domain.Repsitories;
using ApplicatonProcess.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicatonProcess.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApplicatonProcess.Web", Version = "v1" });
                c.EnableAnnotations();
            });
            services.AddHttpClient("countries", c =>
            {
                c.BaseAddress = new Uri("https://restcountries.eu/rest/v2/");
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddDbContext<AssetContext>(opt =>
                opt.UseInMemoryDatabase(databaseName: "Asset"));
            services.AddScoped<IAssetDetailService, AssetDetailService>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IHttpClient, HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicatonProcess.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
