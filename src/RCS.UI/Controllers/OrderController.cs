using Autofac;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RCS.Data.Entities;
using RCS.Services.Services;
using RCS.UI.Areas.Admin.Models;
using RCS.UI.Models;
using RCS.UI.Services;
using RCS.UI.Utilities;
using System.Data;

namespace RCS.UI.Controllers
{
    public class OrderController : Controller
    {

        private readonly ICourseService _courseService;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _contextAccessor;
        ILifetimeScope _scope;
        ILogger<OrderController> _logger;
        public OrderController(
            ICourseService courseService,ICartService cartService,
            IHttpContextAccessor contextAccessor,ILifetimeScope scope,
             ILogger<OrderController> logger
            )
        {
            _courseService = courseService;
            _cartService = cartService;
            _contextAccessor = contextAccessor;
            _scope = scope;
            _logger = logger;
        }

        public async Task<IActionResult> AddToCart(Guid courseId)
        {
            // Retrieve the course details using the courseId
            var course = await _courseService.GetCourseAsync(courseId);

            if (course != null)
            {
                // Retrieve existing CourseIds from session
                var existingCourseIdsString = _contextAccessor.HttpContext.Session.GetString("CourseIds");

                IList<Guid> existingCourseIds = new List<Guid>();

                if (!string.IsNullOrEmpty(existingCourseIdsString))
                {
                    // Deserialize the string into IList<Guid>
                    existingCourseIds = JsonConvert.DeserializeObject<IList<Guid>>(existingCourseIdsString);
                }

                // Check if the courseId is not already in the list
                if (!existingCourseIds.Contains(courseId))
                {
                    // Add the new courseId to the list
                    existingCourseIds.Add(courseId);

                    // Serialize the list back to string
                    var updatedCourseIdsString = JsonConvert.SerializeObject(existingCourseIds);

                    // Store the updated list in session
                    _contextAccessor.HttpContext.Session.SetString("CourseIds", updatedCourseIdsString);

                    _cartService.AddToCart(course, 1);
                }
            }

            return RedirectToAction("Index", "Order");
        }
        public IActionResult Index()
        {
            // Display the cart content or other relevant information
            var cartLines = _cartService.GetCartLines();
            return View(cartLines);
        }

        [HttpPost]
        public IActionResult RemoveCourse(Guid courseId)
        {
            // Retrieve existing CourseIds from session
            var existingCourseIdsString = _contextAccessor.HttpContext.Session.GetString("CourseIds");

            if (!string.IsNullOrEmpty(existingCourseIdsString))
            {
                var existingCourseIds = JsonConvert.DeserializeObject<List<Guid>>(existingCourseIdsString);

                // Remove the course ID from the list
                if (existingCourseIds.Remove(courseId))
                {
                    // Serialize the list back to string
                    var updatedCourseIdsString = JsonConvert.SerializeObject(existingCourseIds);

                    // Store the updated list in session
                    _contextAccessor.HttpContext.Session.SetString("CourseIds", updatedCourseIdsString);

                    // Update the cart item count in the session
                    _contextAccessor.HttpContext.Session.SetInt32("TotalCourseInCart", existingCourseIds.Count);

                    return Ok(); // Return a success status
                }
            }

            return BadRequest(); // Return a failure status if the session does not contain CourseIds or if removal fails
        }



        public async Task<IActionResult> Checkout()
        {
            var model = _scope.Resolve<PlaceOrderModel>();
            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(PlaceOrderModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCourseIdsString = _contextAccessor.HttpContext.Session.GetString("CourseIds");

                    var existingCourseIds = JsonConvert.DeserializeObject<List<Guid>>(existingCourseIdsString);
                    await model.addOrder(existingCourseIds);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully added a new course.",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating course.",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

    }
}
