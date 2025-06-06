using Microsoft.EntityFrameworkCore;

namespace ApiTest
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Person> Person { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=DESKTOP-L3SMK21\\SQLEXPRESS; Database=DotNetTraining;User Id=sa; Password=sasa@123; TrustServerCertificate=true");
            }
        }
    }
}
