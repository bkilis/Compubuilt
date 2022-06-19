namespace Compubuilt.ViewModels
{
    public class CustomerAddressEditViewModel
    {
        public int CustomerAddressId { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string CityName { get; set; }
        public string PostalCode { get; set; }

        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
