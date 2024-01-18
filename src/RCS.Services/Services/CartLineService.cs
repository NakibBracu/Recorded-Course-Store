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

        public async Task DeleteCartLinesByCourseIdAsync(Guid courseId)
        {
            // Implement the logic to delete CartLines associated with the specified Course
            // Example:

            var cartLinesToDelete = await _unitOfWork.CartLines.FindAsync(x => x.CourseId.Id == courseId);

            foreach (var cartLine in cartLinesToDelete)
            {
                await _unitOfWork.CartLines.DeleteAsync(cartLine);
            }
        }

        //public async Task<IList<CartLine>> GetCartLinesAsync(Guid courseId)
        //{
        //    IList<CartLine> cartLines = new List<CartLine>();

        //    foreach (var cartLine in await _unitOfWork.CartLines.FindAsync(x => x.CourseId.Id == courseId))
        //    {
        //        cartLines.Add(cartLine);
        //    }

        //    return cartLines;
        //}

    }
}
