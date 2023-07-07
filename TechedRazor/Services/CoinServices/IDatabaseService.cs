using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices
{
    public interface IDatabaseService
    {
        Task<List<CoinDTO>> GetAllFromDatabaseAsync(string nameSort, string search);
        
        void SaveToDatabase(CoinDTO? coinDTO);

        Task<CoinDTO?> GetCoinFromDatabaseAsync(int? id);

        Task DeleteCoinFromDatabaseAsync(int? id);

        Task UpdateCoinFromDatabase(CoinDTO? coinDTO);
    }
}