using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrchestratorAPI.Contexts;
using OrchestratorAPI.JWT.Filters;
using OrchestratorAPI.Models;

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
        [JwtAuthenticationFilter]
        public async Task<ActionResult<IEnumerable<TurnItem>>> GetTurnItems() =>
           await db.TurnItems.Include(x => x.Turn).ToListAsync();

        [HttpGet("{TurnName}/{status}")]
        [JwtAuthenticationFilter]
        public async Task<ActionResult<TurnItem>> GetTurnItemByStatus(string TurnName, int status)
        {
            var turnItem = await db.TurnItems.Where(x=>x.Turn.TurnName == TurnName)
                .FirstOrDefaultAsync(x=>x.Item_Status == (TurnItem.Status)status);
            if (turnItem == null)
                return NotFound();
            return Ok(turnItem);
        }

        [HttpGet("{TurnName}")]
        [JwtAuthenticationFilter]
        public async Task<ActionResult<TurnItem>> GetTurnItems(string TurnName)
        {
            var turnItem = await db.TurnItems.Where(x => x.Turn.TurnName == TurnName).ToListAsync();
            if (turnItem == null)
                return NotFound();
            return Ok(turnItem);
        }        

        [HttpPost]
        [JwtAuthenticationFilter]
        public async Task<ActionResult<TurnItem>> PostTurnItem(TurnItem turnItem)
        {
            if (turnItem == null)
                return BadRequest();

            if (db.Turns.FirstOrDefault(x => x.TurnName == turnItem.Turn.TurnName) == null)
                db.Turns.Add(new Turn { TurnName = turnItem.Turn.TurnName, TurnItems = new TurnItem[] { turnItem } });
            else
                db.Turns.FirstOrDefault(x => x.TurnName == turnItem.Turn.TurnName).TurnItems.Add(turnItem);

            await db.SaveChangesAsync();
            return Ok(turnItem);
        }

        [HttpPatch("{TurnName}/{id}/{status}")]
        [JwtAuthenticationFilter]
        public async Task<ActionResult<TurnItem>> PatchTurnItemStatus(string TurnName, int id, int status)
        {
            var turnItem = db.TurnItems.Include(x => x.Turn)
                .Where(x => x.Turn.TurnName == TurnName)
                .FirstOrDefault(x => x.TurnItemId == id);
            if(turnItem == null)
                return NotFound();
            turnItem.Item_Status = (TurnItem.Status)status;
            turnItem.Update_Time = DateTime.Parse(DateTime.Now.ToString("g"));
            db.TurnItems.Update(turnItem);
            await db.SaveChangesAsync();
            return Ok(turnItem);
        }
    }
}
