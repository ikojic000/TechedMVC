using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices.Impl;

public class CoinMappingService : ICoinMappingService
{
    public CoinDTO MapToViewModel(CoinEntity coinEntity)
    {
        return new CoinDTO
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

    public CoinEntity MapToEntity(CoinDTO coinDTO)
    {
        return new CoinEntity
        {
            Symbol = coinDTO.Symbol,
            Name = coinDTO.Name,
            ImageURL = coinDTO.ImageURL,
            CurrentPrice = coinDTO.CurrentPrice,
            MarketCapRank = coinDTO.MarketCapRank,
            PriceChangePercentage24h = coinDTO.PriceChangePercentage24h,
            CirculatingSupply = coinDTO.CirculatingSupply,
            TotalSupply = coinDTO.TotalSupply,
            MaxSupply = coinDTO.MaxSupply,
            LastUpdated = coinDTO.LastUpdated,
            ChangedAt = coinDTO.ChangedAt
        };
    }

    public void UpdateCoinEntity(CoinEntity? coinEntity, CoinDTO? coinDTO)
    {
        if (coinDTO == null || coinEntity == null) return;

        coinEntity.Symbol = coinDTO.Symbol;
        coinEntity.Name = coinDTO.Name;
        coinEntity.ImageURL = coinDTO.ImageURL;
        coinEntity.CurrentPrice = coinDTO.CurrentPrice;
        coinEntity.MarketCapRank = coinDTO.MarketCapRank;
        coinEntity.PriceChangePercentage24h = coinDTO.PriceChangePercentage24h;
        coinEntity.CirculatingSupply = coinDTO.CirculatingSupply;
        coinEntity.TotalSupply = coinDTO.TotalSupply;
        coinEntity.MaxSupply = coinDTO.MaxSupply;
        coinEntity.ChangedAt = DateTime.Now;
    }
}