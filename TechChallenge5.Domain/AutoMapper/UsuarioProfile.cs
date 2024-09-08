using AutoMapper;
using TechChallenge5.Domain.DTO.Usuario;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CadastrarUsuarioDTO, UsuarioEntity>().ReverseMap();
        }
    }
}
