using Entities;
using DTOs;
namespace Services
{
    public interface IProductService
    {
        Task<ProductDTO> GetById(int id);
        Task<FinalProducts> GetProducts(string? description, int? minPrice, int? maxPrice, int[] categoriesId, int position=1, int skip=8);
    }
}