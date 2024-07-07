using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Garantiapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GarantiBankController : ControllerBase
    {
        [HttpGet("bakiye/{musteriId}")]
        [Authorize(Policy = "ReadGaranti")]
        public ActionResult<double> Bakiye(int musteriId)
        {
            // Bakiye hesaplama işlemi
            return 1000;
        }

        [HttpGet("hesaplar/{musteriId}/{tutar}")]
       // [HttpGet("{musteriId}/{tutar}")]
        [Authorize(Policy = "AllGaranti")]
        public ActionResult<List<string>> TumHesaplar(int musteriId)
        {
            // Hesapları getirme işlemi
            return new List<string>
            {
                "123456789",
                "987654321",
                "564738291"
            };
        }
        [HttpGet("yatirimYap/{musteriId}/{tutar}")]
        [Authorize(Policy = "AllGaranti")]
        public double YatirimYap(int musteriId, double tutar)
        {
            return tutar * 0.5;
        }
    }

}
