using RCS.Data.Entities;
using RCS.Data.Enums;
using RCS.Data.Identity.Entities;

namespace RCS.Services.Services
{
    public interface ICourseService
    {
        Task<Course> AddCourseAsync(string title, string description, string thumbnailImage, decimal price, DifficultyLevel difficultyLevel);
        Task<Course> GetCourseAsync(Guid id);
        Task<IEnumerable<Course>> GetAll(IEnumerable<Guid> CoursIds);
        Task<IEnumerable<Guid>> GetCourseIds(Guid userId);
        Task UpdateCourseAsync(Guid id, string title, string description, string thumbnailImage, decimal price, DifficultyLevel difficultyLevel);

        Task DeleteCourseAsync(Guid id);

        Task<(int total, int totalDisplay, IList<Course> records)> GetCoursesByPagingAsync(int pageIndex,
        int pageSize, string searchText, string orderby);

        Task<IEnumerable<Course>> GetAll();
    }
}
