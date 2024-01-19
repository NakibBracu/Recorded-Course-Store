using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCS.UI.Areas.Admin.Models;

namespace RCS.UI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ILifetimeScope _scope;
        ILogger<UserController> _logger;


        public UserController(ILifetimeScope scope, ILogger<UserController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetUsers()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<UserListModel>();
            return Json(await model.GetOrdersPagedData(dataTableModel));
        }
    }
}
