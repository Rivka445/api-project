using AutoMapper;
using DTOs;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Services
{
    public class DressService : IDressService
    {
        private readonly IDressRepository _dressRepository;
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;
        public DressService(IDressRepository dressRepository, IMapper mapper, IModelService modelService)
        {
            _mapper = mapper;
            _dressRepository = dressRepository;
            _modelService = modelService;
        }
        public async Task<DressDTO> GetDressById(int id)
        {
            Dress? dress = await _dressRepository.GetDressById(id);

            if (dress == null)
                throw new Exception($"dress with ID {id} not found.");

            DressDTO dressDTO = _mapper.Map<Dress, DressDTO>(dress);
            return dressDTO;
        }
        public async Task<List<string>> GetSizesByModelId(int modelId)
        {
            if (_modelService.GetModelById(modelId) == null)
                throw new ArgumentException($"Model with ID {modelId} not found.");

            return await _dressRepository.GetSizesByModelId(modelId);
        }
        public async Task<int> GetCountByModelIdAndSizeForDate(int modelId, string size, DateOnly date)
        {
            if (await _modelService.GetModelById(modelId) == null)
                throw new ArgumentException($"Model with ID {modelId} not found.");

            if (date < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("The date cannot be in the past.");

            return await _dressRepository.GetCountByModelIdAndSizeForDate(modelId, size, date);
        }
        public async Task<DressDTO> AddDress(NewDressDTO newDress)
        {
            if (newDress == null)
                throw new ArgumentNullException(nameof(newDress), "NewDressDTO cannot be null.");

            if (await _modelService.GetModelById(newDress.ModelId) == null)
                throw new ArgumentException($"Model with ID {newDress.ModelId} not found.");

            if (newDress.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            Dress addedDress = _mapper.Map<NewDressDTO, Dress>(newDress);
            Dress dress = await _dressRepository.AddDress(addedDress);
            DressDTO dressDTO = _mapper.Map<Dress, DressDTO>(dress);
            return dressDTO;
        }
        public async Task UpdateDress(int id, DressDTO updateDress)
        {
            if (updateDress == null)
                throw new ArgumentNullException(nameof(updateDress));

            if (await _dressRepository.GetDressById(id) == null)
                throw new KeyNotFoundException($"Dress with ID {id} not found.");

            if (updateDress.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            Dress update = _mapper.Map<DressDTO, Dress>(updateDress);

            await _dressRepository.UpdateDress(update);
        }
        public async Task DeleteDress(int id, DressDTO deleteDress)
        {
            if (!deleteDress.IsActive)
                throw new InvalidOperationException("Dress is already inactive.");

            if (await _dressRepository.GetDressById(id) == null)
                throw new KeyNotFoundException($"Dress with ID {id} not found.");

            Dress dress = _mapper.Map<DressDTO, Dress>(deleteDress);
            dress.IsActive = false;
            await _dressRepository.DeleteDress(dress);
        }

    }
}
