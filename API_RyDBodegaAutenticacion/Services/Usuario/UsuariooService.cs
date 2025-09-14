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
              u => u.Username == credenciales.Username);

            if (user == null)
            {
                return null;
            }

            // Asegúrate de que los argumentos estén en este orden
            if (!BCrypt.Net.BCrypt.Verify(credenciales.Password, user.Password))
            {
                return null; // La contraseña no coincide
            }

            // Si todo es correcto, el login es exitoso
            var userResponse = new CredencialesResponse
            {
                IdUser = user.Id,
                Username = user.Username,
                IdRol = user.IdRol
            };
            return userResponse;
        }

        public async Task<int> PostUsuario(UsuariosRequest usuario)
        {
            var personalRequest = _mapper.Map<UsuariosRequest, API_RyDBodegaAutenticacion.Models.Usuario>(usuario);

            // Hashea la contraseña usando BCrypt
            personalRequest.Password = BCrypt.Net.BCrypt.HashPassword(personalRequest.Password);

            await _dbContext.Usuarios.AddAsync(personalRequest);
            await _dbContext.SaveChangesAsync();
            return personalRequest.Id;

        }

        public async Task<int> PutUsuario(UsuariosRequest usuario, int id)
        {
            var entity = await _dbContext.Usuarios.FindAsync(id);
            if (entity == null)
            {
                return -1;
            }

            // Almacena la contraseña actual antes del mapeo
            var currentPassword = entity.Password;
            _mapper.Map(usuario, entity);

            // Verifica si la nueva contraseña no es nula o vacía
            if (!string.IsNullOrEmpty(usuario.Password))
            {
                // Si hay una nueva contraseña, la hashea y actualiza la entidad
                entity.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            }
            else
            {
                // Si la contraseña es nula o vacía en la solicitud,
                // restablece la contraseña original para que no se borre
                entity.Password = currentPassword;
            }

            // Actualiza la entidad en la base de datos
            _dbContext.Usuarios.Update(entity);

            return await _dbContext.SaveChangesAsync();

        }
    }
}
