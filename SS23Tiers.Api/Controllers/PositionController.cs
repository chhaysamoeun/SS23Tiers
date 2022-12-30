using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SS23Tiers.Api.Data;
using SS23Tiers.Api.Models;

namespace SS23Tiers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Position
        [HttpGet]
        public async Task<IEnumerable<Position>> Get()
        {
            return  await _context.Position.ToListAsync();
        }

        // GET: api/Position/5
        [HttpGet("{id}")]
        public async Task<Position> Get(Guid id)
            => await _context.Position.FindAsync(id);

        // POST: api/Position
        [HttpPost]
        public async Task<IActionResult> Post(Position position)
        {
            if (ModelState.IsValid)
            {
                if (await IsExist(position.PositionName))
                {
                    return BadRequest("Position name already exist");
                }
                position.PositionId = Guid.NewGuid();
                _context.Position.Add(position);
                await _context.SaveChangesAsync();
                return Ok(position);
            }
            return BadRequest();
        }
        private async Task<bool> IsExist(string name)
            => await _context.Position.AnyAsync(x => x.PositionName.Equals(name));
        // PUT: api/Position/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id,Position position)
        {
            if(ModelState.IsValid || id == position.PositionId)
            {
                var pos = await _context.Position.FindAsync(id);
                if (pos is null) return BadRequest();
                _context.Position.Attach(pos);
                pos.PositionName = position.PositionName;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Position/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var position = await _context.Position.FindAsync(id);
            if(position is not null)
            {
                _context.Position.Remove(position);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
