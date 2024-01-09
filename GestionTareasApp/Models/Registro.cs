using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestionTareasApp.Models
{
    public class Registro
    {
        [Required(ErrorMessage = "Email obligatorio")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Formato incorrecto")]
        [EmailAddress]
        [Remote(action: "ComprobarEmail", controller: "Usuario")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña obligatoria")]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repetir contraseña")]
        [Compare("Contrasenia", ErrorMessage = "La contraseña y su confirmación no coinciden.")]
        public string ConfirmaContrasenia { get; set; }
    }
}
