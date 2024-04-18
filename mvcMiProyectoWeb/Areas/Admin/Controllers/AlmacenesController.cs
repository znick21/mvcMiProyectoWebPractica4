using Microsoft.AspNetCore.Mvc;
using mvcProyectoWeb.AccesoDatos.Data.Repository.IRepository;
using mvcProyectoWeb.Models;

namespace mvcMiProyectoWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlmacenesController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public AlmacenesController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                //logica para guardar en bd
                _contenedorTrabajo.Almacen.Add(almacen);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(almacen);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Almacen almacen = new Almacen();
            almacen = _contenedorTrabajo.Almacen.Get(id);
            if (almacen == null)
            {
                return NotFound();

            }
            return View(almacen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Almacen almacen)
        {

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Almacen.Update(almacen);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(almacen);
        }
        #region llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Almacen.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Almacen.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando almacen" });
            }
            _contenedorTrabajo.Almacen.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se borro la almacen" });
        }
        #endregion
    }
}
