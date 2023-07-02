using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public interface IDatabaseService
    {
        Task<List<CoinViewModel>> GetAllFromDatabaseAsync();
        
        void SaveToDatabase(CoinViewModel? coinViewModel);

        Task<CoinViewModel> GetCoinFromDatabaseAsync(int? id);

        Task DeleteCoinFromDatabaseAsync(int? id);

    }
}