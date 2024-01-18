using Autofac;
using Microsoft.AspNetCore.Mvc;
using RCS.UI.Areas.Admin.Models;

namespace RCS.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        ILifetimeScope _scope;
        ILogger<OrderController> _logger;


        public OrderController(ILifetimeScope scope, ILogger<OrderController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetOrders()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<OrderListModel>();
            return Json(await model.GetOrdersPagedData(dataTableModel));
        }


    }
}
