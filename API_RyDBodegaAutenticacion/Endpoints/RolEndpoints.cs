using API_RyDBodegaAutenticacion.DTOs;
using API_RyDBodegaAutenticacion.Services.Rol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace API_RyDBodegaAutenticacion.Endpoints
{
    public static class RolEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/roles").WithTags("Roles");

            group.MapGet("/", async (IRolServices rolServices) =>
            {
                var roles = await rolServices.GetRol();
                //codigo 200 OK "Correcto"
                return Results.Ok(roles);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener todos los roles",
                Description = "Muestra una lista de roles"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });


            group.MapGet("/{id}", async (int id, IRolServices rolServices) =>
            {
                var roles = await rolServices.GetRolById(id);
                if (roles == null)
                    //codigo 404 Not Found "No encontrado"
                    return Results.NotFound();
                else
                    //codigo 200 OK "Correcto"
                    return Results.Ok(roles);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener un rol por ID",
                Description = "Muestra un rol específico según su ID"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });


            group.MapPost("/", async (RoleRequest role, IRolServices rolServices) =>
            {
                if (role == null)
                    //codigo 400 Bad Request "Solicitud incorrecta"
                    return Results.BadRequest();
                
                await rolServices.PostRol(role);

                return Results.Created($"/api/roles/{role.Id}", role);

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear un nuevo rol",
                Description = "Agrega un nuevo rol a la base de datos"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });


            group.MapPut("/{id}", async (int id, RoleRequest role, IRolServices rolServices) =>
            {
                var result = await rolServices.PutRol(role, id);
                if (result == -1)
                    //codigo 404 Not Found "No encontrado"
                    return Results.NotFound();
                else
                    //codigo 204 No Content "Sin contenido"
                    return Results.NoContent();

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar rol",
                Description = "Actualiza un rol existente"
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" });
        }
    }
}
