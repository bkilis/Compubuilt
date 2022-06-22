namespace Compubuilt.ViewModels
{
    public class ProductPageViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? RatingValue { get; set; }
        public string ProductCategoryName { get; set; }
        public List<string> ProductPhotoThumbnailUrls { get; set; }
        public List<string> ProductPhotoUrls { get; set; }

        public string InStock { get; set; }

        public string AddToCartButtonEnabled { get; set; }
        
        //TODO: dodać recenzje użytkowników

    }
}
