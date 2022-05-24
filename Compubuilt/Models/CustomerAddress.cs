using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class CustomerAddress
    {
        public int CustomerAddressId { get; set; }
        public int CustomerId { get; set; }
        public int AddressTypeId { get; set; }
        public string StreetName { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

        public virtual AddressType AddressType { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
