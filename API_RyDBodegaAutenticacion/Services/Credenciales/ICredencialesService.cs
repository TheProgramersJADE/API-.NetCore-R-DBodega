using API_RyDBodegaAutenticacion.DTOs;

namespace API_RyDBodegaAutenticacion.Services.Credenciales
{
    public interface ICredencialesService
    {
        Task<CredencialesResponse> Login (CredencialesRequest credenciales);
    }
}
