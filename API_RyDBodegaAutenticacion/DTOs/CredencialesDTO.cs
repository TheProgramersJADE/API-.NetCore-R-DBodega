namespace API_RyDBodegaAutenticacion.DTOs
{
    public class CredencialesResponse
    {

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
    public class CredencialesRequest
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }

}
