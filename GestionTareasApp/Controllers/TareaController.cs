using GestionTareasApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace GestionTareasApp.Controllers
{
    public class TareaController : Controller
    {
        //private readonly IMemoryCache _memoryCache;
        private readonly TareaContexto contexto;

        public TareaController(/*IMemoryCache memoryCache*/ TareaContexto contexto)
        {
            /*_memoryCache = memoryCache;*/
            this.contexto = contexto;
        }
        /*List<Tarea> tareas { 
            get 
            {
                if (!_memoryCache.TryGetValue("Tareas", out List<Tarea> tareas))
                {
                    // Si no está en caché, crea una nueva lista y almacénala en caché por 5 minutos
                    tareas = new List<Tarea>();
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    };
                    _memoryCache.Set("Tareas", tareas, cacheEntryOptions);
                }
                return tareas;
            }
            set
            {
                _memoryCache.Set("Tareas", value);
            }
        }*/

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
                //tareas.Add(tarea);
                contexto.Tareas.Add(tarea);
                contexto.SaveChanges();
                return RedirectToAction("ListaTareas");
            }
            return View();
        }

        
        public IActionResult ListaTareas()
        {
            List<Tarea> tareasPendientes = new List<Tarea>();
            /*foreach(var tarea in tareas)*/
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

       
        public IActionResult EliminarTarea(int Id)
        {
            /*var tareasCopia = tareas;
            var tareaAEliminar = tareasCopia.Find( t => t.IdTarea == Id);   
            
            if (tareaAEliminar != null)
            {
                tareasCopia.Remove(tareaAEliminar);
                tareas = tareasCopia;
            }
            return RedirectToAction("ListaTareas");*/
            var tarea = contexto.Tareas.Find(Id);
            if (tarea == null) { 
            contexto.Tareas.Remove(tarea);
            contexto.SaveChanges();
            }
            return RedirectToAction("ListaTareas"); 
        }

        public IActionResult CompletarTarea(int Id)
        {
            /*var tareaACompletar = tareas.Find(t => t.IdTarea == Id);

            if (tareaACompletar != null)
            {
                tareaACompletar.Estado = "Completada";
            }
            return RedirectToAction("ListaTareas");*/
            var tarea = contexto.Tareas.Find(Id);
            if (tarea == null)
            {
                tarea.Estado = "Completada";
                contexto.SaveChanges();
            }
            return RedirectToAction("ListaTareas");
        }

    }
}
