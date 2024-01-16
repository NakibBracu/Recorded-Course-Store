using Autofac;
using RCS.Data.Enums;
using RCS.Services.Services;
using RCS.UI.Services;
using System.ComponentModel.DataAnnotations;

namespace RCS.UI.Areas.Admin.Models
{
    public class CourseCreateModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string? Description { get; set; }

        // Using Display attribute to customize the display name
        //[Display(Name = "Thumbnail Image")]
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 999999.99, ErrorMessage = "Price should be between 0 and 999999.99")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "DifficultyLevel is required")]
        public DifficultyLevel DifficultyLevel { get; set; }

        private ICourseService _courseService;
        private IFileService _fileService;

        public CourseCreateModel()
        {

        }

        public CourseCreateModel(ICourseService courseService,IFileService fileService)
        {
            _courseService = courseService;
            _fileService = fileService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _courseService = scope.Resolve<ICourseService>();
            _fileService = scope.Resolve<IFileService>();
        }

        internal async Task CreateCourseAsync()
        {
            ImageName =  _fileService.SaveFile(Image);
            await _courseService.AddCourseAsync(Title, Description, ImageName, Price, DifficultyLevel);
        }
    }
}
