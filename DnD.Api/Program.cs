using DataAdapter.NoSql;
using DnD.Api.Extensions;
using DnD.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
//v2
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddRepositories(settings =>
{
    settings.MongoConnectionString = builder.Configuration.GetConnectionString("Connectionstring");
    settings.MongoDatabase = builder.Configuration.GetValue<string>("Database");
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
    {
        config.Authority = "http://localhost:5000";
        config.RequireHttpsMetadata = false;
        config.TokenValidationParameters.ValidateActor = false;
        config.TokenValidationParameters.ValidateAudience = false;
        config.TokenValidationParameters.ValidateIssuer = false;
        IdentityModelEventSource.ShowPII = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "jwt_Auth_Api",
        Version = "v1",
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                 Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
        }
    });
    
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5270);
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
