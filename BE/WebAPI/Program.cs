using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Setia.Contexts.Base;
using Setia.Contexts.Gov;
using Setia.Controllers;
using Setia.Models.Base;
using Setia.Models.Gov;
using Setia.Services;
using Setia.Services.Interfaces;
using System.Text;
using System.Text.RegularExpressions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JWT:Issuer"],
        ValidAudience = config["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"] ?? ""))
    };
});

// DbContexts
if (new Regex(config["PostgreSQL_ConnectionString"] ?? "").Match(config["ConnectionStrings"] ?? "").Success)
{
    builder.Services.AddDbContext<BaseContext>(options => options.UseNpgsql(config["ConnectionStrings"]));
    builder.Services.AddDbContext<GovContext>(options => options.UseNpgsql(config["ConnectionStrings"]));
}
else // if (new Regex(config["MSSQL_ConnectionString"]).Match(config["ConnectionStrings"] ?? "").Success)
{
    //builder.Services.AddDbContext<BaseContext>(options => options.UseSqlServer(config["ConnectionStrings"]));
    //builder.Services.AddDbContext<GovContext>(options => options.UseSqlServer(config["ConnectionStrings"]));
}

// Services
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IAudit, AuditService>();
builder.Services.AddTransient<ISender, SenderService>();
// CRUDs
builder.Services.AddScoped<ICRUD<UserModel>, CRUDService<UserModel, BaseContext>>();
builder.Services.AddScoped<ICRUD<NotificationModel>, CRUDService<NotificationModel, BaseContext>>();
builder.Services.AddScoped<ICRUD<TagModel>, CRUDService<TagModel, BaseContext>>();

builder.Services.AddScoped<ICRUD<PontajModel>, CRUDService<PontajModel, GovContext>>();
builder.Services.AddScoped<ICRUD<QuestionModel>, CRUDService<QuestionModel, GovContext>>();
builder.Services.AddScoped<ICRUD<QuestionAnswerModel>, CRUDService<QuestionAnswerModel, GovContext>>();
builder.Services.AddScoped<ICRUD<PostModel>, CRUDService<PostModel, GovContext>>();
builder.Services.AddScoped<ICRUD<UserCollectionModel>, CRUDService<UserCollectionModel, GovContext>>();

// Controllers
builder.Services.AddControllers();

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
                }
            },
            Array.Empty<string>()
        }
    });
});

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(config["Origin"] ?? "")
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
        });
});

// App
WebApplication app = builder.Build();

app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

app.Services.CreateScope().ServiceProvider.GetRequiredService<BaseContext>().Database.Migrate();
app.Services.CreateScope().ServiceProvider.GetRequiredService<GovContext>().Database.Migrate();

app.Run();
