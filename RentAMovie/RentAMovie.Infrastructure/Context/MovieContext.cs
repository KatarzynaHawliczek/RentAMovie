using System;
using Microsoft.EntityFrameworkCore;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Borrow> Borrow { get; set; }
        public DbSet<Client> Client { get; set; }
        
        public MovieContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=dbo.RentAMovieApi.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(x => x.Borrows);
               
            modelBuilder.Entity<Borrow>()
                .HasOne(x => x.Client);
               
            modelBuilder.Entity<Borrow>()
                .HasOne(x => x.Movie);
                
            modelBuilder.Entity<Client>()
                .HasMany(x => x.Borrows);         
        }
    }
}