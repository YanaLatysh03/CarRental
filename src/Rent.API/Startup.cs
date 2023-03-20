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
using Rent.Services.Interfaces;
using Rent.Services.Repositories;
using Rent.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapping;
using Rent.Database;
using System.Net.Http;
using Polly;

namespace Rent.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rent.API", Version = "v1" });
            });


            services.AddDbContext<ApplicationContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IRentRepository, RentRepository>();

            services.AddAutoMapper(typeof(MappingProfile));

            var timeout = Policy.TimeoutAsync<HttpResponseMessage>(
                TimeSpan.FromMinutes(10));
            var longTimeout = Policy.TimeoutAsync<HttpResponseMessage>(
                TimeSpan.FromMinutes(10));

            services.AddHttpClient("conditionalpolicy")
                .AddPolicyHandler(request =>
                    request.Method == HttpMethod.Get ? timeout : longTimeout);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rent.API v1"));
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
