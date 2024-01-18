using Autofac;
using Microsoft.AspNetCore.Identity;
using RCS.Data.Identity.Entities;
using RCS.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace RCS.UI.Models
{
    public class PlaceOrderModel
    {


        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }

        private IOrderService _orderService;
      


        public PlaceOrderModel()
        {

        }

        public PlaceOrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _orderService = scope.Resolve<IOrderService>();
        }

        internal async Task addOrder(IList<Guid> courseIDs,ApplicationUser currentUser)
        {
            // Call AddOrderAsync with the ApplicationUser object
            await _orderService.AddOrderAsync(Name, Line1, Line2, Line3, City, State, Zip, Country, courseIDs, currentUser);
        }



    }
}
