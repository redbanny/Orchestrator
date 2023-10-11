using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrchestratorAPI.JWT.Filters;
using OrchestratorAPI.JWT.Http;
using OrchestratorAPI.Models;

namespace OrchestratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : Controller
    {
        private readonly IOptions<JwtSettings> _settings;

        public JwtController(IOptions<JwtSettings> settings)
        {
            _settings = settings;
        }

        [HttpPost("/token")]
        public IActionResult Token(User user)
        {
            if (!user.Login.Equals("bobot") && !user.Password.Equals("527716"))
                return BadRequest(new { errorText = "Invalid username or password." });


            //var response = JwtGenerator.GenerateJWT("al;skdjfqwoiefa;sldkfm;asleifa;lwsei;fas", "SSCAuth", "LifeIbsRu");
            var response = JwtGenerator.GenerateJWT(_settings.Value.SecretKey, _settings.Value.Subject, _settings.Value.Issuer);

            return Ok(response);
        }
    }
}
