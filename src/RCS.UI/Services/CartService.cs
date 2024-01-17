using RCS.Data.Entities;
using RCS.Services.Services;

namespace RCS.UI.Services
{
    public class CartService : ICartService
    {
        private readonly ISessionService _sessionService;

        public CartService()
        {

        }
        public CartService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        private List<CartLine> CartLines
        {
            get
            {
                // Always get the latest cart lines from the session
                return _sessionService.CartLines;
            }
            set
            {
                // Update the session with the new cart lines
                _sessionService.CartLines = value;
            }
        }

        public void AddToCart(Course course, int quantity)
        {
            var existingLine = CartLines.FirstOrDefault(cl => cl.CourseId.Id == course.Id);

            if (existingLine != null)
            {
                existingLine.Quantity += quantity;
            }
            else
            {
                try
                {
                    CartLines.Add(new CartLine
                    {
                        CourseId = course,
                        Quantity = quantity
                    });
                }
                catch (Exception ex) {
                    throw ex;
                }
                
            }

            // Save the updated cart back to the session
            SaveCartToSession();
        }

        private void SaveCartToSession()
        {
            // Update the session with the latest cart lines
            _sessionService.Set("Cart", CartLines);
            // Update the CoursesID in the session
            var courseIds = CartLines.Select(cl => cl.CourseId.Id).ToList();
            _sessionService.Set("CoursesID", courseIds);
        }


        public void ClearCart()
        {
            CartLines.Clear();
        }

        public Order CreateOrder(Order order)
        {
          //  order.Lines = CartLines.ToList();
            // Additional order-related logic
            // Save the order and associated cart lines to the database
            // ...

            // Clear the cart after creating the order
            ClearCart();
            return order;
        }

        public IEnumerable<CartLine> GetCartLines()
        {
            return CartLines;
        }

        public void RemoveFromCart(Guid courseId)
        {
            var lineToRemove = CartLines.FirstOrDefault(cl => cl.CourseId.Id == courseId);
            if (lineToRemove != null)
            {
                CartLines.Remove(lineToRemove);
            }
        }
    }
}
