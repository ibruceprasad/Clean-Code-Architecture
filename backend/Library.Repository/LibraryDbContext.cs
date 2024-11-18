using libraries.domain;
using library.repository.Configure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace library.repository;
 
    public class LibraryDbContext : DbContext 
    {
        public DbSet<Book> Books => Set<Book>();

    public LibraryDbContext()
    {
        
    }
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var appDataLocalFolder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(appDataLocalFolder);
            optionsBuilder.UseSqlite($"Data Source={Path.Join(path, "library.db")}");
        }
        //optionsBuilder.UseSqlServer("Server=.;Database=librarry-db-mssql;Integrated Security=True;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedOnAdd()
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            base.OnModelCreating(modelBuilder);
        }
    }
 