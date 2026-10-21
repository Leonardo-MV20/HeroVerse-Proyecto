using System.Web.Mvc;
using FluentValidation.Results;
namespace HeroVerseMatrix.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddFluentValidationErrors(this ModelStateDictionary
       modelState,
        ValidationResult validation)
        {
            foreach (var error in validation.Errors)
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

    }
}