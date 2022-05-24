using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class ProductReview
    {
        public int ProductReviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ReviewText { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
