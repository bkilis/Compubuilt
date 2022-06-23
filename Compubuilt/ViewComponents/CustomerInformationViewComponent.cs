using Compubuilt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compubuilt.ViewComponents
{
    public class CustomerInformationViewComponent : ViewComponent
    {
        private readonly compubuiltContext _context;

        public CustomerInformationViewComponent(compubuiltContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.EmailAddress == User.Identity.Name);

            return View("CustomerInformation", customer);
        }
    }
}
