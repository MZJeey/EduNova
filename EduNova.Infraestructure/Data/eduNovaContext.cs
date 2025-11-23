using System;
using System.Collections.Generic;
using EduNova.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EduNova.Infraestructure.Data;

public partial class eduNovaContext : DbContext
{
    public eduNovaContext(DbContextOptions<eduNovaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Curso> Curso { get; set; }

    public virtual DbSet<Especialidades> Especialidades { get; set; }

    public virtual DbSet<Estudiante> Estudiante { get; set; }

    public virtual DbSet<Etiqueta> Etiqueta { get; set; }

    public virtual DbSet<Imagenes> Imagenes { get; set; }

    public virtual DbSet<Matricula> Matricula { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    public virtual DbSet<Planeamiento> Planeamiento { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Seguimiento> Seguimiento { get; set; }

    public virtual DbSet<Sla> Sla { get; set; }

    public virtual DbSet<TicketHistorial> TicketHistorial { get; set; }

    public virtual DbSet<Tickets> Tickets { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240C81004451");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdSla).HasColumnName("idSLA");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdSlaNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.IdSla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categoria_SLA");

            entity.HasMany(d => d.IdEtiqueta).WithMany(p => p.IdCategoriaNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoriaEtiqueta",
                    r => r.HasOne<Etiqueta>().WithMany()
                        .HasForeignKey("IdEtiqueta")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_catEti_Etiqueta"),
                    l => l.HasOne<Categoria>().WithMany()
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_catEti_Categoria"),
                    j =>
                    {
                        j.HasKey("IdCategoria", "IdEtiqueta");
                        j.ToTable("categoria_etiqueta");
                        j.IndexerProperty<int>("IdCategoria").HasColumnName("idCategoria");
                        j.IndexerProperty<int>("IdEtiqueta").HasColumnName("idEtiqueta");
                    });
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Curso__8551ED059932B9CB");

            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Curso)
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Curso_Profesor");
        });

        modelBuilder.Entity<Especialidades>(entity =>
        {
            entity.HasKey(e => e.Idespecialidad).HasName("PK__Especial__2C0C636B9945C430");

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreEspecialidad).HasMaxLength(200);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Especialidades)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Especiali__IdCat__0A9D95DB");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__AEFFDBC5D5249DDE");

            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .HasColumnName("apellidos");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Etiqueta>(entity =>
        {
            entity.HasKey(e => e.IdEtiqueta).HasName("PK__Etiqueta__3C1526A7DDCC4BFE");

            entity.Property(e => e.IdEtiqueta).HasColumnName("idEtiqueta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCategoria1).WithMany(p => p.Etiqueta)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_IdCategoria_Etiqueta");
        });

        modelBuilder.Entity<Imagenes>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__Imagenes__B42D8F2A211907EF");

            entity.Property(e => e.Imagen)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Imagenes)
                .HasForeignKey(d => d.IdTicket)
                .HasConstraintName("FK__Imagenes__IdTick__0B91BA14");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.IdMatricula).HasName("PK__Matricul__72013C99AA17548F");

            entity.Property(e => e.IdMatricula).HasColumnName("idMatricula");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaInscripcion");
            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Curso");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Estudiante");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK__Nota__AD5F462E0A5C92CC");

            entity.Property(e => e.IdNota).HasColumnName("idNota");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdMatricula).HasColumnName("idMatricula");
            entity.Property(e => e.TipoEvaluacion)
                .HasMaxLength(50)
                .HasColumnName("tipoEvaluacion");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdMatriculaNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdMatricula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nota_Matricula");
        });

        modelBuilder.Entity<Planeamiento>(entity =>
        {
            entity.HasKey(e => e.IdPlaneamiento).HasName("PK__Planeami__307A104365079946");

            entity.Property(e => e.IdPlaneamiento).HasColumnName("idPlaneamiento");
            entity.Property(e => e.Actividad)
                .HasMaxLength(250)
                .HasColumnName("actividad");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fechaFin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.Objetivo)
                .HasMaxLength(250)
                .HasColumnName("objetivo");
            entity.Property(e => e.Semana).HasColumnName("semana");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Planeamiento)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Planeamiento_Curso");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F769F57CBA2");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Seguimiento>(entity =>
        {
            entity.HasKey(e => e.IdSeguimiento).HasName("PK__Seguimie__1B37049CAC827E2B");

            entity.Property(e => e.IdSeguimiento).HasColumnName("idSeguimiento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Seguimiento)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seguimiento_Estudiante");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Seguimiento)
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seguimiento_Profesor");
        });

        modelBuilder.Entity<Sla>(entity =>
        {
            entity.HasKey(e => e.IdSla).HasName("PK__SLA__024EBE7A027863EB");

            entity.ToTable("SLA");

            entity.Property(e => e.IdSla).HasColumnName("idSLA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.TiempoMaxResolucion).HasColumnName("tiempo_max_resolucion");
            entity.Property(e => e.TiempoMaxRespuesta).HasColumnName("tiempo_max_respuesta");
        });

        modelBuilder.Entity<TicketHistorial>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__TicketHi__9CC7DBB48325F3D0");

            entity.Property(e => e.EstadoTickets)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCambio).HasColumnType("datetime");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.TicketHistorial)
                .HasForeignKey(d => d.IdTicket)
                .HasConstraintName("FK__TicketHis__IdTic__0C85DE4D");

            entity.HasOne(d => d.IdUsuarioCambioNavigation).WithMany(p => p.TicketHistorial)
                .HasForeignKey(d => d.IdUsuarioCambio)
                .HasConstraintName("FK__TicketHis__IdUsu__0D7A0286");
        });

        modelBuilder.Entity<Tickets>(entity =>
        {
            entity.HasKey(e => e.IdTicket).HasName("PK__Tickets__22B1456F15516A41");

            entity.Property(e => e.IdTicket).HasColumnName("idTicket");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCierre)
                .HasColumnType("datetime")
                .HasColumnName("fechaCierre");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdSla).HasColumnName("idSLA");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(50)
                .HasColumnName("prioridad");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .HasColumnName("titulo");
            entity.Property(e => e.UsuarioSolicitante).HasColumnName("usuarioSolicitante");
            entity.Property(e => e.Valoracion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Categoria");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Tickets__IdRol__151B244E");

            entity.HasOne(d => d.IdSlaNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdSla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_SLA");

            entity.HasOne(d => d.UsuarioSolicitanteNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UsuarioSolicitante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6EFF1A1EE");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
