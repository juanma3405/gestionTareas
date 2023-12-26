using GestionTareasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApp
{
    public class TareaContexto: DbContext
    {
        public TareaContexto(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
