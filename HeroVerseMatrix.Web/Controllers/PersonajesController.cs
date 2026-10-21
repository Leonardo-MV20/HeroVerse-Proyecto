using AutoMapper;
using HeroVerseMatrix.Application.Features.Personaje;
using HeroVerseMatrix.Application.Features.Personaje.Contratos;
using HeroVerseMatrix.Web.ViewModels.Personajes;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HeroVerseMatrix.Web.Controllers
{
    public class PersonajesController : Controller
    {
        private readonly IPersonajeService _service;
        private readonly IMapper _mapper;

        public PersonajesController(IPersonajeService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var personajes = await _service.GetAll();
            var model = _mapper.Map<IEnumerable<PersonajeViewModel>>(personajes);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detalle(Guid? id) 
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            var personaje = await _service.GetById(id.Value);
            if (personaje == null)
            {
                return HttpNotFound("Personaje not found.");
            }

            var model = _mapper.Map<PersonajeViewModel>(personaje);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Editar(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            var personaje = await _service.GetDetailById(id.Value);
            if (personaje == null)
            {
                return HttpNotFound("Personaje not found.");
            }

            var model = _mapper.Map<EditarPersonajeFormModel>(personaje);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(Guid id, EditarPersonajeFormModel model)
        {
            if (id != model.Id)
            {
                return new HttpStatusCodeResult(422, "Unprocessable Entity: ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var request = _mapper.Map<PersonajeRequest>(model);
                await _service.Update(request);
                TempData["SuccessMessage"] = "Personaje updated successfully.";
                return RedirectToAction("Detalle", new { id = model.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the personaje: {ex.Message}");
                return View(model);
            }
        }
    }
}