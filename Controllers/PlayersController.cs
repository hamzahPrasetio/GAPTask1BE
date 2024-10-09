using Microsoft.AspNetCore.Mvc;
using task1be.Data;
using task1be.DTOs;
using task1be.Models;
using Microsoft.EntityFrameworkCore;

namespace task1be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players
                                 .Include(p => p.position)
                                 .Include(p => p.team)
                                 .ToListAsync();
        }

        [HttpGet("lov")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersLov()
        {
            var players = await _context.Players.ToListAsync();
            var response = new ResponseLovDto
            {
                Total = players.Count,
                Data = players.Select(p => new ResponseLovDataDto
                {
                    Id = p.id,
                    Name = p.name
                }).ToList(),
                Message = "Berhasil memuat data LOV Players",
                Code = 200,
                Status = "OK"
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players
                                       .Include(p => p.position)
                                       .Include(p => p.team)
                                       .FirstOrDefaultAsync(p => p.id == id);
            if (player == null)
            {
                return NotFound();
            }
            return player;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            // Validate that the position and team exist
            var position = await _context.Positions.FindAsync(player.positionid);
            var team = await _context.Teams.FindAsync(player.teamid);

            if (position == null || team == null)
            {
                return BadRequest("Invalid position or team ID.");
            }

            player.position = position;
            player.team = team;

            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPlayer", new { id = player.id }, player);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.id)
            {
                return BadRequest();
            }
            
            // Validate that the position and team exist
            var position = await _context.Positions.FindAsync(player.positionid);
            var team = await _context.Teams.FindAsync(player.teamid);

            if (position == null || team == null)
            {
                return BadRequest("Invalid position or team ID.");
            }
            
            player.position = position;
            player.team = team;

            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
