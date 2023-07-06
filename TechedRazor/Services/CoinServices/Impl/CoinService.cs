﻿using Microsoft.AspNetCore.Mvc;
using RazorMovieTutorial.DataTables;
using TechedRazor.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace TechedRazor.Services.CoinServices.Impl
{
    public class CoinService : ICoinService
    {
        private readonly TechedRazorContext _context;

        public CoinService(TechedRazorContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> GetAllMovies(DataTablesRequest request)
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

            return new JsonResult(
                new
                {
                    Draw = request.draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsFiltered,
                    Data = data
                });
        }
    }
}