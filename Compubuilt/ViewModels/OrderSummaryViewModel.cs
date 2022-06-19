using Compubuilt.Models;

namespace Compubuilt.ViewModels
{
    public class OrderSummaryViewModel
    {
        public OrderSummaryViewModel()
        {
            Items = new List<OrderSummaryItem>();
        }
        public List<OrderSummaryItem> Items { get; set; }
        public decimal TotalValue { get; set; }
        public decimal? TotalDiscountedValue { get; set; }
        public string? AppliedDiscountCode { get; set; }
    }

    public class OrderSummaryItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
