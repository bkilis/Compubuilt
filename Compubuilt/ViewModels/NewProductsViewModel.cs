namespace Compubuilt.ViewModels
{
    public class NewProductsViewModel
    {
        public List<NewProductOverview> ProductList { get; set; }
    }

    public class NewProductOverview
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? RatingValue { get; set; }
        public string? ProductPhotoUrl { get; set; }
    }
}

