using FluentValidation;
using Hangfire;
using Hangfire.SqlServer;
using Library.Domain.Context;
using Library.Infrastructure.Common.GenericRepo;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddHangfire(configuration => configuration
//         .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//        .UseSimpleAssemblyNameTypeSerializer()
//        .UseDefaultTypeSerializer()
//        .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
//        {
//            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//            QueuePollInterval = TimeSpan.Zero,
//            UseRecommendedIsolationLevel = true,
//            DisableGlobalLocks = true
//        }));

//builder.Services.AddHangfireServer();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<_Context>();
    var serviceProvider = serviceScope.ServiceProvider;
    if (!serviceScope.ServiceProvider.GetService<_Context>().AllMigrationsApplied())
    {
        serviceScope.ServiceProvider.GetService<_Context>().Migrate();
    }
    //RecurringJobManager.AddHangFireJops(serviceProvider);

}

app.UseCors("AllowCors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseHangfireDashboard();

app.UseRouting();
app.MapControllers();
app.Run();


