using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using RCS.Data.Identity.Entities;
using System.Linq.Dynamic.Core;

namespace RCS.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(int total, int totalDisplay, IList<ApplicationUser> records)>
        GetUsersByPagingAsync(int pageIndex, int pageSize, string searchText, string orderby)
        {
           
            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(searchText))
                .OrderBy(orderby)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var total = await _userManager.Users.CountAsync(u => u.UserName.Contains(searchText));
            var totalDisplay = users.Count;

            return (total, totalDisplay, users);
        }

    }
}
