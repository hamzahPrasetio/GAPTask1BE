using Microsoft.AspNetCore.Mvc;
using task1be.Data;
using task1be.DTOs;
using task1be.Models;
using Microsoft.EntityFrameworkCore;

namespace task1be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        [HttpGet("lov")]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositionsLov()
        {
            var teams = await _context.Teams.ToListAsync();
            var response = new ResponseLovDto
            {
                Total = teams.Count,
                Data = teams.Select(p => new ResponseLovDataDto
                {
                    Id = p.id,
                    Name = p.name
                }).ToList(),
                Message = "Berhasil memuat data LOV Team",
                Code = 200,
                Status = "OK"
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTeam", new { id = team.id }, team);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.id)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.Include(t => t.players).FirstOrDefaultAsync(t => t.id == id);
            if (team == null)
            {
                return NotFound();
            }

            if (team.players.Any())
            {
                return Conflict("Cannot delete the team because it has related players.");
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
