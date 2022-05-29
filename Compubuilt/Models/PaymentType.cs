using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Compubuilt.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Payments = new HashSet<Payment>();
        }

        public int PaymentTypeId { get; set; }

        [Display(Name = "Payment Type Name")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
