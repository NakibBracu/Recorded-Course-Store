using RCS.Data.Entities;
using RCS.Data.Enums;

namespace RCS.Services.Services
{
    public interface ICartLineService
    {
        Task AddCartLineAsync(Course CourseId,int quantity,Order OrderId);
    }
}
