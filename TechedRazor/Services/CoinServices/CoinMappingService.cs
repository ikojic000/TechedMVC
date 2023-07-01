using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public class CoinMappingService : ICoinMappingService
    {

        public CoinViewModel MapToViewModel(CoinEntity coinEntity)
        {
            return new CoinViewModel
            {
                Id = coinEntity.Id.ToString(),
                Symbol = coinEntity.Symbol,
                Name = coinEntity.Name,
                ImageURL = coinEntity.ImageURL,
                CurrentPrice = coinEntity.CurrentPrice,
                MarketCapRank = coinEntity.MarketCapRank,
                PriceChangePercentage24h = coinEntity.PriceChangePercentage24h,
                CirculatingSupply = coinEntity.CirculatingSupply,
                TotalSupply = coinEntity.TotalSupply,
                MaxSupply = coinEntity.MaxSupply,
                LastUpdated = coinEntity.LastUpdated,
                ChangedAt = coinEntity.ChangedAt
            };
        }

        public CoinEntity MapToEntity(CoinViewModel coinViewModel)
        {
            return new CoinEntity
            {
                Symbol = coinViewModel.Symbol,
                Name = coinViewModel.Name,
                ImageURL = coinViewModel.ImageURL,
                CurrentPrice = coinViewModel.CurrentPrice,
                MarketCapRank = coinViewModel.MarketCapRank,
                PriceChangePercentage24h = coinViewModel.PriceChangePercentage24h,
                CirculatingSupply = coinViewModel.CirculatingSupply,
                TotalSupply = coinViewModel.TotalSupply,
                MaxSupply = coinViewModel.MaxSupply,
                LastUpdated = coinViewModel.LastUpdated,
                ChangedAt = coinViewModel.ChangedAt
            };

        }

    }
}
