using ApiNet_290_291_T35.Models.Entities;
using ApiNet_290_291_T35.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNet_290_291_T35.Controllers
{
    [Route("api/products")]
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApiPetShopDbContext _context;
        public ProductController(ApiPetShopDbContext context)
        {
            _context = context;
        }

        //GET: api/products -> Lấy thông tin
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, products);
        }

        //POST: api/products -> Thêm mới
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductInput input)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                Price = input.Price,
                Description = input.Description
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        //PUT: api/products/5 -> Cập nhật thông tin
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, Product product)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            item.Name = product.Name;
            item.Price = product.Price;
            item.Description = product.Description;
            _context.Products.Update(item);
            //_context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        //DELETE: api/products/5 -> Xóa thông tin
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
