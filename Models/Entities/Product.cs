using System.ComponentModel.DataAnnotations;

namespace ApiNet_290_291_T35.Models.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
