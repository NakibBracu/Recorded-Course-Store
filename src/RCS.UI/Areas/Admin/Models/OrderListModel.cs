using RCS.Services.Services;

namespace RCS.UI.Areas.Admin.Models
{
    public class OrderListModel
    {
        private readonly IOrderService _orderService;
        public OrderListModel()
        {
            
        }

        public OrderListModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        internal async Task<object?> GetOrdersPagedData(DataTablesAjaxRequestModel dataTablesModel)
        {
            var data = await _orderService.GetOrdersByPagingAsync(
                dataTablesModel.PageIndex,
                dataTablesModel.PageSize,
                dataTablesModel.SearchText,
                dataTablesModel.GetSortText(new string[] { "Name", "State", "Id" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                        record.Name,
                        record.State,
                        record.Id.ToString(),
                        }).ToArray()
            };
        }

    }
}
