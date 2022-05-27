using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Data
{
    public class ProjetoFinalCurso1500Context : IdentityDbContext<User>
    {
        public ProjetoFinalCurso1500Context (DbContextOptions<ProjetoFinalCurso1500Context> options)
            : base(options)
        {
        }

        public DbSet<ProjetoFinalCurso1500.Models.Car>? Car { get; set; }
        public DbSet<ProjetoFinalCurso1500.Models.Client>? Client { get; set; }
        public DbSet<ProjetoFinalCurso1500.Models.Salesman>? Salesman { get; set; }
        public DbSet<ProjetoFinalCurso1500.Models.Concessionaire>? Concessionaires{ get; set; }
        public DbSet<ProjetoFinalCurso1500.Models.NewsFeed>? NewsFeed { get; set; }
        public DbSet<ProjetoFinalCurso1500.Models.TestDrive>? TestDrive { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Car>(e =>
            {
                e.HasOne(c => c.Concessionaire)
               .WithMany(d => d.Cars)
               .HasForeignKey(d => d.IdConcessionaire).OnDelete(DeleteBehavior.ClientCascade);
            });

            builder.Entity<TestDrive>(e =>
            {
                e.HasOne(c => c.Salesman)
                .WithMany(c => c.TestDrives)
                .HasForeignKey(d => d.IdSalesman).OnDelete(DeleteBehavior.ClientCascade);

                e.HasOne(c => c.Client)
                .WithMany(c => c.TestDrives)
                .HasForeignKey(d => d.IdClient).OnDelete(DeleteBehavior.ClientCascade);

                e.HasOne(c => c.Car)
                .WithMany(c => c.TestDrives)
                .HasForeignKey(d => d.IdCar).OnDelete(DeleteBehavior.ClientCascade);
                e.HasOne(c => c.Concessionaire)
                .WithMany(c => c.TestDrives)
                .HasForeignKey(d => d.IdConcessionaire).OnDelete(DeleteBehavior.ClientCascade);

            });

            builder.Entity<Salesman>(e =>
            {
                e.HasOne(c => c.Concessionaire)
               .WithMany(d => d.Salesmans)
               .HasForeignKey(d => d.IdConcessionaire).OnDelete(DeleteBehavior.ClientCascade);
            });




        }
    }
}
