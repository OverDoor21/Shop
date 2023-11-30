using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Entities;
using Shop.Services;

namespace Shop.Contoller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductContoller : ControllerBase
    {
        private readonly ProductServices productServices; 

        public ProductContoller(ProductServices productServices)
        {
            this.productServices = productServices;
        }


        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
           var products = await productServices.GetProducts();
            return Ok(products.Value);
        }


        [HttpPost("CreateNewProduct")]
        public async Task<ActionResult<Bargain>> PostNewProduct(Product productdto)
        {
            await productServices.CreateLot(productdto);
            return Ok();
        }

        [HttpDelete("DeleteLot")]
        public async Task<ActionResult> DeletePost(Product productdto)
        {
            await productServices.Deletelot(productdto.Id);
            return Ok();
        }

    }
}
