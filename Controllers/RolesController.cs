using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiNet_290_291_T35.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        #region GET: /api/roles -> Lấy thông tin Roles
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var items = _roleManager.Roles.ToList();
            return StatusCode(StatusCodes.Status200OK, items);
        }
        #endregion

        #region POST: /api/roles -> Thêm mới Role
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            var item = new IdentityRole { Name = name };
            var result = await _roleManager.CreateAsync(item);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
        }
        #endregion
    }
}
