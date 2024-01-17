using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RCS.Data.Entities;
using RCS.Services.Services;
using RCS.UI.Models;

namespace RCS.UI.Pages
{
    public class CartModel : PageModel
    {
        private ICourseService _courseService;

        public CartModel(ICourseService courseService, Cart cartService)
        {
            _courseService = courseService;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public async Task<IActionResult> OnPost(Guid courseId, string returnUrl)
        {
            Course course = await _courseService.GetCourseAsync(courseId);
                //.FirstOrDefault(p => p.ProductID == productId);
            Cart.AddItem(course, 1);
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(Guid courseId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
                cl.CourseId.Id == courseId).CourseId);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
