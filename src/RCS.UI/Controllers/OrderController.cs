using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RCS.Data.Entities;
using RCS.Services.Services;
using RCS.UI.Models;
using RCS.UI.Services;
using RCS.UI.Utilities;

namespace RCS.UI.Controllers
{
    public class OrderController : Controller
    {

        private readonly ICourseService _courseService;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _contextAccessor;
        public OrderController(ICourseService courseService,ICartService cartService,IHttpContextAccessor contextAccessor)
        {
            _courseService = courseService;
            _cartService = cartService;
            _contextAccessor = contextAccessor;
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



        public IActionResult Checkout()
        {
            // Retrieve the cart content and pass it to the view
            var cartLines = _cartService.GetCartLines();
            return View(cartLines);
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] OrderDetailsModel orderDetails)
        {
            // Retrieve the cart content
            var cartLines = _cartService.GetCartLines();

            // Validate order details and perform necessary logic
            // ...

            // Create an order
            var order = new Order
            {
                // Set order details based on the form submission and cart content
                // ...
            };

            // Call CreateOrder method in CartService
            var createdOrder = _cartService.CreateOrder(order);

            // Display a confirmation message or redirect to a thank-you page
            return RedirectToAction("OrderConfirmation", new { orderId = createdOrder.Id });
        }

    }
}
