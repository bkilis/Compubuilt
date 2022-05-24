using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class Agreement
    {
        public int AgreementId { get; set; }
        public int AgreementTypeId { get; set; }
        public int CustomerId { get; set; }

        public virtual AgreementType AgreementType { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
