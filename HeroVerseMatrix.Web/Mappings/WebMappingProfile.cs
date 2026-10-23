using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroVerseMatrix.Application.Features.Personaje;
using HeroVerseMatrix.Web.ViewModels.Personajes;
using HeroVerseMatrix.Domain.Entities;
using HeroVerseMatrix.Application.Features.CasaMatrix;
using HeroVerseMatrix.Web.ViewModels.CasaMatrix;

namespace HeroVerseMatrix.Web.Mappings
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<PersonajeDto, PersonajeViewModel>()
                .ForMember(dest => dest.CasaMatriz,
                            opt => opt.MapFrom(src => src.CasaMatrix))
                .ForMember(dest => dest.EsHeroe,
                            opt => opt.MapFrom(src => src.TipoPersonaje == TipoPersonaje.Heroe));

            CreateMap<PersonajeDetalle, EditarPersonajeFormModel>()
                .ForMember(dest => dest.AlterEgoNombre,
                            opt => opt.MapFrom(src => src.AlterEgo_Nombre))
                .ForMember(dest => dest.AlterEgoApellido,
                            opt => opt.MapFrom(src => src.AlterEgo_Apellido))
                .ForMember(dest => dest.EsHeroe,
                            opt => opt.MapFrom(src => src.TipoPersonaje == TipoPersonaje.Heroe))
                .ForMember(dest => dest.CasaMatrizOptions, opt => opt.Ignore());

            CreateMap<EditarPersonajeFormModel, PersonajeRequest>()
                .ForMember(dest => dest.TipoPersonaje,
                            opt => opt.MapFrom(src => src.EsHeroe ? TipoPersonaje.Heroe : TipoPersonaje.Villano))
                .ForMember(dest => dest.AlterEgoApellidos,
                            opt => opt.MapFrom(src => src.AlterEgoApellido))
                .ForMember(dest => dest.CasaMatrixId,
                            opt => opt.MapFrom(src => src.CasaMatrizId));

            // CasaMatrix Mappings
            CreateMap<CasaMatrixDto, CasaMatrixViewModel>();

            CreateMap<CasaMatrixDto, CasaMatrixDetalleViewModel>();

            CreateMap<CasaMatrixDto, ActualizarCasaMatrixViewModel>();

            CreateMap<CrearCasaMatrixViewModel, CasaMatrixRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ActualizarCasaMatrixViewModel, CasaMatrixRequest>();
        }
    }
}