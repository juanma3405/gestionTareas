using GestionTareasApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
            var tareas = await contexto.Tareas.ToListAsync();
            return View(tareas);
        }

        [HttpGet]
        public async Task<ActionResult> FiltrarTareasPorEstado (string Estado)
        {
            List<Tarea> tareasFiltradas;
            if (string.IsNullOrEmpty(Estado))
            {
                tareasFiltradas = await contexto.Tareas.ToListAsync(); 
            }
            else
            {
                tareasFiltradas = await contexto.Tareas.Where(t => t.Estado == Estado).ToListAsync();
            }
            return PartialView("ListaTareasParcial",tareasFiltradas);
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

        [HttpPost]
        public async Task<ActionResult<Tarea>> EditarTarea (int Id)
        {
            var tarea = await contexto.Tareas.FindAsync(Id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        [HttpPost]
        public async Task<ActionResult> ActualizarTarea( Tarea tareaActualizada)
        {
            if (ModelState.IsValid)
            {
                var tareaExistente = await contexto.Tareas.FindAsync(tareaActualizada.IdTarea);
                if (tareaExistente != null)
                {
                    tareaExistente.Titulo = tareaActualizada.Titulo;
                    tareaExistente.Descripcion = tareaActualizada.Descripcion;
                    tareaExistente.Estado = tareaActualizada.Estado;
                    tareaExistente.FechaLimite = tareaActualizada.FechaLimite;
                    await contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(ListaTareas));
                }
            }
            return View("EditarTarea", tareaActualizada);
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
            tarea.Estado = "Eliminada";
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

        public async Task<ActionResult<Tarea>> VerTarea(int Id)
        {
            var tarea = await contexto.Tareas.FindAsync(Id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }
    }
}
