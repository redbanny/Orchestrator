using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using OrchestratorAPI.JWT;
using OrchestratorAPI.JWT.Filters;
using OrchestratorAPI.JWT.Http;
using OrchestratorAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OrchestratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : Controller
    {
        DataHttpService _service;
        public JwtController(DataHttpService service) => _service = service;

        [HttpPost("/token")]
        public IActionResult Token(User user)
        {
            if (!user.Login.Equals("bobot") && !user.Password.Equals("527716"))
                return BadRequest(new { errorText = "Invalid username or password." });

            //var response = JwtGenerator.GenerateJWT(_service);

            return Ok("");
        }
    }
}
