using DataAdapter.NoSql;
using DnD.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
var connection = new MongoDbConnection(builder.Configuration.GetConnectionString("Connectionstring"));
connection.UseDatabase(builder.Configuration.GetValue<string>("Database"));
builder.Services.AddSingleton<IMongoDbConnection>(connection);
builder.Services.AddTransient<CharacterRepository>();
builder.Services.AddTransient<ChapterRepository>();
builder.Services.AddTransient<ClassCategoryRepository>();
builder.Services.AddTransient<ClassRepository>();
builder.Services.AddTransient<WorldObjectRepository>();
builder.Services.AddTransient<LocationRepository>();
builder.Services.AddTransient<RaceRepository>();
builder.Services.AddTransient<RaceCategoryRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserRoleRepository>();



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
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(5001); // to listen for incoming http connection on port 5001
//    options.ListenAnyIP(7001, configure => configure.UseHttps()); // to listen for incoming https connection on port 7001
//});
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
