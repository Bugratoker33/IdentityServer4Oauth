using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalkBankapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HalkBankController : ControllerBase
    {
        [HttpGet("bakiye/{musteriId}")]
        public double Bakiye(int musteriId)
        {
            //....
            return 500.15;
        }
        [HttpGet("hesaplar/{musteriId}")]
        public List<string> TumHesaplar(int musteriId)
        {
            //....
            return new()
        {
            "135792468",
            "019283745",
            "085261060"
        };
        }
    }
}
