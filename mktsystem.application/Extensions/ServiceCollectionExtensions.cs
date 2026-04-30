using Microsoft.Extensions.DependencyInjection;
using mktsystem.application.Interfaces;

namespace mktsystem.application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        
        
        services.AddAutoMapper(applicationAssembly);
        
    }
}