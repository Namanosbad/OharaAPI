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
                .ForMember(dest => dest.NomeAutor, opt => opt.MapFrom(src => src.Autor != null ? src.Autor.Nome : "Autor não informado"));

            CreateMap<Autor, AutorResponse>()
                .ForMember(dest => dest.Livros, opt => opt.MapFrom(src => src.Livros.Select(l => l.Titulo).ToList()));

            CreateMap<LivroRequest, Livro>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid())) //
                .ForMember(dest => dest.Autor, opt => opt.Ignore()); 

            CreateMap<AutorRequest, Autor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
    }
    }
}
