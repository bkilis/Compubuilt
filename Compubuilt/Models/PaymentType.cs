using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Payments = new HashSet<Payment>();
        }

        public int PaymentTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
