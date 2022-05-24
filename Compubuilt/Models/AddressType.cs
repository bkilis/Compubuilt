using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
        }

        public int AddressTypeId { get; set; }
        public string AddressTypeName { get; set; } = null!;

        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
    }
}
