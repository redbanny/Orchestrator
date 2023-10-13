using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OrchestratorAPI.JWT.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthenticationFilter : TypeFilterAttribute
    {
        public JwtAuthenticationFilter() : base(typeof(JwtAuthenticationFilterImplementation))
        {
        }

        private class JwtAuthenticationFilterImplementation : IAuthorizationFilter
        {
            private readonly ILogger<JwtAuthenticationFilterImplementation> _logger;
            private readonly JwtSettings _jwtSettings;

            public JwtAuthenticationFilterImplementation(ILogger<JwtAuthenticationFilterImplementation> logger, IConfiguration configuration)
            {
                _logger = logger;
                _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                try
                {
                    _logger.LogInformation($"Данные авторизации: Issuer = {_jwtSettings.Issuer},\n " +
                        $"Subject = {_jwtSettings.Subject},\n SecretKey = {_jwtSettings.SecretKey}");

                    if (_jwtSettings is null)
                        throw new InvalidOperationException("Не заданы параметры JWT токена");

                    var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                    if (token == null)
                        throw new SecurityTokenException();

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

                    tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = _jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ClockSkew = TimeSpan.FromMinutes(15)
                    }, out var validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var subject = jwtToken.Subject;
                    var problemDetails = new List<ProblemDetails>();
                    if (subject is null)
                        problemDetails.Add(new()
                        {
                            Status = StatusCodes.Status401Unauthorized,
                            Detail = "Отсутствует объект Subject",
                            Title = "Unauthorized",
                            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
                        });

                    var expiresTimeStamp = long.Parse(jwtToken.Claims.FirstOrDefault(p => p.Type == "exp")?.Value);
                    var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expiresTimeStamp).DateTime.ToLocalTime();
                    if ((expirationTime - DateTime.Now).Minutes > 15)
                        problemDetails.Add(new()
                        {
                            Status = StatusCodes.Status401Unauthorized,
                            Detail = "Срок истечения токена не может превышать 15 минут",
                            Title = "Unauthorized",
                            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
                        });
                    var issuer = validatedToken.Issuer;
                    if (problemDetails.Any())
                    {
                        _logger.LogInformation($"Неудачная попытка входа на сервер в {DateTime.Now} с параметрами Issuer={issuer}, Subject={subject}");
                        context.Result = new UnauthorizedObjectResult(problemDetails);
                    }

                    _logger.LogInformation($"Произведена попытка входа на сервер в {DateTime.Now} с параметрами Issuer={issuer}, Subject={subject}");
                }
                catch (SecurityTokenException ex)
                {
                    _logger.LogError(ex.Message);
                    context.Result = new UnauthorizedObjectResult(new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Detail = ex.Message,
                        Title = "Unauthorized",
                        Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}