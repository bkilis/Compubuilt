using System.Net;

namespace Compubuilt.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            Items = new List<ShoppingCartItem>();
            TotalValue = 0;
        }
        public List<ShoppingCartItem> Items { get; set; }
        public string? AppliedPromotionalCode { get; set; }
        public decimal TotalValue { get; set; }
        public decimal? DiscountedTotalValue { get; set; }
    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? RatingValue { get; set; }
        public string? ProductPhotoUrl { get; set; }

    }
}
