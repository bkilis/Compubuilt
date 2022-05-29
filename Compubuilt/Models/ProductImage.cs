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

        public virtual Product Product { get; set; } = null!;
    }
}
