namespace Compubuilt.ViewModels
{
    public class ShoppingCartSessionModel
    {
        public ShoppingCartSessionModel()
        {
            Items = new List<ShoppingCartSessionItem>();
            TotalQuantity = 0;
        }

        public List<ShoppingCartSessionItem> Items { get; set; }

        public int TotalQuantity { get; set; }

        public string AppliedPromotionalCode { get; set; }
        public byte PromotionalCodeDiscountValue { get; set; }

    }

    public class ShoppingCartSessionItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
