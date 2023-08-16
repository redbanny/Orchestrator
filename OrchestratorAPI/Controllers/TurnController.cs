using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        public async Task<ActionResult<IEnumerable<Turn>>> GetTurn()=>        
            //var ff = await db.Turns.Include(X => X.TurnItems)
            //    /*.ThenInclude(x => x.InputDate)*/.ToListAsync();
            //var f = ff.FirstOrDefault().TurnItems.FirstOrDefault().DeserialInDict(ff.FirstOrDefault().TurnItems.FirstOrDefault().InputDate);
             await db.Turns.Include(X=>X.TurnItems).ToListAsync();
        

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
            //db.InputDate.AddRange(turn.TurnItems.FirstOrDefault().InputDate);
            await db.SaveChangesAsync();
            return Ok(turn);
        }
    }
}
