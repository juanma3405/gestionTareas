using GestionTareasApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace GestionTareasApp.Controllers
{
    public class TareaController : Controller
    {
        private readonly TareaContexto contexto;

        public TareaController(TareaContexto contexto)
        {
            this.contexto = contexto;
        }
      
        public IActionResult ListaTareas()
        {
            List<Tarea> tareasPendientes = new List<Tarea>();
            var tareas = contexto.Tareas.ToList();
            foreach (var tarea in tareas)
            {
                if (tarea.Estado == "Pendiente")
                {
                    tareasPendientes.Add(tarea);
                }
            }
            return View(tareasPendientes);  
        }


        [HttpGet]
        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Crear(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                tarea.Estado = "Pendiente";
                contexto.Add(tarea);
                contexto.SaveChanges();
                return RedirectToAction(nameof(ListaTareas));
            }
            return View();
        }

        public IActionResult EliminarTarea(int Id)
        {
            var tarea = contexto.Tareas.Find(Id);
            if (tarea == null) {
                return NotFound();
            }
            return View(tarea);
        }

        [HttpPost]
        public IActionResult ConfirmarEliminar(int Id)
        {
            var tarea = contexto.Tareas.Find(Id);
            contexto.Tareas.Remove(tarea);
            contexto.SaveChanges();
            return RedirectToAction(nameof(ListaTareas));
        }
        public IActionResult CompletarTarea(int Id)
        {
            var tarea = contexto.Tareas.Find(Id);
            if (tarea == null)
            {
                return NotFound();
            }
            tarea.Estado = "Completada";
            contexto.SaveChanges();

            return RedirectToAction(nameof(ListaTareas));
        }

    }
}
