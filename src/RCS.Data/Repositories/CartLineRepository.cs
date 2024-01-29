using NHibernate;
using RCS.Data.Entities;

namespace RCS.Data.Repositories
{
    public class CartLineRepository : Repository<CartLine, Guid>, ICartLineRepository
    {
        public CartLineRepository(ISession session) : base(session)
        {

        }
    }
}
