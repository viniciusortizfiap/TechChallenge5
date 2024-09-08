using AutoMapper;
using TechChallenge5.Domain.DTO.Ativo;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.AutoMapper
{
    public class AtivoProfile : Profile
    {
        public AtivoProfile()
        {
            CreateMap<CadastrarAtivoDTO, AtivoEntity>().ReverseMap();
        }

    }
}
