using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Text;
using Mapping;
using User.Services;
using User.Services.Interfaces;
using System.Threading.Tasks;
using Car.Services.Interfaces;
using User.Services.Repositories;
using Car.Services;
using Car.Services.Repositories;
using Rent.Services.Interfaces;
using Rent.Services;
using Rent.Services.Repositories;
using Stripe;
using Org.BouncyCastle.Ocsp;
using static System.Net.Mime.MediaTypeNames;

namespace Car.Rental
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
            //Configuration for authentication. JwtBearer and Cookie.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["AuthOptions:ISSUER"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["AuthOptions:AUDIENCE"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthOptions:KEY"])),
                        ValidateIssuerSigningKey = true,
                    };
                    options.SaveToken = true;
                    options.Events = new JwtBearerEvents();
                    options.Events.OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("AccessToken"))
                        {
                            context.Token = context.Request.Cookies["AccessToken"];
                        }

                        return Task.CompletedTask;
                    };
                })
                .AddCookie(options =>
                {
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.IsEssential = true;
                });

            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder
                    .WithOrigins(Configuration.GetSection("frontend_url").Get<string>())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }));

            services.AddControllersWithViews();

            services.AddDbContext<ApplicationContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddAutoMapper(typeof(MappingProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("ApiCorsPolicy");
            StripeConfiguration.ApiKey = "sk_test_51M5e6kA7xouVGvdGYKdicBAxBTrygNKpqzAUI8iZhx98wPk6mZ5IhVXvOeaVb3POQndfYW6PTURzfQzxUJd6e6Hj00YymQJHa8";

            app.UseStatusCodePagesWithReExecute("/Error/Errors", "?statusCode={0}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
