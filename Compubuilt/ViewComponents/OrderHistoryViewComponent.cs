using Compubuilt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compubuilt.ViewComponents
{
    public class OrderHistoryViewComponent : ViewComponent
    {
        private readonly compubuiltContext _context;

        public OrderHistoryViewComponent(compubuiltContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.EmailAddress == User.Identity.Name);

            var compubuiltContext = _context.Orders
                .Where(o => o.CustomerId == customer.CustomerId)
                .Include(o => o.Customer)
                .Include(o => o.Delivery)
                .Include(o => o.OrderStatusType)
                .Include(o => o.Payment)
                .Include(o => o.PromotionalCode)
                .Include(o => o.Delivery.DeliveryType)
                .Include(o => o.Payment.PaymentType)
                .Include(o => o.PromotionalCode);

            return View("OrderHistory", await compubuiltContext.ToListAsync());
        }
    }
}
