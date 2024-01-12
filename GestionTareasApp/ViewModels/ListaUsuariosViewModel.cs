using Microsoft.AspNetCore.Identity;

namespace GestionTareasApp.ViewModels
{
    public class ListaUsuariosViewModel
    {
        public List<IdentityUser> Usuarios { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public UserManager<IdentityUser> UserManager { get; set; }
    }
}
