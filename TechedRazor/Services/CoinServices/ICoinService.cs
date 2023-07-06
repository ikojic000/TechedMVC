using Microsoft.AspNetCore.Mvc;
using RazorMovieTutorial.DataTables;
using System.Data;

namespace TechedRazor.Services.CoinServices
{
    public interface ICoinService
    {
        Task<JsonResult> GetAllMovies(DataTablesRequest request);
    }
}
