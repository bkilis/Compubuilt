namespace Compubuilt.ViewModels
{
    public class ProductCatalogViewModel
    {
        public List<ProductOverview> ProductList { get; set; }
    }

    public class ProductOverview
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? RatingValue { get; set; } 
        public string? ProductPhotoUrl { get; set; }
    }
}
