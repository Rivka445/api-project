using DTOs;

namespace Services
{
    public interface IDressService
    {
        Task<DressDTO> GetDressById(int id);
        Task<List<string>> GetSizesByModelId(int modelId);
        Task<int> GetCountByModelIdAndSizeForDate(int modelId, string size, DateOnly date);
        Task<DressDTO> AddDress(NewDressDTO newDress);
        Task UpdateDress(int id, DressDTO updateDress);
        Task DeleteDress(int id, DressDTO deleteDress);

    }
}