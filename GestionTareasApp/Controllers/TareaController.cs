using GestionTareasApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace GestionTareasApp.Controllers
{
    public class TareaController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public TareaController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        List<Tarea> tareas { 
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
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Tarea tarea)
        {
            tareas.Add(tarea);
            return RedirectToAction("ListaTareas");
        }

        
        public IActionResult ListaTareas()
        {
            return View(tareas);
        }

       
        public IActionResult EliminarTarea(int Id)
        {
            var tareasCopia = tareas;
            var tareaAEliminar = tareasCopia.Find( t => t.IdTarea == Id);   
            
            if (tareaAEliminar != null)
            {
                tareasCopia.Remove(tareaAEliminar);
                tareas = tareasCopia;
            }
            return RedirectToAction("ListaTareas");
        }
    }
}
