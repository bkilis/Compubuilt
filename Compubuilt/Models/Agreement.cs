using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class Agreement
    {
        public int AgreementId { get; set; }
        public int AgreementTypeId { get; set; }
        public int CustomerId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual AgreementType AgreementType { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
