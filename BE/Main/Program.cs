using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services;
using Main.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.WebSockets;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Prepare Data Base
Action<DbContextOptionsBuilder> dbOptions = options =>
{
    switch (config["DBTech"])
    {
        case "PgSQL": options.UseNpgsql(config["DBConnectionStrings"]); break;
            //case "MsSQL": options.UseSqlServer(config["DBConnectionStrings"]); break;
    }
};

// Create Tabels
builder.Services.AddDbContext<BaseContext>(dbOptions);
builder.Services.AddDbContext<GovContext>(dbOptions);

// Register Services
builder.Services.AddScoped<ICRUD<IntervalModel>, CRUDService<IntervalModel, GovContext>>();
builder.Services.AddScoped<ICRUD<QuestionModel>, CRUDService<QuestionModel, GovContext>>();
builder.Services.AddScoped<ICRUD<QuestionAnswerModel>, CRUDService<QuestionAnswerModel, GovContext>>();
builder.Services.AddScoped<ICRUD<PostModel>, CRUDService<PostModel, GovContext>>();

builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IAudit, AuditService>();
builder.Services.AddTransient<ISender, SenderService>();

builder.Services.AddScoped<ICRUD<AuditModel>, CRUDService<AuditModel, BaseContext>>();
builder.Services.AddScoped<ICRUD<NotificationModel>, CRUDService<NotificationModel, BaseContext>>();

// Dependencies Services
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CryptKey"] ?? ""))
    });

// Controllers
builder.Services.AddControllers(options =>
{
    // Apply global authorization filter to all controllers
    options.Filters.Add(new AuthorizeFilter());
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

builder.WebHost.UseUrls(config["Server"] ?? "");

// App
WebApplication app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod());

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
app.Map("/ws", async context =>
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
    else context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
});

//app.UseSpa(options => options.UseProxyToSpaDevelopmentServer(config["Origin"] ?? ""));

await app.RunAsync();
