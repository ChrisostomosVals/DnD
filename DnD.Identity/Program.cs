using DataAdapter.NoSql;
using DnD.Data.Repositories;
using DnD.Identity.Stores;
using IdentityServer4.Stores;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = new MongoDbConnection(builder.Configuration.GetConnectionString("Connectionstring"));
connection.UseDatabase(builder.Configuration.GetValue<string>("Database"));
builder.Services.AddSingleton<IMongoDbConnection>(connection);
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserRoleRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential(persistKey: true)
                .AddClientStore<ClientStore>()
                .AddProfileService<ProfileService>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddResourceStore<ResourceStore>();
IdentityModelEventSource.ShowPII = true;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

//app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();
