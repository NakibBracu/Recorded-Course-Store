using NHibernate;
using RCS.Data.Entities;

namespace RCS.Data.Repositories
{
    public class OrderRepository : Repository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(ISession session) : base(session)
        {

        }
    }
}
