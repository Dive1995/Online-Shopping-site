using System;
using AutoMapper;
using BusinessLogicLayer.Profile;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer.Services
{
    public static class RegisterService
    {
        public static void ConfigureServices(IServiceCollection services)
        {

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
