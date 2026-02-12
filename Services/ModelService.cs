using AutoMapper;
using Entities;
using DTOs;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IDressService _dressService;
        private readonly IMapper _mapper;
        public ModelService(IModelRepository modelRepository, IMapper mapper, IDressService dressService)
        {
            _mapper = mapper;
            _dressService = dressService;
            _modelRepository = modelRepository;
        }
        public async Task<ModelDTO> GetModelById(int id)
        {
            Model? model = await _modelRepository.GetModelById(id);
            if (model == null)
                throw new KeyNotFoundException($"Model with ID {id} not found.");
            ModelDTO modelDTO = _mapper.Map<Model, ModelDTO>(model);
            return modelDTO;
        }
        public async Task<FinalModels> GetModelds(string? description, int? minPrice, int? maxPrice,
            int[] categoriesId, string? color, int position = 1, int skip = 8)
        {
            if (position <= 0)
                throw new ArgumentException("Position must be greater than 0.");
            if (skip <= 0)
                throw new ArgumentException("Skip must be greater than 0.");
            if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
                throw new ArgumentException("minPrice cannot be greater than maxPrice.");

            (List<Model> Items, int TotalCount) products = await _modelRepository
                        .GetModels(description, minPrice, maxPrice, categoriesId, color, position, skip);
            List<ModelDTO> productsDTO = _mapper.Map<List<Model>, List<ModelDTO>>(products.Items);
            bool hasNext = (products.TotalCount - (position * skip)) > 0;
            bool hasPrev = position > 1;
            FinalModels finalProducts = new()
            {
                Items = productsDTO,
                TotalCount = products.TotalCount,
                HasNext = hasNext,
                HasPrev = hasPrev
            };
            return finalProducts;
        }
        public async Task<ModelDTO> AddModel(NewModelDTO newModel)
        {
            if (newModel == null)
                throw new ArgumentNullException(nameof(newModel));
            if (newModel.BasePrice <= 0)
                throw new ArgumentException("BasePrice must be greater than 0.");

            Model addedModel = _mapper.Map<NewModelDTO, Model>(newModel);
            Model model = await _modelRepository.AddModel(addedModel);
            ModelDTO modelDTO = _mapper.Map<Model, ModelDTO>(model);
            return modelDTO;
        }
        public async Task UpdateModel(int id, ModelDTO updateModel)
        {
            if (updateModel == null)
                throw new ArgumentNullException(nameof(updateModel));

            if (updateModel.BasePrice <= 0)
                throw new ArgumentException("BasePrice must be greater than 0.");
            if (await _modelRepository.GetModelById(id) == null)
                throw new KeyNotFoundException($"Model with ID {id} not found.");

            Model update = _mapper.Map<ModelDTO, Model>(updateModel);
            await _modelRepository.UpdateModel(update);
        }
        public async Task DeleteModel(int id, ModelDTO deleteModel)
        {
            if (deleteModel == null)
                throw new ArgumentNullException(nameof(deleteModel));
            if (await _modelRepository.GetModelById(id) == null)
                throw new KeyNotFoundException($"Model with ID {id} not found.");

            Model model = _mapper.Map<ModelDTO, Model>(deleteModel);
            model.IsActive = false;
            foreach (var dress in model.Dresses)
            {
                DressDTO dressDTO = _mapper.Map<Dress, DressDTO>(dress);
                await _dressService.DeleteDress(dress.Id, dressDTO);
            }
            await _modelRepository.DeleteModel(model);
        }
    }
}
