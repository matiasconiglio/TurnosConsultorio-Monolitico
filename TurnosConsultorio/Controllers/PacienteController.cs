using Microsoft.AspNetCore.Mvc;
using TurnosConsultorio.Models;
using TurnosConsultorio.Services;

namespace TurnosConsultorio.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public IActionResult Index()
        {
            var pacientes = _pacienteService.Get();
            return View(pacientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _pacienteService.Create(paciente);
                return RedirectToAction(nameof(Index));
            }

            return View(paciente);
        }

        public IActionResult Edit(string id)
        {
            var paciente = _pacienteService.Get(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpPost]
        public IActionResult Edit(string id, Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _pacienteService.Update(id, paciente);
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        public IActionResult Delete(string id)
        {
            var paciente = _pacienteService.Get(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        // 🔁 POST directo sin ActionName → más claro y funcional
        [HttpPost]
        public IActionResult DeletePost(string id)
        {
            _pacienteService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
