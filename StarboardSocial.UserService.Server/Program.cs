using System.Text.Json.Serialization;
using RabbitMQ.Client;
using StarboardSocial.UserService.Data.Database;
using StarboardSocial.UserService.Domain.Services;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using MySqlConnector;
using StarboardSocial.UserService.Data.Repositories;
using StarboardSocial.UserService.Domain.DataInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

// RabbitMQ Config
try
{
    ConnectionFactory factory = new()
    {
        UserName = builder.Configuration["Rabbit:UserName"]!,
        Password = builder.Configuration["Rabbit:Password"]!,
        VirtualHost = builder.Configuration["Rabbit:VirtualHost"]!,
        HostName = builder.Configuration["Rabbit:HostName"]!,
        Port = int.Parse(builder.Configuration["Rabbit:Port"]!)
    };

    IConnection conn = await factory.CreateConnectionAsync();
    IChannel channel = await conn.CreateChannelAsync();

    builder.Services.AddSingleton(channel);
} catch (Exception e)
{
    Console.WriteLine("Error connecting to RabbitMQ");
    Console.WriteLine(e.Message);
}

// DB Context
builder.Services.AddDbContext<StarboardDbContext>(optionsBuilder => optionsBuilder.UseMySQL(builder.Configuration.GetConnectionString("MySql")!));

builder.Services.AddScoped<DataDeletionConsumer>();
builder.Services.AddHostedService<DataDeletionListener>();

// Application services
builder.Services.AddScoped<DataDeletionHandler>();

// Services
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddScoped<IDataDeletionRepository, DataDeletionRepository>();
builder.Services.AddScoped<IDataDeletionService, DataDeletionService>();


builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    StarboardDbContext context = serviceScope.ServiceProvider.GetRequiredService<StarboardDbContext>();
    context.Database.Migrate();
}

app.UsePathBase("/user");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();