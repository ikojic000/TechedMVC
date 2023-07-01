using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public interface ICoinMappingService
    {
        CoinViewModel MapToViewModel(CoinEntity coinEntity);
        CoinEntity MapToEntity(CoinViewModel coinViewModel);

    }
}
