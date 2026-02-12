using Entities;

namespace Repositories
{
    public interface IDressRepository
    {
        Task<Dress?> GetDressById(int id);
        Task<List<string>> GetSizesByModelId(int modelId);
        Task<int> GetCountByModelIdAndSizeForDate(int modelId, string size, DateOnly date);
        Task<Dress> AddDress(Dress dress);
        Task UpdateDress(Dress dress);
        Task DeleteDress(Dress dress);
    }
}