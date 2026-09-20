using Microsoft.EntityFrameworkCore;
using Site_Cadastro.Models;

namespace Site_Cadastro.Data
{   
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
    
        public DbSet<User> Users{get;set;}
    }
}