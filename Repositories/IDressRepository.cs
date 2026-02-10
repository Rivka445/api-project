using Entities;

namespace Repositories
{
    public interface IDressRepository
    {
        Task<Dress> addDress(Dress dress);
        Task<int> GetCountByIdAndSize(int id, string size);
        Task<Dress> GetDressById(int id);
    }
}