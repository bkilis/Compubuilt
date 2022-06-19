using Compubuilt.Enums;
using Compubuilt.Extensions;
using Compubuilt.Models;
using Compubuilt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compubuilt.ViewComponents
{
    public class OrderSummaryViewComponent : ViewComponent
    {
        private readonly compubuiltContext _context;

        public OrderSummaryViewComponent(compubuiltContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var shoppingCart = HttpContext.Session.Get<ShoppingCartSessionModel>("cartContents");

            var products = _context.Products
                .Where(t => shoppingCart.Items.Select(i => i.ProductId).Contains(t.ProductId))
                .ToList();

            var orderSummary = new OrderSummaryViewModel();

            foreach (var product in products)
            {
                var quantity = shoppingCart.Items
                    .Where(i => i.ProductId == product.ProductId)
                    .Select(i => i.Quantity).First();

                orderSummary.Items.Add(new OrderSummaryItem()
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Quantity = quantity,
                    TotalPrice = product.Price * quantity,
                });

                orderSummary.TotalValue += (product.Price * quantity);
            }

            return View("OrderSummary", orderSummary);
        }
    }
}
