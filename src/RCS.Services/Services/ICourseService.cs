using RCS.Data.Entities;
using RCS.Data.Enums;

namespace RCS.Services.Services
{
    public interface ICourseService
    {
        Task<Course> AddCourseAsync(string title, string description, string thumbnailImage, decimal price, DifficultyLevel difficultyLevel);
        Task<Course> GetCourseAsync(Guid id);

        Task UpdateCourseAsync(Guid id, string title, string description, string thumbnailImage, decimal price, DifficultyLevel difficultyLevel);

        Task DeleteCourseAsync(Guid id);

        Task<(int total, int totalDisplay, IList<Course> records)> GetCoursesByPagingAsync(int pageIndex,
        int pageSize, string searchText, string orderby);
    }
}
