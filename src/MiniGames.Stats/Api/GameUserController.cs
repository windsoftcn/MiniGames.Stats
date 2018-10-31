using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniGames.Stats.Services;

namespace MiniGames.Stats.Api
{
    [Route("api/[controller]")]
    public class GameUserController : ControllerBase
    {
        private readonly IGameAppService gameAppService;
        private readonly IGameUserService gameUserService;

        public GameUserController(IGameAppService gameAppService,
            IGameUserService gameUserService)
        {
            this.gameAppService = gameAppService ?? throw new ArgumentNullException(nameof(gameAppService));
            this.gameUserService = gameUserService ?? throw new ArgumentNullException(nameof(gameUserService));
        }         

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] GameUserDto gameUserDto)
        {
            // ��֤ App�Ƿ����
            if(!await gameAppService.GameAppExistsAsync(gameUserDto.AppId))
            {
                return BadRequest("app does not exist.");
            }
            // ����û���¼
            await gameUserService.AddUserLoginCountAsync(gameUserDto.OpenId, DateTimeOffset.Now);

            return Ok();
        }
    }
}