using Microsoft.AspNetCore.Mvc;
using RazorMovieTutorial.DataTables;
using TechedRazor.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices.Impl
{
    public class CoinService : ICoinService
    {
        private readonly TechedRazorContext _context;
        private readonly ICoinMappingService _coinMappingService;

        public CoinService(TechedRazorContext context, ICoinMappingService coinMappingService)
        {
            _context = context;
            _coinMappingService = coinMappingService;
        }

        public async Task<JsonResult> GetAllCoins(DataTablesRequest request)
        {
            var recordsTotal = _context.Coins.Count();
            var coinsQuery = _context.Coins.AsQueryable();

            var searchText = request.search.value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                coinsQuery = coinsQuery.Where(m => m.Name.ToUpper().Contains(searchText) ||
                                                     m.Symbol.ToUpper().Contains(searchText) ||
                                                     m.CurrentPrice.ToString().Contains(searchText)
                );
            }

            var recordsFiltered = coinsQuery.Count();

            var sortColumnName = request.columns.ElementAt(request.order.ElementAt(0).column).name;
            var sortDirection = request.order.ElementAt(0).dir.ToLower();

            coinsQuery = coinsQuery.OrderBy($"{sortColumnName} {sortDirection}");

            var skip = request.start;
            var take = request.length;
            var data = await coinsQuery
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            
            var dtoData = data.Select(coinEntity => _coinMappingService.MapToViewModel(coinEntity));

            return new JsonResult(
                new
                {
                    Draw = request.draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsFiltered,
                    Data = dtoData
                });
        }

        public async Task<CoinDTO?> GetCoinDetails(int? id)
        {
            if (id == null) { return null; }

            var coin = await _context.Coins.Where(coin => coin.Id == id)
                                           .FirstOrDefaultAsync();

            if (coin == null) { return null; }

            return _coinMappingService.MapToViewModel(coin);

        }
    }
}
