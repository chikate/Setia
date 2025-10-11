using Main.Data.Context;
using Main.Features.CRUDs;
using Main.Modules.Adm;
using Main.Modules.Audit;
using Main.Modules.Auth;
using Main.Modules.Drive;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

IConfiguration config = new ConfigurationManager()
.AddJsonFile("appsettings.json")
.AddEnvironmentVariables()
.AddUserSecrets<Program>()
//.Get<UserSettings>()
.Build();

WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions { WebRootPath = config["StoragePath"], Args = args });

builder.Configuration.AddConfiguration(config);

// DB
builder.Services
.AddDbContext<BaseContext>(options =>
{
    switch (config["DBTech"])
    {
        //case "MsSQL": options.UseSqlServer(config["DBConnectionStrings"]); break;
        case "PgSQL": options.UseNpgsql(config["DBConnectionStrings"]); break;
        default: throw new NotSupportedException($"Unsupported database technology: {config["DBTech"]}");
    }
})
// Features
.AddScoped<IAdmService, AdmService>()
.AddScoped<IAuditService, AuditService>()
.AddScoped<IAuthService, AuthService>()
.AddTransient<IDriveService, DriveService>()
// CRUDs
.AddScoped<ICRUDService<UserModel>, CRUDService<UserModel>>()
.AddScoped<ICRUDService<SettingsModel>, CRUDService<SettingsModel>>()
// Standards
.AddHttpContextAccessor()
.AddControllers();

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = config["HOST_Server"],
    ValidAudience = config["HOST_Client"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CryptKey"]!))
});
builder.Services
.AddExceptionHandler<CustomExceptionHandler>()
.AddProblemDetails()
.AddSignalR();

builder.Services.AddOpenApi(options => options.AddDocumentTransformer((document, context, cancellationToken) =>
{
    document.Components ??= new OpenApiComponents();
    document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    };
    document.SecurityRequirements.Add(new OpenApiSecurityRequirement
    {
        [new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        }
    ] = Array.Empty<string>()
    });
    return Task.CompletedTask;
})
);

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
app
.UseHttpsRedirection()
.UseStaticFiles()
.UseRouting()
.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
.UseAuthentication()
.UseAuthorization()
.UseExceptionHandler()
.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization(); }); // Suppresed ASP0014 Warning

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
app.UseSpa(spa => spa.UseProxyToSpaDevelopmentServer(config["HOST_Client"]!));
#endif

await app.RunAsync();