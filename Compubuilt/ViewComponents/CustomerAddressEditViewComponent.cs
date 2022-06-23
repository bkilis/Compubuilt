using Compubuilt.Extensions;
using Compubuilt.Models;
using Compubuilt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compubuilt.ViewComponents
{
    public class CustomerAddressEditViewComponent : ViewComponent
    {
        private readonly compubuiltContext _context;

        public CustomerAddressEditViewComponent(compubuiltContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.EmailAddress == User.Identity.Name);

            var customerAddress = await _context.CustomerAddresses
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerId == customer.CustomerId);

            var customerAddressEditViewModel = new CustomerAddressEditViewModel();

            if(customerAddress != null)
            {
                customerAddressEditViewModel.CustomerAddressId = customerAddress.CustomerAddressId;
                customerAddressEditViewModel.StreetName = customerAddress.StreetName;
                customerAddressEditViewModel.StreetNumber = customerAddress.StreetNumber;
                customerAddressEditViewModel.CityName = customerAddress.CityName;
                customerAddressEditViewModel.PostalCode = customerAddress.PostalCode;
                customerAddressEditViewModel.Action = "Checkout";
                customerAddressEditViewModel.Controller = "Orders";
            };

            return View("CustomerAddressEdit", customerAddressEditViewModel);
        }


    }
}
