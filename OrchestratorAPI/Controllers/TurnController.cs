using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrchestratorAPI.Contexts;
using OrchestratorAPI.Models;

namespace OrchestratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnController : Controller
    {
        private readonly TurnDbContext db;

        public TurnController(TurnDbContext context) => db = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turn>>> GetTurn() =>
             await db.Turns.Include(X => X.TurnItems).ToListAsync();

        [HttpGet("{TurnName}")]
        public async Task<ActionResult<IEnumerable<Turn>>> GetTurnByName(string TurnName) =>
            await db.Turns.Where(x => x.TurnName == TurnName).Include(x => x.TurnItems).ToListAsync();

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Turn>> GetTurnById(int id) =>
        //    db.Turns.Include(x => x.TurnItems).FirstOrDefault(x => x.TurnId == id);

        [HttpPost]
        public async Task<ActionResult<Turn>> PostTurn(Turn turn)
        {
            if (turn == null)
                return BadRequest();

            if (db.Turns.FirstOrDefault(x => x.TurnName == turn.TurnName) == null)
                db.Turns.Add(turn);
            else
                turn.TurnItems.FirstOrDefault().TurnId = db.Turns.FirstOrDefault(x => x.TurnName == turn.TurnName).TurnId;
            db.TurnItems.AddRange(turn.TurnItems);
            await db.SaveChangesAsync();
            return Ok(turn);
        }
    }
}
