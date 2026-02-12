using DTOs;

namespace Services
{
    public interface IModelService
    {
        Task<ModelDTO> GetModelById(int id);
        Task<FinalModels> GetModelds(
            string? description, int? minPrice, 
            int? maxPrice, int[] categoriesId, 
            string? color, int position = 1, int skip = 8);
        Task<ModelDTO> AddModel(NewModelDTO newModel);
        Task UpdateModel(int id, ModelDTO updateModel);
        Task DeleteModel(int id, ModelDTO deleteModel);
    }
}