namespace API_RyDBodegaAutenticacion.DTOs
{
    public class RoleResponse
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public string NombreRol { get; set; } = null!;
    }

    public class RoleRequest
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public string NombreRol { get; set; } = null!;
    }
}
