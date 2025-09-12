using API_RyDBodegaAutenticacion.DTOs;
using API_RyDBodegaAutenticacion.Models;
using API_RyDBodegaAutenticacion.Services.Usuario;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_RyDBodegaAutenticacion.Services.Usuarioo
{
    public class UsuariooService : IUsuarioService
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

        public async Task<List<UsuariosResponse>> GetUsuario()
        {
            var user = await _dbContext.Usuarios.ToListAsync();
            var userList = _mapper.Map<List<API_RyDBodegaAutenticacion.Models.Usuario>, List<UsuariosResponse>>(user);
            return userList;
        }

        public async Task<UsuariosResponse> GetUsuarioById(int id)
        {
            var user = await _dbContext.Usuarios.FindAsync(id);
            var userResponse = _mapper.Map<API_RyDBodegaAutenticacion.Models.Usuario, UsuariosResponse>(user);
            return userResponse;
        }

        public async Task<CredencialesResponse> Login(CredencialesRequest credenciales)
        {
            var user = await _dbContext.Usuarios
                .FirstOrDefaultAsync(
                    u => u.Username == credenciales.Username
                    && u.Password == credenciales.Password);

            if (user == null)
                return null;

            var userResponse = _mapper.Map<API_RyDBodegaAutenticacion.Models.Usuario, CredencialesResponse>(user);
            return userResponse;
        }

        public async Task<int> PostUsuario(UsuariosRequest usuario)
        {
            var entity = _mapper.Map<UsuariosRequest, API_RyDBodegaAutenticacion.Models.Usuario>(usuario);
            await _dbContext.Usuarios.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> PutUsuario(UsuariosRequest usuario, int id)
        {
            var entity = await _dbContext.Usuarios.FindAsync(id);
            if (entity == null)
                return -1;

            entity.NombreCompleto = usuario.NombreCompleto;
            entity.CorreoElectronico = usuario.CorreoElectronico;
            entity.Direccion = usuario.Direccion;
            entity.Password = usuario.Password;
            entity.Telefono = usuario.Telefono;
            entity.Username = usuario.Username;
            entity.IdRol = usuario.IdRol;
            entity.Status = usuario.Status;

            _dbContext.Usuarios.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
