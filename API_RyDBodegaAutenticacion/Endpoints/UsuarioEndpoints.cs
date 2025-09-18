using API_RyDBodegaAutenticacion.DTOs;
using API_RyDBodegaAutenticacion.Services.Rol;
using API_RyDBodegaAutenticacion.Services.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_RyDBodegaAutenticacion.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/users").WithTags("Users");

            group.MapGet("/", async (IUsuarioService usuarioService) =>
            {
                var users = await usuarioService.GetUsuario();
                //codigo 200 OK "Correcto"
                return Results.Ok(users);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener todos los usuarios",
                Description = "Muestra una lista de usuarios"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });

            group.MapGet("/{id}", async (int id, IUsuarioService usuarioService) =>
            {
                var users = await usuarioService.GetUsuarioById(id);
                if (users == null)
                    //codigo 404 Not Found "No encontrado"
                    return Results.NotFound();
                else
                    //codigo 200 OK "Correcto"
                    return Results.Ok(users);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener un usuario por ID",
                Description = "Muestra un usuario específico según su ID"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });

            group.MapPost("/", async (UsuariosRequest user, IUsuarioService usuarioService) =>
            {
                if (user == null)
                    //codigo 400 Bad Request "Solicitud incorrecta"
                    return Results.BadRequest();

                await usuarioService.PostUsuario(user);

                return Results.Created($"/api/users/{user.Id}", user);

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear un nuevo usuario",
                Description = "Agrega un nuevo usuario a la base de datos"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });


            group.MapPut("/{id}", async (int id, UsuariosRequest user, IUsuarioService usuarioService) =>
            {
                var result = await usuarioService.PutUsuario(user, id);
                if (result == -1)
                    //codigo 404 Not Found "No encontrado"
                    return Results.NotFound();
                else
                    //codigo 204 No Content "Sin contenido"
                    return Results.NoContent();

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar usuario",
                Description = "Actualiza un usuario existente"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });

            group.MapDelete("/{id}", async (int id, IUsuarioService usuarioService) =>
            {
                var result = await usuarioService.DeleteUsuario(id);
                if (result == -1)
                    //codigo 404 Not Found "No encontrado"
                    return Results.NotFound();
                else
                    //codigo 204 No Content "Sin contenido"
                    return Results.NoContent();
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Eliminar usuario",
                Description = "Elimina un usuario existente"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });


            group.MapPost("/login", async (CredencialesRequest credenciales, IUsuarioService usuarioService, IConfiguration config) =>
            {
                var login = await usuarioService.Login(credenciales);

                if (login is null)
                    //codigo 401 Unauthorized "No autorizado"
                    return Results.Unauthorized();
                else
                {
                    var jwtSettings = config.GetSection("JwtSetting");
                    var secretKey = jwtSettings.GetValue<string>("SecretKey");
                    var issuer = jwtSettings.GetValue<string>("Issuer");
                    var audience = jwtSettings.GetValue<string>("Audience");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(secretKey);

                    var roleName = login.IdRol switch
                    {
                        1 => "Administrador",
                        2 => "SupervisorBodega",
                        _ => "usuario" // Por defecto, si el ID no se reconoce
                    };

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                       {
                            new Claim(ClaimTypes.Name, credenciales.Username),
                            new Claim(ClaimTypes.Role, roleName),
                             // Opcional: También puedes agregar el RoleId si lo necesitas
                             new Claim("rolid", login.IdRol.ToString())
                       }),
                        Expires = DateTime.UtcNow.AddHours(8),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    //Crear el token usando aparámetros definidos
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    //Convertir el token a string
                    var jwt = tokenHandler.WriteToken(token);

                    return Results.Ok(jwt);
                }
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Iniciar sesión",
                Description = "Autentica a un usuario y devuelve un token JWT"
            });

        }
    }
}
