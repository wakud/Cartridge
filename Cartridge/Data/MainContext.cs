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
        public DbSet<ModelCartridge> CartridgesModel { get; set; }
        public DbSet<ModelPrinter> PrintersModel { get; set; }
        public DbSet<Punkt> Punkts { get; set; }
        public DbSet<Service> Zapravka { get; set; }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
            Database.EnsureCreated();     //створення БД без міграції
        }
    }
}
