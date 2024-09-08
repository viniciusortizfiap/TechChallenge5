using AutoMapper;
using TechChallenge5.Domain.DTO.Transacao;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.AutoMapper
{
    public class TransacaoProfile : Profile
    {
        public TransacaoProfile()
        {
            CreateMap<TransacaoOutputDto, TransacaoEntity>().ReverseMap();
            CreateMap<TransacaoInputDto, TransacaoEntity>();
        }

    }
}
