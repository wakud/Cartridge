using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cartridge.Models;

namespace Cartridge.Data
{
    public class MainContext : DbContext
    {
        public DbSet<Cartridges> Cartridges { get; set; }
        public DbSet<ModelCartridge> CartridgesModels { get; set; }
        public DbSet<ModelPrinter> PrintersModels { get; set; }
        public DbSet<Operations> Operation { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Punkt> Punkts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Stan> Stans { get; set; }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
            Database.EnsureCreated();     //створення БД без міграції
        }
    }
}
