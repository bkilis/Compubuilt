using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class ProductImageType
    {
        public ProductImageType()
        {
            ProductImages = new HashSet<ProductImage>();
        }

        public int ProductImageTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
