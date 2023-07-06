using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public interface ICoinValidationService
    {
        Object IsCoinDTOValid(CoinDTO coinDTO);
    }
}
