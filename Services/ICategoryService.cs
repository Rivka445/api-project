using Entities;
using Entities.DTO;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategories();
    }
}