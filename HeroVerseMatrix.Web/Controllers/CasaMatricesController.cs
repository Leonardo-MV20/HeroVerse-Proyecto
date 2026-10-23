using AutoMapper;
using HeroVerseMatrix.Application.Features.CasaMatrix;
using HeroVerseMatrix.Application.Features.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using HeroVerseMatrix.Web.ViewModels;
using HeroVerseMatrix.Web.ViewModels.CasaMatrix;

namespace HeroVerseMatrix.Web.Controllers
{
    public class CasaMatricesController : Controller
    {
        private readonly ICasaMatrixService _service;
        private readonly IMapper _mapper;

        public CasaMatricesController(ICasaMatrixService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var casas = await _service.GetAll();
            
            var model = _mapper.Map<IEnumerable<CasaMatrixViewModel>>(casas);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detalle(Guid? id)
        {
            if(!id.HasValue|| id.Value == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            var casa = await _service.GetById(id.Value);

            if(casa ==null)
            {
                return HttpNotFound("Casa Matrix not found.");
            }
            var model = _mapper.Map<CasaMatrixDetalleViewModel>(casa);
            return View(model);
        }

        //---------------- editar get
        [HttpGet]
        public async Task<ActionResult> Editar(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            var casa = await _service.GetById(id.Value);

            if (casa == null)
            {
                return HttpNotFound("Casa matriz not found.");
            }

            var model =_mapper.Map<ActualizarCasaMatrixViewModel>(casa);

            return View(model);
        }


        //----- post del editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Guid id, ActualizarCasaMatrixViewModel model)
        {
            if (id == Guid.Empty || id != model.Id)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var request = _mapper.Map<CasaMatrixRequest>(model);
                var resultado = await _service.Update(request);
                if (!resultado.IsValid)
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    return View(model);
                }

                TempData["SuccessMessage"] = "Casa matriz updated successfully.";

                return RedirectToAction(
                    "Detalle", new { id = model.Id });
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Ocurrió un error al actualizar la casa matriz.");

                return View(model);
            }
        }

        //------------ get de eliminar
        [HttpGet]
        public async Task<ActionResult> Eliminar(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            var casa = await _service.GetById(id.Value);

            if (casa == null)
            {
                return HttpNotFound("Casa matriz not found");
            }
            var model =_mapper.Map<CasaMatrixDetalleViewModel>(casa);

            return View(model);
        }


        // --------------post eliminar

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarEliminar(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID mismatch.");
            }

            try
            {
                var casa = await _service.GetById(id);
                if (casa == null)
                {
                    return HttpNotFound("Casa matriz not found");
                }

                var resultado = await _service.Delete(id);

                if (!resultado.IsValid)
                {
                    TempData["Error"] = string.Join(" ",resultado.Errors.Select(error => error.ErrorMessage));
                    return RedirectToAction("Index");
                }

                TempData["Exito"] ="Casa Matrix success delete";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error al eliminar la casa matriz.";
                return RedirectToAction("Index");
            }
        }

        //---------------- crear get
        [HttpGet]
        public ActionResult Crear()
        {
            return View(new CrearCasaMatrixViewModel());
        }

        //---------------- crear post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(CrearCasaMatrixViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var request =_mapper.Map<CasaMatrixRequest>(model);
                var resultado = await _service.Insert(request);

                if (!resultado.IsValid)
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                    return View(model);
                }

                TempData["Exito"] ="La casa matriz se creó correctamente.";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,"Ocurrió un error al crear la casa matriz.");
                return View(model);
            }
        }
    }
}