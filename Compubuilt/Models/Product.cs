﻿using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            ProductParameters = new HashSet<ProductParameter>();
            ProductReviews = new HashSet<ProductReview>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal? AverageRatingValue { get; set; }

        public virtual ProductCategory ProductCategory { get; set; } = null!;
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductParameter> ProductParameters { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
