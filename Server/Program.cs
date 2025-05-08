using Main.Data.Context;
using Main.Features.CRUDs;
using Main.Modules.Adm;
using Main.Modules.Audit;
using Main.Modules.Auth;
using Main.Modules.Drive;
using Main.Modules.Sessions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

IConfiguration config = new ConfigurationManager()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions { WebRootPath = config["StoragePath"], Args = args });

builder.Configuration.AddConfiguration(config);

// DB
Action<DbContextOptionsBuilder> dbOptions = options =>
{
    switch (config["DBTech"])
    {
        //case "MsSQL": options.UseSqlServer(config["DBConnectionStrings"]); break;
        case "PgSQL": options.UseNpgsql(config["DBConnectionStrings"]); break;
        default: throw new NotSupportedException($"Unsupported database technology: {config["DBTech"]}");
    }
};

builder.Services.AddDbContext<BaseContext>(dbOptions);

// Features
builder.Services.AddScoped<IAdmService, AdmService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IDriveService, DriveService>();
builder.Services.AddSingleton<SSEClientManager>();
// CRUDs
builder.Services.AddScoped<ICRUDService<UserModel>, CRUDService<UserModel>>();
builder.Services.AddScoped<ICRUDService<SettingsModel>, CRUDService<SettingsModel>>();

// Standards
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = config["HOST_Server"],
    ValidAudience = config["HOST_Client"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CryptKey"]!))
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme,Id = "Bearer"} }, Array.Empty<string>() }
    });
});

#if RELEASE
builder.Services.AddSpaStaticFiles(cfg => cfg.RootPath = "dist");
#endif

WebApplication app = builder.Build();

// Dynamicaly run all migrations
using (var scope = app.Services.CreateScope())
{
    Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(DbContext)) && !t.IsAbstract).ToList()
        .ForEach(dbContextType => ((DbContext)scope.ServiceProvider.GetRequiredService(dbContextType)).Database.Migrate());
}

// Standards
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization(); }); // Suppresed ASP0014 Warning

app.UseSwagger();
app.UseSwaggerUI(options =>
{
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