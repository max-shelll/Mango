using AutoMapper;
using Mango.Services.ProductAPI.DAL.Models;
using Mango.Services.ProductAPI.DAL.Models.Dtos;
using Mango.Services.ProductAPI.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.DAL.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _db;
		private readonly IMapper _mapper;

		public ProductRepository(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
		{
			var product = _mapper.Map<Product>(productDto);

			_db.Products.Add(product);

			await _db.SaveChangesAsync();
			return _mapper.Map<ProductDto>(product);
		}

		public async Task<IEnumerable<ProductDto>> GetProductsAsync()
		{
			var productList = await _db.Products.ToListAsync();

			return _mapper.Map<List<ProductDto>>(productList);
		}

		public async Task<ProductDto> GetProductByIdAsync(int productId)
		{
			var product = await _db.Products.FirstAsync(i => i.Id == productId);

			return _mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto> GetProductByNameAsync(string productName)
		{
			var product = await _db.Products.FirstAsync(i => i.Name == productName);

			return _mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
		{
			var product = _mapper.Map<Product>(productDto);

			_db.Products.Update(product);

			await _db.SaveChangesAsync();
			return _mapper.Map<ProductDto>(product);
		}

		public async Task DeleteProductAsync(int productId)
		{
			var product = await _db.Products.FirstAsync(i => i.Id == productId);

			_db.Products.Remove(product);

			await _db.SaveChangesAsync();
		}
	}
}
