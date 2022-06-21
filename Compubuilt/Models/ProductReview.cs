using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class ProductReview
    {
        public int ProductReviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string? ReviewText { get; set; }
        public decimal RatingValue { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
