using Newtonsoft.Json;
using System.Diagnostics;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.ApiServices.Impl
{
    public class PublicApiService : IPublicApiService
    {

        private IList<CoinViewModel> _coinList = new List<CoinViewModel>();
        private readonly string baseURL = "https://api.coingecko.com/api/v3/coins/";
        private readonly string apiQueryString = "markets?vs_currency=usd&order=market_cap_desc&per_page=20&locale=en";
        private readonly string apiCookie = "__cf_bm=IRAAmfvSJyYkqyl8r.nK8R90NfFQ2zXq2dm6JDCy9As-1684805341-0-AbjIMogb/pRtg7YiSKMwB1sKVFUFJbifWs4yCrTA6kMtmjyR4udCXnzVmb00GZksYNWjvPAHC4pwTS3RmoGRjV4=";
        private readonly string apiUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 Safari/537.36";

        public async Task<IList<CoinViewModel>> GetCoinList()
        {
            if (_coinList.Count != 0) { return _coinList; }

            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, baseURL + apiQueryString);
            request.Headers.Add("User-Agent", apiUserAgent);
            request.Headers.Add("Cookie", apiCookie);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Request failed. Error status code: " + response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            _coinList = JsonConvert.DeserializeObject<List<CoinViewModel>>(jsonResponse);

            return _coinList;
        }
    }
}
