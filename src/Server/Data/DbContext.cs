using Domain;
using System;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class CantinaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=cesidb;Username=postgres;Password:pgSTRONGpass;Port=5444");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavedReservation>()
                .Haskey(ref:SavedReservation => new { r.Date, r.User });
        }
        public DbSet<SavedReservation> Reservations => Set<SavedReservation>();

    }
}
