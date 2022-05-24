using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class AgreementType
    {
        public AgreementType()
        {
            Agreements = new HashSet<Agreement>();
        }

        public int AgreementTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Text { get; set; } = null!;
        public bool Required { get; set; }

        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}
