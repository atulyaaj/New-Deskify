﻿using DeskEntity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskData.Data
{
    public class DeskDbContext:DbContext
    {
        public DeskDbContext()
        {

        }
        public DeskDbContext(DbContextOptions<DeskDbContext> options) : base(options)
        {

        }

        public DbSet<LoginTable> LoginTables { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<BookingRoom> bookingRooms { get; set; }
        public DbSet<BookingSeat> bookingSeats { get; set; }
        public DbSet<Seat> seats { get; set; }
        public DbSet<Choices> choices { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Floor> floors { get; set; }
        public DbSet<QRScanner> qrscanners { get; set; }
        public DbSet<Receptionist> receptionists { get; set; }

        public DbSet<SecretKey> secretKeys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {

           dbContextOptionsBuilder.UseSqlServer("Data Source=DESKTOP-SSFI2H6\\SQLEXPRESS;Initial Catalog=deskdb; Integrated Security=true; ");

        }
    }
}
