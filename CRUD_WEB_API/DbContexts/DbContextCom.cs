using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CRUD_WEB_API.Models;

#nullable disable

namespace CRUD_WEB_API.DbContexts
{
    public partial class DbContextCom : DbContext
    {
        public DbContextCom()
        {
        }

        public DbContextCom(DbContextOptions<DbContextCom> options)
            : base(options)
        {
        }

        public virtual DbSet<TblStudent> TblStudents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-USOLJHE;Initial Catalog=Razib;User ID=sa;Password=12345678;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblStudent>(entity =>
            {
                entity.HasKey(e => e.IntStudentId);

                entity.Property(e => e.InsertDateTime).HasColumnType("datetime");

                entity.Property(e => e.StrAddress).HasMaxLength(50);

                entity.Property(e => e.StrBloodGroup).HasMaxLength(50);

                entity.Property(e => e.StrPhoneNo).HasMaxLength(50);

                entity.Property(e => e.StrStudentName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
