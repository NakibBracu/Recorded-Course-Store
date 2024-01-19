using Mapster;
using RCS.Data.Entities;
using RCS.Data.Enums;
using RCS.Data.UnitOfWorks;
using System.Data;

namespace RCS.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartLineService _cartLineService;


        public CourseService(IUnitOfWork unitOfWork, ICartLineService cartLineService)
        {
            _unitOfWork = unitOfWork;
            _cartLineService = cartLineService;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _unitOfWork.Courses.GetAllAsync();
        }

        public async Task<IEnumerable<Course>> GetAll(IEnumerable<Guid> CoursIds)
        {
            var courses = new List<Course>();

            foreach (var courseId in CoursIds)
            {
                var course = await _unitOfWork.Courses.GetSingleAsync(courseId);
                courses.Add(course);
            }

            return courses;
        }

        public async Task<(int total, int totalDisplay, IList<Course> records)>
        GetCoursesByPagingAsync(int pageIndex, int pageSize, string searchText, string orderby)
        {
            var results = await _unitOfWork
                .Courses
                .GetByPagingAsync(x => x.Title.Contains(searchText), orderby, pageIndex, pageSize);

            var courses = new List<Course>();

            foreach (var course in results.data)
            {
                courses.Add(course.Adapt<Course>());
            }

            return (results.total, results.totalDisplay, courses);
        }

        public async Task<Course> AddCourseAsync(string title, string description, string thumbnailImage, decimal price, DifficultyLevel difficultyLevel)
        {
            var courseExist = await _unitOfWork.Courses.GetCountAsync(x => x.Title == title);
            if (courseExist != 0)
            {
                throw new DuplicateNameException("Course name is duplicate");
            }
            Course course = new Course()
            {
                Title = title,
                Description = description,
                ThumbnailImageName = thumbnailImage,
                Price = price,
                DifficultyLevel = difficultyLevel
            };

            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.Commit();

            return course;
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            var course = await _unitOfWork.Courses.GetSingleAsync(id);

            if (course == null)
                throw new Exception("Course not found");

            await _unitOfWork.BeginTransaction();
            // Delete related CartLines first
            await _cartLineService.DeleteCartLinesByCourseIdAsync(id);
            await _unitOfWork.Courses.DeleteAsync(course);
            await _unitOfWork.Commit();
        }

        public async Task<Course> GetCourseAsync(Guid id)
        {
            var course = await _unitOfWork.Courses.GetSingleAsync(x => x.Id == id);
            return course;
        }

        public async Task UpdateCourseAsync(Guid id, string title, string description, string thumbnailImage, decimal price, DifficultyLevel difficultyLevel)
        {
            if (await _unitOfWork.Courses.IsDuplicateNameAsync(title, id))
            {
                throw new DuplicateNameException("Course name is duplicate");
            }

            Course course = await _unitOfWork.Courses.GetSingleAsync(id);
            course.Title = title;
            course.Description = description;
            course.ThumbnailImageName = thumbnailImage;
            course.Price = price;
            course.DifficultyLevel = difficultyLevel;

            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Courses.UpdateAsync(course);
            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Guid>> GetCourseIds(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new InvalidOperationException($"User with ID '{userId}' not found.");
            }

            var orders = await _unitOfWork.Orders.GetAllAsync();
            var cartLines = await _unitOfWork.CartLines.GetAllAsync();

            var courseIds = (
                from order in orders
                join cartline in cartLines on order.Id equals cartline.OrderId.Id
                where order.User != null && order.User.Id == userId
                select new
                {
                    Order = order,
                    CartlineCourseId = cartline.CourseId.Id
                }
            ).ToList();

            return courseIds
                .Where(item => item.CartlineCourseId != null) // Check if CourseId is not null
                .Select(item => item.CartlineCourseId)
                .AsEnumerable();
        }



    }
}
