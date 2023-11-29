using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Entities;

namespace Shop.Contoller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductContoller : ControllerBase
    {
        private readonly DataContext context;

        public ProductContoller(DataContext context)
        {
            this.context = context;
        }


        [HttpPost]
        public async Task<ActionResult<Bargain>> PostNewProduct(Product productdto)
        {
            var product = new Product
            {
                Name = productdto.Name,
                Price = productdto.Price,
                Description = productdto.Description,
                PhotoUrl = productdto.PhotoUrl
            };

          

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePost(int postId)
        {
            return Ok();
        }

    }
}
