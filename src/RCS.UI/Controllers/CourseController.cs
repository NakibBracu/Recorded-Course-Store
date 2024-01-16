using Microsoft.AspNetCore.Mvc;
using RCS.Services.Services;
using RCS.UI.Models;

namespace RCS.UI.Controllers
{
    public class CourseController : Controller
    {
        public ICourseService _courseService;
        public CourseController( ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IActionResult> Index(string title, int pageNumber = 1)
        {
            var pageSize = 10; // Set your desired page size

            var courseList = await _courseService.GetAll();
            var filteredCourses = string.IsNullOrEmpty(title)
                ? courseList
                : courseList.Where(c => c.Title == title);

            var paginatedCourses = filteredCourses
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new GeneralCourseListModel
            {
                CourseList = paginatedCourses,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = pageSize,
                    TotalItems = filteredCourses.Count()
                },
                CourseTitle = title
            };

            return View(viewModel);
        }
    }
}
