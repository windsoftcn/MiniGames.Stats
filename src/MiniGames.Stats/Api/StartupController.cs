using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniGames.Stats.Services;

namespace MiniGames.Stats.Api
{
    [Route("api/[controller]")]
    public class StartupController : ControllerBase
    {
        private readonly IGameAppService gameAppService;

        public StartupController(IGameAppService gameAppService)
        {
            this.gameAppService = gameAppService ?? throw new ArgumentNullException(nameof(gameAppService));
        }         

        [HttpPost]
        public async Task<IActionResult> GameStartupAsync([FromBody] GameDto gameDto)
        {
            // 验证 App是否存在
            if(!await gameAppService.GameAppExistsAsync(gameDto.AppId))
            {
                return BadRequest("app does not exist.");
            }
            // 验证 渠道规则
            

            


            return Ok();
        }
    }
}