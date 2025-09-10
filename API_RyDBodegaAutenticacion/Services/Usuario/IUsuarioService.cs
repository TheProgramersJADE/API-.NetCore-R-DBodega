using API_RyDBodegaAutenticacion.DTOs;

namespace API_RyDBodegaAutenticacion.Services.Usuario
{
    public interface IUsuarioService
    {
        Task<int> PostUsuario(UsuariosRequest usuario);
        Task<List<UsuariosResponse>> GetUsuario();
        Task<UsuariosResponse> GetUsuarioById(int id);
        Task<int> PutUsuario(UsuariosRequest usuario, int id);
        Task<int> DeleteUsuario(int id);
    }
}
