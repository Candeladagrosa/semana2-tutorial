using Microsoft.EntityFrameworkCore;
using semana2_tutorial.Models;

namespace semana2_tutorial.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
