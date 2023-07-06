using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.ApiServices
{
    public interface IPublicApiService
    {
        Task<IList<CoinDTO>> GetCoinList();
    }
}