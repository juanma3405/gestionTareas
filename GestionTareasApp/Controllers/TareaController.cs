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

        public async Task<ActionResult> ListaTareas()
        {
            List<Tarea> tareasPendientes = new List<Tarea>();
            var tareas = await contexto.Tareas.ToListAsync();
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
        public async Task<ActionResult> Crear(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                tarea.Estado = "Pendiente";
                await contexto.AddAsync(tarea);
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(ListaTareas));
            }
            return View();
        }

        public async Task<ActionResult<Tarea>> EliminarTarea(int Id)
        {
            var tarea = await contexto.Tareas.FindAsync(Id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmarEliminar(int Id)
        {
            var tarea = await contexto.Tareas.FindAsync(Id);
            contexto.Tareas.Remove(tarea);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(ListaTareas));
        }

        public async Task<ActionResult> CompletarTarea(int Id)
        {
            var tarea = await contexto.Tareas.FindAsync(Id);
            if (tarea == null)
            {
                return NotFound();
            }
            tarea.Estado = "Completada";
            await contexto.SaveChangesAsync();

            return RedirectToAction(nameof(ListaTareas));
        }
    }
}
