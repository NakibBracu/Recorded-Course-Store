using Autofac;
using AutoMapper;
using NHibernate.Mapping.ByCode.Impl;
using RCS.Data.Entities;
using RCS.Data.Enums;
using RCS.Services.Services;
using RCS.UI.Services;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RCS.UI.Areas.Admin.Models
{
    public class CourseUpdateModel
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "DifficultyLevel is required")]
        public DifficultyLevel DifficultyLevel { get; set; }

        private ICourseService _courseService;
        private IFileService _fileService;
        private IMapper _mapper;

        public CourseUpdateModel()
        {
            
        }

        public CourseUpdateModel(ICourseService courseService, IFileService fileService,IMapper mapper)
        {
            _courseService = courseService;
            _fileService = fileService;
            _mapper = mapper;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _courseService = scope.Resolve<ICourseService>();
            _fileService = scope.Resolve<IFileService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal async Task Load(Guid id)
        {
            Course course = await _courseService.GetCourseAsync(id);
            if (course != null)
            {
                _mapper.Map(course, this);
            }
        }

        internal async Task UpdateCourseAsync()
        {
            ImageName = _fileService.SaveFile(Image);
            await _courseService.UpdateCourseAsync(Id,Title,Description,ImageName,Price,DifficultyLevel);
        }

    }
}
