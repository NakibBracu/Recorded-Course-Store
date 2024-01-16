using RCS.Services.Services;

namespace RCS.UI.Areas.Admin.Models
{
    public class CourseListModel
    {
        private readonly ICourseService _courseService;
        public CourseListModel()
        {
            
        }

        public CourseListModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        internal async Task DeleteCourse(Guid id)
        {
            await _courseService.DeleteCourseAsync(id);
        }


        internal async Task<object?> GetCoursePagedData(DataTablesAjaxRequestModel dataTablesModel)
        {
            var data = await _courseService.GetCoursesByPagingAsync(
                dataTablesModel.PageIndex,
                dataTablesModel.PageSize,
                dataTablesModel.SearchText,
                dataTablesModel.GetSortText(new string[] { "Title",  "Price" ,"Id"}));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                        record.Title,
                        record.Price.ToString("0,000"),
                        record.Id.ToString(),
                        }).ToArray()
            };
        }


    }
}
