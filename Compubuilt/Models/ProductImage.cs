using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class ProductImage
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int ProductImageTypeId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
        public virtual ProductImageType ProductImageType { get; set; } = null!;
    }
}
