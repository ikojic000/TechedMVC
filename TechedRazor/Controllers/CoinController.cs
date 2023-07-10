using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorMovieTutorial.DataTables;
using TechedRazor.Services.CoinServices;
using TechedRazor.Models;
using TechedRazor.Models.ViewModel;
using TechedRazor.Models.DTO;

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

        // For Pagination, Sorting and Filtering Table
        [HttpPost]
        [Route("GetCoins")]
        public async Task<JsonResult> GetCoins(DataTablesRequest request)
        {
            return await _coinService.GetAllCoins(request);
        }

        // AJAX GET call on Coin/Index for showing CoinDetailsPartial
        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            var coin = await _coinService.GetCoinDetails(id);
            return PartialView("_CoinDetailsPartial", coin);
        }

        //[HttpGet]
        //[Route("EditCoinName")]
        //public async Task<IActionResult> EditCoinName(int? id)
        //{
        //    var coin = await _coinService.GetCoinDetails(id);
        //    return PartialView("_ChangeNameForm", coin);
        //}

        [HttpGet]
        [Route("EditCoinName")]
        public async Task<IActionResult> EditCoinName(int? id, string columnName)
        {
            var coin = await _coinService.GetCoinDetails(id);
            ViewData["ColumnName"] = columnName;
            return PartialView("_EditCoinParamForm", coin);
        }

        //[HttpPost]
        //[Route("EditCoinName")]
        //public async Task<IActionResult> EditCoinName(int id, [FromBody] string newName)
        //{
        //    await _coinService.EditCoinParam(id, newName);
        //    return Ok(new { success = true });
        //}

        //[HttpPost]
        //[Route("EditCoinPrice")]
        //public async Task<IActionResult> EditCoinPrice(int id, [FromBody] double newPrice)
        //{
        //    await _coinService.EditCoinParam(id, newPrice);
        //    return Ok(new { success = true });
        //}

        [HttpPost]
        [Route("EditCoinParam")]
        public async Task<IActionResult> EditCoinParam(int id, [FromBody] CoinEditParamDTO param)
        {
            if (param.Name != null)
            {
                await _coinService.EditCoinParam(id, param.Name);
            }
            else if (param.Price.HasValue)
            {
                await _coinService.EditCoinParam(id, param.Price.Value);
            }
            else
            {
                return BadRequest("Invalid parameter.");
            }

            return Ok(new { success = true });
        }

    }
}
