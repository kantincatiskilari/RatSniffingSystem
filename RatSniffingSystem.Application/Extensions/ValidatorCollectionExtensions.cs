using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Extensions
{
    public static class ValidatorCollectionExtensions
    {
        /// <summary>
        /// FluentValidation sınıflarını assembly üzerinden otomatik olarak kaydeder.
        /// </summary>
        /// <param name="services">IServiceCollection nesnesi</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
