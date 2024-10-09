using Microsoft.AspNetCore.Mvc;
using task1be.Data;
using task1be.DTOs;
using task1be.Models;
using Microsoft.EntityFrameworkCore;

namespace task1be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            return await _context.Positions.ToListAsync();
        }

        [HttpGet("lov")]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositionsLov()
        {
            var positions = await _context.Positions.ToListAsync();
            var response = new ResponseLovDto
            {
                Total = positions.Count,
                Data = positions.Select(p => new ResponseLovDataDto
                {
                    Id = p.id,
                    Name = p.name
                }).ToList(),
                Message = "Berhasil memuat data LOV Position",
                Code = 200,
                Status = "OK"
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return position;
        }

        [HttpPost]
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPosition", new { id = position.id }, position);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, Position position)
        {
            if (id != position.id)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var position = await _context.Positions.Include(t => t.players).FirstOrDefaultAsync(t => t.id == id);
            if (position == null)
            {
                return NotFound();
            }

            if (position.players.Any())
            {
                return Conflict("Cannot delete the position because it has related players.");
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
