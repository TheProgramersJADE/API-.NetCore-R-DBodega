using AutoMapper;

namespace API_RyDBodegaAutenticacion.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Modelo a DTO  
            CreateMap<Models.Role, DTOs.RoleResponse>();
            CreateMap<Models.Usuario, DTOs.UsuariosResponse>();
            CreateMap<Models.Usuario, DTOs.CredencialesDTO>();

            //DTO a Modelo
            CreateMap<DTOs.RoleRequest, Models.Role>();
            CreateMap<DTOs.UsuariosRequest, Models.Usuario>();
            CreateMap<Models.Usuario, DTOs.CredencialesDTO>();
        }
    }
}
