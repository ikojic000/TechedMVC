using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TechedRazor.Models.ViewModel
{
    public class CoinViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string ImageURL { get; set; }

        [JsonProperty("current_price")]
        public double CurrentPrice { get; set; }

        [JsonProperty("market_cap_rank")]
        public int MarketCapRank { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public double PriceChangePercentage24h { get; set; }

        [JsonProperty("circulating_supply")]
        public double? CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public double? TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public double? MaxSupply { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }

        public string LastUpdatedFormatted => LastUpdated.ToString("dd/MM/yyyy HH:mm:ss");

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ChangedAt { get; set; }
    }
}
