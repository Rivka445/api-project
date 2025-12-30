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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _mapper=mapper;
            _productRepository = productRepository;
        }
        public async Task<FinalProducts> GetProducts(string? description, int? minPrice, int? maxPrice, int[] categoriesId, int position = 1, int skip = 8)
        {
            (List<Product> Items, int TotalCount) products = await _productRepository.GetProducts(description, minPrice, maxPrice, categoriesId, position, skip);
            List<ProductDTO> productsDTO = _mapper.Map<List<Product>, List<ProductDTO>>(products.Items);
            bool hasNext = (products.TotalCount - (position * skip)) > 0;
            bool hasPrev = position > 1;
            FinalProducts finalProducts = new()
            {
                Items = productsDTO,
                TotalCount=products.TotalCount,
                HasNext=hasNext,
                HasPrev=hasPrev
            };
            return finalProducts;
        }
        public async Task<ProductDTO> GetById(int id)
        {
            Product product = await _productRepository.GetById(id);
            ProductDTO productDTO = _mapper.Map<Product, ProductDTO>(product);
            return productDTO;
        }
    }
}
