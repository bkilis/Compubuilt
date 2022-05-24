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

        public virtual ICollection<ProductParameter> ProductParameters { get; set; }
    }
}
