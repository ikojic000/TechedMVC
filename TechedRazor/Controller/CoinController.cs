using Microsoft.AspNetCore.Mvc;
using RazorMovieTutorial.DataTables;
using TechedRazor.Services.CoinServices;

namespace TechedRazor.Controller
{

    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService _coinService;

        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [HttpPost]
        public async Task<JsonResult> GetCoins(DataTablesRequest request)
        {
            return await _coinService.GetAllMovies(request);
        }

    }
}
