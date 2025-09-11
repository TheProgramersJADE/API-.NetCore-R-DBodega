using API_RyDBodegaAutenticacion.Endpoints;
using API_RyDBodegaAutenticacion.Models;
using API_RyDBodegaAutenticacion.Services.Rol;
using API_RyDBodegaAutenticacion.Services.Usuario;
using API_RyDBodegaAutenticacion.Services.Usuarioo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RyDapibodegaAutenticacionContext>(

o=>o.UseSqlServer(builder.Configuration.GetConnectionString("RyDAPIBodegaAutenticacionConnection")) 
);

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddScoped<IRolServices, RolServices>();
builder.Services.AddScoped<IUsuarioService, UsuariooService>();

//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var secretKey = jwtSettings.GetValue<string>("SecretKey");

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(
//    options =>
//    {
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    }).AddJwtBearer(
//    options => { 
    
//        options.RequireHttpsMetadata = false;
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters { 
        
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
//            ValidAudience = jwtSettings.GetValue<string>("Audience"),
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
//        };
//    }
//    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint GET
app.MapGet("/hola", () =>
{
    return Results.Ok("¡Hola, tu API está funcionando!");
})
.WithName("ObtenerSaludo")
.WithTags("Ejemplo");

//app.UseAuthentication();
//app.UseAuthorization();

app.UseEndpoints();

app.Run();

