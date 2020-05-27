using System;
using System.Collections.Generic;
using System.Text;
using E_Learning.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kurs> Kurs { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Kviz> Kviz { get; set; }
        public DbSet<Lekcija> Lekcija { get; set; }
        public DbSet<Oblast> Oblast { get; set; }
        public DbSet<Odgovor> Odgovor { get; set; }
        public DbSet<Pitanje> Pitanje { get; set; }
        public DbSet<Upisivanje> Upisivanje { get; set; }


        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Korisnik>().ToTable("Korisnik");
        }*/
    }
}
