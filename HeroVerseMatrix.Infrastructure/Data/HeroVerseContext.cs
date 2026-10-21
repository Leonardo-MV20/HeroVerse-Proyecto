using HeroVerseMatrix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Infrastructure.Data
{
    public class HeroVerseContext : DbContext
    {
        public HeroVerseContext()
            : base("HeroVerseConnection")
        {

        }

        public DbSet<CasaMatrix> CasasMatrix { get; set; }

        public DbSet<Personaje> Personajes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personaje>()
                .HasRequired(p => p.CasaMatrix)
                    .WithMany(r => r.Personajes)
                    .HasForeignKey(p => p.CasaMatrixId)
                    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

