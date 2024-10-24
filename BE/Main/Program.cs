#pragma warning disable CS8604 // Possible null reference argument. For configs can be null but the app will not run anyways without the configs

using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

//WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "D:\\Dragos\\Downloads\\0Test"
});
ConfigurationManager config = builder.Configuration;

#region DB Connection & Setup
Action<DbContextOptionsBuilder> dbOptions = options =>
{
    switch (config["DBTech"])
    {
        //case "MsSQL": options.UseSqlServer(config["DBConnectionStrings"]); break;
        case "PgSQL": options.UseNpgsql(config["DBConnectionStrings"]); break;
        default: throw new NotSupportedException($"Unsupported database technology: {config["DBTech"]}");
    }
    options.LogTo(Console.WriteLine, LogLevel.Information);
};

builder.Services.AddDbContext<BaseContext>(dbOptions);
builder.Services.AddDbContext<GovContext>(dbOptions);
#endregion

#region Services
builder.Services.AddTransient<ISenderService, SenderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IFileManagerService, FileManagerService>();

builder.Services.AddScoped<ICRUDService<UserModel>, CRUDService<UserModel, BaseContext>>();
builder.Services.AddScoped<ICRUDService<PostModel>, CRUDService<PostModel, GovContext>>();
#endregion

#region Dependency Services
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
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CryptKey"]))
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
#endregion

WebApplication app = builder.Build();

// Setup CORS
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Run Migrations
app.Services.CreateScope().ServiceProvider.GetRequiredService<BaseContext>().Database.Migrate();
app.Services.CreateScope().ServiceProvider.GetRequiredService<GovContext>().Database.Migrate();

// Standards
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization()); // Ensure endpoints with UseEndpoints for the spa proxy. ToDo: remove endopoints but the spa proxy does not work if it is removed
#pragma warning restore ASP0014 // Suggest using top level route registrations

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
    app.UseSpa(options => options.UseProxyToSpaDevelopmentServer(config["Origin"]));
}
else
{
    app.UseSpaStaticFiles(new StaticFileOptions { FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "dist")) });
    app.UseSpa(options => options.Options.SourcePath = "dist");
}

await app.RunAsync();