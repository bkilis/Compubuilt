using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class PaymentStatusType
    {
        public PaymentStatusType()
        {
            Payments = new HashSet<Payment>();
        }

        public int PaymentStatusTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
