using Car.Database;
using Car.Services.Interfaces;
using Car.Services.Repositories;
using Car.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Polly;

namespace Car.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Car.API", Version = "v1" });
            });

            services.AddDbContext<ApplicationContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICarRepository, CarRepository>();

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
