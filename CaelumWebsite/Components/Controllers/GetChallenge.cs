using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaelumWebsite.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetChallenge : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Hello";
        }
        
        // // GET api/GetChallenge/<shareCode>
        // [HttpGet("{shareCode:alpha}")]
        // public IActionResult Get(string shareCode)
        // {
        //     var shared = AppData.GetWinchallenges().Find(x => x.IsShared && x.ShareCode == shareCode);
        //     return shared == null ? new NotFoundResult() : Ok(shared.ToJson());
        // }
        
        // GET api/GetChallenge/<shareCode>
        [HttpGet("{shareCode}")]
        public string Get(string shareCode)
        {
            var shared = AppData.GetWinchallenges().Find(x => x.IsShared && x.ShareCode == shareCode);
            return shared == null ? "nicht gefunden" : shared.ToJson();
        }
    }
}
