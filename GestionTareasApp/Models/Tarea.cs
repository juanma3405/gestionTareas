using System.ComponentModel.DataAnnotations;

namespace GestionTareasApp.Models
{
    public class Tarea
    {
        [Key]
        [Required(ErrorMessage = "El campo id es obligatorio.")]
        public int IdTarea { get; set; }

        [Required(ErrorMessage = "El campo titulo es obligatorio.")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo descripcion es obligatorio.")]
        public string? Descripcion { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(DateValidator), "ValidateDate")]
        public DateTime? FechaLimite { get; set; }
        public string? Estado { get; set; }

    }

    public static class DateValidator
    {
        public static ValidationResult ValidateDate(DateTime date, ValidationContext context)
        {
            if (date < DateTime.Today)
            {
                return new ValidationResult("The date cannot be in the past.");
            }

            return ValidationResult.Success;
        }
    }

}
