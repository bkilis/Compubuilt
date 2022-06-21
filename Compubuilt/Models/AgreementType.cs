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
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}
