using RCS.Data.Entities;

namespace RCS.Services.Services
{
    public interface ICartLineService
    {
        Task AddCartLineAsync(Course CourseId, int quantity, Order OrderId);
        Task DeleteCartLinesByCourseIdAsync(Guid courseId);
        //Task<IList<CartLine>> GetCartLinesAsync(Guid courseId);
    }
}
