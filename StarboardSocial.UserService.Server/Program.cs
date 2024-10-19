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

// DB Context
builder.Services.AddDbContext<StarboardDbContext>(optionsBuilder => optionsBuilder.UseMySQL(builder.Configuration.GetConnectionString("MySql")!));


// Services
builder.Services.AddTransient<IRegistrationService, RegistrationService>();
builder.Services.AddTransient<IRegistrationRepository, RegistrationRepository>();

builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<IProfileRepository, ProfileRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePathBase("/user");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();