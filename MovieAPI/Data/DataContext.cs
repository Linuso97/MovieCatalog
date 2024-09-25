using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    /*
    Summary: This class represents the database context for the application. 
    It is responsible for interacting with the database using Entity Framework Core and provides 
    a DbSet<Movie> to manage CRUD operations for the Movie entity.
    */
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
