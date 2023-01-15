using DataAdapter.Sql;
using DnD.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<SqlAdapter>(new SqlAdapter(builder.Configuration.GetConnectionString("Connectionstring"), "MSSQL"));
builder.Services.AddTransient<CharacterArsenalRepository>();
builder.Services.AddTransient<CharacterGearRepository>();
builder.Services.AddTransient<CharacterMainStatsRepository>();
builder.Services.AddTransient<CharacterRepository>();
builder.Services.AddTransient<CharacterSkillRepository>();
builder.Services.AddTransient<SkillRepository>();
builder.Services.AddTransient<ClassCategoryRepository>();
builder.Services.AddTransient<ClassRepository>();
builder.Services.AddTransient<WorldObjectRepository>();
builder.Services.AddTransient<WorldObjectPropRepository>();
builder.Services.AddTransient<WorldMiscRepository>();
builder.Services.AddTransient<LocationRepository>();
builder.Services.AddTransient<LocationEventRepository>();
builder.Services.AddTransient<RaceRepository>();
builder.Services.AddTransient<RaceCategoryRepository>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "ApiKey must appear in header",
        Type = SecuritySchemeType.ApiKey,
        Name = "ApiKey",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
    options.AddSecurityRequirement(requirement);
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

app.UseAuthorization();

app.MapControllers();

app.Run();