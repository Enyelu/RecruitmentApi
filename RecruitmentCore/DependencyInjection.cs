using Microsoft.Extensions.DependencyInjection;
using RecruitmentInfrastructure.Data.Interface;
using RecruitmentInfrastructure.Data;
using System.Reflection;

namespace RecruitmentCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            var serviceProvider = services
            .AddSingleton<ICosmoDbService, CosmosDbService>();
            

            services.AddMediatR(m => m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}