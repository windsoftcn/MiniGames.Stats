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
        public async Task<IActionResult> GameStartupAsync([FromBody] GameDto gameDto)
        {
            // 1. ��֤ App�Ƿ����

            // 2. ��֤ ��������, 

            // 3. ��֤ �汾

            return Ok();
        }
    }
}