using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudDepartamentos2023.Models;
using MvcCoreApiCrudDepartamentos2023.Services;

namespace MvcCoreApiCrudDepartamentos2023.Controllers
{
    public class DepartamentosController : Controller
    {
        private ServiceDepartamentos service;

        public DepartamentosController(ServiceDepartamentos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Departamento> departamentos = 
                await this.service.GetDepartamentosAsync();
            return View(departamentos);
        }

        public async Task<IActionResult> Details(int iddepartamento)
        {
            Departamento departamento =
                await this.service.FindDepartamentoAsync(iddepartamento);
            return View(departamento);
        }

        public IActionResult BuscarLocalidades()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuscarLocalidades(string localidad)
        {
            List<Departamento> departamentos =
                await this.service.GetDepartamentosLocalidadAsync(localidad);
            return View(departamentos);
        }
    }
}
