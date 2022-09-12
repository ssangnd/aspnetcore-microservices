﻿using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;


namespace Basket.API.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
            => services.AddScoped<IBasketRepositor, BasketRepository>()
            .AddTransient<ISerializeService, SerializeService>()
            ;
        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetSection("CacheSettings:ConnectionString").Value;
            if (string.IsNullOrEmpty(redisConnectionString)) throw new ArgumentNullException("Redis Connection string is not configured.");
            //Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });
        }
    }
}
