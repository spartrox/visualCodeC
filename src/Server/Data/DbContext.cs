using Domain;
using Microsoft.EntityFrameworkCore;
using System;
namespace Server.Data
{
    public class CantinaContext : DbContext
    {

    protected override void OnConfiguring(DbContextOptionsBuilder optionsDuilder)
    {
        optionsDuilder.UseNpgsql("Host=localhost;Database=cesidb;Username=postgres;Password=pgSTRONGpass;Port=5444");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SavedReservation>()
            .HasKey(r => new { r.Date, r.User });
    }

    public DbSet<SavedReservation> Reservations => Set<SavedReservation>();
    }
}


