<div align="center">

<img src="assets/logo.jpg" width="170"/>

# 🎓 EduNova

### Plataforma Web para la Gestión Académica y Administración de Incidencias

Sistema desarrollado con ASP.NET Core para optimizar la gestión de tickets, incidencias y procesos administrativos dentro de instituciones educativas.

<br>

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=.net&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=black)
![Status](https://img.shields.io/badge/Status-Finalizado-success)

</div>

---

#  Descripción

EduNova es una plataforma web desarrollada como proyecto académico para optimizar la administración y seguimiento de incidencias dentro de instituciones educativas.

La aplicación permite administrar usuarios, categorías, tickets, asignaciones y reportes mediante una interfaz moderna, intuitiva y responsive. Además, incorpora autenticación por roles, seguimiento mediante SLA, internacionalización y herramientas para el análisis de información.

---

#  Funcionalidades

-  Inicio de sesión seguro.
-  Gestión completa de usuarios.
-  Administración de categorías.
-  Registro y seguimiento de tickets.
-  Asignación de tickets.
-  Dashboard con estadísticas.
-  Exportación de reportes en PDF.
-  Exportación de reportes en Excel.
-  Cambio dinámico entre Español e Inglés.
-  Gestión de roles.
-  Valoración de tickets.
-  Seguimiento mediante SLA.

---

#  Tecnologías utilizadas

| Backend | Frontend | Base de Datos | Herramientas |
|----------|----------|---------------|--------------|
| ASP.NET Core 8 | HTML5 | SQL Server | Visual Studio |
| C# | CSS3 | Entity Framework Core | Git |
| AutoMapper | Bootstrap | LINQ | Serilog |
| Razor Views | JavaScript | SQL | Newtonsoft.Json |

---

#  Arquitectura

```text
                  Usuario
                     │
                     ▼
              EduNova.Web
                     │
                     ▼
         EduNova.Application
                     │
                     ▼
      EduNova.Infrastructure
                     │
                     ▼
                SQL Server
```

---

#  Vista previa del sistema

##  Inicio de sesión

<p align="center">
<img src="assets/login.png" width="900">
</p>

---

##  Dashboard principal

<p align="center">
<img src="assets/dashboard.png" width="900">
</p>

---

##  Gestión de Usuarios

<p align="center">
<img src="assets/Usuarios.png" width="900">
</p>

---

##  Gestión de Categorías

<p align="center">
<img src="assets/Categorias.png" width="900">
</p>

---

##  Gestión de Tickets

<p align="center">
<img src="assets/tickets.png" width="900">
</p>

---

##  Asignación de Tickets

<p align="center">
<img src="assets/asignaciones.png" width="900">
</p>

---

##  Dashboard de Reportes

<p align="center">
<img src="assets/reportes.png" width="900">
</p>

---

#  Internacionalización

EduNova incorpora soporte para múltiples idiomas, permitiendo cambiar dinámicamente entre Español e Inglés para ofrecer una mejor experiencia de usuario.

| Español | English |
|---------|---------|
| <img src="assets/espanol.png"> | <img src="assets/englishc.png"> |

---

#  Mi participación

Participé activamente como desarrollador **Full Stack** durante el desarrollo del proyecto.

Entre mis principales responsabilidades se encuentran:

- Desarrollo de interfaces de usuario.
- Desarrollo de funcionalidades Backend.
- Integración entre Frontend y Backend.
- Implementación del sistema de internacionalización (Español / Inglés).
- Desarrollo de módulos administrativos.
- Optimización de procesos.
- Corrección de errores.
- Participación en pruebas funcionales y validaciones.

---

# 📂 Estructura del proyecto

```text
EduNova
│
├── EduNova.Web
├── EduNova.Application
├── EduNova.Infrastructure
├── assets
└── README.md
```

---

#  Instalación

```bash
git clone https://github.com/TU-USUARIO/EduNova.git

cd EduNova

dotnet restore

dotnet build

dotnet run
```

---

#  Estado del proyecto

🟢 **Proyecto finalizado**

Desarrollado como parte de la carrera de Ingeniería de Software.

---

#  Autores

- Berny Dávila
- Jeeyson Martínez 

---

#  Licencia

Proyecto desarrollado con fines académicos.
