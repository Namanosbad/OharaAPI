using AutoMapper;
using Ohara.API.Application.Requests;
using Ohara.API.Application.Responses;
using Ohara.API.Domain.Entities;

namespace Ohara.API.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Livro, LivroResponse>()
                .ForMember(dest => dest.NomeAutor, opt => opt.MapFrom(src => src.NomeAutor != null ? src.NomeAutor.Nome : null));

            CreateMap<Autor, AutorResponse>();

            // REQUEST -> ENTIDADE (Entrada)
            CreateMap<LivroRequest, Livro>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.NomeAutor, opt => opt.Ignore());

            CreateMap<AutorRequest, Autor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
