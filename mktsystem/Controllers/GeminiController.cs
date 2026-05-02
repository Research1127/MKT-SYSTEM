using Microsoft.AspNetCore.Mvc;
using mktsystem.application.Interfaces;

namespace mktsystem.Controllers
{
    [ApiController]
    [Route("api/gemini")]
    public class GeminiController(IAIService aiService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Ask([FromBody] string prompt)
        {
            var result = await aiService.GenerateAsync(prompt);
            return Ok(result);
        }
    }
}
