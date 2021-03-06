using MiniGames.Stats.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Xunit;

namespace MiniGames.Stats.Tests
{
    public class MockGameUserLoginTest
    {
        
        private readonly string LoginTestUrl = "https://localhost:44346/api/game/login";
        private const int MockUserNumber = 10000;

        public MockGameUserLoginTest()
        {           
            
        }

        [Fact]
        public void LoginTest()
        {
            foreach(var mockUser in GetMockUsers())
            {                
                HttpContent content = new StringContent(JsonConvert.SerializeObject(mockUser));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = new HttpClient();
                httpClient.PostAsync(LoginTestUrl, content).ConfigureAwait(false); 
            }
        } 

        private IEnumerable<GameUserDto> GetMockUsers()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            for(int i = 0; i< MockUserNumber;i++)
            {
                yield return new GameUserDto
                {
                    GameAppId = "TestAppId0123456789",
                    OpenId = Guid.NewGuid().ToString("N"),
                    Version = "1.0.0",
                    From = "default",
                    SharedBy = ""
                };
            }
        }


    }
}
