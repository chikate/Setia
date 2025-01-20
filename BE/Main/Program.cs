using Main.Data.Contexts;
using Main.Modules.Adm;
using Main.Modules.Audit;
using Main.Modules.Auth;
using Main.Modules.Chat;
using Main.Modules.Drive;
using Main.Modules.Sessions;
using Main.Standards.Data.Models;
using Main.Standards.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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

Action<DbContextOptionsBuilder> dbOptions = options =>
{
    switch (config["DBTech"])
    {
        //case "MsSQL": options.UseSqlServer(config["DBConnectionStrings"]); break;
        case "PgSQL": options.UseNpgsql(config["DBConnectionStrings"]); break;
        default: throw new NotSupportedException($"Unsupported database technology: {config["DBTech"]}");
    }
};

// Modules
builder.Services.AddDbContext<AdmContext>(dbOptions);
builder.Services.AddScoped<IAdmService, AdmService>();

builder.Services.AddDbContext<AuditContext>(dbOptions);
builder.Services.AddScoped<IAuditService, AuditService>();

builder.Services.AddDbContext<AuthContext>(dbOptions);
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDbContext<DriveContext>(dbOptions);
builder.Services.AddTransient<IDriveService, DriveService>();

builder.Services.AddDbContext<ChatContext>(dbOptions);
builder.Services.AddTransient<IChatService, ChatService>();

builder.Services.AddDbContext<SessionsContext>(dbOptions);
builder.Services.AddSingleton<SSEClientManager>();

builder.Services.AddDbContext<GovContext>(dbOptions);
builder.Services.AddScoped<ICRUDService<UserModel>, CRUDService<UserModel, AuthContext>>();
builder.Services.AddScoped<ICRUDService<SettingsModel>, CRUDService<SettingsModel, AdmContext>>();
builder.Services.AddScoped<ICRUDService<PostModel>, CRUDService<PostModel, GovContext>>();

// Standards
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = config["Server"],
    ValidAudience = config["Origin"],
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

WebApplication app = builder.Build();

// Dynamicaly run all migrations
using (var scope = app.Services.CreateScope())
{
    Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(DbContext)) && !t.IsAbstract).ToList()
        .ForEach(dbContextType => ((DbContext)scope.ServiceProvider.GetRequiredService(dbContextType)).Database.Migrate());
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireAuthorization();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
    app.UseSpa(options => options.UseProxyToSpaDevelopmentServer(config["Origin"]!));
}
else
{
    app.UseSpaStaticFiles(new StaticFileOptions { FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "dist")) });
}

await app.RunAsync();