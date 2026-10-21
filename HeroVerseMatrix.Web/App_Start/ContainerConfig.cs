using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using FluentValidation;
using HeroVerseMatrix.Application.Features.Personaje;
using HeroVerseMatrix.Application.Features.Personaje.Contratos;
using HeroVerseMatrix.Infrastructure.Data;
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

            builder.RegisterType<IPersonajeRepository>()
                .As<IPersonajeRepository>()
                .InstancePerRequest();

            builder.RegisterType<PersonajeService>()
                .As<IPersonajeService>()
                .InstancePerRequest();

            builder.RegisterType<PersonajeRequestValidator>()
                .As<IValidator<PersonajeRequest>>()
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}