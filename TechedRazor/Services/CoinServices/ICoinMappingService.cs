using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public interface ICoinMappingService
    {
        CoinDTO MapToViewModel(CoinEntity coinEntity);
        CoinEntity MapToEntity(CoinDTO coinDTO);
        void UpdateCoinEntity(CoinEntity coinEntity, CoinDTO coinDTO);

    }
}
