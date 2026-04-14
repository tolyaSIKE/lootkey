using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace LootkeyAPI.Tests
{
    public class GameTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GameTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetGames_ReturnsOkAndList()
        {
            var response = await _client.GetAsync("/api/games");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Assert.False(string.IsNullOrEmpty(content));
        }

        [Fact]
        public async Task GetGame_ById_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/games/1");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetGame_InvalidId_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/games/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}