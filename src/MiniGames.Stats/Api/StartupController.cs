using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MiniGames.Stats.Api
{
    [Route("api/[controller]")]
    public class StartupController : ControllerBase
    {
        public StartupController()
        {

        }         

        [HttpPost]
        public async Task<IActionResult> UserActive([FromBody] GameDto gameDto)
        {
            return Ok();
        }
    }
}