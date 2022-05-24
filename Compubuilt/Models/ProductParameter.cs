using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class ProductParameter
    {
        public int ProductParameterId { get; set; }
        public int ParameterTypeId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; } = null!;

        public virtual ParameterType ParameterType { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
