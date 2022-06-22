namespace Compubuilt.ViewModels
{
    public class ProductFiltersViewModel
    {
        public ProductFiltersViewModel()
        {
            Categories = new List<ProductCategoryViewModel>();
        }

        public List<ProductCategoryViewModel> Categories { get; set; }

        public class ProductCategoryViewModel
        {
            public int ProductCategoryId { get; set; }
            public string Name { get; set; }
        }
    }
}
