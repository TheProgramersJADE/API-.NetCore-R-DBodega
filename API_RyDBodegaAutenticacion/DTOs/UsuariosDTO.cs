using API_RyDBodegaAutenticacion.Models;

namespace API_RyDBodegaAutenticacion.DTOs
{
    public class UsuariosResponse
    {
        public int Id { get; set; }

        public string CorreoElectronico { get; set; } = null!;

        public string? Direccion { get; set; }

        public string NombreCompleto { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Telefono { get; set; }

        public string Username { get; set; } = null!;

        public int IdRol { get; set; }

        public int Status { get; set; }

    }

    public class UsuariosRequest
    {
        public int Id { get; set; }

        public string CorreoElectronico { get; set; } = null!;

        public string? Direccion { get; set; }

        public string NombreCompleto { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Telefono { get; set; }

        public string Username { get; set; } = null!;

        public int IdRol { get; set; }

        public int Status { get; set; }
    }
}
