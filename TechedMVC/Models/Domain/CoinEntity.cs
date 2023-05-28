using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TechedMVC.Models.Domain
{
    public class CoinEntity
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public double CurrentPrice { get; set; }

        public int MarketCapRank { get; set; }

        public double PriceChangePercentage24h { get; set; }

        public double? CirculatingSupply { get; set; }

        public double? TotalSupply { get; set; }

        public double? MaxSupply { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime? ChangedAt { get; set; }

    }
}
