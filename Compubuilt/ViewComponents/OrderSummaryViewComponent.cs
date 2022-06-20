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
            var shoppingCartSession = HttpContext.Session.Get<ShoppingCartSessionModel>("cartContents");

            var products = _context.Products
                .Where(t => shoppingCartSession.Items.Select(i => i.ProductId).Contains(t.ProductId))
                .ToList();

            var deliveryTypes = _context.DeliveryTypes;
            var paymentTypes = _context.PaymentTypes;

            var orderSummary = new OrderSummaryViewModel();

            foreach (var deliveryType in deliveryTypes)
            {
                orderSummary.OrderDeliveryTypes.Add(new OrderDeliveryType
                {
                    DeliveryTypeId = deliveryType.DeliveryTypeId,
                    DeliveryTypeName = deliveryType.DeliveryTypeName + " $" + deliveryType.Price
                });
            }

            foreach (var paymentType in paymentTypes)
            {
                orderSummary.OrderPaymentTypes.Add(new OrderPaymentType
                {
                    PaymentTypeId = paymentType.PaymentTypeId,
                    PaymentTypeName = paymentType.Name
                });
            }

            foreach (var product in products)
            {
                var quantity = shoppingCartSession.Items
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

            if (!string.IsNullOrWhiteSpace(shoppingCartSession.AppliedPromotionalCode))
            {
                var promotionalCode = _context.PromotionalCodes.FirstOrDefault(pc =>
                    pc.Code == shoppingCartSession.AppliedPromotionalCode.ToLower() && pc.ValidFrom < DateTime.Now &&
                    pc.ValidTo > DateTime.Now);

                orderSummary.AppliedPromotionalCode = promotionalCode.Code;
                orderSummary.DiscountedTotalValue = orderSummary.TotalValue -
                                                    orderSummary.TotalValue * ((decimal)promotionalCode.DiscountValue / 100);
            }

            return View("OrderSummary", orderSummary);
        }
    }
}
