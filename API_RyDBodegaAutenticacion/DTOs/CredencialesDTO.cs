namespace API_RyDBodegaAutenticacion.DTOs
{
    public class CredencialesResponse
    {

        public int IdUser { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int IdRol { get; set; }


    }
    public class CredencialesRequest
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;



    }

}
