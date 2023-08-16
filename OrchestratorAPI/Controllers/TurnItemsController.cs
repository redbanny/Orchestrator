using Microsoft.AspNetCore.Mvc;
using OrchestratorAPI.Contexts;
using OrchestratorAPI.Models;
using System.Data.Entity;

namespace OrchestratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnItemsController : Controller
    {
        private readonly TurnDbContext db;

        public TurnItemsController(TurnDbContext context) => 
            db = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurnItem>>> GetTurn()
        {
            return await db.TurnItems.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TurnItem>> PostTurn(TurnItem turnItem)
        {
            if (turnItem == null)
                return BadRequest();

            if (db.Turns.FirstOrDefault(x => x.TurnName == turnItem.Turn.TurnName) == null)
                db.Turns.Add(new Turn { TurnName = turnItem.Turn.TurnName });
            else
            {
                turnItem.Turn.TurnId = db.Turns.FirstOrDefault(x => x.TurnName == turnItem.Turn.TurnName).TurnId;
                //turnItem.TurnId = db.Turns.FirstOrDefault(x => x.TurnName == turnItem.Turn.TurnName).TurnId;
            }
            db.TurnItems.Add(turnItem);
            //db.InputDate.AddRange(turn.TurnItems.FirstOrDefault().InputDate);
            await db.SaveChangesAsync();
            return Ok(turnItem);
        }
    }
}
