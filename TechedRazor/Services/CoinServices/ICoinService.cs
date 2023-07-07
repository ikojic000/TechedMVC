using Microsoft.AspNetCore.Mvc;
using RazorMovieTutorial.DataTables;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public interface ICoinService
    {
        Task<JsonResult> GetAllCoins(DataTablesRequest request);
        Task<CoinDTO?> GetCoinDetails(int? id);
    }
}
