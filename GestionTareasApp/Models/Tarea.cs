using System.ComponentModel.DataAnnotations;

namespace GestionTareasApp.Models
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
      
        //public DateTime FechaLimite { get; set; }
        //public string? Estado { get; set; }

       /* public Tarea(string titulo, string descripcion) 
        {
            this.Titulo = titulo;
            this.Descripcion = descripcion;
        }*/
    }
}
