using RCS.Data.Entities;

namespace RCS.UI.Models
{
    public class GeneralCourseListModel
    {
        public IList<Course> CourseList { get; set; } = new List<Course>();
        public PagingInfo PagingInfo { get; set; }
        public string CourseTitle { get; set; }
    }
}
