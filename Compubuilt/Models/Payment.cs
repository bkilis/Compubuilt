using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public int PaymentTypeId { get; set; }
        public int PaymentStatusTypeId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual PaymentStatusType PaymentStatusType { get; set; } = null!;
        public virtual PaymentType PaymentType { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
