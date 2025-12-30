using Entities;
using Moq;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Tests
{
    public class ProductRepositoryUnitTesting
    {
        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            // Arrange
            var _mockContext = new Mock<WebApiShopContext>();
            var product = new Product { Id = 1, Name = "Product1", Price = 100, Description = "Description1", CategoryId = 1 };
            var products = new List<Product> { product };

            _mockContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            var _productRepository = new ProductRepository(_mockContext.Object);

            // Act
            var result = await _productRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Product1", result.Name);
        }

        [Fact]
        public async Task GetProductById_ReturnsNull()
        {
            // Arrange
            var products = new List<Product>();
            var _mockContext = new Mock<WebApiShopContext>();

            _mockContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            var _productRepository = new ProductRepository(_mockContext.Object);

            // Act
            var result = await _productRepository.GetById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetProducts_ReturnsFilteredProducts()
        {
            // Arrange
            var _mockContext = new Mock<WebApiShopContext>();
            var categoryId = 1;
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 1200, CategoryId = categoryId },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 800, CategoryId = categoryId },
                new Product { Id = 3, Name = "Headphones", Description = "Noise cancelling headphones", Price = 100, CategoryId = categoryId }
            };

            _mockContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            var _productRepository = new ProductRepository(_mockContext.Object);
            int[] c = { categoryId };
            // Act
            var (items, totalCount) = await _productRepository.GetProducts("smart", 50, 1000, c,1,2 );

            // Assert
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(1, totalCount);
            Assert.Equal("Smartphone", items.First().Name); // Verify the returned product is the smartphone
        }

        [Fact]
        public async Task GetProducts_ReturnsEmptyList()
        {
            // Arrange
            var _mockContext = new Mock<WebApiShopContext>();
            var products = new List<Product>();

            _mockContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            var _productRepository = new ProductRepository(_mockContext.Object);

            // Act
            var (items, totalCount) = await _productRepository.GetProducts("NonExisting", 1000, 2000, new int[] { 1 });

            // Assert
            Assert.NotNull(items);
            Assert.Empty(items); // Verify that no products are returned
            Assert.Equal(0, totalCount); // Verify that total count is 0
        }
    }
}