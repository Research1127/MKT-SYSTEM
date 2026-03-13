using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mktsystem.domain.Repositories;
using mktsystem.infrastructure.Persistence;
using mktsystem.infrastructure.Repository;

namespace mktsystem.infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<MktSystemDbContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<IStudentRepository, StudentRepository>();
    }
}