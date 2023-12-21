using System.ComponentModel.DataAnnotations;

namespace GestionTareasApp.Models
{
    public class Tarea
    {
        [Required(ErrorMessage = "El campo id es obligatorio.")]
        public int IdTarea { get; set; }

        [Required(ErrorMessage = "El campo titulo es obligatorio.")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo descripcion es obligatorio.")]
        public string? Descripcion { get; set; }
      
        //public DateTime FechaLimite { get; set; }
        public string? Estado { get; set; }

       /* public Tarea(string titulo, string descripcion) 
        {
            this.Titulo = titulo;
            this.Descripcion = descripcion;
        }*/
    }
}
