using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechedRazor.Data;
using TechedRazor.Models.Domain;
using TechedRazor.Models.Validators;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices.Impl
{
    public class DatabaseService : IDatabaseService
    {
        private readonly TechedRazorContext _context;
        private readonly ICoinValidationService _coinValidationService;
        private readonly ICoinMappingService _coinMappingService;

        public DatabaseService(TechedRazorContext context, ICoinValidationService _validatorService, ICoinMappingService coinMappingService)
        {
            _context = context;
            _coinValidationService = _validatorService;
            _coinMappingService = coinMappingService;

        }

        public async Task<List<CoinDTO>> GetAllFromDatabaseAsync(string nameSort, string search)
        {

            List<CoinDTO> coinList = new();
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

        public void SaveToDatabase(CoinDTO? coinDTO)
        {
            if (coinDTO == null) { return; }

            if (_coinValidationService.IsCoinDTOValid(coinDTO) is not bool || false) { return; }

            CoinEntity coinEntity = _coinMappingService.MapToEntity(coinDTO);
            coinEntity.ChangedAt = DateTime.Now;

            _context.Add(coinEntity);
            _context.SaveChanges();
        }

        public async Task<CoinDTO?> GetCoinFromDatabaseAsync(int? id)
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

        public async Task UpdateCoinFromDatabase(CoinDTO? coinDTO)
        {
            var coinEntity = await _context.Coins.FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(coinDTO.Id));

            _context.Entry(coinEntity).State = EntityState.Modified;

            _coinMappingService.UpdateCoinEntity(coinEntity, coinDTO);

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