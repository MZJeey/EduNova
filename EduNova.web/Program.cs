using EduNova.Application.DTOs;
using EduNova.Application.Profiles;
using EduNova.Application.Services.Implementations;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Repository.Implementations;
using EduNova.Infraestructure.Repository.Interfaces;
using EduNova.web.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Repositorios
builder.Services.AddTransient<IRepositoryUsuario,RepositoryUsuario>();
builder.Services.AddTransient<IRepositoyCategoria, RepositoryCategoria>();
builder.Services.AddTransient<IRepositoryTickets, RepositoryTickets>();
//servicios
builder.Services.AddTransient<IServiceUsuario, ServiceUsuario>();
builder.Services.AddTransient<IserviceCategoria, ServiceCategoria>();
builder.Services.AddTransient<IServiceTickets, ServiceTickets>();
//builder.Services.AddTransient<IServiceDetalleCategoria, ServiceDetalleCategoria>();
//Configuracion AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<UsuarioProfile>();
    config.AddProfile<CategoriaProfile>();
    config.AddProfile<TicketProfile>();
    //config.AddProfile<DetalleCategoriaProfile>();
});

/*builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CategoriaProfile>();
});*/
//Configuracion conexion a la base de datos
builder.Services.AddDbContext<eduNovaContext>(options =>
{
    // it read appsettings.json file 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDataBase"));
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

//builder.Services.AddScoped<IServiceUsuario, ServiceUsuario>();

//***********************
//Configuración Serilog
// Logger. P.E. Verbose = muestra SQl Statement
var logger = new LoggerConfiguration()
                    // Limitar la información de depuración
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                    .Enrich.FromLogContext()
                    // Log LogEventLevel.Verbose muestra mucha información, pero no es necesaria solo para el proceso de depuración
                    .WriteTo.Console(LogEventLevel.Information)
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.File(@"Logs\Info-.log", shared: true, encoding: Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo.File(@"Logs\Debug-.log", shared: true, encoding: System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo.File(@"Logs\Warning-.log", shared: true, encoding: System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.File(@"Logs\Error-.log", shared: true, encoding: Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo.File(@"Logs\Fatal-.log", shared: true, encoding: Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .CreateLogger();

builder.Host.UseSerilog(logger);
//***************************



var app = builder.Build();














// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Error control Middleware
    app.UseMiddleware<ErrorHandlingMiddleware>();
}
 
//Activar soporte a la solicitud de registro con SERILOG 
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseAntiforgery();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
