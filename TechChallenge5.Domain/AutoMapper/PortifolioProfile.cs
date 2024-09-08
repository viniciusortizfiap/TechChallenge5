using AutoMapper;
using TechChallenge5.Domain.DTO.Portfolio;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.AutoMapper
{
    public class PortifolioProfile : Profile
    {
        public PortifolioProfile()
        {
            CreateMap<CadastrarPortifolioDTO, PortifolioEntity>().ReverseMap();
        }
    }
}
