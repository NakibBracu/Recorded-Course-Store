using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCS.Data.Identity.Entities;
using RCS.Services.Services;
using RCS.UI.Models;

namespace RCS.UI.Areas.User.Controllers
{
    [Area("User"),Authorize]
    public class DashBoardController : Controller
    {
        public ICourseService _courseService;
        private UserManager<ApplicationUser> _userManager;
        public DashBoardController(ICourseService courseService, UserManager<ApplicationUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string title, int pageNumber = 1)
        {
            var pageSize = 10; // Set your desired page size

            var currentUserId = (await _userManager.GetUserAsync(User)).Id;
            var currentUserCourseIds = await _courseService.GetCourseIds(currentUserId);
            var currentUserCourse = await _courseService.GetAll(currentUserCourseIds);

            var courseList = currentUserCourse;
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
