using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Compubuilt.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductParameters = new HashSet<ProductParameter>();
            ProductReviews = new HashSet<ProductReview>();
        }

        public int ProductId { get; set; }

        [Display(Name = "Nazwa produktu")]
        public string Name { get; set; } = null!;

        [Display(Name = "Opis")]
        public string Description { get; set; } = null!;

        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Display(Name = "ID Kategorii produktu")]
        public int ProductCategoryId { get; set; }

        [Display(Name = "Kategoria produktu")]
        public virtual ProductCategory ProductCategory { get; set; } = null!;
        public virtual ICollection<ProductParameter> ProductParameters { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
