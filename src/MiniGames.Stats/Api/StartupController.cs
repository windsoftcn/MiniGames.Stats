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
            // 1. 验证 App是否存在

            // 2. 验证 渠道规则, 

            // 3. 验证 版本

            return Ok();
        }
    }
}