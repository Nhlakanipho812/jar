using kinetic.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace kinetic.Controllers
{
    [ApiController]
    [Route("api/")]
    public class KineticController : ControllerBase
    {
        private readonly ICoinJar _coinJar;
        private readonly ICoin _coin;


        public KineticController(ICoinJar coinJar, ICoin coin)
        {
            _coinJar = coinJar;
            _coin = coin;
        }


        [HttpGet("transactions")]
        public IActionResult GetTransactions()
        {
            //store response
            var res = _coinJar.GetTransactions();
            return StatusCode(res.Code, res);
        }

        
        [HttpGet("Total-Amount")]
        public IActionResult GetTotalAmount()
        {
            //store response
            var res = _coinJar.GetTotalAmount();
            return StatusCode(res.Code, res);
        }
        
        [HttpPut("Reset")]
        public IActionResult Reset()
        {
            //store response
            var res = _coinJar.Reset();
            return StatusCode(res.Code, res);
        }

        [HttpPut("add-coin")]
        public IActionResult AddCoin(decimal amount, decimal volume)
        {
            //assign values to Interface
            _coin.Amount = amount;
            _coin.Volume = volume;

            //store response
            var res = _coinJar.AddCoin(_coin);
            return StatusCode(res.Code, res);
        }
    }
}