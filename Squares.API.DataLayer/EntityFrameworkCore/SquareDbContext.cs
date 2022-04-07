using Microsoft.EntityFrameworkCore;
using Squares.API.DataLayer.Core.Repository;
using Squares.API.DataLayer.Entities;

namespace Squares.API.DataLayer.EntityFrameworkCore
{
    public class SquareDbContext : DbContext
    {

        public SquareDbContext(DbContextOptions<SquareDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<Coordinate> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetail>().HasIndex(i => i.Email).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }

    public class SquareUnitOfWork:ISquareUOW
    {
        public SquareUnitOfWork(SquareDbContext context)
        {
            dbContext = context;
        }

        public DbContext dbContext { get; set ; }
    }
}
