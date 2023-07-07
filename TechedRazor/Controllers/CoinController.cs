using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorMovieTutorial.DataTables;
using TechedRazor.Services.CoinServices;
using TechedRazor.Models;

namespace TechedRazor.Controllers
{

    [ApiController]
    [Route("/api/v1/[controller]")]
    [IgnoreAntiforgeryToken]
    public class CoinController : Controller
    {
        private readonly ICoinService _coinService;

        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [HttpPost]
        [Route("GetCoins")]
        public async Task<JsonResult> GetCoins(DataTablesRequest request)
        {
            return await _coinService.GetAllCoins(request);
        }

        //[HttpGet]
        //[Route("Details")]
        //public async Task<CoinDTO?> Details(int? id)
        //{
        //    return await _coinService.GetCoinDetails(id);
        //}

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            var coin = await _coinService.GetCoinDetails(id);
            return PartialView("_CoinDetailsPartial", coin);
        }
    }
}
