using RCS.Data.Entities;

namespace RCS.UI.Services
{
    public interface ICartService
    {
        void AddToCart(Course course, int quantity);
        void RemoveFromCart(Guid courseId);
        IEnumerable<CartLine> GetCartLines();
        void ClearCart();
        Order CreateOrder(Order order);
    }
}
