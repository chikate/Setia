using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services;
using Setia.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContexts
builder.Services.AddDbContext<SetiaContext>(options => options.UseSqlServer("Server=DRAGOS;Database=Setia;Trusted_Connection=True;TrustServerCertificate=True;"));

// AutoMappers
builder.Services.AddAutoMapper(typeof(AuditModel));
builder.Services.AddAutoMapper(typeof(UserModel));
builder.Services.AddAutoMapper(typeof(PontajModel));

// Services
builder.Services.AddScoped<IAuth, AuthenticationService>();
builder.Services.AddScoped<IAudit, AuditService>();

// App
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
