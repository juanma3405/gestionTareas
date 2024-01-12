using GestionTareasApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApp
{
    public class TareaContexto: IdentityDbContext
    {
        public TareaContexto(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SembrarData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SembrarData(ModelBuilder modelBuilder)
        {
            var rolesAAgregar = new List<IdentityRole>
            {
                new IdentityRole { Id = "4a472684-c541-4341-b389-f40a20db53cf", Name = "Administrador"},
                new IdentityRole { Id = "d9acd762-ff16-44be-987f-aba546dee4f5", Name = "Usuario"},
            };
            foreach (var role in rolesAAgregar)
            {
                modelBuilder.Entity<IdentityRole>().HasData(role);
            }

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var usuarioAdministrador = new IdentityUser()
            {
                Id = "5f1188f5 - 3616 - 402f - bcee - 0c8fb8ff2557",
                UserName = "juanprueba@gmail.com",
                NormalizedUserName = "JUANPRUEBA@GMAIL.COM",
                Email = "juanprueba@gmail.com",
                NormalizedEmail = "JUANPRUEBA@GMAIL.COM",
                PasswordHash = passwordHasher.HashPassword(null, "Aa123456!")
            };

            modelBuilder.Entity<IdentityUser>()
                 .HasData(usuarioAdministrador);

            var usuarioRol = new IdentityUserRole<string>()
            {
                UserId = "5f1188f5 - 3616 - 402f - bcee - 0c8fb8ff2557",
                RoleId = "4a472684-c541-4341-b389-f40a20db53cf"
            };

            modelBuilder.Entity<IdentityUserRole<string>>()
                 .HasData(usuarioRol);
        }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
