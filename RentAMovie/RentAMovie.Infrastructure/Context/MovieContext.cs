using Microsoft.EntityFrameworkCore;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Address> Address { get; set; }
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
                .HasOne(x => x.Client)
                .WithMany(y => y.Movies)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Borrow>()
                .HasOne(x => x.Movie)
                .WithOne(y => y.Borrow)
                .HasForeignKey<Movie>(z => z.Id);
            modelBuilder.Entity<Borrow>()
                .HasOne(x => x.Client)
                .WithOne(y => y.Borrow)
                .HasForeignKey<Client>(z => z.Id);
        }
    }
}