using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class ParameterType
    {
        public ParameterType()
        {
            ProductParameters = new HashSet<ProductParameter>();
        }

        public int ParameterTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual ICollection<ProductParameter> ProductParameters { get; set; }
    }
}
