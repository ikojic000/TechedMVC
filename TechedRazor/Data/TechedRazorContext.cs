using Microsoft.EntityFrameworkCore;
using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Data
{
    public class TechedRazorContext : DbContext
    {
        public TechedRazorContext(DbContextOptions<TechedRazorContext> options)
            : base(options)
        {
        }

        public DbSet<CoinEntity> Coins { get; set; } = default!;

        public DbSet<TechedRazor.Models.ViewModel.CoinDTO> CoinViewModel { get; set; } = default!;
    }
}
