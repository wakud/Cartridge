using Cartridge.Models;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
            //Database.EnsureCreated();     //створення БД без міграції
        }
    }
}
