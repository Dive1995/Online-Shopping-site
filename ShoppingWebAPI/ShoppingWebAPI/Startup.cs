using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccessLayer.Contexts;
using DataAccessLayer.Models;
using BusinessLogicLayer;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.IServices;
using DataAccessLayer.Models.IRepositories;
using DataAccessLayer.Models.Repositories;

namespace ShoppingWebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly string _myPolicyName = "MyPolicy";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_myPolicyName,
                builder =>
                {
                    builder.WithOrigins("http://localhost:5000",
                                        "http://localhost:4200"
                                        )
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });



            services.AddMvc(options => options.EnableEndpointRouting = false);

            var connectionString = _configuration.GetConnectionString("ShoppingConnectionString");
            services.AddDbContextPool<ShoppingContext>(opt => opt.UseSqlServer(connectionString));

            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // JWT
            var appSettings = appSettingsSection.Get<AppSettings>();
            var jwtKey = Encoding.ASCII.GetBytes(appSettings.JWTkey);
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<ProductBLL>();
            services.AddScoped<CategoryBLL>();
            services.AddScoped<CustomerBLL>();
            services.AddSingleton<AppSettings>();
            //services.AddAutoMapper(typeof(Startup));

            RegisterService.ConfigureServices(services);

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemsRepository, OrderItemRepository>();
            services.AddScoped<IShippingRepository, ShippingRepository>();
            services.AddScoped<IDeliveryOptionsRepository, DeliveryOptionsRepository>();
            services.AddScoped<IOrderBLL, OrderBLL>();
            services.AddScoped<IDeliveryBLL, DeliveryBLL>();

            // Email settings
            var mailSetting = _configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailSetting);
            var appSettin = mailSetting.Get<MailSettings>();
            services.AddTransient<IMailService, MailService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(_myPolicyName);

            app.UseExceptionHandler("/api/error");

            app.UseMvc();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
