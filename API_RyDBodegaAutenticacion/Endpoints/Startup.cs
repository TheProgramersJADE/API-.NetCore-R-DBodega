namespace API_RyDBodegaAutenticacion.Endpoints
{
    public static class Startup
    {
        public static void UseEndpoints(this WebApplication app) { 
            RolEndpoints.Add(app);
        }
    }
}
