using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<(List<Product> Items, int TotalCount)> GetProducts(string? description, int? minPrice, int? maxPrice, int[] categoriesId, int position = 1, int skip = 8);
        Task<Product> GetById(int id);

    }
}