using Microsoft.AspNetCore.Mvc;
using Rentacar.Models;
using Rentacar.Servicios;

namespace Rentacar.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IServicioMarcas marcas;
        public MarcaController(IServicioMarcas servicioMarcas)
        {
            marcas = servicioMarcas;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Marcas = await marcas.Obtener();
            return View(Marcas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(MarcaViewModel Marcas)
        {
            if(!ModelState.IsValid)
            {
                return View(Marcas);
            }

            var Existe = await marcas.ExisteCrear(Marcas.Descripcion);

            if(Existe)
            {
                ModelState.AddModelError(nameof(Marcas.Descripcion), $"La marca {Marcas.Descripcion} ya se encuentra registrada");
                return View(Marcas);
            }

            await marcas.Crear(Marcas);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Editar(int id)
        {

            var Marca = await marcas.ObtenerPorId(id);

            if (Marca is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(Marca);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(MarcaViewModel Marca)
        {
            var Marcas = await marcas.ObtenerPorId(Marca.Id);

            if (Marcas is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var Existe = await marcas.Existe(Marca.Descripcion, Marca.Id);

            if (Existe)
            {
                ModelState.AddModelError(nameof(Marca.Descripcion), $"La marca {Marca.Descripcion} ya se encuentra registrada");
                return View(Marca);
            }

            await marcas.Actualizar(Marca);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var Marca = await marcas.ObtenerPorId(id);

            if (Marca is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(Marca);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarMarca(int id)
        {
            var Marca = await marcas.ObtenerPorId(id);

            if (Marca is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await marcas.Borrar(id);
            return RedirectToAction("Index");
        }


    }
}
