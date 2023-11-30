using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Entities;
using Shop.Services;

namespace Shop.Contoller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BargainContoller : ControllerBase
    {
        private readonly BargainService bargainService; 

        public BargainContoller(BargainService bargainService)
        {
            this.bargainService = bargainService;
        }

        [HttpPost("CreateBergain")]
        public async Task<ActionResult<Bargain>> CreateBargain(Bargain bargain)
        {
            await bargainService.CreateBargain(bargain.UserId,bargain.OfferId,
            bargain.NewPrice);
            return Ok();
        }

        [HttpPut("AcceptBargain")]
        public async Task<ActionResult<Bargain>> AcceptBargain(Bargain bargain)
        {
            await bargainService.AcceptBargain(bargain.UserId,bargain.Id);
            return Ok();
        }

        [HttpPut("DeclainBargain")]
        public async Task<ActionResult<Bargain>> DeclainBargain (Bargain bargain)
        {
            await bargainService.DeclainBargain(bargain.UserId, bargain.Id);
            return Ok();
        }

    }
}
