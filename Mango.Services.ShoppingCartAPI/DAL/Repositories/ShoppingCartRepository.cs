using AutoMapper;
using Mango.Services.ShoppingCartAPI.DAL.Models;
using Mango.Services.ShoppingCartAPI.DAL.Models.Dtos;
using Mango.Services.ShoppingCartAPI.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.DAL.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ShoppingCartRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CartHeaderDto> CreateCartHeaderAsync(CartHeaderDto cartHeaderDto)
        {
            var cartHeader = _mapper.Map<CartHeader>(cartHeaderDto);

            _db.CartHeaders.Add(cartHeader);

            await _db.SaveChangesAsync();
            return _mapper.Map<CartHeaderDto>(cartHeader);
        }

        public async Task<CartDetailsDto> CreateCartDetailsAsync(CartDetailsDto cartDetailsDto)
        {
            var cartDetails = _mapper.Map<CartDetails>(cartDetailsDto);

            _db.CartDetails.Add(cartDetails);

            await _db.SaveChangesAsync();
            return _mapper.Map<CartDetailsDto>(cartDetails);
        }

        public async Task<CartHeaderDto> GetCartHeaderByUserIdAsync(string? userId)
        {
            var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);

            return _mapper.Map<CartHeaderDto>(cartHeaderFromDb);
        }

        public async Task<CartHeaderDto> GetCartHeaderByIdAsync(int cartHeaderId)
        {
            var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.Id == cartHeaderId);

            return _mapper.Map<CartHeaderDto>(cartHeaderFromDb);
        }

        public async Task<CartDetailsDto> GetCartDetailsByIdAsync(int cartDetailsId)
        {
            var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.Id == cartDetailsId);

            return _mapper.Map<CartDetailsDto>(cartDetailsFromDb);
        }

        public int GetTotalCountOfCartItem(int cartHeaderId)
        {
            return _db.CartDetails.Where(u => u.CartHeaderId == cartHeaderId).Count();
        }

        public async Task<CartDetailsDto> GetCartDetailsByProductAndHeaderIdAsync(int? productId, int? headerId)
        {
            var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == productId && u.CartHeaderId == headerId);

            return _mapper.Map<CartDetailsDto>(cartDetailsFromDb);
        }

        public async Task<IEnumerable<CartDetailsDto>> GetCartDetailsByHeaderIdAsync(int cartHeaderId)
        {
            var cartDetails = _db.CartDetails.Where(u => u.CartHeaderId == cartHeaderId);

            return _mapper.Map<IEnumerable<CartDetailsDto>>(cartDetails);
        }

        public async Task<CartHeaderDto> UpdateCartHeaderAsync(CartHeaderDto cartHeaderDto)
        {
            var cartHeader = _mapper.Map<CartHeader>(cartHeaderDto);

            _db.CartHeaders.Update(cartHeader);

            await _db.SaveChangesAsync();
            return _mapper.Map<CartHeaderDto>(cartHeader);
        }

        public async Task<CartDetailsDto> UpdateCartDetailsAsync(CartDetailsDto cartDetailsDto)
        {
            var cartDetails = _mapper.Map<CartDetails>(cartDetailsDto);

            _db.CartDetails.Update(cartDetails);

            await _db.SaveChangesAsync();
            return _mapper.Map<CartDetailsDto>(cartDetails);
        }

        public async Task DeleteCartHeaderAsync(CartHeaderDto cartHeaderDto)
        {
            var cartHeader = _mapper.Map<CartHeader>(cartHeaderDto);

            _db.Remove(cartHeader);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCartDetailsAsync(CartDetailsDto cartDetailsDto)
        {
            var cartDetails = _mapper.Map<CartDetails>(cartDetailsDto);

            _db.Remove(cartDetails);
            await _db.SaveChangesAsync();
        }
    }
}
