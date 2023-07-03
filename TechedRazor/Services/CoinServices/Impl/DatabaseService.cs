using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TechedRazor.Data;
using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices.Impl
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

        public async Task<List<CoinViewModel>> GetAllFromDatabaseAsync(string nameSort, string search)
        {

            List<CoinViewModel> coinList = new();
            List<CoinEntity> dbList = await _context.Coins.ToListAsync();

            coinList.AddRange(from CoinEntity coin in dbList
                              select _coinMappingService.MapToViewModel(coin));

            if (!String.IsNullOrEmpty(search))
            {
                coinList = coinList.Where(coin => coin.Name.Contains(search)).ToList();
            }

            coinList = nameSort switch
            {
                "name_desc" => coinList.OrderByDescending(coin => coin.Name).ToList(),
                _ => coinList.OrderBy(coin => coin.Name).ToList(),
            };
            return coinList;
        }

        public void SaveToDatabase(CoinViewModel? coinViewModel)
        {
            if (coinViewModel == null) { return; }

            CoinEntity coinEntity = _coinMappingService.MapToEntity(coinViewModel);
            coinEntity.ChangedAt = DateTime.Now;

            _context.Add(coinEntity);
            _context.SaveChanges();
        }

        public async Task<CoinViewModel> GetCoinFromDatabaseAsync(int? id)
        {
            var coinEntity = await _context.Coins.FirstOrDefaultAsync(m => m.Id == id);

            if (coinEntity == null) { return null; }

            return _coinMappingService.MapToViewModel(coinEntity);
        }

        public async Task DeleteCoinFromDatabaseAsync(int? id)
        {
            try
            {
                if (id == null) { return; }

                var coinEntity = await _context.Coins.FirstOrDefaultAsync(m => m.Id == id);

                if (coinEntity == null) { return; }

                _context.Coins.Remove(coinEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }

        }

        public async Task UpdateCoinFromDatabase(CoinViewModel? coinViewModel)
        {
            var coinEntity = await _context.Coins.FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(coinViewModel.Id));

            _context.Entry(coinEntity).State = EntityState.Modified;

            _coinMappingService.UpdateCoinEntity(coinEntity, coinViewModel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinEntityExists(coinEntity.Id)) { return; }
                else { throw; }
            }
        }

        private bool CoinEntityExists(int id)
        {
            return (_context.Coins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}