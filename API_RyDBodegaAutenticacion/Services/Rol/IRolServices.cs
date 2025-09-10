using API_RyDBodegaAutenticacion.DTOs;

namespace API_RyDBodegaAutenticacion.Services.Rol
{
    public interface IRolServices
    {
        Task<int> PostRol(RoleRequest role);
        Task<List<RoleResponse>> GetRol();
        Task<RoleResponse> GetRolById(int id);
        Task<int> PutRol(RoleRequest role, int id);
        Task<int> DeleteRol(int id);
    }
}
