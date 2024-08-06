using GestionTareasApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using GestionTareasApp.ViewModels;

namespace GestionTareasApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<IdentityUser> gestionUsuario;
        private readonly SignInManager<IdentityUser> gestionLogin;
        private readonly RoleManager<IdentityRole> gestionRol;

        public UsuarioController(UserManager<IdentityUser> gestionUsuario, SignInManager<IdentityUser> gestionLogin, RoleManager<IdentityRole> gestionRol)
        {
            this.gestionUsuario = gestionUsuario;
            this.gestionLogin = gestionLogin;
            this.gestionRol = gestionRol;
        }

        public IActionResult Registro()
        {
            Registro registro = new Registro();
            return View(registro);
        }

        [HttpPost]
        public async Task<ActionResult> Registro(Registro registro)
        {
            var usuario = new IdentityUser
            {
                UserName = registro.Email,
                Email = registro.Email
            };

            var resultado = await gestionUsuario.CreateAsync(usuario, registro.Contrasenia);
           
            if (resultado.Succeeded)
            {
                return RedirectToAction(nameof(IniciarSesion));
            }
            else
            {
                ViewData["Mensaje"] = "No se puedo crear el usuario";
                return View();
            }
        }

        public /*async Task<ActionResult>*/ IActionResult IniciarSesion()
        {
            //await CrearRoles();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IniciarSesion(Login login)
        {
            var resultado = await gestionLogin.PasswordSignInAsync(login.Email,
                login.Contrasenia, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, login.Email)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    properties);

               // await HacerAdmin(login.Email);

                return RedirectToAction("ListaTareas","Tarea");
            }
            else
            {
                ViewData["Mensaje"] = "Login incorrecto";
                return View();
            }
        }

        public async Task<ActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await gestionLogin.SignOutAsync();
            return RedirectToAction(nameof(IniciarSesion));
        }

        public async Task<ActionResult> ListaUsuarios()
        {
            /*var usuarios = await gestionUsuario.Users.ToListAsync();
            return View(usuarios);*/
            var usuarios = await gestionUsuario.Users.ToListAsync();
            var roles = await gestionRol.Roles.ToListAsync();

            var model = new ListaUsuariosViewModel
            {
                Usuarios = usuarios,
                Roles = roles,
                UserManager = gestionUsuario
            };

            return View(model);
        }

        [HttpPost("HacerAdmin", Name = "hacerAdmin")]
        public async Task<ActionResult> HacerAdmin(string email)
        {
            /*var usuario = await gestionUsuario.FindByEmailAsync(email);
            await gestionUsuario.AddClaimAsync(usuario, new Claim("esAdmin", "1"));
            return NoContent();*/
            var usuario = await gestionUsuario.FindByEmailAsync(email);
            if (usuario != null)
            {
                await gestionUsuario.AddToRoleAsync(usuario, "Administrador");

            }
            //return NoContent();
            return View(usuario);
        }

        [HttpPost("RemoverAdmin", Name = "removerAdmin")]
        public async Task<ActionResult> RemoverAdmin(string email)
        {
            /*var usuario = await gestionUsuario.FindByEmailAsync(email);
            await gestionUsuario.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));
            return NoContent();*/
            var usuario = await gestionUsuario.FindByEmailAsync(email);
            if (usuario != null)
            {
                await gestionUsuario.RemoveFromRoleAsync(usuario, "Administrador");
            }
            //return NoContent();
            return View(usuario);
        }

        /*private async Task CrearRoles()
        {
            string[] roles = { "Administrador", "Usuario" };
            IdentityResult resultadoRol;

            foreach (var rol in roles)
            {
                var existeRol = await gestionRol.RoleExistsAsync(rol);

                if (!existeRol)
                {
                    resultadoRol = await gestionRol.CreateAsync(new IdentityRole(rol));
                }
            }

            /*var usuario = await gestionUsuario.FindByEmailAsync("juanprueba@gmail.com");

            if (usuario != null)
            {
                await gestionUsuario.AddToRoleAsync(usuario, "Administrador");
            }
        }*/
    }

}
