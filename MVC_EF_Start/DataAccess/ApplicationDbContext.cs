using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

      //  public DbSet<FinancialDataCollection> FinancialDataCollection { get; set; }
       // public DbSet<Pagination> Pagination { get; set; }
        public DbSet<Models.Result> results { get; set; }
        public DbSet<Models.Filing> filings { get; set; }

        public DbSet<Models.CandOrCommittee> cand { get; set; }

        public DbSet<Models.Committee> com { get; set; }
    }
}