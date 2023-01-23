using DataAdapter.Sql;
using DnD.Data.Repositories;
using DnD.Identity.Stores;
using IdentityServer4.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<SqlAdapter>(new SqlAdapter(builder.Configuration.GetConnectionString("Connectionstring"), "MSSQL"));
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();
