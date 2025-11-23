using EduNova.Application.Config;
using EduNova.Application.DTOs;
using EduNova.Application.Profiles;
using EduNova.Application.Services.Implementations;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Repository.Implementations;
using EduNova.Infraestructure.Repository.Interfaces;
using EduNova.web.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//Repositorios
builder.Services.AddTransient<IRepositoryUsuario,RepositoryUsuario>();
builder.Services.AddTransient<IRepositoyCategoria, RepositoryCategoria>();
builder.Services.AddTransient<IRepositoryTickets, RepositoryTickets>();
builder.Services.AddTransient<IRepositoryHistorialTicket, RepositoryHistorialTicket>();
builder.Services.AddTransient<IRepositoryImagen, RepositoryImagen>();
//servicios



builder.Services.AddTransient<IServiceUsuario, ServiceUsuario>();
builder.Services.AddTransient<IserviceCategoria, ServiceCategoria>();
builder.Services.AddTransient<IServiceTickets, ServiceTickets>();
builder.Services.AddTransient<IServiceHistorialTicket, ServiceHistorialTicket>();
builder.Services.AddTransient<IServiceImagen, ServiceImagen>();

//AUTENTICACIÓN CON COOKIES (igual que el profe)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";         
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.AccessDeniedPath = "/Login/Forbidden/";
    });





//builder.Services.AddTransient<IServiceDetalleCategoria, ServiceDetalleCategoria>();
//Configuracion AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<UsuarioProfile>();
    config.AddProfile<CategoriaProfile>();
    config.AddProfile<TicketProfile>();
    config.AddProfile<HistorialTicketProfile>();
    config.AddProfile<ImagenProfile>();
    config.AddProfile<RolProfile>();
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




//app.UseRequestLocalization();
//var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;










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


// Configurar archivos estáticos
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "uploads")),
    RequestPath = "/uploads"
});

//Activar soporte a la solicitud de registro con SERILOG 
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  
app.UseAuthorization();

// Antiforgery
app.UseAntiforgery();

// Ruta por defecto: que abra el login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();