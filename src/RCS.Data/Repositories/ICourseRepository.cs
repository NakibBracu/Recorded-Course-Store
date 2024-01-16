using RCS.Data.Entities;

namespace RCS.Data.Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course, Guid>
    {
        Task<bool> IsDuplicateNameAsync(string name, Guid? id);
    }
}
