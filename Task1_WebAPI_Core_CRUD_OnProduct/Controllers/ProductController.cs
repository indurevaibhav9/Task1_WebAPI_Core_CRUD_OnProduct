using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1_WebAPI_Core_CRUD_OnProduct.Models;
using Task1_WebAPI_Core_CRUD_OnProduct.Repository;

namespace Task1_WebAPI_Core_CRUD_OnProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            return Ok(_repo.GetAll());
        }

        [HttpPost]
        public IActionResult InsetProduct([FromBody] Product product)
        {
            if (_repo.InsertProduct(product) > 0)
            {
                return Ok("inserted success");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("deleteById")]
        public IActionResult DeleteProductById([FromQuery] int id)
        {
            if (_repo.DeleteById(id))
            {
                return Ok("record deleted success");
            }
            return BadRequest("Error occured whiling deleting");
        }

        [HttpGet("ById")]
        public IActionResult GetEmployeeById([FromQuery]int id)
        {
            return Ok(_repo.GetById(id));
        }

        [HttpPut]
        public IActionResult UpdateEmployeeById([FromQuery] int id, [FromBody] Product product)
        {
            Product pr = _repo.UpdateById(id, product);
            if (pr != null)
            {
                return Ok(pr);
            }
            return BadRequest("Error occured");

        }

    }
}
