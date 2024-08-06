using Main.Data.Contexts;
using Main.Data.Models;
using Main.Data.Models.Base;
using Main.Services.Base;
using Main.Services.Base.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.WebSockets;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

switch (config["DBTech"])
{
    case "PgSQL":
        builder.Services.AddDbContext<BaseContext>(options => options.UseNpgsql(config["DBConnectionStrings"]));
        builder.Services.AddDbContext<GovContext>(options => options.UseNpgsql(config["DBConnectionStrings"]));
        break;
    case "MsSQL":
        //builder.Services.AddDbContext<BaseContext>(options => options.UseSqlServer(config["DBConnectionStrings"]));
        //builder.Services.AddDbContext<GovContext>(options => options.UseSqlServer(config["DBConnectionStrings"]));
        break;
    default:
        throw new InvalidOperationException("Unsupported DBTech value: " + config["DBTech"]);
}

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Server"],
        ValidAudience = config["Origin"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Key"] ?? ""))
    }
);

#region Services

builder.Services.AddScoped<ICRUD<IntervalModel>, CRUDService<IntervalModel, GovContext>>();
builder.Services.AddScoped<ICRUD<QuestionModel>, CRUDService<QuestionModel, GovContext>>();
builder.Services.AddScoped<ICRUD<QuestionAnswerModel>, CRUDService<QuestionAnswerModel, GovContext>>();
builder.Services.AddScoped<ICRUD<PostModel>, CRUDService<PostModel, GovContext>>();

#region Base Services
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IAudit, AuditService>();
builder.Services.AddTransient<ISender, SenderService>();

builder.Services.AddScoped<ICRUD<UserModel>, CRUDService<UserModel, BaseContext>>();
builder.Services.AddScoped<ICRUD<NotificationModel>, CRUDService<NotificationModel, BaseContext>>();
#endregion

#endregion

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

builder.WebHost.UseUrls(config["Server"] ?? "");

// Cors
builder.Services.AddCors(options =>
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins(config["Origin"] ?? "")
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()));

// App
WebApplication app = builder.Build();

app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles();

app.Services.CreateScope().ServiceProvider.GetRequiredService<BaseContext>().Database.Migrate();
app.Services.CreateScope().ServiceProvider.GetRequiredService<GovContext>().Database.Migrate();

app.UseWebSockets();
app.Map("/send", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();

        byte[] bytes = Encoding.UTF8.GetBytes("Hello");
        await ws.SendAsync
        (
            new ArraySegment<byte>(bytes, 0, bytes.Length),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});

await app.RunAsync();
