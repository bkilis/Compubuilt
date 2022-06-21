using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class CustomerAddress
    {
        public CustomerAddress()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerAddressId { get; set; }
        public int CustomerId { get; set; }
        public string StreetName { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
