using GoodDadsAPI.Services.Repositories;
using MediatR;

namespace GoodDadsAPI.Services.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            RegisterMediatR(services);
            RegisterRepositories(services);

            return services;
        }

        private static void RegisterMediatR(IServiceCollection services) 
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            });

            services.AddScoped<IMediator, Mediator>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IDadRepository, DadRepository>();
            services.AddScoped<IDependentRepository, DependentRepository>();
            services.AddScoped<IMaritalStatusRepository, MaritalStatusRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
        }
    }
}
