using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeroVerseMatrix.Web.ViewModels.Personajes
{
    public abstract class PersonajeFormModelBase
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una casa matriz.")]
        [Display(Name = "Casa matriz")]
        public Guid? CasaMatrizId { get; set; }

        [Display(Name = "¿Es héroe?")]
        public bool EsHeroe { get; set; }

        [Required(ErrorMessage = "El alter ego es obligatorio.")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Alter ego")]
        public string AlterEgoNombre { get; set; }

        [Required(ErrorMessage = "El alter ego apellido es obligatorio.")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Alter ego Apellido")]
        public string AlterEgoApellido { get; set; }

        public IEnumerable<SelectListItem> CasaMatrizOptions { get; set; }
    }

    public sealed class EditarPersonajeFormModel : PersonajeFormModelBase
    {
        [Required]
        public Guid Id { get; set; }
    }

    public sealed class CrearPersonajeFormModel : PersonajeFormModelBase
    {

    }
}