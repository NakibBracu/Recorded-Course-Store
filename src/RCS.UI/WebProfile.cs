using AutoMapper;
using RCS.Data.Entities;
using RCS.UI.Areas.Admin.Models;

namespace RCS.UI
{
    public class WebProfile: Profile
    {
        public WebProfile()
        {
            CreateMap<CourseUpdateModel, Course>()
                .ReverseMap();
        }
    }
}
