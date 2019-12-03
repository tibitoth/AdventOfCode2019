using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static T RegisterOption<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class, new()
        {
            var name = typeof(T).Name;
            var option = new T();
            configuration.GetSection(typeof(T).Name).Bind(option);
            services.Configure<T>(configuration.GetSection(name));

            return option;
        }
    }
}
