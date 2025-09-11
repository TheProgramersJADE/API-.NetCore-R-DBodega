using API_RyDBodegaAutenticacion.DTOs;
using API_RyDBodegaAutenticacion.Services.Rol;
using API_RyDBodegaAutenticacion.Services.Usuario;
using Microsoft.OpenApi.Models;

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
            });

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
            });

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
            });


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
            });

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
            });

        }
    }
}
