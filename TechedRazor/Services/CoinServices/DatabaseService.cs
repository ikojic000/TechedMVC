using Microsoft.EntityFrameworkCore;
using TechedRazor.Data;
using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public class DatabaseService : IDatabaseService
    {
        private readonly TechedRazorContext _context;
        private readonly ICoinMappingService _coinMappingService;

        public DatabaseService(TechedRazorContext context, ICoinMappingService coinMappingService)
        {
            _context = context;
            _coinMappingService = coinMappingService;
        }

        public async Task<List<CoinViewModel>> GetAllFromDatabaseAsync()
        {
            List<CoinViewModel> coinList = new();
            List<CoinEntity> dbList = await _context.Coins.ToListAsync();

            foreach (CoinEntity coin in dbList)
            {
                coinList.Add(_coinMappingService.MapToViewModel(coin));
            }

            return coinList;
        }

        public void SaveToDatabase(CoinViewModel? coinViewModel)
        {

            if (coinViewModel != null)
            {
                CoinEntity coinEntity = _coinMappingService.MapToEntity(coinViewModel);
                coinEntity.ChangedAt = DateTime.Now;

                _context.Add(coinEntity);
                _context.SaveChanges();

            }
        }

        public async Task<CoinViewModel> GetCoinFromDatabaseAsync(int? id)
        {
            if (id != null)
            {
                var coinEntity = await _context.Coins.FirstOrDefaultAsync(m => m.Id == id);
                if (coinEntity != null)
                {
                    CoinViewModel coinViewModel = _coinMappingService.MapToViewModel(coinEntity);

                    return coinViewModel;
                }
            }
            return null;
        }

        public async Task DeleteCoinFromDatabaseAsync(int? id)
        {
            try
            {
                if (id != null)
                {
                    var coinEntity = await _context.Coins.FirstOrDefaultAsync(m => m.Id == id);

                    if (coinEntity != null)
                    {
                        _context.Coins.Remove(coinEntity);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
