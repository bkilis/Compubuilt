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
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual ParameterType ParameterType { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
