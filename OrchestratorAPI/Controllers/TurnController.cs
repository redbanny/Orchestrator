using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrchestratorAPI.Contexts;
using OrchestratorAPI.JWT;
using OrchestratorAPI.JWT.Filters;
using OrchestratorAPI.Models;

namespace OrchestratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnController : Controller
    {
        private readonly TurnDbContext _db;
        private ILogger<TurnController> _logger;

        public TurnController(TurnDbContext context, ILogger<TurnController> logger)
        {
            _db = context;
            _logger = logger;
        }

        [HttpGet]
        [JwtAuthenticationFilter]
        public async Task<ActionResult<IEnumerable<Turn>>> GetTurn()
        {
            _logger.LogInformation("Получение списка очередей");
            var turns = await _db.Turns.Include(X => X.TurnItems).ToListAsync();
            _logger.LogInformation($"Количество очередей: {turns.Count}");
            return Ok(turns);
        }

        [HttpGet("{TurnName}")]
        [JwtAuthenticationFilter]
        public async Task<ActionResult<IEnumerable<Turn>>> GetTurnByName(string TurnName) =>
            await _db.Turns.Where(x => x.TurnName == TurnName).Include(x => x.TurnItems).ToListAsync();


        [HttpPost]
        [JwtAuthenticationFilter]
        public async Task<ActionResult<Turn>> PostTurn(Turn turn)
        {
            if (turn == null)
                return BadRequest();

            if (_db.Turns.FirstOrDefault(x => x.TurnName == turn.TurnName) == null)
                _db.Turns.Add(turn);
            else
                turn.TurnItems.FirstOrDefault().TurnId = _db.Turns.FirstOrDefault(x => x.TurnName == turn.TurnName).TurnId;
            _db.TurnItems.AddRange(turn.TurnItems);
            await _db.SaveChangesAsync();
            return Ok(turn);
        }

        [HttpDelete("{TurnName}")]
        [JwtAuthenticationFilter]
        public async Task<ActionResult<Turn>> DeleteTurn(string turnName)
        {
            var turn = _db.Turns.FirstOrDefault(turn => turn.TurnName == turnName);
            if(turn == null) return BadRequest();
            _db.Turns.Remove(turn);
            return Ok();
        }
    }
}
