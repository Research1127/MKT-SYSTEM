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
using mktsystem.application.Interfaces;
using mktsystem.infrastructure.Services;

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
        services.AddScoped<IExcelService, ExcelService>();

        services.AddIdentityApiEndpoints<Users>().AddEntityFrameworkStores<MktSystemDbContext>();
    }
}