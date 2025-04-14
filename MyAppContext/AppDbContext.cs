using API_1.Models;
using Microsoft.EntityFrameworkCore;

namespace API_1.MyAppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {


          
            
        }



        public DbSet<Product> Products { get; set; }
        public DbSet<Catigory> Catigories { get; set; }

    }
}
