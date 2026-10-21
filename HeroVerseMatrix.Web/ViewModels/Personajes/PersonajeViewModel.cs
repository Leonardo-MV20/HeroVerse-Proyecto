using System;

namespace HeroVerseMatrix.Web.ViewModels.Personajes
{
    public class PersonajeViewModel
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string CasaMatriz { get; set; }

        public bool EsHeroe { get; set; }


    }
}