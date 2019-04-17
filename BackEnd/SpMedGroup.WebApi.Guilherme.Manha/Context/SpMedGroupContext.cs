using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpMedGroup.WebApi.Guilherme.Manha.Domains
{
    public partial class SpMedGroupContext : DbContext
    {
        public SpMedGroupContext()
        {
        }

        public SpMedGroupContext(DbContextOptions<SpMedGroupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clinica> Clinica { get; set; }
        public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<Especialidade> Especialidade { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Prontuario> Prontuario { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress; Initial Catalog = SpMedicalGroup; user id = sa; pwd = 132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clinica>(entity =>
            {
                entity.ToTable("CLINICA");

                entity.HasIndex(e => e.NomeFantasia)
                    .HasName("UQ__CLINICA__62CD2EF57331CA76")
                    .IsUnique();

                entity.HasIndex(e => e.RazaoSocial)
                    .HasName("UQ__CLINICA__10D918D95012DFCC")
                    .IsUnique();

                entity.HasIndex(e => e.Telefone)
                    .HasName("UQ__CLINICA__D6F1694F8DA07C5D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasColumnName("ENDERECO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasColumnName("NOME_FANTASIA")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("RAZAO_SOCIAL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasColumnName("TELEFONE")
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.ToTable("CONSULTAS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Data)
                    .HasColumnName("DATA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("DESCRICAO")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IdMedico).HasColumnName("ID_MEDICO");

                entity.Property(e => e.IdProntuario).HasColumnName("ID_PRONTUARIO");

                entity.Property(e => e.IdStatus).HasColumnName("ID_STATUS");

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(d => d.IdMedico)
                    .HasConstraintName("FK__CONSULTAS__ID_ME__6E01572D");

                entity.HasOne(d => d.IdProntuarioNavigation)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(d => d.IdProntuario)
                    .HasConstraintName("FK__CONSULTAS__ID_PR__6D0D32F4");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK__CONSULTAS__ID_ST__6EF57B66");
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.ToTable("ESPECIALIDADE");

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__ESPECIAL__E2AB1FF4CB1FC9E9")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("MEDICO");

                entity.HasIndex(e => e.Crm)
                    .HasName("UQ__MEDICO__C1F887FFEB0F817E")
                    .IsUnique();

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__MEDICO__E2AB1FF4684D627E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Crm)
                    .IsRequired()
                    .HasColumnName("CRM")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdClinica).HasColumnName("ID_CLINICA");

                entity.Property(e => e.IdEspecialidade).HasColumnName("ID_ESPECIALIDADE");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClinicaNavigation)
                    .WithMany(p => p.Medico)
                    .HasForeignKey(d => d.IdClinica)
                    .HasConstraintName("FK__MEDICO__ID_CLINI__66603565");

                entity.HasOne(d => d.IdEspecialidadeNavigation)
                    .WithMany(p => p.Medico)
                    .HasForeignKey(d => d.IdEspecialidade)
                    .HasConstraintName("FK__MEDICO__ID_ESPEC__656C112C");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Medico)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__MEDICO__ID_USUAR__7B5B524B");
            });

            modelBuilder.Entity<Prontuario>(entity =>
            {
                entity.ToTable("PRONTUARIO");

                entity.HasIndex(e => e.Cpf)
                    .HasName("UniqueCpf")
                    .IsUnique();

                entity.HasIndex(e => e.Rg)
                    .HasName("UniqueRg")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasColumnName("ENDERECO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rg)
                    .IsRequired()
                    .HasColumnName("RG")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Prontuario)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__PRONTUARI__ID_US__7C4F7684");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("STATUS");

                entity.HasIndex(e => e.Situacao)
                    .HasName("UQ__STATUS__4E84C4A21CC64C7F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Situacao)
                    .IsRequired()
                    .HasColumnName("SITUACAO")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.ToTable("TIPO_USUARIO");

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__TIPO_USU__E2AB1FF4B8B45FCA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("USUARIO");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__USUARIO__161CF72471641C4A")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.IdTipoUsuario).HasColumnName("ID_TIPO_USUARIO");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasColumnName("SENHA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__USUARIO__ID_TIPO__5441852A");
            });
        }
    }
}
