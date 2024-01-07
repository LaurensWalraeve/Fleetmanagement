using AutoMapper;
using FleetManagement.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.Sources.Clear();

    var env = hostingContext.HostingEnvironment;

    config.SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
        .AddJsonFile("appsettings.local.json", true, true)
        .AddEnvironmentVariables();

    if (args != null) config.AddCommandLine(args);
});

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapper(typeof(Mapper));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add JWT Configurations.
builder.Services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowLocalhost3000");
}

app.UseAuthConfiguration();

app.MapControllers();

app.UseSwaggerSetup();

app.Run();