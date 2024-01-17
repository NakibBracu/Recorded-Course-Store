using RCS.Data.Entities;
using RCS.Data.UnitOfWorks;

namespace RCS.Services.Services
{
    public class CartLineService : ICartLineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartLineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddCartLineAsync(Course CourseId, int quantity, Order OrderId)
        {
            
            OrderId.Lines = new List<CartLine>();
            CartLine cartLine = new CartLine()
            {
                CourseId = CourseId,
                Quantity = quantity,
                OrderId = OrderId
            };

            
            await _unitOfWork.CartLines.AddAsync(cartLine);
          
        }
    }
}
