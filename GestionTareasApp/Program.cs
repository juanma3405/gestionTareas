using GestionTareasApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();


/* Desde aca vieja clase program

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TareaContexto>(options =>
            options.UseSqlServer(GetConnectionString("defaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    /*endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Tarea}/{action=Crear}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tarea}/{action=Crear}"
    );



//app.MapRazorPages();

app.Run();*/
