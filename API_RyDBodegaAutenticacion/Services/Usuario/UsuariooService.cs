using API_RyDBodegaAutenticacion.DTOs;
using API_RyDBodegaAutenticacion.Models;
using API_RyDBodegaAutenticacion.Services.Usuario;
using AutoMapper;

namespace API_RyDBodegaAutenticacion.Services.Usuarioo
{
    public class UsuariooService : IUsuarioService // Fixed missing colon to implement the interface
    {
        private readonly RyDapibodegaAutenticacionContext _dbContext;
        private readonly IMapper _mapper;

        public UsuariooService(RyDapibodegaAutenticacionContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> DeleteUsuario(int id)
        {
            var user = await _dbContext.Usuarios.FindAsync(id);
            if (user == null) 
                return -1;
            _dbContext.Usuarios.Remove(user);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<List<UsuariosResponse>> GetUsuario()
        {
            throw new NotImplementedException();
        }

        public async Task<UsuariosResponse> GetUsuarioById(int id)
        {
            var user = await _dbContext.Usuarios.FindAsync(id);
            var userResponse = _mapper.Map<API_RyDBodegaAutenticacion.Models.Usuario, UsuariosResponse>(user);
            return userResponse;
        }

        public Task<int> PostUsuario(UsuariosRequest usuario)
        {
            throw new NotImplementedException();
        }

        public Task<int> PutUsuario(UsuariosRequest usuario, int id)
        {
            throw new NotImplementedException();
        }
    }
}
