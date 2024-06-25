namespace ApiNet_290_291_T35.Models.Product
{
    public class ProductOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
    public class ListProductOutput
    {
        public List<ProductOutput> Products { get; set; }
        public ListProductOutput()
        {
            Products = new List<ProductOutput>();
        }
    }
}
