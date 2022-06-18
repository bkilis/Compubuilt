namespace Compubuilt.ApiModels
{
    public class GetProductsStockResponse
    {
        public GetProductsStockResponse()
        {
            Stocks = new List<ProductStock>();
        }

        public List<ProductStock> Stocks { get; set; }
    }

    public class ProductStock
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
