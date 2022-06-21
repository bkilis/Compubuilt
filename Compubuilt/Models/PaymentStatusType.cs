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
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
