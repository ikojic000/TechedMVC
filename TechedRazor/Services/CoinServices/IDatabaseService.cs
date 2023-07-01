using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public interface IDatabaseService
    {
        Task<List<CoinViewModel>> GetAllFromDatabaseAsync();
        
        void SaveToDatabase(CoinViewModel? coinViewModel);

        Task<CoinViewModel> GetCoinFromDatabaseAsync(int? id);

        void DeleteCoinFromDatabaseAsync(int? id);

    }
}