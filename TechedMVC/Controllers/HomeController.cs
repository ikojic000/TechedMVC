using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechedMVC.Models;
using Newtonsoft.Json;
using TechedMVC.Models.ViewModel;
using TechedMVC.Models.Domain;
using TechedMVC.Database;
using TechedMVC.Controllers.HomeService;

namespace TechedMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApiService apiService;

        public HomeController(ApiService apiService)
        {
            this.apiService = apiService;
        }


        // GET: Home
        public async Task<IActionResult> Index()
        {
            IList<CoinViewModel> coinList = await apiService.GetCoinList();

            return View(coinList);
        }


        // GET: Home/Details/2
        // Lista se nanovo popunjava s podacima s API-ja pri svakom pozivanju 
        public async Task<IActionResult> Details(string CoinId)
        {
            IList<CoinViewModel> coinList = await apiService.GetCoinList();
            var coin = coinList.FirstOrDefault(i => i.Id == CoinId);

            return View(coin);
        }


        // GET: Home/Details2/Json Value Of CoinViewModel object
        // Umjesto popunjavanja liste nanovo, metodi se salje objekt modela u JSON formatu
        public IActionResult Details2(string Coin)
        {
            CoinViewModel coinViewModel = JsonConvert.DeserializeObject<CoinViewModel>(Coin);

            return View("Details", coinViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
