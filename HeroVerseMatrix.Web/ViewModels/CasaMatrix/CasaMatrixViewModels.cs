using System;
using System.ComponentModel.DataAnnotations;

namespace HeroVerseMatrix.Web.ViewModels.CasaMatrix
{
    // Para el listado Index
    public class CasaMatrixViewModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    // Para la vista de Detalle
    public class CasaMatrixDetalleViewModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    // Para el formulario de Crear
    public class CrearCasaMatrixViewModel
    {
        [Required(ErrorMessage = "El nombre de la casa matriz es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripcion no puede superar los 250 caracteres.")]
        [Display(Name = "descripcion")]
        public string Descripcion { get; set; }
    }

    // Para el formulario de Editar/Actualizar
    public class ActualizarCasaMatrixViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre de la casa matriz es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripcion no puede superar los 250 caracteres.")]
        [Display(Name = "descripcion")]
        public string Descripcion { get; set; }
    }
}