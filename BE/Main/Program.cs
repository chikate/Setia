using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services;
using Main.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

#region DB Connection
// Connect to DataBase
Action<DbContextOptionsBuilder> dbOptions = options =>
{
    switch (config["DBTech"])
    {
        case "PgSQL": options.UseNpgsql(config["DBConnectionStrings"]); break;
        case "MsSQL": /*options.UseSqlServer(config["DBConnectionStrings"]);*/ break;
    }
};

// Create Tabels
builder.Services.AddDbContext<BaseContext>(dbOptions);
builder.Services.AddDbContext<GovContext>(dbOptions);
#endregion

builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IAudit, AuditService>();
builder.Services.AddTransient<ISender, SenderService>();

builder.Services.AddScoped<ICRUD<UserModel>, CRUDService<UserModel, BaseContext>>();
builder.Services.AddScoped<ICRUD<PostModel>, CRUDService<PostModel, GovContext>>();

#region Dependency Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Server"],
        ValidAudience = config["Origin"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CryptKey"] ?? ""))
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
            }   },
            Array.Empty<string>()
        }
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
app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());

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
    app.UseSpaStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "dist"))
    });
    app.UseSpa(options => options.Options.SourcePath = "dist");
}

await app.RunAsync();
