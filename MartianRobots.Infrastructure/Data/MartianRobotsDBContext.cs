using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MartianRobots.Core.Entities;

namespace MartianRobots.Infrastructure.Data
{
    public class MartianRobotsDBContext : DbContext
    {
        public MartianRobotsDBContext(DbContextOptions<MartianRobotsDBContext> options)
            : base(options)
        {

        }

        public DbSet<Robot> Robots { get; set; }
        public DbSet<CurrentPosition> CurrentPositions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Inserting basic Robot information
            modelBuilder.Entity<Robot>()
                .HasData(
                    new Robot { Id = 1, Name = "Robot 1"},
                    new Robot { Id = 2, Name = "Robot 2" },
                    new Robot { Id = 3, Name = "Robot 3" }
                );
        }
    }
}
