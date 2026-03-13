using Microsoft.EntityFrameworkCore;
using mktsystem.infrastructure.Extensions;
using mktsystem.infrastructure.Persistence;

using DotNetEnv;
using mktsystem.application.Extensions;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(); // Important

builder.Services.AddApplication(); // Because of our new ServiceCollectionExtensions in Application Module

// Register Service Collection Infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();



// Run migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MktSystemDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // Important
app.Run();
