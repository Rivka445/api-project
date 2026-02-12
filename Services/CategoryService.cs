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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;       
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategoryDTO>> GetCategories()
        {
            List<Category> categories = await _categoryRepository.GetCategories();
            List<CategoryDTO> categoriesDTO = _mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return categoriesDTO;    
        }
        public async Task<CategoryDTO> AddCategory(CategoryDTO newCategory)
        {
            if (newCategory == null)
                throw new ArgumentNullException(nameof(newCategory));
            Category category = _mapper.Map<CategoryDTO ,Category>(newCategory);
            Category addedCategory = await _categoryRepository.AddCategory(category);
            CategoryDTO categoryDTO = _mapper.Map<Category, CategoryDTO>(addedCategory);
            return categoryDTO;
        }
    }
}
