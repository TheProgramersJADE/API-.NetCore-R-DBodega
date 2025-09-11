using API_RyDBodegaAutenticacion.DTOs;
using API_RyDBodegaAutenticacion.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_RyDBodegaAutenticacion.Services.Credenciales
{
    public class CredencialesService : ICredencialesService
    {
        private readonly RyDapibodegaAutenticacionContext _dbContext;
        private readonly IMapper _mapper;

        public CredencialesService(RyDapibodegaAutenticacionContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CredencialesResponse> Login(CredencialesRequest credenciales)
        {
            var credencial = await _dbContext.Usuarios
                .FirstOrDefaultAsync(c => c.Username == credenciales.Username 
                && c.Password == credenciales.Password);

            var credencialResponse = _mapper.Map< API_RyDBodegaAutenticacion.Models.Usuario, CredencialesResponse>(credencial);
            return credencialResponse;
        }
    }
}
