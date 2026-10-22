using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using FluentValidation;
using HeroVerseMatrix.Application.Features.CasaMatrix;
using HeroVerseMatrix.Application.Features.Contratos;
using HeroVerseMatrix.Application.Features.Personaje;
using HeroVerseMatrix.Application.Features.Personaje.Contratos;
using HeroVerseMatrix.Infrastructure.Data;
using HeroVerseMatrix.Infrastructure.Persistence.CasaMatrix;
using HeroVerseMatrix.Infrastructure.Persistence.Personaje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HeroVerseMatrix.Web.App_Start
{
    public static class ContainerConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<HeroVerseContext>()
                .InstancePerRequest();

            builder.RegisterInstance(AutoMapperConfig.Mapper)
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterType<PersonajeRepository>()
            .As<IPersonajeRepository>()
            .InstancePerRequest();

            builder.RegisterType<PersonajeService>()
                .As<IPersonajeService>()
                .InstancePerRequest();

            builder.RegisterType<PersonajeRequestValidator>()
                .As<IValidator<PersonajeRequest>>()
                .InstancePerRequest();

            builder.RegisterType<CasaMatrixRepository>()
            .As<ICasaMatrixRepository>()
            .InstancePerRequest();

            builder.RegisterType<CasaMatrixService>()
                .As<ICasaMatrixService>()
                .InstancePerRequest();

            builder.RegisterType<CasaMatrixRequestValidator>()
                .As<IValidator<CasaMatrixRequest>>()
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}