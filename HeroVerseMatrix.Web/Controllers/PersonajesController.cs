using AutoMapper;
using FluentValidation.Results;
using HeroVerseMatrix.Application.Features.Contratos;
using HeroVerseMatrix.Application.Features.Personaje;
using HeroVerseMatrix.Application.Features.Personaje.Contratos;
using HeroVerseMatrix.Web.ViewModels.Personajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HeroVerseMatrix.Web.Controllers
{
	public class PersonajesController : Controller
	{
		// Campos del formulario que pueden recibir un error de validación.
		// El resto de errores se muestran en el resumen general (string.Empty).
		private static readonly HashSet<string> CamposFormulario = new HashSet<string>
		{
			"Nombre", "CasaMatrizId", "AlterEgoNombre", "AlterEgoApellido"
		};

		// El validador de Application usa otros nombres de propiedad que el formulario.
		private static readonly Dictionary<string, string> EquivalenciaCampos = new Dictionary<string, string>
		{
			{ "CasaMatrixId", "CasaMatrizId" },
			{ "AlterEgoApellidos", "AlterEgoApellido" }
		};

		private readonly IPersonajeService _service;
		private readonly ICasaMatrixService _casaService;
		private readonly IMapper _mapper;

		public PersonajesController(
			IPersonajeService service,
			ICasaMatrixService casaService,
			IMapper mapper)
		{
			this._service = service;
			this._casaService = casaService;
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

		// ---------------- crear get
		[HttpGet]
		public async Task<ActionResult> Crear()
		{
			var model = new CrearPersonajeFormModel();
			await CargarCasasAsync(model);
			return View(model);
		}

		// ---------------- crear post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Crear(CrearPersonajeFormModel model)
		{
			if (model == null)
			{
				return new HttpStatusCodeResult(400, "Bad Request: model is required.");
			}

			if (!ModelState.IsValid)
			{
				await CargarCasasAsync(model);
				return View(model);
			}

			try
			{
				var request = _mapper.Map<PersonajeRequest>(model);
				var resultado = await _service.Insert(request);

				if (!resultado.IsValid)
				{
					AgregarErrores(resultado);
					await CargarCasasAsync(model);
					return View(model);
				}

				TempData["Exito"] = "El personaje se creó correctamente.";
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el personaje.");
				await CargarCasasAsync(model);
				return View(model);
			}
		}

		// ---------------- editar get
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
			await CargarCasasAsync(model);
			return View(model);
		}

		// ---------------- editar post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Editar(Guid id, EditarPersonajeFormModel model)
		{
			if (id == Guid.Empty || model == null || id != model.Id)
			{
				return new HttpStatusCodeResult(400, "Bad Request: ID mismatch.");
			}

			if (await _service.GetById(id) == null)
			{
				return HttpNotFound("Personaje not found.");
			}

			if (!ModelState.IsValid)
			{
				await CargarCasasAsync(model);
				return View(model);
			}

			try
			{
				var request = _mapper.Map<PersonajeRequest>(model);
				var resultado = await _service.Update(request);

				if (!resultado.IsValid)
				{
					AgregarErrores(resultado);
					await CargarCasasAsync(model);
					return View(model);
				}

				TempData["Exito"] = "El personaje se actualizó correctamente.";
				return RedirectToAction("Detalle", new { id = model.Id });
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Ocurrió un error al actualizar el personaje.");
				await CargarCasasAsync(model);
				return View(model);
			}
		}

		// ---------------- eliminar get
		[HttpGet]
		public async Task<ActionResult> Eliminar(Guid? id)
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

		// ---------------- eliminar post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ConfirmarEliminar(Guid id)
		{
			if (id == Guid.Empty)
			{
				return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
			}

			try
			{
				var resultado = await _service.Delete(id);

				if (!resultado.IsValid)
				{
					TempData["Error"] = string.Join(" ", resultado.Errors.Select(e => e.ErrorMessage));
					return RedirectToAction("Index");
				}

				TempData["Exito"] = "El personaje se eliminó correctamente.";
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				TempData["Error"] = "Ocurrió un error al eliminar el personaje.";
				return RedirectToAction("Index");
			}
		}

		// ---------------- helpers

		/// <summary>
		/// Llena el DropDownList de casas matrices y deja seleccionada la casa actual del modelo.
		/// </summary>
		private async Task CargarCasasAsync(PersonajeFormModelBase model)
		{
			var casas = await _casaService.GetAll();

			model.CasaMatrizOptions = casas
				.Select(c => new SelectListItem
				{
					Value = c.Id.ToString(),
					Text = c.Nombre,
					Selected = model.CasaMatrizId.HasValue && model.CasaMatrizId.Value == c.Id
				})
				.ToList();
		}

		/// <summary>
		/// Pasa los errores de FluentValidation (capa Application) al ModelState,
		/// traduciendo el nombre de la propiedad al del campo del formulario.
		/// </summary>
		private void AgregarErrores(ValidationResult resultado)
		{
			foreach (var error in resultado.Errors)
			{
				string campo;
				if (!EquivalenciaCampos.TryGetValue(error.PropertyName, out campo))
				{
					campo = error.PropertyName;
				}

				if (!CamposFormulario.Contains(campo))
				{
					campo = string.Empty;
				}

				ModelState.AddModelError(campo, error.ErrorMessage);
			}
		}
	}
}
