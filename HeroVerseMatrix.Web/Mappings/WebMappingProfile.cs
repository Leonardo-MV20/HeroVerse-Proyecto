using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroVerseMatrix.Application.Features.Personaje;
using HeroVerseMatrix.Web.ViewModels.Personajes;
using HeroVerseMatrix.Domain.Entities;

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
                            opt => opt.MapFrom(src => src.TipoPersonaje == TipoPersonaje.Heroe));

            CreateMap<EditarPersonajeFormModel, PersonajeRequest>()
                .ForMember(dest => dest.TipoPersonaje,
                            opt => opt.MapFrom(src => src.EsHeroe ? TipoPersonaje.Heroe : TipoPersonaje.Villano));
        }
    }
}