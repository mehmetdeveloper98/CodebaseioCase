using Customer.Application.Consumers;
using Customer.Application.Validations;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace Customer.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
           
        }
    }
}
