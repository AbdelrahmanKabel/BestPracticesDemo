using BestPracticesDemo.Application;
using BestPracticesDemo.Persistence;
using BestPracticesDemo.API.Utility;
using BestPracticesDemo.Infrastructure;
using Microsoft.OpenApi.Models;
using BestPracticesDemo.API.Middleware;
using Serilog;
using BestPracticesDemo.Identity;
using Microsoft.AspNetCore.Identity;
using BestPracticesDemo.Identity.Models;
using BestPracticesDemo.Identity.Seed;

//Serilog
var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name ="Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GloboTicket Ticket Mangement API"
    });

    c.OperationFilter<FileResultContentTypeOperationFilter>();
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await UserCreator.SeedAsync(userManager);
        Log.Information("Application Starting");
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "An error occured while starting the application");
    }
}
// Configure the HTTP request pipeline.

app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Custom exception handler
//app.UseCustomExceptionHandler();
//app cors
app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
