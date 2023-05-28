using Microsoft.EntityFrameworkCore;
using TechedMVC.Models.Domain;

namespace TechedMVC.Database
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CoinEntity> Coins { get; set; }

    }
}
