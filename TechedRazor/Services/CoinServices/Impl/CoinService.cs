using Microsoft.AspNetCore.Mvc;
using RazorMovieTutorial.DataTables;
using TechedRazor.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using TechedRazor.Models.ViewModel;
using FluentValidation.Results;

namespace TechedRazor.Services.CoinServices.Impl
{
    public class CoinService : ICoinService
    {
        private readonly TechedRazorContext _context;
        private readonly ICoinMappingService _coinMappingService;
        private readonly ICoinValidationService _coinValidationService;
        private readonly IDatabaseService _databaseService;

        public CoinService(TechedRazorContext context, ICoinMappingService coinMappingService, ICoinValidationService coinValidationService, IDatabaseService databaseService)
        {
            _context = context;
            _coinMappingService = coinMappingService;
            _coinValidationService = coinValidationService;
            _databaseService = databaseService;
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

        //public async Task EditCoinParam(int id, string newName)
        //{
        //    var coinDTO = await _databaseService.GetCoinFromDatabaseAsync(id);
        //    if (coinDTO == null) { return; }
        //    coinDTO.Name = newName;
        //    var result = _coinValidationService.IsCoinDTOValid(coinDTO);
        //    if (result is ValidationResult)
        //    {
        //        return;
        //    }
        //    await _databaseService.UpdateCoinFromDatabase(coinDTO);
        //}

        //public async Task EditCoinParam(int id, double newPrice)
        //{
        //    var coinDTO = await _databaseService.GetCoinFromDatabaseAsync(id);
        //    if (coinDTO == null) { return; }
        //    coinDTO.CurrentPrice = newPrice;
        //    var result = _coinValidationService.IsCoinDTOValid(coinDTO);
        //    if (result is ValidationResult)
        //    {
        //        return;
        //    }
        //    await _databaseService.UpdateCoinFromDatabase(coinDTO);
        //}

        public async Task EditCoinParam(int id, object newValue)
        {
            var coinDTO = await _databaseService.GetCoinFromDatabaseAsync(id);

            if (coinDTO == null) { return; }

            if (newValue is string newName)
            {
                coinDTO.Name = newName;
            }
            else if (newValue is double newPrice)
            {
                coinDTO.CurrentPrice = newPrice;
            }
            else
            {
                return;
            }

            var result = _coinValidationService.IsCoinDTOValid(coinDTO);
            if (result is ValidationResult)
            {
                return;
            }

            await _databaseService.UpdateCoinFromDatabase(coinDTO);
        }


    }
}
