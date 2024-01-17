using NHibernate;
using RCS.Data.Repositories;

namespace RCS.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;
        private readonly ICourseRepository _courseRepository;
        private readonly ICartLineRepository _cartLineRepository;
        private readonly IOrderRepository _orderRepository;

        public UnitOfWork(ISession session,
            ICourseRepository courseRepository,
            IOrderRepository orderRepository,
            ICartLineRepository cartLineRepository
            )
        {
            _session = session;
            _transaction = _session.BeginTransaction();
            _courseRepository = courseRepository;
            _orderRepository = orderRepository;
            _cartLineRepository = cartLineRepository;
        }
        public async Task BeginTransaction()
        {
            await Task.Run(() => _transaction.Begin());
        }

        public async Task Commit()
        {
            await Task.Run(() => _transaction.Commit());
        }

        public async Task Rollback()
        {
            await Task.Run(() => _transaction.Rollback());
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _session.Dispose();
        }

        public ICourseRepository Courses => _courseRepository;
        public ICartLineRepository CartLines => _cartLineRepository;
        public IOrderRepository Orders => _orderRepository;

    }
}
