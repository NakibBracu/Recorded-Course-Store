using RCS.Data.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCS.Services.Services
{
    public interface IUserService
    {
        Task<(int total, int totalDisplay, IList<ApplicationUser> records)>
        GetUsersByPagingAsync(int pageIndex, int pageSize, string searchText, string orderby);
    }
}
