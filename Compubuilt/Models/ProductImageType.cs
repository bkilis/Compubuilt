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
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
