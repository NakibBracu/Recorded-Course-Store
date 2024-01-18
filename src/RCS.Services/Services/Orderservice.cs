
using Mapster;
using RCS.Data.Entities;
using RCS.Data.UnitOfWorks;

namespace RCS.Services.Services
{
    public class Orderservice : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICartLineService _cartLineService;
        private readonly ICourseService _courseService;

        public Orderservice(IUnitOfWork unitOfWork, ICartLineService cartLineService, ICourseService courseService)
        {
            _unitOfWork = unitOfWork;
            _cartLineService = cartLineService;
            _courseService = courseService;
        }
        public async Task AddOrderAsync(string name, string line1, string line2, string line3,
            string city, string state, string zip, string country, IList<Guid> CourseIDs)
        {
            Order order = new Order()
            {
                Name = name,
                Line1 = line1,
                Line2 = line2,
                Line3 = line3,
                City = city,
                State = state,
                Zip = zip,
                Country = country
            };

            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Orders.AddAsync(order);
           

            var CartLines = CourseIDs;
            foreach(var cartLine in CartLines)
            {
                var course = await _courseService.GetCourseAsync(cartLine);
                await _cartLineService.AddCartLineAsync(course, 1, order);
            }

            await _unitOfWork.Commit();
        }

        public async Task<(int total, int totalDisplay, IList<Order> records)>
        GetOrdersByPagingAsync(int pageIndex, int pageSize, string searchText, string orderby)
        {
            var results = await _unitOfWork
                .Orders
                .GetByPagingAsync(x => x.Name.Contains(searchText), orderby, pageIndex, pageSize);

            var orders = new List<Order>();

            foreach (var order in results.data)
            {
                orders.Add(order);
            }

            return (results.total, results.totalDisplay, orders);
        }
    }
}
