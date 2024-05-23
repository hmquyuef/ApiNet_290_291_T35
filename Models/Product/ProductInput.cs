namespace ApiNet_290_291_T35.Models.Product
{
    public class ProductInput
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }

    public class ProductNameInput
    {
        public string Name { get; set; }
    }

    public class ProductPriceInput
    {
        public int Price { get; set; }
    }

    public class ProductDescriptionInput
    {
        public string Description { get; set; }
    }
}
