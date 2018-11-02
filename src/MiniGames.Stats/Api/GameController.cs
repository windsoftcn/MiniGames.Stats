using Microsoft.AspNetCore.Mvc;
using MiniGames.Stats.Providers;
using MiniGames.Stats.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameAppService gameAppService;
        private readonly IGameStatsService gameStatsService;
        private readonly IRedisProvider redisProvider;

        public GameController(IGameAppService gameAppService,
            IGameStatsService gameStatsService,
            IRedisProvider redisProvider)
        {
            this.gameAppService = gameAppService ?? throw new ArgumentNullException(nameof(gameAppService));
            this.gameStatsService = gameStatsService ?? throw new ArgumentNullException(nameof(gameStatsService));
            this.redisProvider = redisProvider ?? throw new ArgumentNullException(nameof(redisProvider));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] GameUserDto gameUserDto)
        {            
            //// 验证 App是否存在
            //if (!await gameAppService.GameAppExistsAsync(gameUserDto.GameAppId))
            //{
            //    return BadRequest("app does not exist.");
            //}
            //// 添加用户登录统计
            //await gameStatsService.GameUserLoginAsync(gameUserDto.GameAppId, gameUserDto.OpenId, DateTimeOffset.Now);

            await redisProvider.GetGameUserSequentialIdAsync("TestIds").ConfigureAwait(false);

            return Ok();
        }

        
    }
}
