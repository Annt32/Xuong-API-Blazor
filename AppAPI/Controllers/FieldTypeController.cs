using AppAPI.Repositories;
using AppData.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FieldTypeController : ControllerBase
	{
        private readonly IRepository<FieldType> _repos;
        public FieldTypeController(IRepository<FieldType> repos)
        {
            _repos = repos;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var lst = _repos.GetAll();
            return Ok(lst);
        }
    }
}
