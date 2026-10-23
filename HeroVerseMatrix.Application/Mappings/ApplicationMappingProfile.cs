using AutoMapper;
using HeroVerseMatrix.Application.Features.Personaje;
using HeroVerseMatrix.Application.Features.CasaMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonajeEntity = HeroVerseMatrix.Domain.Entities.Personaje;
using CasaMatrixEntity = HeroVerseMatrix.Domain.Entities.CasaMatrix;
using HeroVerseMatrix.Domain.Entities;

namespace HeroVerseMatrix.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            // Personaje mappings: Entity -> Dto
            CreateMap<PersonajeEntity, PersonajeDto>()
                .ForMember(dest => dest.TipoPersonaje, src => src.MapFrom(s => s.Tipo))
                .ForMember(dest => dest.AlterEgo, src => src.MapFrom(s => s.AlterEgo.NombreCompleto))
                .ForMember(dest => dest.CasaMatrix, src => src.MapFrom(s => s.CasaMatrix.Nombre));

            // Personaje mappings: Request -> Entity
            CreateMap<PersonajeRequest, PersonajeEntity>()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.Tipo, src => src.MapFrom(s => s.TipoPersonaje))
                .ForMember(dest => dest.AlterEgo.Nombre, src => src.MapFrom(s => s.AlterEgoNombre))
                .ForMember(dest => dest.AlterEgo.Apellido, src => src.MapFrom(s => s.AlterEgoApellidos))
                .ForMember(dest => dest.CasaMatrix, src => src.Ignore())
                .ForMember(dest => dest.Imagen, src => src.Ignore())
                .ForMember(dest => dest.ImagenContentType, src => src.Ignore());

            CreateMap<PersonajeEntity, PersonajeDetalle>()
                .ForMember(dest => dest.TipoPersonaje, src => src.MapFrom(s => s.Tipo))
                .ForMember(dest => dest.AlterEgo_Nombre, src => src.MapFrom(s => s.AlterEgo.Nombre))
                .ForMember(dest => dest.AlterEgo_Apellido, src => src.MapFrom(s => s.AlterEgo.Apellido))
                .ForMember(dest => dest.CasaMatrizId, src => src.MapFrom(s => s.CasaMatrix.Id))
                .ForMember(dest => dest.CasaMatriz, src => src.MapFrom(s => s.CasaMatrix.Nombre));

            // CasaMatrix mappings
            CreateMap<CasaMatrix, CasaMatrixDto>();
            CreateMap<CasaMatrixDto, CasaMatrix>();
            CreateMap<CasaMatrixRequest, CasaMatrix>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
