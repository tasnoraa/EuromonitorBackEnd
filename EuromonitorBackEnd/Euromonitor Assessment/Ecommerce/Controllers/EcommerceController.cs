using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EcommerceController : ControllerBase
    {
        IEcommerceService _service;
        public EcommerceController(IEcommerceService service)
        {
            _service = service;
        }

        [HttpGet("getBooks")]
        public IActionResult Get()
        {
            return Ok(_service.GetBooks());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            return Ok(_service.GetBook(id));
        }

    }
}
