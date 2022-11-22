using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.Models;
using Rentacar.Servicios;
using System.Reflection.Metadata.Ecma335;

namespace Rentacar.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly IMapper mapper;
        private readonly IServicioMarcas marcas;
        private readonly IServicioVehiculos vehiculos;
        public VehiculoController(IMapper automapper, IServicioMarcas servicioMarcas, IServicioVehiculos servicioVehiculos)
        {
            mapper = automapper;
            marcas = servicioMarcas;
            vehiculos = servicioVehiculos;
        }

        public async Task<IActionResult> Index()
        {
            var Vehiculo = await vehiculos.Obtener();
            return View(Vehiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new VehiculoCreacionViewModel();
            modelo.MarcaViewModel = await ObtenerMarcas();
            return View (modelo);
        }


        [HttpPost]
        public async Task<IActionResult> Crear(VehiculoCreacionViewModel Vehiculo)
        {
            if (!ModelState.IsValid)
            {
                Vehiculo.MarcaViewModel = await ObtenerMarcas();
                return View(Vehiculo);
            }

            var Existe = await vehiculos.ExisteCrear(Vehiculo.Placa);

            if (Existe)
            {
                ModelState.AddModelError(nameof(Vehiculo.Placa), $"la placa {Vehiculo.Placa} ya se encuentra registrada en el sistema con otro vehiculo");
                Vehiculo.MarcaViewModel = await ObtenerMarcas();
                return View(Vehiculo);
            }

            await vehiculos.Crear(Vehiculo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var vehiculo = await vehiculos.ObtenerPorId(id);

            if(vehiculo is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

            var modelo = mapper.Map<VehiculoCreacionViewModel>(vehiculo);

            modelo.MarcaViewModel = await ObtenerMarcas();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(VehiculoCreacionViewModel Vehiculo)
        {
            var vehiculo = await vehiculos.ObtenerPorId(Vehiculo.Id);

            if(vehiculo is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

            var Existe = await vehiculos.Existe(Vehiculo.Placa, Vehiculo.Id);

            if(Existe)
            {
                ModelState.AddModelError(nameof(Vehiculo.Placa), $"La placa {Vehiculo.Placa} Ya se encuentra registrada en el sistema con otro vehiculo");
                Vehiculo.MarcaViewModel = await ObtenerMarcas();
                return View(Vehiculo);
            }

            await vehiculos.Actualizar(Vehiculo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            var vehiculo = await vehiculos.ObtenerPorId(id);

            if(vehiculo is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

            return View(vehiculo);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarVehiculo(int id)
        {
            var vehiculo = await vehiculos.ObtenerPorId(id);

            if(vehiculo is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

            await vehiculos.Borrar(id);
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerMarcas()
        {
            var Marcas = await marcas.Obtener();
            return Marcas.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString()));
        }
    }
}
