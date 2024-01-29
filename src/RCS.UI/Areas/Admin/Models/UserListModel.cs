using RCS.Services.Services;

namespace RCS.UI.Areas.Admin.Models
{
    public class UserListModel
    {
        private readonly IUserService _userService;
        public UserListModel()
        {

        }

        public UserListModel(IUserService userService)
        {
            _userService = userService;
        }

        internal async Task<object?> GetOrdersPagedData(DataTablesAjaxRequestModel dataTablesModel)
        {
            var data = await _userService.GetUsersByPagingAsync(
                dataTablesModel.PageIndex,
                dataTablesModel.PageSize,
                dataTablesModel.SearchText,
                dataTablesModel.GetSortText(new string[] { "FirstName", "Email", "Id" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                        record.FirstName,
                        record.Email,
                        record.Id.ToString(),
                        }).ToArray()
            };
        }
    }
}
