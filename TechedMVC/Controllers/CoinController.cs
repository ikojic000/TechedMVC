using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing;
using TechedMVC.Controllers.CoinService;
using TechedMVC.Controllers.HomeService;
using TechedMVC.Database;
using TechedMVC.Models.Domain;
using TechedMVC.Models.ViewModel;

namespace TechedMVC.Controllers
{
    public class CoinController : Controller
    {
        private readonly DefaultDbContext dbContext;
        private readonly ApiService apiService;
        private readonly CoinMappingService coinMappingService;

        public CoinController(DefaultDbContext dbContext, ApiService apiService, CoinMappingService coinMappingService)
        {
            this.dbContext = dbContext;
            this.apiService = apiService;
            this.coinMappingService = coinMappingService;
        }


        // GET: Coin
        public async Task<IActionResult> Index()
        {
            var coins = await dbContext.Coins.ToListAsync();
            var coinViewModels = coins.Select(coinEntity => coinMappingService.MapToViewModel(coinEntity));

            return View(coinViewModels);
        }

        // GET: Coin/Add/Json Value Of CoinViewModel object
        // Umjesto popunjavanja liste nanovo, metodi se salje objekt modela u JSON formatu
        [HttpPost]
        public IActionResult Add(string CoinViewModel)
        {
            CoinViewModel coinViewModel = JsonConvert.DeserializeObject<CoinViewModel>(CoinViewModel);

            if (coinViewModel == null)
            {
                return BadRequest();
            }
            else
            {
                CoinEntity coinEntity = coinMappingService.MapToEntity(coinViewModel);
                coinEntity.ChangedAt = DateTime.Now;

                dbContext.Add(coinEntity);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Coin");
        }


        // POST: Coin/AddToDb/2
        [HttpPost]
        public async Task<IActionResult> AddToDb(string CoinId)
        {
            IList<CoinViewModel> coinList = await apiService.GetCoinList();
            var coinViewModel = coinList.FirstOrDefault(i => i.Id == CoinId);

            if (coinViewModel == null)
            {
                return BadRequest();

            }
            else
            {
                CoinEntity coinEntity = coinMappingService.MapToEntity(coinViewModel);
                coinEntity.ChangedAt = DateTime.Now;

                dbContext.Add(coinEntity);
                dbContext.SaveChanges();

                return RedirectToAction("Index", "Coin");
            }
        }


        // GET: Coin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.Coins == null)
            {
                return NotFound();
            }

            var coinEntity = await dbContext.Coins
                .FirstOrDefaultAsync(m => m.Id == id);

            if (coinEntity == null)
            {
                return NotFound();
            }

            CoinViewModel coinViewModel = coinMappingService.MapToViewModel(coinEntity);

            return View(coinViewModel);
        }


        // GET: Coin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.Coins == null)
            {
                return NotFound();
            }

            var coinEntity = await dbContext.Coins.FindAsync(id);

            if (coinEntity == null)
            {
                return NotFound();
            }

            CoinViewModel coinViewModel = coinMappingService.MapToViewModel(coinEntity);

            return View(coinViewModel);
        }

        // POST: Coin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CoinViewModel coinViewModel)
        {
            try
            {
                if (id != Int32.Parse(coinViewModel.Id))
                {
                    return NotFound();
                }
            }
            catch (FormatException ex)
            {
                return BadRequest();
            }


            if (ModelState.IsValid)
            {
                var coinEntity = await dbContext.Coins.FindAsync(id);

                if (coinEntity == null)
                {
                    return NotFound();
                }

                coinMappingService.UpdateCoinEntity(coinEntity, coinViewModel);

                dbContext.Update(coinEntity);
                await dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(coinViewModel);
        }

        // GET: Coin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.Coins == null)
            {
                return NotFound();
            }

            var coinEntity = await dbContext.Coins
                .FirstOrDefaultAsync(m => m.Id == id);

            if (coinEntity == null)
            {
                return NotFound();
            }

            CoinViewModel coinViewModel = coinMappingService.MapToViewModel(coinEntity);

            return View(coinViewModel);
        }

        // POST: Coin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.Coins == null)
            {
                return Problem("Entity set 'DefaultDbContext.Coins'  is null.");
            }
            var coinEntity = await dbContext.Coins.FindAsync(id);
            if (coinEntity != null)
            {
                dbContext.Coins.Remove(coinEntity);
            }

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}