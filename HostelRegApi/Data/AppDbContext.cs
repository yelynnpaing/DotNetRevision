using HostelRegApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HostelRegApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Resident> Residents { get; set; }
    }
}
