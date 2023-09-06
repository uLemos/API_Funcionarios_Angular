using API_Angular.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Angular.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FuncionarioModel> Funcionarios { get; set; }
    }
}
