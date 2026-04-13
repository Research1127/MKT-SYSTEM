using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mktsystem.domain.Entities;
using mktsystem.domain.Repositories;
using mktsystem.domain.Seeders;
using mktsystem.infrastructure.Persistence;
using mktsystem.infrastructure.Repository;
using mktsystem.infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace mktsystem.infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<MktSystemDbContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<IClassSeeder, ClassSeeder>();
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();

        services.AddIdentityApiEndpoints<Users>().AddEntityFrameworkStores<MktSystemDbContext>();
    }
}