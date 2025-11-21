using Data.Context;
using Features.CRUDs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Modules.Audit;
using Modules.Auth;
using Modules.Drive;
using Services;
using System.Text;
#if RELEASE
using System.Reflection;
#endif

IConfiguration config = new ConfigurationManager()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    //.Get<UserSettings>()
    .Build();

WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions { WebRootPath = config["StoragePath"], Args = args });

builder.Configuration.AddConfiguration(config);

// DB
builder.Services.AddDbContext<BaseContext>(options =>
{
    switch (config["DB:Tech"])
    {
        //case "MsSQL": options.UseSqlServer(config["DBConnectionStrings"]); break;
        case "PgSQL": options.UseNpgsql($"Server=SERVER;Database={config["DB:Database"]};User ID={config["DB:User"]};Password={config["DB:Password"]};"); break;
        default: throw new NotSupportedException($"Unsupported database technology: {config["DB:Tech"]}");
    }
});
// Features
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IDriveService, DriveService>();
// CRUDs
builder.Services.AddScoped<ICRUDService<UserModel>, CRUDService<UserModel>>();
// Essentials
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = config["Origin"],
    ValidAudience = config["Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CryptKey"]!))
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSignalR();

builder.Services.AddOpenApi();

#if RELEASE
builder.Services.AddSpaStaticFiles(cfg => cfg.RootPath = "dist");
#endif

WebApplication app = builder.Build();

#if RELEASE
// Dynamicaly run all migrations
using (var scope = app.Services.CreateScope())
{
Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(DbContext)) && !t.IsAbstract).ToList()
.ForEach(dbContextType => ((DbContext)scope.ServiceProvider.GetRequiredService(dbContextType)).Database.Migrate());
}
#endif

// Standards
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<UserRightsMiddleware>();
app.UseMiddleware<AuditMiddleware>();

app.UseExceptionHandler();
app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization(); }); // Suppresed ASP0014 Warning

app.MapHub<EventsHub>("/events");

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
    options.DefaultModelsExpandDepth(-1);
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

#if RELEASE
app.UseSpaStaticFiles();
app.UseSpa(spa => spa.Options.SourcePath = "dist");
#endif

#if DEBUG
app.UseSpa(spa => spa.UseProxyToSpaDevelopmentServer(config["Audience"]!));
#endif

await app.RunAsync();