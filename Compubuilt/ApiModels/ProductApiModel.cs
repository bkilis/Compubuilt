using System.Security.Cryptography;
using Compubuilt.Enums;

namespace Compubuilt.ApiModels
{
    public class ProductApiModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal? AverageRatingValue { get; set; }

        public string? Validate()
        {
            if (!string.IsNullOrWhiteSpace(Description) && Description.Length > 4000)
                return "Product description cannot exceed 4000 characters.";
            if (Price < 0)
                return "Product price cannot be less than 0.";
            if(Quantity < 0)
                return "Quantity cannot be less than 0";
            if (AverageRatingValue.HasValue && (AverageRatingValue < 0 || AverageRatingValue > 5))
                return "Incorrect Average Rating Value";

            return null;
        }
    }


}
