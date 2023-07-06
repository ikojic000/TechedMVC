using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechedRazor.Models.ViewModel
{
    public class CoinDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [DisplayName("Simbol")]
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [DisplayName("Naziv")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DisplayName("Slika")]
        [JsonProperty("image")]
        public string ImageURL { get; set; }

        [DisplayName("Cijena")]
        [JsonProperty("current_price")]
        public double CurrentPrice { get; set; }

        [DisplayName("Tržišni rang")]
        [JsonProperty("market_cap_rank")]
        public int MarketCapRank { get; set; }

        [DisplayName("Promjena cijene u 24h")]
        [JsonProperty("price_change_percentage_24h")]
        public double PriceChangePercentage24h { get; set; }

        [DisplayName("Circulating Supply")]
        [JsonProperty("circulating_supply")]
        public double? CirculatingSupply { get; set; }

        [DisplayName("Total Supply")]
        [JsonProperty("total_supply")]
        public double? TotalSupply { get; set; }

        [DisplayName("Max Supply")]
        [JsonProperty("max_supply")]
        public double? MaxSupply { get; set; }

        [DisplayName("Zadnje ažurirano")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }

        public string LastUpdatedFormatted => LastUpdated.ToString("dd/MM/yyyy HH:mm:ss");

        [DisplayName("Spremljeno")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ChangedAt { get; set; }
    }
}
