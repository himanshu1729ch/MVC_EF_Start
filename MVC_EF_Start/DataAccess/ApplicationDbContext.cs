using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FinancialDataCollection> FinancialDataCollection { get; set; }
        public DbSet<Pagination> Pagination { get; set; }
        public DbSet<Result> results { get; set; }
    }
}