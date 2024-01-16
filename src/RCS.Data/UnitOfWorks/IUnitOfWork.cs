using RCS.Data.Repositories;

namespace RCS.Data.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransaction();

        Task Commit();

        Task Rollback();

        ICourseRepository Courses { get; }

    }
}
