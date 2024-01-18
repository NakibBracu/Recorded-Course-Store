using RCS.Data.Entities;
using RCS.Data.Identity.Entities;

namespace RCS.Services.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(string name, string line1,string line2,
            string line3,string city,string state,string zip,string country,IList<Guid> CourseIDs, ApplicationUser userId);

        Task<(int total, int totalDisplay, IList<Order> records)> GetOrdersByPagingAsync(int pageIndex,
      int pageSize, string searchText, string orderby);
    }
}
