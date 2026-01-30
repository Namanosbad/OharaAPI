using AutoMapper;
using Ohara.API.Domain.Entities;
using Ohara.API.Shared.Requests;
using Ohara.API.Shared.Responses;

namespace Ohara.API.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Livro, LivroResponse>()
            .ForMember(dest => dest.NomeAutor,
               opt => opt.MapFrom(src => src.Autor != null ? src.Autor.Nome : null));

            CreateMap<Autor, AutorResponse>();

            // REQUEST -> ENTIDADE (Entrada)
            CreateMap<LivroRequest, Livro>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Autor, opt => opt.Ignore())
                .ForMember(dest => dest.AutorId, opt => opt.Ignore());

            CreateMap<AutorRequest, Autor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
