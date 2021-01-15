﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.Data;

namespace Server.Migrations
{
    [DbContext(typeof(CantinaContext))]
    [Migration("20210115100219_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Server.Data.SavedReservation", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.Property<int>("BeginMinute")
                        .HasColumnType("integer");

                    b.Property<int>("EndMinute")
                        .HasColumnType("integer");

                    b.HasKey("Date", "User");

                    b.ToTable("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}