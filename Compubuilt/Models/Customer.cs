using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Agreements = new HashSet<Agreement>();
            CustomerAddresses = new HashSet<CustomerAddress>();
            Orders = new HashSet<Order>();
            ProductReviews = new HashSet<ProductReview>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public byte[]? AzureAdsid { get; set; }

        public virtual ICollection<Agreement> Agreements { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
