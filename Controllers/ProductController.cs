using ApiNet_290_291_T35.Models.Entities;
using ApiNet_290_291_T35.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiNet_290_291_T35.Controllers
{
    [Route("api/products")]
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ApiPetShopDbContext _context;
        public ProductController(ApiPetShopDbContext context)
        {
            _context = context;
        }

        #region GET: api/products -> Lấy thông tin
        /// <summary>
        ///     API lấy thông tin danh sách sản phẩm
        /// </summary>
        /// <param name="keyword"></param>
        /// <response code="200">Thành công lấy được danh sách các sản phẩm</response>
        [HttpGet]
        //public async Task<IActionResult> GetProducts(string ProductId = "", string Name = "", int Price = 0)
        public async Task<IActionResult> GetProducts(string keyword = "")
        {
            //var products = await _context.Products
            //    .Where(x => x.Id.ToString().Contains(ProductId) &&
            //                x.Name.ToLower().Contains(Name.ToLower()) &&
            //                (Price == 0 || x.Price.Equals(Price)))
            //    .ToListAsync();

            var products = await _context.Products
                .Where(x => x.Keyword.ToLower().Contains(keyword.ToLower()))
                .Select(x => new ProductOutput
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description
                })
                .ToListAsync();
            return StatusCode(StatusCodes.Status200OK, products);
        }
        #endregion

        #region POST: api/products -> Thêm mới
        /// <summary>
        ///     API khởi tạo thông tin sản phẩm
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <param name="Discription"></param>
        /// <response code="200">Khởi tạo sản phẩm thành công</response>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductInput input)
        //public async Task<IActionResult> CreateProduct(string name, int price, string des)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                Price = input.Price,
                Description = input.Description
            };
            product.Keyword = product.Id.ToString() + " " + product.Name + " " + product.Price + " " + product.Description;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            //var item = new ProductOutput
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Price = product.Price,
            //    Description = product.Description
            //};
            return StatusCode(StatusCodes.Status201Created);
        }
        #endregion

        #region PUT: api/products/5 -> Cập nhật thông tin
        /// <summary>
        ///     API cập nhật thông tin sản phẩm
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <param name="Discription"></param>
        /// <response code="200">Cập nhật thông tin sản phẩm thành công</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductInput input)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            item.Name = input.Name;
            item.Keyword = item.Id.ToString() + " " + item.Name + " " + item.Price + " " + item.Description;
            _context.Products.Update(item);
            //_context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        #endregion

        #region DELETE: api/products/5 -> Xóa thông tin
        /// <summary>
        ///     API xóa thông tin sản phẩm
        /// </summary>
        /// <param name="Id"></param>
        /// <response code="204">Xóa thông tin sản phẩm thành công</response>
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
        #endregion

        #region DELETE: api/products/5 -> Xóa thông tin
        /// <summary>
        ///     API xóa danh sách thông tin sản phẩm
        /// </summary>
        /// <param name="Ids"></param>
        /// <response code="204">Xóa thông tin sản phẩm thành công</response>
        [HttpDelete]
        public async Task<IActionResult> DeleteListProduct(List<Guid> id)
        {
            foreach (var i in id)
            {
                var product = await _context.Products.FindAsync(i);
                if(product != null)
                {
                    _context.Products.Remove(product);
                }
            }
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        #endregion
    }
}
