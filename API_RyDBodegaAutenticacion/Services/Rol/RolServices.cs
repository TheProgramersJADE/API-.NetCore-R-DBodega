using API_RyDBodegaAutenticacion.DTOs;
using API_RyDBodegaAutenticacion.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_RyDBodegaAutenticacion.Services.Rol
{
    public class RolServices : IRolServices
    {
        private readonly RyDapibodegaAutenticacionContext _dbContext;
        private readonly IMapper _mapper;

        public RolServices(RyDapibodegaAutenticacionContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> DeleteRol(int id)
        {
            var rol = await _dbContext.Roles.FindAsync(id);
            if (rol == null) 
                return -1;
            _dbContext.Roles.Remove(rol);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<RoleResponse>> GetRol()
        {
            var rol = await _dbContext.Roles.ToListAsync();
            var rolList = _mapper.Map<List<Role>, List<RoleResponse>>(rol);
            return rolList;
        }

        public async Task<RoleResponse> GetRolById(int id)
        {
            var rol = await _dbContext.Roles.FindAsync(id);
            var rolResponse = _mapper.Map< Role, RoleResponse>(rol);

            return rolResponse;
        }

        public async Task<int> PostRol(RoleRequest role)
        {
            var rolRequest = _mapper.Map<RoleRequest, Role>(role);
            await _dbContext.Roles.AddAsync(rolRequest);
            await _dbContext.SaveChangesAsync();

            return rolRequest.Id;
        }

        public async Task<int> PutRol(RoleRequest role, int id)
        {
            var rol = await _dbContext.Roles.FindAsync(id);
            if (rol == null)
                return -1;

            rol.NombreRol = role.NombreRol;
            rol.Descripcion = role.Descripcion;

            _dbContext.Roles.Update(rol);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
