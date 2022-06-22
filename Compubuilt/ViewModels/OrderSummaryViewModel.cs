using Compubuilt.Models;

namespace Compubuilt.ViewModels
{
    public class OrderSummaryViewModel
    {
        public OrderSummaryViewModel()
        {
            Items = new List<OrderSummaryItem>();
            OrderDeliveryTypes = new List<OrderDeliveryType>();
            OrderPaymentTypes = new List<OrderPaymentType>();
        }
        public List<OrderSummaryItem> Items { get; set; }
        public decimal TotalValue { get; set; }
        public decimal? DiscountedTotalValue { get; set; }
        public string? AppliedPromotionalCode { get; set; }
        public int SelectedOrderDeliveryTypeId { get; set; }
        public int SelectedPaymentTypeId { get; set; }

        public string CustomerHasAddressInformation { get; set; }

        public List<OrderDeliveryType> OrderDeliveryTypes { get; set; }
        public List<OrderPaymentType> OrderPaymentTypes { get; set; }
    }

    public class OrderSummaryItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDeliveryType
    {
        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; }
    }

    public class OrderPaymentType
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
    }
}
